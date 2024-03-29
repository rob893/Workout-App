using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WorkoutApp.API.Data;
using WorkoutApp.API.Data.Repositories;
using WorkoutApp.API.Helpers;
using WorkoutApp.API.Models.Domain;
using WorkoutApp.API.Models.Settings;

namespace WorkoutApp.API
{
    public class Startup
    {
        public IWebHostEnvironment AppEnvironment { get; private set; }
        public IConfiguration Configuration { get; }


        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            AppEnvironment = env;
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                // This allows for global authorization. No need to have [Authorize] attribute on controllers with this.
                // This is what requires tokens for all endpoints. Add [AllowAnonymous] to any endpoint not requiring tokens (like login)
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            })
            .ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = ctx => new ValidationProblemDetailsResult();
            })
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            // Add the MySQL database context with connection info set in appsettings.json
            services.AddDbContext<DataContext>(x => x.UseMySql(Configuration.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(Configuration.GetConnectionString("DefaultConnection")), options =>
            {
                options.EnableRetryOnFailure();
                options.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            }));

            // Configure identity
            IdentityBuilder builder = services.AddIdentityCore<User>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequiredLength = 4;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
            });

            builder = new IdentityBuilder(builder.UserType, typeof(Role), builder.Services);
            builder.AddEntityFrameworkStores<DataContext>();
            builder.AddRoleValidator<RoleValidator<Role>>();
            builder.AddRoleManager<RoleManager<Role>>();
            builder.AddSignInManager<SignInManager<User>>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
                options.AddPolicy("RequireUserRole", policy => policy.RequireRole("User"));
            });

            services.Configure<AuthenticationSettings>(Configuration.GetSection("AuthenticationSettings"));
            services.Configure<SwaggerAuthSettings>(Configuration.GetSection("SwaggerAuthSettings"));

            ConfigureAuthentication(services);
            ConfigureSwagger(services);
            ConfigureHealthChecks(services);

            services.AddCors();

            var mapperConfiguration = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfiles(Configuration["BaseUrl"]));
            });
            services.AddSingleton(mapperConfiguration.CreateMapper());

            //Must add repos here so they can be injected. Interface => concrete implementation
            services.AddScoped<UserRepository>();
            services.AddScoped<MuscleRepository>();
            services.AddScoped<EquipmentRepository>();
            services.AddScoped<ExerciseCategoryRepository>();
            services.AddScoped<ExerciseRepository>();
            services.AddScoped<WorkoutRepository>();
            services.AddScoped<ExerciseGroupRepository>();
            services.AddScoped<ScheduledWorkoutRepository>();
            services.AddScoped<WorkoutInvitationRepository>();

            services.AddLogging();

            services.AddTransient<Seeder>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseSwaggerAuthorized();
            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swaggerDoc, httpRequest) =>
                {
                    if (!httpRequest.Headers.ContainsKey("X-Forwarded-Host"))
                    {
                        return;
                    }

                    var serverUrl = $"{httpRequest.Headers["X-Forwarded-Proto"]}://" +
                        $"{httpRequest.Headers["X-Forwarded-Host"]}/" +
                        $"{httpRequest.Headers["X-Forwarded-Prefix"]}";

                    swaggerDoc.Servers = new List<OpenApiServer>()
                    {
                        new OpenApiServer { Url = serverUrl }
                    };
                });
            });

            if (AppEnvironment.IsDevelopment())
            {
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Workout App API V1");
                });
            }
            else
            {
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("../swagger/v1/swagger.json", "Workout App API V1");
                });
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors(header =>
                header.WithOrigins(Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>())
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders(new string[] { "X-Token-Expired" })
            );
            app.UseGlobalExceptionHandler();
            app.UseHsts();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health", new HealthCheckOptions()
                {
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapHealthChecks("/health/db", new HealthCheckOptions()
                {
                    Predicate = check => check.Tags.Contains("db"),
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapControllers();
            });
        }

        private void ConfigureAuthentication(IServiceCollection services)
        {
            var authSettings = Configuration.GetSection("AuthenticationSettings").Get<AuthenticationSettings>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    // Set token validation options. These will be used when validating all tokens.
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ClockSkew = TimeSpan.Zero,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authSettings.APISecrect)),
                        RequireSignedTokens = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        RequireExpirationTime = true,
                        ValidateLifetime = true,
                        ValidAudience = authSettings.TokenAudience,
                        ValidIssuers = new string[] { authSettings.TokenIssuer }
                    };

                    options.Events = new JwtBearerEvents
                    {
                        // Add custom responses when token validation fails.
                        OnChallenge = context =>
                        {
                            // Skip the default logic.
                            context.HandleResponse();
                            context.Response.StatusCode = 401;
                            context.Response.ContentType = "application/json";

                            string errorMessage = string.IsNullOrWhiteSpace(context.ErrorDescription) ? context.Error : $"{context.Error}. {context.ErrorDescription}.";

                            var problem = new ProblemDetailsWithErrors(errorMessage ?? "Invalid token", 401, context.Request);

                            var settings = new JsonSerializerSettings
                            {
                                ContractResolver = new CamelCasePropertyNamesContractResolver()
                            };

                            return context.Response.WriteAsync(JsonConvert.SerializeObject(problem, settings));
                        },
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("X-Token-Expired", "true");
                            }

                            return Task.CompletedTask;
                        }
                    };
                });
        }

        private void ConfigureHealthChecks(IServiceCollection services)
        {
            services.AddHealthChecks()
                .AddDbContextCheck<DataContext>(tags: new[] { "db" });
        }

        private void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Workout App API", Version = "v1" });

                // Add the security token option to swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        }, new List<string>()
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            services.ConfigureSwaggerGen(c => c.CustomSchemaIds(x => x.FullName));
        }
    }
}

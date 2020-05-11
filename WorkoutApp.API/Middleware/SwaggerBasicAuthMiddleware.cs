using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using WorkoutApp.API.Models.Settings;

namespace WorkoutApp.API.Middleware
{
    public class SwaggerBasicAuthMiddleware
    {

        private readonly RequestDelegate next;

        public SwaggerBasicAuthMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, IOptions<SwaggerAuthSettings> swaggerAuthSettings)
        {
            var settings = swaggerAuthSettings.Value;

            // Make sure we are hitting the swagger path
            if (context.Request.Path.StartsWithSegments("/swagger"))
            {
                if (!settings.Enabled)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    return;
                }

                if (!settings.RequireAuth)
                {
                    await next.Invoke(context);
                    return;
                }

                string authHeader = context.Request.Headers["Authorization"];
                if (authHeader != null && authHeader.StartsWith("Basic "))
                {
                    // Get the encoded username and password
                    var encodedUsernamePassword = authHeader.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries)[1]?.Trim();

                    // Decode from Base64 to string
                    var decodedUsernamePassword = Encoding.UTF8.GetString(Convert.FromBase64String(encodedUsernamePassword));

                    // Split username and password
                    var username = decodedUsernamePassword.Split(':', 2)[0];
                    var password = decodedUsernamePassword.Split(':', 2)[1];

                    // Check if login is correct
                    if (IsAuthorized(username, password, settings))
                    {
                        await next.Invoke(context);
                        return;
                    }
                }

                // Return authentication type (causes browser to show login dialog)
                context.Response.Headers["WWW-Authenticate"] = "Basic";

                // Return unauthorized
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            else
            {
                await next.Invoke(context);
            }
        }

        public bool IsAuthorized(string username, string password, SwaggerAuthSettings settings)
        {
            // Check that username and password are correct
            return username.Equals(settings.Username, StringComparison.InvariantCultureIgnoreCase) && password.Equals(settings.Password);
        }
    }
}
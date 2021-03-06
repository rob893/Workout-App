using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using WorkoutApp.API.Middleware;

namespace WorkoutApp.API.Helpers
{
    public static class Extensions
    {
        private static readonly Random rng = new Random();


        public static void AddPagination(this HttpResponse response, int pageNumber, int pageSize, int totalItems, int totalPages)
        {
            response.Headers.Add("X-Pagination-PageNumber", pageNumber.ToString());
            response.Headers.Add("X-Pagination-PageSize", pageSize.ToString());
            response.Headers.Add("X-Pagination-TotalItems", totalItems.ToString());
            response.Headers.Add("X-Pagination-TotalPages", totalPages.ToString());
            response.Headers.Add("Access-Control-Expose-Headers", new StringValues(new string[]
            {
                "X-Pagination-PageNumber",
                "X-Pagination-PageSize",
                "X-Pagination-TotalItems",
                "X-Pagination-TotalPages"
            }));
        }

        public static void AddPagination<T>(this HttpResponse response, OffsetPagedList<T> pagedList)
        {
            AddPagination(response, pagedList.PageNumber, pagedList.PageSize, pagedList.TotalItems, pagedList.TotalPages);
        }

        public static int CalculateAge(this DateTime theDateTime)
        {
            int age = DateTime.Today.Year - theDateTime.Year;

            if (theDateTime.AddYears(age) > DateTime.Today)
            {
                age--;
            }

            return age;
        }

        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseExceptionHandler(b => b.UseMiddleware<GlobalExceptionHandlerMiddleware>());
        }

        public static IApplicationBuilder UseSwaggerAuthorized(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SwaggerBasicAuthMiddleware>();
        }

        public static IList<T> Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

            return list;
        }

        public static int ConvertToInt32FromBase64(this string str)
        {
            try
            {
                return BitConverter.ToInt32(Convert.FromBase64String(str), 0);
            }
            catch
            {
                throw new ArgumentException($"{str} is not a valid base 64 encoded int32.");
            }
        }

        public static string ConvertInt32ToBase64(this int i)
        {
            return Convert.ToBase64String(BitConverter.GetBytes(i));
        }
    }
}
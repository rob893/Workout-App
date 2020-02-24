using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WorkoutApp.API.Middleware;

namespace WorkoutApp.API.Helpers
{
    //These add functions to exsisting classes basically.
    public static class Extensions
    {
        private static readonly Random rng = new Random();


        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error"); //prevents cors
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }

        public static void AddPagination(this HttpResponse response, int pageNumber, int pageSize, int totalItems, int totalPages)
        {
            var paginationHeader = new PaginationHeader
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages
            };
            var camelCaseFormatter = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            response.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationHeader, camelCaseFormatter));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }

        public static void AddPagination<T>(this HttpResponse response, PagedList<T> pagedList)
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
    }
}
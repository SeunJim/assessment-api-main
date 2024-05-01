using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json;

namespace Assessment.Application.Helpers
{
    public static class ExtensionHelpers
    {
        public static string ModelStateError(this ModelStateDictionary modelState)
        {
            var error = new StringBuilder();
            foreach (var model in modelState.Values)
            {
                foreach (var modelError in model.Errors)
                {
                    error.AppendLine(modelError.ErrorMessage);
                }
            }
            return error.ToString();

        }
        public static void AddPaginationHeader(this HttpResponse response, int currentPage, int itemsPerPage, int totalItems, int totalPages)
        {
            var paginationHeader = new PaginationHeader(currentPage, itemsPerPage, totalItems, totalPages);
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            response.Headers.Add("Pagination", System.Text.Json.JsonSerializer.Serialize(paginationHeader, options));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }

    }
}

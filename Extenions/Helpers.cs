using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecondAssignmentApi.Extenions
{
    public static class Helpers
    {
        public static void AddPagination(this HttpResponse httpResponse, int currentPage,
            int itemsperpage,int totalItems, int totalPages)
        {
            var paginationHeader = new PaginationHeader(currentPage, itemsperpage, totalItems, totalPages);
            var camelCaseFormatter = new JsonSerializerSettings();
            camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();
            httpResponse.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationHeader, camelCaseFormatter));
            httpResponse.Headers.Add("Accsess-Control-Expose-Headers", "Pagination");
        }
    }
}

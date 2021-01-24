using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Yms.Contracts.Production;

namespace Yms.Web.HttpHandlers
{
    public interface IYmsApiHttpHandler
    {
        Task<CategoryHierarchyDto> GetCategoryTree();
    }

    public class YmsApiHttpHandler : IYmsApiHttpHandler
    {
        private readonly HttpClient httpClient;

        public YmsApiHttpHandler(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<CategoryHierarchyDto> GetCategoryTree()
        {
            var response = await httpClient.GetAsync("api/production/category/get-category-tree");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CategoryHierarchyDto>(json);
        }
    }
}

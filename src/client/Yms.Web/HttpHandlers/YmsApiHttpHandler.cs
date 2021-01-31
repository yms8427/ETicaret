using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Yms.Contracts.Production;

namespace Yms.Web.HttpHandlers
{
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

        public async Task<List<ProductDto>> GetProducts(int limit)
        {
            var response = await httpClient.GetAsync($"api/production/home/list?count={limit}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ProductDto>>(json);
        }
        public async Task<List<SupplierDto>> GetSuppliers()
        {
            var response = await httpClient.GetAsync("api/production/supplier/list");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<SupplierDto>>(json);
        }
        public async Task<List<ProductDto>> GetProductsByCategory(int limit, Guid id)
        {
            var response = await httpClient.GetAsync($"api/production/home/list-by-category?count={limit}&id={id}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ProductDto>>(json);
        }
        public async Task<List<ProductDto>> GetProductsBySubCategory(int limit, Guid id)
        {
            var response = await httpClient.GetAsync($"api/production/home/list-by-subcategory?count={limit}&id={id}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ProductDto>>(json);
        }
        public async Task<List<ProductDto>> GetProductsBySupplier(int limit, Guid id)
        {
            var response = await httpClient.GetAsync($"api/production/home/list-by-supplier?count={limit}&id={id}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ProductDto>>(json);
        }
        public async Task<string> GetCategoryNameById(Guid categoryId)
        {
            var response = await httpClient.GetAsync($"api/production/category/category/{categoryId}");
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
        }
        public async Task<string> GetSubCategoryNameById(Guid categoryId)
        {
            var response = await httpClient.GetAsync($"api/production/category/sub-category/{categoryId}");
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
        }
        public async Task<string> GetSupplierNameById(Guid supplierId)
        {
            var response = await httpClient.GetAsync($"api/production/supplier/supplier/{supplierId}");
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
        }
        public async Task<string> GetDocumentNameById(Guid id)
        {
            var response = await httpClient.GetAsync($"api/management/image/get-name/{id}");
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
        }
    }
}

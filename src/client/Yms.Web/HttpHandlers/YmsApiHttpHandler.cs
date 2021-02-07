﻿using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Yms.Common.Contracts;
using Yms.Contracts.Production;
using Yms.Web.Models;

namespace Yms.Web.HttpHandlers
{
    public class YmsApiHttpHandler : IYmsApiHttpHandler
    {
        private readonly HttpClient httpClient;
        private readonly string token;

        public YmsApiHttpHandler(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                this.token = httpContextAccessor.HttpContext.User.Claims.First(i => i.Type == "JwtToken").Value;
            }
            
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

        public async Task<bool> AddToCart(Guid productId, byte count)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.PostAsync($"api/sales/cart/add/{productId}/{count}", null);

            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.Unauthorized:
                case System.Net.HttpStatusCode.BadRequest:
                    return false;
                case System.Net.HttpStatusCode.OK:
                    return true;
            }
            return false;
        }

        public async Task<DetailedSessionInformation> Authenticate(string userName, string password)
        {
            var loginInfo = JsonConvert.SerializeObject(new { username = userName, password = password });
            var content = new StringContent(loginInfo, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"api/account/login", content);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<DetailedSessionInformation>(await response.Content.ReadAsStringAsync());
        }
    }
}

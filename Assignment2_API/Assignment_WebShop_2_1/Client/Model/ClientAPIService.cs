﻿using Data;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client.Model
{
    public class ClientAPIService
    {
        private readonly HttpClient _client;

        public ClientAPIService(string baseAddress)
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri(baseAddress)
            };
        }

        #region Authentication

        public async Task<bool> LoginAsync(string name, string password)
        {
            LoginDTO user = new LoginDTO
            {
                UserName = name,
                Password = password
            };

            HttpResponseMessage response = await _client.PostAsJsonAsync("api/Account/Login", user);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return false;
            }

            throw new NetworkException("Service returned response: " + response.StatusCode);
        }

        public async Task LogoutAsync()
        {
            HttpResponseMessage response = await _client.PostAsync("api/Account/Logout", null);

            if (response.IsSuccessStatusCode)
            {
                return;
            }

            throw new NetworkException("Service returned response: " + response.StatusCode);
        }

        public async Task<bool> RegisterAsync(string fullName, string userName, string password)
        {
            RegisterDTO user = new RegisterDTO
            {
                FullName = fullName,
                UserName = userName,
                Password = password
            };

            HttpResponseMessage response = await _client.PostAsJsonAsync("api/Account/Register", user);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return false;
            }

            throw new NetworkException("Service returned response: " + response.StatusCode);
        }

        #endregion


        public async Task<IEnumerable<CategoryDTO>> LoadListsAsync()
        {
            HttpResponseMessage response = await _client.GetAsync("api/categories/");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<CategoryDTO>>();
            }

            throw new NetworkException("Service returned response: " + response.StatusCode);
        }

        public async Task<IEnumerable<OrderDTO>> LoadOrdersAsync()
        {
            HttpResponseMessage response = await _client.GetAsync("api/order/");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<OrderDTO>>();
            }

            throw new NetworkException("Service returned response: " + response.StatusCode);
        }



        public async Task<IEnumerable<ProductDTO>> LoadItemsAsync(int listId)
        {
            HttpResponseMessage response = await _client.GetAsync("api/products/category/" + listId.ToString()); //Check TODO

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<ProductDTO>>();
            }

            throw new NetworkException("Service returned response: " + response.StatusCode);
        }

        public async Task<ProductDTO> LoadProductAsync(int listId)
        {
            HttpResponseMessage response = await _client.GetAsync("api/products/" + listId.ToString()); //Check TODO

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<ProductDTO>();
            }

            throw new NetworkException("Service returned response: " + response.StatusCode);
        }

        public async Task<OrderDTO> LoadOrderProductAsync(int listId)
        {
            HttpResponseMessage response = await _client.GetAsync("api/order/" + listId.ToString()); //Check TODO

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<OrderDTO>();
            }

            throw new NetworkException("Service returned response: " + response.StatusCode);
        }

        public async Task UpdateOrderAsync(OrderDTO item)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync($"api/order/{item.ID}", item);

            if (!response.IsSuccessStatusCode)
            {
                throw new NetworkException("Service returned response: " + response.StatusCode);
            }
        }

        public async Task UpdateProductAsync(ProductDTO item)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync($"api/products/{item.Id}", item);

            if (!response.IsSuccessStatusCode)
            {
                throw new NetworkException("Service returned response: " + response.StatusCode);
            }
        }


    }
}

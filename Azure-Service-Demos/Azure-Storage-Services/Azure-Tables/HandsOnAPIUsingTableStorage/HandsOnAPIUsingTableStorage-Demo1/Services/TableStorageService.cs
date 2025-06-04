﻿using Azure.Data.Tables;
using HandsOnAPIUsingTableStorage_Demo1.Entities;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HandsOnAPIUsingTableStorage_Demo1.Services
{
    public class TableStorageService : ITableStorageService
    {
        private const string TableName = "Item";
        private readonly IConfiguration _configuration;

        public TableStorageService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private async Task<TableClient> GetTableClient()
        {
            var serviceClient = new TableServiceClient(_configuration["StorageConnectionString"]);
            var tableClient = serviceClient.GetTableClient(TableName);
            await tableClient.CreateIfNotExistsAsync();
            return tableClient;
        }
        public async Task<GroceryItemEntity> GetEntityAsync(string category, string id)
        {
            var tableClient = await GetTableClient();
            return await tableClient.GetEntityAsync<GroceryItemEntity>(category, id);
        }

        public async Task<GroceryItemEntity> UpsertEntityAsync(GroceryItemEntity entity)
        {
            var tableClient = await GetTableClient();
            await tableClient.UpsertEntityAsync(entity);
            return entity;
        }

        public async Task DeleteEntityAsync(string category, string id)
        {
            var tableClient = await GetTableClient();
            await tableClient.DeleteEntityAsync(category, id);
        }

        //public async Task<List<GroceryItemEntity>> GetEntitiesAsync()
        //{
        //    var tableClient = await GetTableClient();
        //    return await tableClient.GetEntity<GroceryItemEntity>()
        //}
    }
}

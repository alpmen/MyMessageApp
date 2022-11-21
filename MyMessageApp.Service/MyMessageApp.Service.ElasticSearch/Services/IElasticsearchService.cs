using MyMessageApp.Data.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMessageApp.Service.MyMessageApp.Service.ElasticSearch.Services
{
    public interface IElasticsearchService
    {
        Task InsertDocument(string indexName, Message message);
        Task DeleteIndex(string indexName);
        Task DeleteByIdDocument(string indexName, Message message);
        Task InsertBulkDocuments(string indexName, List<Message> message);
        Task<Message> GetDocument(string indexName, string id);
        Task<List<Message>> GetDocuments(string indexName);
    }
}

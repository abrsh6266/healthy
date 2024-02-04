using System.Text.RegularExpressions;
using Auth.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Auth.Services
{
    public class DrugService
    {
        private readonly IMongoCollection<Drug> _drugs;

        public DrugService(IMongoClient client)
        {
            var database = client.GetDatabase("auth");
            _drugs = database.GetCollection<Drug>("drugs");
        }

        public async Task<List<Drug>> GetAllAsync()
        {
            var drugs = await _drugs.Find(_ => true).ToListAsync();
            return drugs;
        }

        public async Task<List<Drug>> GetByNameAsync(string name)
        {
            var filter = Builders<Drug>.Filter.Regex("Name", new BsonRegularExpression(new Regex(name, RegexOptions.IgnoreCase)));

            return await _drugs.Find(filter).ToListAsync();
        }

        public async Task CreateAsync(Drug drug)
        {
            await _drugs.InsertOneAsync(drug);
        }
    }
}

using Auth.Models;
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
            var filter = Builders<Drug>.Filter.Eq(d => d.Name, name);
            var drugs = await _drugs.Find(filter).ToListAsync();
            return drugs;
        }

        public async Task CreateAsync(Drug drug)
        {
            await _drugs.InsertOneAsync(drug);
        }
    }
}

using System.Text.RegularExpressions;
using Auth.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Auth.Services
{
    public class SymptomService
    {
        private readonly IMongoCollection<Symptom> _symptoms;

        public SymptomService(IMongoClient client)
        {
            var database = client.GetDatabase("auth");
            _symptoms = database.GetCollection<Symptom>("symptoms");
        }

        public async Task<List<Symptom>> GetAllAsync()
        {
            var symptoms = await _symptoms.Find(_ => true).ToListAsync();
            return symptoms;
        }

        public async Task<List<Symptom>> GetByNameAsync(string name)
        {
            var filter = Builders<Symptom>.Filter.Regex("Name", new BsonRegularExpression(new Regex(name, RegexOptions.IgnoreCase)));

            return await _symptoms.Find(filter).ToListAsync();
        }

        public async Task CreateAsync(Symptom symptom)
        {
            await _symptoms.InsertOneAsync(symptom);
        }
    }
}

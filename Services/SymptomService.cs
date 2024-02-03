using Auth.Models;
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
            var filter = Builders<Symptom>.Filter.Eq(s => s.Name, name);
            var symptoms = await _symptoms.Find(filter).ToListAsync();
            return symptoms;
        }

        public async Task CreateAsync(Symptom symptom)
        {
            await _symptoms.InsertOneAsync(symptom);
        }
    }
}

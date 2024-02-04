using System.Text.RegularExpressions;
using Auth.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Auth.Services
{
    public class DiseaseService
    {
        private readonly IMongoCollection<Disease> _diseases;

        public DiseaseService(IMongoDatabase database)
        {
            _diseases = database.GetCollection<Disease>("diseases");
        }

        public async Task<List<Disease>> GetAsync()
        {
            return await _diseases.Find(d => true).ToListAsync();
        }

        public async Task<List<Disease>> GetByNameAsync(string name)
        {
            var filter = Builders<Disease>.Filter.Regex("Name", new BsonRegularExpression(new Regex(name, RegexOptions.IgnoreCase)));

            return await _diseases.Find(filter).ToListAsync();
        }

        public async Task CreateAsync(Disease disease)
        {
            await _diseases.InsertOneAsync(disease);
        }
        public async Task<bool> RemoveDiseaseAsync(string diseaseId)
        {
            var filter = Builders<Disease>.Filter.Eq(d => d.Id, diseaseId);
            var result = await _diseases.DeleteOneAsync(filter);

            return result.DeletedCount > 0;
        }

    }
}
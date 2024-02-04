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
    }
}
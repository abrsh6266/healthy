using Auth.Models;
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
            return await _diseases.Find(d => d.Name == name).ToListAsync();
        }

        public async Task CreateAsync(Disease disease)
        {
            await _diseases.InsertOneAsync(disease);
        }
    }
}
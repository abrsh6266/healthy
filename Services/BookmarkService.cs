// BookmarkService.cs

using Auth.Models;
using Auth.Services;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

public class BookmarkService
{
    private readonly IMongoCollection<Bookmark> _bookmarkCollection;
    private readonly DiseaseService _diseaseService;
    private readonly DrugService _drugService;
    private readonly SymptomService _symptomService;

    public BookmarkService(IMongoDatabase database, DiseaseService diseaseService, DrugService drugService, SymptomService symptomService)
    {
        _bookmarkCollection = database.GetCollection<Bookmark>("bookmarks");
        _diseaseService = diseaseService;
        _drugService = drugService;
        _symptomService = symptomService;
    }
    public async Task<Bookmark> GetByUserIdAsync(string userId)
    {
        return await _bookmarkCollection.Find(b => b.UserId == userId).FirstOrDefaultAsync();
    }
    public async Task<bool> AddItemToBookmarkAsync(string userId, string itemName, string itemType)
    {
        var bookmark = await _bookmarkCollection.Find(b => b.UserId == userId).FirstOrDefaultAsync();
        if (bookmark == null)
        {
            bookmark = new Bookmark
            {
                Id = ObjectId.GenerateNewId().ToString(),
                UserId = userId,
                Diseases = new List<Disease>(),
                Drugs = new List<Drug>(),
                Symptoms = new List<Symptom>()
            };
        }

        switch (itemType.ToLower())
        {
            case "disease":
                var diseases = await GetDiseasesByNameAsync(itemName);
                var firstDisease = diseases.FirstOrDefault();
                Console.WriteLine(firstDisease?.Name);
#pragma warning disable CS8604 // Possible null reference argument.
                if (firstDisease != null &&  !bookmark.Diseases.Any(d => d.Id == firstDisease.Id))
                {
                    Console.WriteLine(firstDisease?.Name);
                    bookmark.Diseases.Add(firstDisease);
                }
#pragma warning restore CS8604 // Possible null reference argument.
                break;

            case "drug":
                var drugs = await GetDrugsByNameAsync(itemName);
                var firstDrug = drugs.FirstOrDefault();
                Console.WriteLine(firstDrug?.Name);
#pragma warning disable CS8604 // Possible null reference argument.
                if (firstDrug != null && !bookmark.Drugs.Any(d => d.Id == firstDrug.Id))
                {
                    Console.WriteLine(firstDrug?.Name);
                    bookmark.Drugs.Add(firstDrug);
                }
#pragma warning restore CS8604 // Possible null reference argument.
                break;

            case "symptom":
                var symptoms = await GetSymptomsByNameAsync(itemName);
                var firstSymptom = symptoms.FirstOrDefault();
                Console.WriteLine(firstSymptom?.Name);
#pragma warning disable CS8604 // Possible null reference argument.
                if (firstSymptom != null && !bookmark.Symptoms.Any(s => s.Id == firstSymptom.Id))
                {
                    Console.WriteLine(firstSymptom?.Name);
                    bookmark.Symptoms.Add(firstSymptom);
                }
#pragma warning restore CS8604 // Possible null reference argument.
                break;

            default:
                // Handle unsupported item type
                return false;
        }

        var result = await _bookmarkCollection.ReplaceOneAsync(b => b.UserId == userId, bookmark, new ReplaceOptions { IsUpsert = true });
        return result.IsAcknowledged;
    }

    private async Task<List<Disease>> GetDiseasesByNameAsync(string name)
    {
        return await _diseaseService.GetByNameAsync(name);
    }

    private async Task<List<Drug>> GetDrugsByNameAsync(string name)
    {
        return await _drugService.GetByNameAsync(name);
    }

    private async Task<List<Symptom>> GetSymptomsByNameAsync(string name)
    {
        return await _symptomService.GetByNameAsync(name);
    }
}
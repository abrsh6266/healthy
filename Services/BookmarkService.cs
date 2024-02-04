// BookmarkService.cs

using Auth.Models;
using Auth.Services;
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

    public async Task<bool> AddItemToBookmarkAsync(string userId, string itemName, string itemType)
    {
        var bookmark = await _bookmarkCollection.Find(b => b.UserId == userId).FirstOrDefaultAsync();

        if (bookmark == null)
        {
            bookmark = new Bookmark
            {
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
                if (firstDisease != null && !bookmark.Diseases.Any(d => d.Id == firstDisease.Id))
                {
                    bookmark.Diseases.Add(firstDisease);
                }
                break;

            case "drug":
                var drugs = await GetDrugsByNameAsync(itemName);
                var firstDrug = drugs.FirstOrDefault();
                if (firstDrug != null && !bookmark.Drugs.Any(d => d.Id == firstDrug.Id))
                {
                    bookmark.Drugs.Add(firstDrug);
                }
                break;

            case "symptom":
                var symptoms = await GetSymptomsByNameAsync(itemName);
                var firstSymptom = symptoms.FirstOrDefault();
                if (firstSymptom != null && !bookmark.Symptoms.Any(s => s.Id == firstSymptom.Id))
                {
                    bookmark.Symptoms.Add(firstSymptom);
                }
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
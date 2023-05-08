using DPOBackend.Models.UserModels;
using DPOBackend.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookStoreApi.Services;

public class UserService
{
    private readonly IMongoCollection<UserModel> _testsCollection;

    public UserService(
        IOptions<UserSettings> userStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            userStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            userStoreDatabaseSettings.Value.DatabaseName);

        _testsCollection = mongoDatabase.GetCollection<UserModel>(
            userStoreDatabaseSettings.Value.CollectionName);
    }

    public async Task<List<UserModel>> GetAsync() =>
        await _testsCollection.Find(_ => true).ToListAsync();

    public async Task<UserModel?> GetAsync(int id) =>
        await _testsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(UserModel newTest) =>
        await _testsCollection.InsertOneAsync(newTest);
    
    public async Task UpdateAsync(int id, UserModel updatedUser) =>
        await _testsCollection.ReplaceOneAsync(x => x.Id == id, updatedUser);

    public async Task RemoveAsync(int id) =>
        await _testsCollection.DeleteOneAsync(x => x.Id == id);
}
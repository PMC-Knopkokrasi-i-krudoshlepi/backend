using DPOBackend.Models.UserModels;
using DPOBackend.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookStoreApi.Services;

public class UserService
{
    private readonly IMongoCollection<UserModel> _userCollections;

    public UserService(
        IOptions<UserSettings> userStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            userStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            userStoreDatabaseSettings.Value.DatabaseName);

        _userCollections = mongoDatabase.GetCollection<UserModel>(
            userStoreDatabaseSettings.Value.CollectionName);
    }

    public async Task<List<UserModel>> GetAsync() =>
        await _userCollections.Find(_ => true).ToListAsync();

    public async Task<UserModel?> GetAsync(int id) =>
        await _userCollections.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(UserModel newUser) =>
        await _userCollections.InsertOneAsync(newUser);
    
    public async Task UpdateAsync(int id, UserModel updatedUser) =>
        await _userCollections.ReplaceOneAsync(x => x.Id == id, updatedUser);

    public async Task RemoveAsync(int id) =>
        await _userCollections.DeleteOneAsync(x => x.Id == id);
    
   

}
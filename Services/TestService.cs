using DPOBackend.Models;
using DPOBackend.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookStoreApi.Services;

public class TestService
{
    private readonly IMongoCollection<TestModel> _testsCollection;

    public TestService(
        IOptions<TestSettings> bookStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            bookStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            bookStoreDatabaseSettings.Value.DatabaseName);

        _testsCollection = mongoDatabase.GetCollection<TestModel>(
            bookStoreDatabaseSettings.Value.BooksCollectionName);
    }

    public async Task<List<TestModel>> GetAsync() =>
        await _testsCollection.Find(_ => true).ToListAsync();

    public async Task<TestModel?> GetAsync(int id) =>
        await _testsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(TestModel newTest) =>
        await _testsCollection.InsertOneAsync(newTest);

    public async Task UpdateAsync(int id, TestModel updatedBook) =>
        await _testsCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

    public async Task RemoveAsync(int id) =>
        await _testsCollection.DeleteOneAsync(x => x.Id == id);
}
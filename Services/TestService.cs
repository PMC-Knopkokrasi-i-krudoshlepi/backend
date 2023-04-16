using DPOBackend.Models;
using DPOBackend.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookStoreApi.Services;

public class TestService
{
    private readonly IMongoCollection<TestModel> _booksCollection;

    public TestService(
        IOptions<TestSettings> bookStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            bookStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            bookStoreDatabaseSettings.Value.DatabaseName);

        _booksCollection = mongoDatabase.GetCollection<TestModel>(
            bookStoreDatabaseSettings.Value.BooksCollectionName);
    }

    public async Task<List<TestModel>> GetAsync() =>
        await _booksCollection.Find(_ => true).ToListAsync();

    public async Task<TestModel?> GetAsync(string id) =>
        await _booksCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(TestModel newBook) =>
        await _booksCollection.InsertOneAsync(newBook);

    public async Task UpdateAsync(string id, TestModel updatedBook) =>
        await _booksCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

    public async Task RemoveAsync(string id) =>
        await _booksCollection.DeleteOneAsync(x => x.Id == id);
}
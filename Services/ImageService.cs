using DPOBackend.Models;
using DPOBackend.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace BookStoreApi.Services;

public class ImageService
{
    IGridFSBucket gridFS;

    public ImageService(
        IOptions<ImageServiceSettings> imageStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            imageStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            imageStoreDatabaseSettings.Value.DatabaseName);

        gridFS = new GridFSBucket(mongoDatabase);
    }

    public async Task UploadFromStreamAsync(string fileFileName, Stream stream)
    {
        await gridFS.UploadFromStreamAsync(fileFileName, stream);
    }
    
    public async Task<ObjectId[]> UploadFromStreamAsyncAndGetIds(IFormFileCollection collection)
    {
        var ids = new ObjectId[collection.Count];
        int i = 0;
        foreach (var file in collection)
        {
            await using var stream = file.OpenReadStream();
            ids[i++] = await gridFS.UploadFromStreamAsync(file.FileName, stream);
        }

        return ids;
    }

    public async Task DownloadToStreamAsync(ObjectId objectId, Stream stream)
    {
        await gridFS.DownloadToStreamAsync(objectId, stream);
    }
}
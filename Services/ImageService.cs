using System.Reflection.Metadata;
using DPOBackend.Db;
using DPOBackend.Models;
using DPOBackend.Settings;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace BookStoreApi.Services;

public class ImageService
{
    public ImageService(){}

    public async Task<int> UploadFromStreamAsync(string fileFileName, Stream stream)
    {
        int result;
        var rawData = ReadFully(stream);
        using (var ctx =  new TestDbContext())
        {
            var file = new ImageModel()
            {
                Id = new Random(DateTime.Now.Millisecond).Next(),
                Name = fileFileName,
                Data = rawData,
                Size = rawData.Length
            };
            result = file.Id;
            ctx.Images.Add(file);
            await ctx.SaveChangesAsync();
        }

        return result;
    }
    
    public static byte[] ReadFully(Stream input)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            input.CopyTo(ms);
            return ms.ToArray();
        }
    }
    
    public async Task<int[]> UploadFromStreamAsyncAndGetIds(IFormFileCollection collection)
    {
        var ids = new int[collection.Count];
        int i = 0;
        foreach (var file in collection)
        {
            await using var stream = file.OpenReadStream();
            ids[i++] = await UploadFromStreamAsync(file.FileName, stream);
        }

        return ids;
    }

    public async Task DownloadToStreamAsync(int objectId, Stream stream)
    {
        byte[] data;
        using (var ctx = new TestDbContext())
        {
            data = ctx.Images.FirstOrDefault(img => img.Id == objectId)?.Data;
        }

        await stream.WriteAsync(data);
    }
}
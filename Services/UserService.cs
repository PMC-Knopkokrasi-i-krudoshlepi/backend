using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using DPOBackend.Db;
using DPOBackend.Models.UserModels;
using DPOBackend.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookStoreApi.Services;

public class UserService
{
    public UserService() {}

    public async Task<UserModel?> GetAsync(int id)
    {
        UserModel? result;
        using (var ctx = new TestDbContext())
        {
            result = await ctx.Users.FirstOrDefaultAsync(user => user.Id == id);
        }

        return result;
    }

    public async Task CreateAsync(UserModel? newUser) {
        using (var ctx = new TestDbContext())
        {
            ctx.Users.Add(newUser);
            await ctx.SaveChangesAsync();
        }
    }
    
    public async Task<int> CreateAsync(UserRegisttrationModel? urm)
    {
        int uid;
        using (var ctx = new TestDbContext())
        {
            uid = await ctx.Users.CountAsync() + 1;
            var newUser = new UserModel(uid, urm);
            ctx.Users.Add(newUser);
            await ctx.SaveChangesAsync();
        }

        return uid;
    }

    public async Task UpdateAsync(int id, UserModel? updatedUser) {
        using (var ctx = new TestDbContext())
        {
            var prev = await ctx.Users.FirstOrDefaultAsync(t => t.Id == id);
            ctx.Users.Remove(prev);
            ctx.Users.Add(updatedUser);
            await ctx.SaveChangesAsync();
        }
    }

    public async Task RemoveAsync(int id) {
        using (var ctx = new TestDbContext())
        {
            var removed = await ctx.Tests.FirstOrDefaultAsync(test => test.Id == id);
            ctx.Tests.Remove(removed);
            await ctx.SaveChangesAsync();
        }
    }

    public async Task<UserModel?> GetByNameAndPasswordAsync(string name, string password)
    {
        UserModel? result;
        using (var ctx = new TestDbContext())
        {
            result = await ctx.Users.FirstOrDefaultAsync(user => user.Name == name && user.Password == password);
        }
        return result;
    }

    public async Task<long> GetLenthAsync(){
        int count;
        using (var ctx = new TestDbContext())
        {
            count = await ctx.Users.CountAsync();
        }
        return count;
    }

    public async Task<MemoryStream> GroupRegistration(IFormFileCollection formFiles)
    {
        var csv = formFiles.FirstOrDefault(file => file.FileName.EndsWith(".csv"));
        if (csv == null)
            throw new Exception("Invalid data");

        var csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture)
        {
            HasHeaderRecord = true,
            Delimiter = ";",
            Encoding = Encoding.UTF8
        };
        List<UserRegisttrationModel> records;
        using (var reader = new StreamReader(csv.OpenReadStream()))
        using (var csvReader = new CsvReader(reader, csvConfig))
        {
            records = csvReader.GetRecords<UserRegisttrationModel>().ToList();
        }

        var ids = new List<int>();
        foreach (var urm in records)
        {
            ids.Add(await CreateAsync(urm));
        }

        var users = new List<GroupRegistrationResponseModel>();
        foreach (var id in ids)
        {
            users.Add(new GroupRegistrationResponseModel(await GetAsync(id)));
        }

        return SaveToCSV(users);
    }
    public MemoryStream SaveToCSV(List<GroupRegistrationResponseModel> users)
    {
        var csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture)
        {
            HasHeaderRecord = true,
            Delimiter = ";",
            Encoding = Encoding.UTF8
        };

        using (var mem = new MemoryStream())
        using (var writer = new StreamWriter(mem))
        using (var csvWriter = new CsvWriter(writer, csvConfig))
        {
            csvWriter.WriteHeader<GroupRegistrationResponseModel>();
            csvWriter.WriteRecords(users);
            writer.Flush();

            return mem;
        }
    }
}
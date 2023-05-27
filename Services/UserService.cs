using DPOBackend.Db;
using DPOBackend.Models.UserModels;
using DPOBackend.Settings;
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
            result = await ctx.Users.FirstOrDefaultAsync();
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
}
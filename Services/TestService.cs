using DPOBackend.Db;
using DPOBackend.Models;
using DPOBackend.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory.Query.Internal;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using TestModel = DPOBackend.Models.TestModel;

namespace BookStoreApi.Services;

public class TestService
{
    public TestService()
    {
    }

    public async Task<TestModel?> GetAsync(int id)
    {
        TestModel? result;
        using (var ctx = new TestDbContext())
        {
            result = await ctx.Tests
                .Include(test => test.QuestionsList)
                .FirstOrDefaultAsync();
        }

        return result;
    }

    /*public async Task<TestModel?> GetAsyncWithoutAnswers(int id) =>
        await _testsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();*/

    public async Task CreateAsync(TestModel newTest)
    {
        using (var ctx = new TestDbContext())
        {
            await ctx.Questions.AddRangeAsync(newTest.QuestionsList);
            ctx.Tests.Add(newTest);
            await ctx.SaveChangesAsync();
        }
    }


    public async Task UpdateAsync(int id, TestModel updatedBook)
    {
        using (var ctx = new TestDbContext())
        {
            var prev = await ctx.Tests.FirstOrDefaultAsync(t => t.Id == id);
            ctx.Tests.Remove(prev);
            ctx.Tests.Add(updatedBook);
            await ctx.SaveChangesAsync();
        }
    }

    public async Task RemoveAsync(int id)
    {
        using (var ctx = new TestDbContext())
        {
            var removed = await ctx.Tests.FirstOrDefaultAsync(test => test.Id == id);
            ctx.Tests.Remove(removed);
            await ctx.SaveChangesAsync();
        }
    }

    public async Task<int[]> GetAsync()
    {
        using (var ctx = new TestDbContext())
        {
            return await ctx.Tests.Select(model => model.Id).ToArrayAsync();
        }
    }

    public async Task<int> GetLenth()
    {
        using (var ctx =new TestDbContext())
        {
            return await ctx.Tests.CountAsync();
        }
    }

    /*public async Task<bool> TryUpdateImageIds(int id, ObjectId[] objectIds)
    {
        var t = await GetAsync(id);
        if (t is null)
            return false;
        t.UpdateImageIds(objectIds);
        await UpdateAsync(id, t);
        return true;
    }*/

    public async Task<(int, int)> GetTestResult(int id, string[][] answers) =>
        await Task.Run(async () =>
            {
                var test = await GetAsync(id);
                if (test is null)
                    return (0, 0); //TODO: cringe
                return (await test.GetRightAnswerCount(answers), test.QuestionsList.Count);
            }
        );
}
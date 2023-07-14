using DPOBackend.Db;
using DPOBackend.Models;
using DPOBackend.Models.Profession;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Services;

public class ProfessionService
{
    public ProfessionService() { }
    
    public async Task<ProfessionToFront[]> GetAllAsync()
    {
        ProfessionToFront[] result;
        using (var ctx = new TestDbContext())
        {
            result = await ctx.Professions
                .Select(prof => new ProfessionToFront(prof))
                .ToArrayAsync();
        }

        return result;
    }

    public async Task<Profession?> GetAsync(int id)
    {
        Profession? result;
        using (var ctx = new TestDbContext())
        {
            result = await ctx.Professions
                .FirstOrDefaultAsync(prof => prof.Id == id);
        }

        return result;
    }

    public async Task CreateAsync(Profession newProfession){
        using (var ctx = new TestDbContext())
        {
            ctx.Professions.Add(newProfession);
            await ctx.SaveChangesAsync();
        }
    }
    

    public async Task UpdateAsync(int id, Profession updatedProfession){
        using (var ctx = new TestDbContext())
        {
            ctx.Professions.Update(updatedProfession);
            await ctx.SaveChangesAsync();
        }
    }

    public async Task RemoveAsync(int id){
        using (var ctx = new TestDbContext())
        {
            var removed = await ctx.Professions.FirstOrDefaultAsync(prof => prof.Id == id);
            ctx.Professions.Remove(removed);
            await ctx.SaveChangesAsync();
        }
    }
    
    public async Task<int[]> GetAllIdsAsync()
    {
        int[] result;
        using (var ctx = new TestDbContext())
        {
            result = await ctx.Professions.Select(test => test.Id).ToArrayAsync();
        }

        return result;
    }
}
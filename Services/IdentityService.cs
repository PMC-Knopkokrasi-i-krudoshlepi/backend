using DPOBackend.Db;
using DPOBackend.Models.Identity;
using DPOBackend.Models.Profession;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Services;

public class IdentityService
{
    public async Task<IdentityDTO[]> GetAllAsync()
    {
        IdentityDTO[] result;
        using (var ctx = new TestDbContext())
        {
            result = await ctx.Identities
                .Select(ident => new IdentityDTO(ident))
                .ToArrayAsync();
        }

        return result;
    }
    
    public async Task<int> CreateAsync(IdentityDTO newIdent){
        int uid;
        using (var ctx = new TestDbContext())
        {
            uid = await ctx.Identities.CountAsync() + 1;
            var newIdentity = new IdentityType(uid, newIdent);
            ctx.Identities.Add(newIdentity);
            await ctx.SaveChangesAsync();
        }

        return uid;
    }
}
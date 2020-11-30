using Microsoft.EntityFrameworkCore;
using LocationsApi.Domain.Entities;
using System.Threading.Tasks;

namespace LocationsApi.Persistence
{
    public interface IApplicationDbContext
    {
        DbSet<Location> Locations { get; set; }        
        DbSet<Category> Categories { get; set; }

        Task<int> SaveChangesAsync();
    }
}

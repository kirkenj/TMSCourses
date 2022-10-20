using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebApiDatabase.Entities;

namespace WebApiDatabase.Interfaces
{
    public interface IAutoparkDBContext
    {
        DbSet<Car> Cars { get; }
        DbSet<CarType> CarTypes { get; }
        DbSet<Client> Clients { get; }
        DbSet<Journal> Journals { get; }
        DbSet<ParkingPlace> ParkingPlaces { get; }

        public int SaveChanges();
        EntityEntry<TEntity> Entry<TEntity>(TEntity obj) where TEntity : class;
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

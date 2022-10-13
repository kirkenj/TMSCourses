using Microsoft.EntityFrameworkCore;
using WEB_EF.Models.Classes;

namespace WEB_EF.Models.Interfaces
{
    public interface IAutoparkDBContext
    {
        DbSet<Car> Cars { get; }
        DbSet<CarType> CarTypes { get; }
        DbSet<Client> Clients { get; }
        DbSet<Journal> Journals { get; }
        DbSet<ParkingPlace> ParkingPlaces { get; }

        public int SaveChanges();
    }
}

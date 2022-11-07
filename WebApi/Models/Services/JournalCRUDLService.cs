using WebApi.Models.Interfaces;
using WebApiDatabase.Entities;
using WebApiDatabase.Interfaces;

namespace WebApi.Models.Services
{
    public class JournalCRUDLService : ICRUDLService<JournalCreateModel, JournalUpdateModel, int, JournalItemModel>
    {
        public JournalCRUDLService(IAutoparkDBContext context)
        {
            _context = context;
        }

        private readonly IAutoparkDBContext _context;

        public async Task CreateAsync(JournalCreateModel item)
        {
            var record = new Journal()
            {
                ComingDate = item.ComingDate,
                CarId = item.CarId,
                DepartureDate = item.DepartureDate,
                ParkingPlace = item.ParkingPlace,
            };

            if (!IsValid(record, out string explanation))
            {
                throw new Exception(explanation);
            }

            _context.Journals.Add(record);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var record = _context.Journals.Single(c => c.Id == id);
            _context.Journals.Remove(record);
            await _context.SaveChangesAsync();
        }

        private IQueryable<JournalItemModel> ItemModels => _context.Journals.Select(j => new JournalItemModel
        {
            Id = j.Id,
            CarId = j.CarId,
            ComingDate = j.ComingDate,
            DepartureDate = j.DepartureDate,
            ParkingPlace = j.ParkingPlace
        });

        public async Task<List<JournalItemModel>> GetAllAsync()
        {
            return await Task.Run(() => ItemModels.ToList());
        }

        public async Task<JournalItemModel> GetFirstAsync()
        {
            return await Task.Run(() => ItemModels.First());
        }

        public IQueryable<JournalItemModel> GetViaIQueriable()
        {
            return ItemModels;
        }

        public async Task<JournalItemModel?> GetFirstAsync(Func<JournalItemModel, bool> func)
        {
            return await Task.Run(() => ItemModels.FirstOrDefault(func));
        }

        public async Task UpdateAsync(JournalUpdateModel item)
        {
            var record = _context.Journals.First(j => j.Id == item.Id);
            record.ComingDate = item.NewComingDate;
            record.CarId = item.NewCarId;
            record.DepartureDate = item.NewDepartureDate;
            record.ParkingPlace = item.NewParkingPlace;
            
            if (!IsValid(record, out string explanation))
            {
                throw new Exception(explanation);
            }

            _context.Journals.Add(record);
            await _context.SaveChangesAsync();
        }

        public bool IsValid(Journal record, out string explanation)
        {
            if (record == null)
            {
                explanation = "Record is null";
                return false;
            }

            var car = _context.Cars.FirstOrDefault(c => c.Id == record.CarId);
            if (car == null)
            {
                explanation = "Car not found";
                return false;
            }

            var parkingPlace = _context.ParkingPlaces.FirstOrDefault(c => c.Id == record.ParkingPlace);
            if (parkingPlace == null)
            {
                explanation = "Parking place not found";
                return false;
            }

            if (car.CarType != parkingPlace.CarType)
            {
                explanation = "Car's type is not equal to parking place's type";
                return false;
            }

            var comingDate = record.ComingDate;
            var departureDate = record.DepartureDate ?? DateTime.MaxValue;
            if (comingDate > departureDate)
            {
                explanation = "Coming date is bigger than departure date";
                return false;
            }

            if (_context.Journals.Any(j => j.Id != record.Id && j.CarId == record.CarId && (j.ComingDate <= departureDate && (j.DepartureDate ?? ((DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue)) >= comingDate)))
            {
                explanation = "Car is in parking at this period";
                return false;
            }

            if (_context.Journals.Any(j => j.Id != record.Id && j.ParkingPlace == record.ParkingPlace && (j.ComingDate <= departureDate && (j.DepartureDate ?? ((DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue)) >= comingDate)))
            {
                explanation = "Place is taken at this period";
                return false;
            }

            explanation = string.Empty;
            return true;
        }
    }
}
﻿using WEB_EF.Models.Classes;
using WEB_EF.Models.Interfaces;

namespace WEB_EF.Models.Services
{
    public class JournalValidateService : IValidateService<Journal>
    {
        private readonly IAutoparkDBContext _context;
        private readonly ICRUDlService<Journal> _service;

        public JournalValidateService(IAutoparkDBContext context, ICRUDlService<Journal> service)
        {
            _context = context;
            _service = service;
        }

        public bool Validate(Journal record, out string explanation)
        {
            if (record == null)
            {
                explanation = "Record is null";
                return false;
            }

            if (record.IsDeleted)
            {
                explanation = "Record is deleted";
                return false;
            }

            var car = _context.Cars.FirstOrDefault(c => c.Id == record.CarId && !c.IsDeleted);
            if (car == null)
            {
                explanation = "Car is null";
                return false;
            }

            var parkingPlace = _context.ParkingPlaces.FirstOrDefault(c => c.Id == record.ParkingPlace && !c.IsDeleted);
            if (parkingPlace == null)
            {
                explanation = "Parking place is null";
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

            if (_service.GetViaIQueriable().Any(j => !j.IsDeleted && j.Id != record.Id && j.CarId == record.CarId && (j.ComingDate <= departureDate && (j.DepartureDate ?? ((DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue)) >= comingDate)))
            {
                explanation = "Car is in parking at this period";
                return false;
            }

            if (_service.GetViaIQueriable().Any(j => !j.IsDeleted && j.Id != record.Id && j.ParkingPlace == record.ParkingPlace && (j.ComingDate <= departureDate && (j.DepartureDate ?? ((DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue)) >= comingDate)))
            {
                explanation = "Place is taken at this period";
                return false;
            }


            explanation = string.Empty;
            return true;
        }
    }
}

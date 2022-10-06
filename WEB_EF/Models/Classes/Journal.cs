using System;
using System.Collections.Generic;

namespace WEB_EF.Models.Classes
{
    public partial class Journal
    {
        public int Id { get; set; }
        public DateTime ComingDate { get; set; }
        public int CarId { get; set; }
        public int ParkingPlace { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DepartureDate { get; set; }

        public virtual Car Car { get; set; } = null!;
        public virtual ParkingPlace ParkingPlaceNavigation { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;

namespace WEB_EF.Models.Classes
{
    public partial class CarType
    {
        public CarType()
        {
            Cars = new HashSet<Car>();
            ParkingPlaces = new HashSet<ParkingPlace>();
        }

        public int Id { get; set; }
        public string TypeName { get; set; } = null!;

        public virtual ICollection<Car> Cars { get; set; }
        public virtual ICollection<ParkingPlace> ParkingPlaces { get; set; }
    }
}

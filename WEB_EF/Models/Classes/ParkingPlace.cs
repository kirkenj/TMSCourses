using System;
using System.Collections.Generic;

namespace WEB_EF.Models.Classes
{
    public partial class ParkingPlace
    {
        public ParkingPlace()
        {
            Journals = new HashSet<Journal>();
        }

        public int Id { get; set; }
        public int CarType { get; set; }

        public virtual CarType CarTypeNavigation { get; set; } = null!;
        public virtual ICollection<Journal> Journals { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;

namespace WEB_EF.Models.Classes
{ 
    public partial class Client
    {
        public Client()
        {
            Cars = new HashSet<Car>();
        }

        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public int Id { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}

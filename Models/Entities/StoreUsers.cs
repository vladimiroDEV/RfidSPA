﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RfidSPA.Models.Entities
{
    public class StoreUsers
    {
        public long StoreUsersID { get; set; }
        public string UserRole { get; set; }
        public string UserID { get; set; }

        public long StoreID { get; set; }
        public virtual Store Store { get; set; }

    }
}
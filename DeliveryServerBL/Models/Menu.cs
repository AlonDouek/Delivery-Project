﻿using System;
using System.Collections.Generic;

#nullable disable

namespace DeliveryServerBL.Models
{
    public partial class Menu
    {
        public Menu()
        {
            MenuItems = new HashSet<MenuItem>();
        }

        public int MenuId { get; set; }
        public int RestaurantId { get; set; }

        public virtual Restaurant Restaurant { get; set; }
        public virtual ICollection<MenuItem> MenuItems { get; set; }
    }
}

﻿using System;

namespace ContosoShop
{
    public class OrderDetails
    {
        public string OrderID { get; set; }
        public string TrackingNumber { get; set; }
        public string ItemName { get; set; }
        public DateTime OrderPlacedTime { get; set; }
        public string OrderStatus { get; set; }

    }
}

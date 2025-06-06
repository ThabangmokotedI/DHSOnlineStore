﻿namespace DHSOnlineStore.Models
{
    public class CartDetail
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
    }
}

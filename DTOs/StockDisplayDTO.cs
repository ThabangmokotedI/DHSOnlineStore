﻿namespace DHSOnlineStore.DTOs
{
	public class StockDisplayDTO
	{
		public int Id { get; set; }
		public int ProductId { get; set; }
		public int Quantity { get; set; }
		public string? ProductName { get; set; }
	}
}

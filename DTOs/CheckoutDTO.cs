﻿using System.ComponentModel.DataAnnotations;

namespace DHSOnlineStore.DTOs
{
    public class CheckoutDTO
    {
        [Required]
        [MaxLength(200)]
        public string? Name { get; set; }
        [EmailAddress]
        [MaxLength(30)]
        public string? Email { get; set; }
        [Required]
        public string? MobileNumber { get; set; }
        [Required]
        [MaxLength(200)]
        public string? Address { get; set; }
        [Required]
        [MaxLength(30)]
        public string? PaymentMethod { get; set; }
    }
}

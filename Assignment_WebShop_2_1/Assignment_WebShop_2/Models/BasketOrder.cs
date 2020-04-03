using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment_WebShop_2.Models
{
    public class BasketOrder
    {
        /// <summary>
        /// Vendég neve.
        /// </summary>
        [Required(ErrorMessage = "The name is required")] // feltételek a validáláshoz
        [StringLength(60, ErrorMessage = "Max 60 length")]
        public String Name { get; set; }

        /// <summary>
        /// Vendég e-mail címe.
        /// </summary>
        [Required(ErrorMessage = "E-mail is required")]
        [EmailAddress(ErrorMessage = "Bad format for e-mail")]
        [DataType(DataType.EmailAddress)] // pontosítjuk az adatok típusát
        public String Email { get; set; }

        /// <summary>
        /// Vendég címe.
        /// </summary>
        [Required(ErrorMessage = "Address is required.")]
        public String Address { get; set; }

        /// <summary>
        /// Vendég telefonszáma.
        /// </summary>
        [Required(ErrorMessage = "Phone Number is required")]
        [Phone(ErrorMessage = "Bad format for phone number")]
        [DataType(DataType.PhoneNumber)]
        public String PhoneNumber { get; set; }

        /// <summary>
        /// Teljes ár.
        /// </summary>
        [DataType(DataType.Currency)]
        public double TotalPrice { get; set; }
    }
}

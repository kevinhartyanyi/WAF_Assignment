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
        [Required(ErrorMessage = "A név megadása kötelező.")] // feltételek a validáláshoz
        [StringLength(60, ErrorMessage = "A foglaló neve maximum 60 karakter lehet.")]
        public String Name { get; set; }

        /// <summary>
        /// Vendég e-mail címe.
        /// </summary>
        [Required(ErrorMessage = "Az e-mail cím megadása kötelező.")]
        [EmailAddress(ErrorMessage = "Az e-mail cím nem megfelelő formátumú.")]
        [DataType(DataType.EmailAddress)] // pontosítjuk az adatok típusát
        public String Email { get; set; }

        /// <summary>
        /// Vendég címe.
        /// </summary>
        [Required(ErrorMessage = "A cím megadása kötelező.")]
        public String Address { get; set; }

        /// <summary>
        /// Vendég telefonszáma.
        /// </summary>
        [Required(ErrorMessage = "A telefonszám megadása kötelező.")]
        [Phone(ErrorMessage = "A telefonszám formátuma nem megfelelő.")]
        [DataType(DataType.PhoneNumber)]
        public String PhoneNumber { get; set; }

        /// <summary>
        /// Teljes ár.
        /// </summary>
        [DataType(DataType.Currency)]
        public double TotalPrice { get; set; }
    }
}

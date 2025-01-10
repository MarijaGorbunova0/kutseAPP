using System;
using System.ComponentModel.DataAnnotations;

namespace Kutse_App.Models
{
    public class Guest
    {
        [Required(ErrorMessage = "Sisestage nimi!")]
        [StringLength(100, ErrorMessage = "Nimi ei saa olla pikem kui 100 tähemärki.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Sisestage email!")]
        [EmailAddress(ErrorMessage = "Valesti sisestatud email!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Sisestage telefoni number!")]
        [RegularExpression(@"\+372.+", ErrorMessage = "Numbri alguses peab olema +372 ja järgneb 7 numbrit.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Sisestage oma valik!")]
        public bool? WillAttend { get; set; }
    }
}

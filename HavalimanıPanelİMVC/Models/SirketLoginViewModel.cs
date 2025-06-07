using System.ComponentModel.DataAnnotations;

namespace HavalimaniPanelMVC.Models
{
    public class SirketLoginViewModel
    {
        [Required(ErrorMessage = "E-posta alanı boş bırakılamaz.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre alanı boş bırakılamaz.")]
        public string Sifre { get; set; }
    }
}

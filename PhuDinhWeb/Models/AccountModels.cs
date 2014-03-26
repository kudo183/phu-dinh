using System.ComponentModel.DataAnnotations;

namespace PhuDinhWeb.Models
{
    public class LogOnModel
    {
        //[Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mat khau")]
        public string Password { get; set; }

        [Display(Name = "Ghi nho dang nhap?")]
        public bool RememberMe { get; set; }
    }
}
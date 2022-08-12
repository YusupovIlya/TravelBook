using System.ComponentModel.DataAnnotations;

namespace TravelBook.Web.ViewModels.AccountViewModels;

public class AboutMeViewModel
{
    [Display(Name = "Your username: ", Description = "jkjd")]
    public string UserName { get; set; }

    [Display(Name = "Your email: ", Description = "1")]
    public string Email { get; set; }

    [Display(Name = "Your phonenumber: ", Description = "2")]
    public string PhoneNumber { get; set; }
}

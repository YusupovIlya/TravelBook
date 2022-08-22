using System.ComponentModel.DataAnnotations;

namespace TravelBook.Web.ViewModels.TravelViewModels;

public class NewTravelViewModel : IValidatableObject
{
    public string UserId { get; set; }
    public string City { get; set; }
    public string Country { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Travel start date")]
    public DateTime? DateStartTravel { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Travel finish date")]
    public DateTime? DateFinishTravel { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (DateFinishTravel < DateStartTravel)
        {
            yield return new ValidationResult(
                errorMessage: "Travel finish date must be greater than start date",
                memberNames: new[] { "DateFinishTravel" }
           );
        }
    }
}

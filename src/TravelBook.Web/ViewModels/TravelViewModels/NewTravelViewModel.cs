using System.ComponentModel.DataAnnotations;

namespace TravelBook.Web.ViewModels.TravelViewModels;

public class NewTravelViewModel : IValidatableObject
{
    public string UserId { get; set; }
	[Required]
    public string City { get; set; }
	[Required]
    public string Country { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Travel start date")]
    public DateTime? DateStartTravel { get; set; }

	[Required]
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

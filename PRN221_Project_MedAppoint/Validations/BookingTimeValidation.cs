using System.ComponentModel.DataAnnotations;
using static PRN221_Project_MedAppoint.Areas.User.Pages.Customer.CheckoutModel;

namespace PRN221_Project_MedAppoint.Validations
{
    public class BookingTimeValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var appointmentInput = (AppointmentInput)validationContext.ObjectInstance;

            if (appointmentInput.BeginTime >= appointmentInput.EndTime)
            {
                return new ValidationResult("End Time must be greater than Begin Time.");
            }

            return ValidationResult.Success;
        }
    }
}

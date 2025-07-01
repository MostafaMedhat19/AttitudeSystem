using AttitudeSystem.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace AttitudeSystem.Application.DTOs.RegisterDtos
{
    public class StudentRegisterDto
    {
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100, ErrorMessage = "Name must be less than 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "National ID is required.")]
        [StringLength(14, MinimumLength = 10, ErrorMessage = "National ID must be between 10 and 14 digits.")]
        public string NationalId { get; set; }

        [Required(ErrorMessage = "Guardian phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        [MaxLength(20, ErrorMessage = "Phone number must be less than 20 characters.")]
        public string GuardianPhoneNumber { get; set; }

        [Required(ErrorMessage = "Class name is required.")]
        [MaxLength(50, ErrorMessage = "Class name must be less than 50 characters.")]
        public string ClassName { get; set; }

        public Grade Grade { get; set; }

    }
}

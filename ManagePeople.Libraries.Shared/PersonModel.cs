using System.ComponentModel.DataAnnotations;

namespace ManagePeople.Libraries.Shared
{
    public class PersonModel
    {
        public int PersonId { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Surname { get; set; }

        [Required]
        public string IdNumber { get; set; } = string.Empty;
    }
}

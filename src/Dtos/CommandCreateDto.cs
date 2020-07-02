using System.ComponentModel.DataAnnotations;

namespace Commander.Dtos
{
    public class CommandCreateDto
    {
        // Annotations are added to reflect the configuration of the db table
        // i.e. Mark required properties for non-nullable fields in db to prevent errors
        [Required]
        [MaxLength(250)]
        public string HowTo { get; set; }

        [Required]
        public string Line { get; set; }

        [Required]
        public string Platform { get; set; }
    }
}
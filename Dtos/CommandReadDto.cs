using System.ComponentModel.DataAnnotations;

namespace netcoreapi.Dtos
{

    public class CommandReadDto
    {

        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string HowTo { get; set; }

        [Required]
        [MaxLength(250)]
        public string Line { get; set; }

    }

}

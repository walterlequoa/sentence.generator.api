using System.ComponentModel.DataAnnotations;

namespace sentence.generator.api.RequestModel
{
    public class Sentence
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Words { get; set; }
        public ApplicationUser User { get; set; }
    }
}

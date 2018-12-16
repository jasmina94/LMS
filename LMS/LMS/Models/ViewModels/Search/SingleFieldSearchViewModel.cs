using System.ComponentModel.DataAnnotations;

namespace LMS.Models.ViewModels.Search
{
    public class SingleFieldSearchViewModel
    {
        [Required]
        public string FieldName { get; set; }

        [Required]
        public string FieldValue { get; set; }

        [RegularExpression("^(STANDARD|PHRASE|FUZZY)$")]
        public string QueryType { get; set; }

        [Required]
        public string Language { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace LMS.Models.ViewModels.Search
{
    public class MultiFieldSearchViewModel
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public string Keywords { get; set; }

        public string Content { get; set; }

        public string Language { get; set; }

        [RegularExpression("^(STANDARD|PHRASE|FUZZY)$")]
        public string QueryType { get; set; }

        [RegularExpression("^(OR|AND)$")]
        public string QueryOperator{ get; set; }
    }
}
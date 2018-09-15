using System.Collections.Generic;

namespace LMS.DomainModel.Infrastructure.FilterMapper.Model
{
    public class FilterSorterModel
    {
        public List<FilterModel> Filter { get; set; }

        public SorterModel Sorter { get; set; }
    }
}

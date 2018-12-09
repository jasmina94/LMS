using LMS.IR.Model;
using System.Collections.Generic;

namespace LMS.Models.ViewModels.Search
{
    public class SearchViewModel
    {
        public SingleFieldSearchViewModel SFSFilter { get; set; }

        public MultiFieldSearchViewModel MFSFilter { get; set; }

        public List<ResultData> Result { get; set; }

        public bool IsSuccess => (Result != null && Result.Count != 0) ? true : false;

        public SearchViewModel()
        {

        }

        public SearchViewModel(SingleFieldSearchViewModel sfsFilter, List<ResultData> results)
        {
            SFSFilter = sfsFilter;
            Result = results;
        }

        public SearchViewModel(MultiFieldSearchViewModel mfsFilter, List<ResultData> results)
        {
            MFSFilter = mfsFilter;
            Result = results;
        }

        public SearchViewModel(SingleFieldSearchViewModel sfsFilter, MultiFieldSearchViewModel mfsFilter, 
            List<ResultData> results)
        {
            SFSFilter = sfsFilter;
            MFSFilter = mfsFilter;
            Result = results;
        }
    }
}
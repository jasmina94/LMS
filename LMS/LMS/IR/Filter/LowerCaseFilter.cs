using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Tokenattributes;

namespace LMS.IR.Filter
{
    public class LowerCaseFilter : TokenFilter
    {
        private TermAttribute termAttribute;

        public LowerCaseFilter(TokenStream input) : base(input)
        {
            termAttribute = input.AddAttribute<TermAttribute>();
        }

        public override bool IncrementToken()
        {
            bool success = false;
            if (input.IncrementToken())
            {
                string text = termAttribute.ToString();
                termAttribute.Clear();
                termAttribute.SetTermBuffer(text.ToLower());
                success = true;
            }
            return success;
        }
    }
}
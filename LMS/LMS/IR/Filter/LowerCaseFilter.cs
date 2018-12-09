using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Tokenattributes;

namespace LMS.IR.Filter
{
    public class LowerCaseFilter : TokenFilter
    {
        private ITermAttribute termAttribute;

        public LowerCaseFilter(TokenStream input) : base(input)
        {
            termAttribute = input.AddAttribute<ITermAttribute>();
        }

        public override bool IncrementToken()
        {
            bool success = false;
            if (input.IncrementToken())
            {
                string text = termAttribute.ToString();
                termAttribute.ResizeTermBuffer(0);
                termAttribute.SetTermBuffer(text.ToLower());
                success = true;
            }
            return success;
        }
    }
}
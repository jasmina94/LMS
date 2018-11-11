using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Tokenattributes;

namespace LMS.IR.Filter
{
    public class CyrillicToLatinFilter : TokenFilter
    {
        private TermAttribute termAttribute;

        public CyrillicToLatinFilter(TokenStream input) : base(input)
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
                termAttribute.SetTermBuffer(CyrillicLatinConverter.Cir2lat(text));
                success = true;
            }
            return success;
        }
    }
}
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Tokenattributes;

namespace LMS.IR.Filter
{
    public class CyrillicToLatinFilter : TokenFilter
    {
        private readonly ITermAttribute termAttribute;

        public CyrillicToLatinFilter(TokenStream input) : base(input)
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
                termAttribute.SetTermBuffer(CyrillicLatinConverter.Cir2lat(text));
                success = true;
            }
            return success;
        }
    }
}
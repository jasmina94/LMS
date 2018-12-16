using Lucene.Net.Analysis;
using Lucene.Net.Analysis.TokenAttributes;

namespace LMS.IR.Filter
{
    public sealed class CyrllicToLatinFilter : TokenFilter
    {
        private CharTermAttribute termAttribute;

        public CyrllicToLatinFilter(TokenStream input) : base(input)
        {
            termAttribute = (CharTermAttribute)input.AddAttribute<ICharTermAttribute>();
        }

        public override bool IncrementToken()
        {
            bool success = false;
            if (m_input.IncrementToken())
            {
                string text = termAttribute.ToString();
                termAttribute.Clear();
                termAttribute.Append(CyrillicLatinConverter.Cir2lat(text));
                success = true;
            }
            return success;
        }
    }
}
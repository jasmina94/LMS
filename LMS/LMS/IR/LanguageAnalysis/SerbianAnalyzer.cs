using System.IO;
using Lucene.Net.Analysis;
using Lucene.Net.Util;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Analysis.Core;
using LMS.IR.Filter;
using Lucene.Net.Analysis.Miscellaneous;
using Lucene.Net.Analysis.Snowball;
using LMS.IR.Stemmer;

namespace LMS.IR.LanguageAnalysis
{
    public sealed class SerbianAnalyzer : Analyzer
    {
        private readonly LuceneVersion matchVersion;

        private readonly  string[] STOP_WORDS = {
            "biti", "jesam", "jeste", "jesmo", "jesu", "sam",
            "si", "je", "smo", "ste", "su",
            "nisam", "niste", "nismo", "nisu",
            "hoću", "hoćeš", "hoće", "hoćemo", "hoćete", "hoće",
            "ću", "ćeš", "će", "ćemo", "ćete",
            "hocu", "hoces", "hoce", "hocemo", "hocete", "hoce",
            "ce", "ces", "ce", "cemo", "cete",
            "budem", "budeš", "bude", "budemo", "budete", "budu",
            "bio", "bili", "bih", "bi", "bismo", "biste",
            "i", "ili", "pa", "te", "ni", "a", "ali", "na", "u", "po", "li", "da", "sa", "dok"
        };

        public SerbianAnalyzer(LuceneVersion matchVersion)
        {
            this.matchVersion = matchVersion;
        }

        protected override TokenStreamComponents CreateComponents(string fieldName, TextReader reader)
        {
            TokenStreamComponents tokenStreamComponents = null;
            Tokenizer tokenizer = new StandardTokenizer(matchVersion, reader);
            TokenStream stream = new LowerCaseFilter(matchVersion, tokenizer);

            stream = new CyrllicToLatinFilter(stream);
            stream = new StopFilter(matchVersion, stream, StopFilter.MakeStopSet(matchVersion, STOP_WORDS));
            stream = new SnowballFilter(stream, new SimpleSerbianStemmer());
            stream = new ASCIIFoldingFilter(stream);

            tokenStreamComponents = new TokenStreamComponents(tokenizer, stream);

            return tokenStreamComponents;
        }
    }
}
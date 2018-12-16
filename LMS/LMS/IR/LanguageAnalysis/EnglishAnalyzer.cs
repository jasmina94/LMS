using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Core;
using Lucene.Net.Analysis.En;
using Lucene.Net.Analysis.Snowball;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Tartarus.Snowball.Ext;
using Lucene.Net.Util;
using System.IO;

namespace LMS.IR.LanguageAnalysis
{
    public sealed class EnglishAnalyzer : Analyzer
    {
        private readonly LuceneVersion matchVersion;

        public EnglishAnalyzer(LuceneVersion matchVersion)
        {
            this.matchVersion = matchVersion;
        }

        protected override TokenStreamComponents CreateComponents(string fieldName, TextReader reader)
        {
            TokenStreamComponents tokenStreamComponents = null;
            Tokenizer tokenizer = new StandardTokenizer(matchVersion, reader);
            TokenStream stream = new StandardFilter(matchVersion, tokenizer);

            stream = new LowerCaseFilter(matchVersion, stream);
            stream = new StopFilter(matchVersion, stream, StopAnalyzer.ENGLISH_STOP_WORDS_SET);
            stream = new PorterStemFilter(stream);
            stream = new SnowballFilter(stream, new EnglishStemmer());

            tokenStreamComponents = new TokenStreamComponents(tokenizer, stream);

            return tokenStreamComponents;
        }

        protected override TextReader InitReader(string fieldName, TextReader reader)
        {
            return base.InitReader(fieldName, reader);
        }
    }
}
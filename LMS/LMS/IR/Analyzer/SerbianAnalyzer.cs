using LMS.IR.Filter;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Util;
using System.Collections.Generic;
using System.IO;

namespace LMS.IR.Analyzer
{
    public class SerbianAnalyzer : StandardAnalyzer
    {
        private readonly Version version;

        private readonly string[] STOP_WORDS = {
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

        public SerbianAnalyzer(Version version) : base(version)
        {
            this.version = version;
        }


        public override TokenStream TokenStream(string fieldName, TextReader reader)
        {

            Tokenizer tokenizer = new StandardTokenizer(version, reader);
            TokenStream tokenStream = new Filter.LowerCaseFilter(tokenizer);        

            tokenStream = new CyrillicToLatinFilter(tokenStream);
            
            tokenStream = new StopFilter(true, tokenStream, StopFilter.MakeStopSet(STOP_WORDS));

            //tokenStream = new SnowballFilter(tokenStream, new SerbianStemmer());

            tokenStream = new ASCIIFoldingFilter(tokenStream);

            return tokenStream;
        }
    }
}
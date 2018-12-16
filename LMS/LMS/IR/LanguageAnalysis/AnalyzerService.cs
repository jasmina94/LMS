using LMS.IR.Indexer;
using Lucene.Net.Analysis;
using Lucene.Net.Util;

namespace LMS.IR.LanguageAnalysis
{
    public static class AnalyzerService
    {
        private static readonly LuceneVersion VERSION = LuceneVersion.LUCENE_48;

        public static Analyzer GetAnalyzer(IndexerType type)
        {
            Analyzer analyzer = null;

            if (type.Equals(IndexerType.ENGLISH))
            {
                analyzer = new EnglishAnalyzer(VERSION);
            }
            else
            {
                analyzer = new SerbianAnalyzer(VERSION);
            }

            return analyzer;
        }

        public static IndexerType GetIndexerType(string language)
        {
            IndexerType type;
            if (language.Equals("English"))
            {
                type = IndexerType.ENGLISH;
            }
            else
            {
                type = IndexerType.SERBIAN;
            }

            return type;
        }
    }
}
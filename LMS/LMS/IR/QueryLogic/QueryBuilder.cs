using LMS.IR.Analyzer;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Util;

namespace LMS.IR.QueryLogic
{
    public class QueryBuilder
    {
        private static Version version = Version.LUCENE_20;

        private static StandardAnalyzer analyzer = new SerbianAnalyzer(version);

        public static Query BuildQuery(QueryType queryType, string fieldName, string fieldValue)
        {
            Query query = null;
            QueryParser queryParser = new QueryParser(version, fieldName, analyzer);

            fieldName = fieldName.Trim();
            fieldValue = fieldValue.Trim();

            if (queryType.Equals(QueryType.STANDARD))
            {
                try
                {
                    query = queryParser.Parse(fieldValue);
                }
                catch (ParseException exception)
                {
                    System.Console.WriteLine(exception);
                }
            }
            else if (queryType.Equals(QueryType.PHRASE))
            {
                string[] tokens = fieldValue.Split(' ');
                query = new PhraseQuery();
                foreach (string token in tokens)
                {
                    Term term = new Term(fieldName, token);
                    ((PhraseQuery)query).Add(term);
                }
            }
            else if (queryType.Equals(QueryType.FUZZY))
            {
                Term term = new Term(fieldName, fieldValue);
                query = new FuzzyQuery(term);
            }

            return query;
        }
    }
}
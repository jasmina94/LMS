using LMS.IR.Indexer;
using LMS.IR.LanguageAnalysis;
using Lucene.Net.Analysis;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Support;
using Lucene.Net.Util;
using System;

namespace LMS.IR.QueryLogic
{
    public class QueryBuilder
    {
        private static readonly LuceneVersion VERSION = LuceneVersion.LUCENE_48;

        public static Query BuildQuery(IndexerType indexerType, QueryType queryType, string fieldName, string fieldValue)
        {
            Query query = null;
            Analyzer analyzer = AnalyzerService.GetAnalyzer(indexerType);

            QueryParser queryParser = new QueryParser(VERSION, fieldName, analyzer);

            fieldName = fieldName.Trim();
            fieldValue = fieldValue.Trim();

            if (queryType.Equals(QueryType.STANDARD))
            {
                try {
                    query = queryParser.Parse(fieldValue);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            else if (queryType.Equals(QueryType.FUZZY))
            {
                Term term = new Term(fieldName, fieldValue);
                query = new FuzzyQuery(term);
            }

            else if (queryType.Equals(QueryType.PHRASE))
            {
                StringTokenizer tokenizedFieldValue = new StringTokenizer(fieldValue);
                query = new PhraseQuery();

                while (tokenizedFieldValue.HasMoreTokens()) {
                    Term term = new Term(fieldName, tokenizedFieldValue.NextToken());
                    ((PhraseQuery)query).Add(term);
                }
            }

            return query;
        }
    }
}
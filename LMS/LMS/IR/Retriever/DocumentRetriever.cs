using System;
using System.IO;
using LMS.IR.Analyzer;
using Lucene.Net.Store;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Documents;
using Lucene.Net.QueryParsers;
using System.Collections.Generic;
using Lucene.Net.Analysis.Standard;

namespace LMS.IR.Retriever
{
    public class DocumentRetriever
    {
        private StandardAnalyzer analyzer;
        private string indexDirectoryPath;

        private readonly int MAX_HITS = 10;
        private readonly Lucene.Net.Util.Version version = Lucene.Net.Util.Version.LUCENE_20;

        public DocumentRetriever(string indexDirPath, int maxHits)
        {
            analyzer = new SerbianAnalyzer(version);
            indexDirectoryPath = indexDirPath;
            MAX_HITS = maxHits;
        }

        public DocumentRetriever(string indexDirPath)
        {
            analyzer = new SerbianAnalyzer(version);
            indexDirectoryPath = indexDirPath;
        }

        public List<Document> RetrieveDocuments(Query query, bool analyze, Sort sort)
        {
            var documents = new List<Document>();
            if (query != null)
            {
                try
                {
                    Lucene.Net.Store.Directory indexDirectory = FSDirectory.Open(new DirectoryInfo(indexDirectoryPath));

                    IndexReader indexReader = IndexReader.Open(indexDirectory, true);
                    Searcher indexSearcher = new IndexSearcher(indexReader);
                    var filter = new QueryWrapperFilter(query);

                    ScoreDoc[] scoreDocs = null;

                    if (analyze)
                    {
                        QueryParser queryParser = new QueryParser(version, "", analyzer);
                        query = queryParser.Parse(query.ToString());
                    }

                    if (sort == null)
                    {
                        sort = Sort.INDEXORDER;
                    }

                    scoreDocs = indexSearcher.Search(query, filter, MAX_HITS, sort).ScoreDocs;

                    foreach (ScoreDoc scoreDoc in scoreDocs)
                    {
                        documents.Add(indexSearcher.Doc(scoreDoc.Doc));
                    }

                }
                catch (Exception e)
                {
                    throw e;
                }

            }

            return documents;
        }
    }
}
using LMS.IR.Indexer;
using LMS.IR.LanguageAnalysis;
using Lucene.Net.Analysis;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;
using System;
using System.Collections.Generic;
using System.IO;

namespace LMS.IR.Retriever
{
    public class DocumentRetriever
    {
        private static readonly LuceneVersion VERSION = LuceneVersion.LUCENE_48;

        private readonly int MAX_HITS = 10;

        private string indexDirectoryPath;

        private Analyzer analyzer;

        public DocumentRetriever(IndexerType indexerType, string indexDirPath, int maxHits)
        {
            MAX_HITS = maxHits;
            indexDirectoryPath = indexDirPath;
            analyzer = AnalyzerService.GetAnalyzer(indexerType);
        }

        public DocumentRetriever(IndexerType indexerType, string indexDirPath)
        {
            indexDirectoryPath = indexDirPath;
            analyzer = AnalyzerService.GetAnalyzer(indexerType);
        }

        public List<Document> RetrieveDocuments(Query query, bool analyze, Sort sort)
        {
            var documents = new List<Document>();
            if (query != null)
            {
                try
                {
                    Lucene.Net.Store.Directory indexDirectory = FSDirectory.Open(new DirectoryInfo(indexDirectoryPath));
                    DirectoryReader directoryReader = DirectoryReader.Open(indexDirectory);

                    IndexSearcher indexSearcher = new IndexSearcher(directoryReader);
                    ScoreDoc[] scoreDocs = null;

                    if (analyze)
                    {
                        QueryParser queryParser = new QueryParser(VERSION, "", analyzer);
                        query = queryParser.Parse(query.ToString());
                    }
                    if (sort == null)
                    {
                        sort = Sort.INDEXORDER;
                    }

                    scoreDocs = indexSearcher.Search(query, MAX_HITS, sort).ScoreDocs;

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
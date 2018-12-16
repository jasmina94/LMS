using LMS.IR.Indexer;
using LMS.IR.LanguageAnalysis;
using LMS.IR.Model;
using Lucene.Net.Analysis;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Search.Highlight;
using Lucene.Net.Store;
using Lucene.Net.Util;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LMS.IR.Retriever
{
    public class InformationRetriever
    {
        private static readonly LuceneVersion VERSION = LuceneVersion.LUCENE_48;

        private string rawDirectoryPath;

        private string indexDirectoryPath;

        private DocumentRetriever documentRetriever;

        public InformationRetriever(IndexerType indexerType, string rawPath, string indexPath, int maxHits)
        {
            rawDirectoryPath = rawPath;
            indexDirectoryPath = indexPath;
            documentRetriever = new DocumentRetriever(indexerType, indexDirectoryPath, maxHits);
        }

        public InformationRetriever(IndexerType indexerType, string rawPath, string indexPath)
        {
            rawDirectoryPath = rawPath;
            indexDirectoryPath = indexPath;
            documentRetriever = new DocumentRetriever(indexerType, indexDirectoryPath);
        }

        public List<ResultData> RetrieveEBooks(IndexerType indexerType, Query query, List<string> fieldNames, Sort sort)
        {
            var results = new List<ResultData>();
            List<Document> documents = documentRetriever.RetrieveDocuments(query, true, sort);
            Analyzer analyzer = AnalyzerService.GetAnalyzer(indexerType);

            foreach (Document document in documents)
            {
                ResultData resultData = GenerateResultData(document);
                resultData.Highlights = GenerateHighlights(analyzer, query, document, fieldNames);
                results.Add(resultData);
            }

            return results;
        }

        private ResultData GenerateResultData(Document document)
        {
            ResultData resultData = new ResultData();

            resultData.Id = int.Parse(document.Get("Id"));
            resultData.Title = document.Get("Title");
            resultData.Language = document.Get("Language");
            resultData.Category = document.Get("Category");
            resultData.Filename = document.Get("Filename");
            resultData.Author = document.Get("Author");
            resultData.Keywords = GenerateKeywords(document);
            resultData.Year = document.Get("Year");

            return resultData;
        }

        private string GenerateKeywords(Document document)
        {
            string result = "";
            StringBuilder stringBuilder = new StringBuilder("");

            string[] keywords = document.GetValues("Keyword");
            foreach (string keyword in keywords)
            {
                stringBuilder.Append(keyword).Append(",");
            }

            if (!string.IsNullOrEmpty(stringBuilder.ToString()))
            {
                result = stringBuilder.ToString().Trim();
                result = result.Substring(0, result.Length - 1);
            }            

            return result;
        }

        private string GenerateHighlights(Analyzer analyzer, Query query, Document document, List<string> fieldNames)
        {
            string highlights = "";
            StringBuilder stringBuilder = new StringBuilder("");

            foreach (string fieldName in fieldNames)
            {
                try
                {
                    Lucene.Net.Store.Directory indexDirectory = FSDirectory.Open(new DirectoryInfo(indexDirectoryPath));
                    DirectoryReader directoryReader = DirectoryReader.Open(indexDirectory);

                    Highlighter highlighter = new Highlighter(new QueryScorer(query, directoryReader, fieldName));

                    string fieldValue = document.Get(fieldName);
                    string highlight = highlighter.GetBestFragment(analyzer, fieldName, fieldValue);

                    if (!string.IsNullOrEmpty(highlight))
                    {
                        stringBuilder.Append(fieldName).Append(": ").Append(highlight.Trim()).Append("...");
                        highlights = stringBuilder.ToString();
                    }
                }
                catch (IOException e)
                {
                    throw e;
                }
                catch (InvalidTokenOffsetsException e)
                {
                    throw e;
                }
            }

            return highlights;
        }
    }
}
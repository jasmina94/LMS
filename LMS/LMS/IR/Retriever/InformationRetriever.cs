using System;
using System.IO;
using System.Text;
using LMS.IR.Model;
using Lucene.Net.Store;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Documents;
using System.Collections.Generic;
using Lucene.Net.Search.Highlight;
using Lucene.Net.Analysis.Standard;

namespace LMS.IR.Retriever
{
    public class InformationRetriever
    {
        private string rawDirectoryPath;

        private string indexDirectoryPath;

        private StandardAnalyzer analyzer;

        private DocumentRetriever documentRetriever;

        public InformationRetriever(string rawDirectoryPath, string indexDirectoryPath, int maxHits)
        {
            this.rawDirectoryPath = rawDirectoryPath;
            this.indexDirectoryPath = indexDirectoryPath;
            documentRetriever = new DocumentRetriever(indexDirectoryPath, maxHits);
        }

        public InformationRetriever(string rawDirectoryPath, string indexDirectoryPath)
        {
            this.rawDirectoryPath = rawDirectoryPath;
            this.indexDirectoryPath = indexDirectoryPath;
            documentRetriever = new DocumentRetriever(indexDirectoryPath);
        }

        //TODO: Check query before call
        //TODO: Set sort properly
        public List<ResultData> RetrieveEBooks(Query query, List<string> fieldNames, Sort sort)
        {
            var results = new List<ResultData>();
            List<Document> documents = documentRetriever.RetrieveDocuments(query, true, sort);

            foreach (Document document in documents)
            {
                ResultData resultData = GenerateResultData(document);
                resultData.Highlights = GenerateHighlights(query, document, fieldNames);
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
            result = stringBuilder.ToString().Trim();
            result = result.Substring(0, result.Length - 1);

            return result;
        }

        private string GenerateHighlights(Query query, Document document, List<string> fieldNames)
        {
            string highlights = "";
            var stringBuilder = new StringBuilder("");
            foreach (string fieldName in fieldNames)
            {
                try
                {
                    Lucene.Net.Store.Directory directory = FSDirectory.Open(new DirectoryInfo(indexDirectoryPath));
                    IndexReader directoryReader = IndexReader.Open(directory, true);

                    Highlighter highlighter = new Highlighter(new QueryScorer(query, directoryReader, fieldName));

                    string fieldValue = document.Get(fieldName);
                    string highlight = highlighter.GetBestFragment(analyzer, fieldName, fieldValue);

                    if (highlight != null && !string.IsNullOrEmpty(highlight))
                    {
                        stringBuilder.Append(fieldName).Append(": ").Append(highlight.Trim()).Append("...");
                        highlights = stringBuilder.ToString();
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            return highlights;
        }
    }
}
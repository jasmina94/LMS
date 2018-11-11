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
    public class InformationRetreiver
    {
        private string rawDirectoryPath;

        private string indexDirectoryPath;

        private StandardAnalyzer analyzer;

        private DocumentRetriever documentRetriever;

        public InformationRetreiver(string rawDirectoryPath, string indexDirectoryPath, int maxHits)
        {
            this.rawDirectoryPath = rawDirectoryPath;
            this.indexDirectoryPath = indexDirectoryPath;
            documentRetriever = new DocumentRetriever(indexDirectoryPath, maxHits);
        }

        public InformationRetreiver(string rawDirectoryPath, string indexDirectoryPath)
        {
            this.rawDirectoryPath = rawDirectoryPath;
            this.indexDirectoryPath = indexDirectoryPath;
            documentRetriever = new DocumentRetriever(indexDirectoryPath);
        }

        //TODO: Check query before call
        //TODO: Set sort properly
        public List<ResultData> retrieveEBooks(Query query, List<string> fieldNames, Sort sort)
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
            resultData.Id = int.Parse(document.Get("id"));
            resultData.Title = document.Get("title");
            resultData.Language = document.Get("language");
            resultData.Category = document.Get("category");
            resultData.Filename = document.Get("filename");
            resultData.Author = document.Get("author");
            resultData.Keywords = GenerateKeywords(document);
            resultData.Year = document.Get("year");

            return resultData;
        }

        private string GenerateKeywords(Document document)
        {
            string result = "";
            StringBuilder stringBuilder = new StringBuilder("");

            string[] keywords = document.GetValues("keyword");
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
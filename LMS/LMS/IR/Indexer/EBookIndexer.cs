using System;
using Lucene.Net.Documents;
using Lucene.Net.Store;
using System.Web.Hosting;
using Lucene.Net.Index;
using Lucene.Net.Analysis;
using LMS.IR.LanguageAnalysis;
using Lucene.Net.Util;
using System.IO;

namespace LMS.IR.Indexer
{
    public class EBookIndexer : IEBookIndexer
    {
        private static readonly LuceneVersion VERSION = LuceneVersion.LUCENE_48;        

        private readonly string INDEX_PATH = HostingEnvironment.MapPath(@"~/Index");

        private readonly Lucene.Net.Store.Directory indexDirectory;

        private IndexWriterConfig indexWriterConfig;

        private IndexWriter indexWriter;

        private Analyzer englishAnalyzer = new EnglishAnalyzer(VERSION);

        private Analyzer serbianAnalyzer = new SerbianAnalyzer(VERSION);

        public EBookIndexer(string path) : this(path, false)
        {
            
        }

        public EBookIndexer(string path, bool restart)
        {
            try
            {
                indexDirectory = FSDirectory.Open(new DirectoryInfo(INDEX_PATH));
                if (restart)
                {
                    indexWriterConfig.OpenMode = OpenMode.CREATE;
                    indexWriter = new IndexWriter(indexDirectory, indexWriterConfig);
                    indexWriter.Commit();
                    indexWriter.Dispose();
                }

            }
            catch (IOException e)
            {
                throw e;
            }
        }       

        public Document[] Get()
        {
            Document[] documents = null;
            try
            {
                DirectoryReader directoryReader = DirectoryReader.Open(indexDirectory);
                documents = new Document[directoryReader.MaxDoc];
                for (int i = 0; i < directoryReader.MaxDoc; i++)
                {
                    documents[i] = directoryReader.Document(i);
                }
                directoryReader.Dispose();
            }
            catch (IOException e)
            {
                documents = null;
            }

            return documents;
        }

        public bool Add(Document document, IndexerType type)
        {
            bool success;
            try
            {
                OpenIndexWriter(type);
                indexWriter.AddDocument(document);
                indexWriter.Commit();
                indexWriter.Dispose();
                success = true;
            }
            catch (IOException e)
            {
                success = false;
            }

            return success;
        }

        public bool Update(Document document, Field[] fields, IndexerType type)
        {
            bool success;
            string id = document.Get("Id");
            ReplaceFields(document, fields);
            try
            {
                lock(this) {
                    OpenIndexWriter(type);
                    indexWriter.UpdateDocument(new Term("Id", id), document);
                    indexWriter.ForceMergeDeletes();
                    indexWriter.DeleteUnusedFiles();
                    indexWriter.Commit();
                    indexWriter.Dispose();
                }
                success = true;
            }
            catch (Exception ex)
            {
                success = false;
            }

            return success;
        }

        public bool DeleteByDocument(Document document, IndexerType type)
        {
            bool success = false;
            if (document != null)
            {
                success = Delete("Id", document.Get("Id"), type);
            }
            return success;
        }

        public bool DeleteById(string fieldValue, IndexerType type)
        {
            return Delete("Id", fieldValue, type);
        }

        private bool Delete(string fieldName, string fieldValue, IndexerType type)
        {
            bool success;
            Term term = new Term(fieldName, fieldValue);
            success = DeleteDocuments(type, term);
            return success;
        }

        private bool DeleteDocuments(IndexerType type, params Term[] terms)
        {
            bool success;
            try
            {
                lock(this) {
                    OpenIndexWriter(type);
                    indexWriter.DeleteDocuments(terms);
                    indexWriter.DeleteUnusedFiles();
                    indexWriter.ForceMergeDeletes();
                    indexWriter.Commit();
                    indexWriter.Dispose();
                }
                success = true;
            }
            catch (IOException e)
            {
                success = false;
            }
            return success;
        }        

        private void OpenIndexWriter(IndexerType type)
        {
            Analyzer analyzer;
            if (type == IndexerType.ENGLISH)
            {
                analyzer = new EnglishAnalyzer(VERSION);
            }
            else
            {
                analyzer = new SerbianAnalyzer(VERSION);
            }
            indexWriterConfig = new IndexWriterConfig(VERSION, analyzer);
            indexWriterConfig.OpenMode = OpenMode.CREATE_OR_APPEND;
            indexWriter = new IndexWriter(indexDirectory, indexWriterConfig);
        }

        private void ReplaceFields(Document document, Field[] fields)
        {
            foreach (Field field in fields)
            {
                document.RemoveFields(field.Name);
            }

            foreach (Field field in fields)
            {
                document.Add(field);
            }
        }
    }
}
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using Lucene.Net.Util;
using System.IO;

namespace LMS.IR.Indexer
{
    public class EBookIndexer : IEBookIndexer
    {
        private readonly Version version;

        private StandardAnalyzer analyzer;

        private IndexWriter indexWriter;

        private Lucene.Net.Store.Directory indexDirectory;

        public EBookIndexer(string path) : this(path, false)
        {
                        
        }

        public EBookIndexer(string path, bool restart)
        {
            try
            {
                indexDirectory = FSDirectory.Open(new DirectoryInfo(path)); 
                if (restart)
                {
                    indexWriter = new IndexWriter(indexDirectory, analyzer, true, IndexWriter.MaxFieldLength.UNLIMITED);
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
                IndexReader reader = IndexReader.Open(indexDirectory, true);
                documents = new Document[reader.MaxDoc];

                for (int i = 0; i < reader.MaxDoc; i++)
                {
                    documents[i] = reader.Document(i);
                }

                reader.Dispose();
            }
            catch (IOException e)
            {
                documents = null;
            }
            return documents;
        }


        public bool Add(Document document)
        {
            bool success;
            try
            {
                OpenIndexWriter();
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

        public bool Update(Document document, Field[] fields)
        {
            bool success;
            string id = document.Get("id");
            ReplaceFields(document, fields);
            try
            {
                lock(this) {
                    OpenIndexWriter();
                    indexWriter.UpdateDocument(new Term("id", id), document);
                    indexWriter.Commit();
                    indexWriter.Dispose();
                }
                success =  true;
            }
            catch (System.Exception e)
            {
                success =  false;
            }

            return success;
        }

        public bool Delete(Document document)
        {
            bool success = false;
            if (document != null)
            {
                success = Delete("id", document.Get("id"));
            }
            return success;
        }

        public bool Delete(string fieldValue)
        {
            return Delete("id", fieldValue);
        }

        public bool Delete(string fieldName, string fieldValue)
        {
            bool success;
            Term term = new Term(fieldName, fieldValue);

            success = Delete(term);

            return success;
        }

        private bool Delete(Term term)
        {
            bool success;
            try
            {
                lock(this) {
                    OpenIndexWriter();
                    indexWriter.DeleteDocuments(term);
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

        private void OpenIndexWriter()
        {
            indexWriter = new IndexWriter(indexDirectory, analyzer, false, IndexWriter.MaxFieldLength.UNLIMITED);
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
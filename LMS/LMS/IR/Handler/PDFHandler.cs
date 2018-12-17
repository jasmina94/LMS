using System;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using LMS.DomainModel.DomainObject;
using Lucene.Net.Documents;

namespace LMS.IR.Handler
{
    public class PDFHandler : DocumentHandler
    {
        public override Document GetDocument(BookData book, string filePath)
        {
            var document = new Document();
            string error = string.Empty;

            AddMetadataToDocument(book, document);

            error = AddContentToDocument(filePath, document);

            if (!string.IsNullOrEmpty(error))
            {
                Console.WriteLine("PDF doc " + book.Title + " error.");
                error += "Document is incomplete. An exception occurred";
                //throw exception
            }

            return document;
        }

        private void AddMetadataToDocument(BookData book, Document document)
        {
            document.Add(new StringField("Id", book.Id.ToString(), Field.Store.YES));
            document.Add(new Lucene.Net.Documents.TextField("Title", book.Title, Field.Store.YES));
            document.Add(new Lucene.Net.Documents.TextField("Filename", book.Filename, Field.Store.YES));
            document.Add(new Lucene.Net.Documents.TextField("Language", book.LanguageId.ToString(), Field.Store.YES));
            document.Add(new Lucene.Net.Documents.TextField("Category", book.CategoryId.ToString(), Field.Store.YES));

            //Check non-required properties
            if (!string.IsNullOrEmpty(book.Author.Trim()))
            {
                document.Add(new Lucene.Net.Documents.TextField("Author", book.Author, Field.Store.YES));
            }
            if (!string.IsNullOrEmpty(book.PublicationYear.ToString()))
            {
                document.Add(new Lucene.Net.Documents.TextField("Year", book.PublicationYear.ToString(), Field.Store.YES));
            }

            if (!string.IsNullOrEmpty(book.Keywords.Trim()))
            {
                string[] keywords = book.Keywords.Trim().Split(',');
                foreach (string keyword in keywords)
                {
                    if (!string.IsNullOrEmpty(keyword.Trim()))
                    {
                        document.Add(new Lucene.Net.Documents.TextField("Keyword", keyword.Trim(), Field.Store.YES));
                    }
                }
            }
        }

        private string AddContentToDocument(string filePath, Document document)
        {
            var error = string.Empty;
            var content = GenerateTextFromPdf(filePath);

            if (!string.IsNullOrEmpty(content))
            {
                document.Add(new Lucene.Net.Documents.TextField("Content", content, Field.Store.YES));
            }
            else
            {
                error = "Document without content.";
            }

            return error;
        }

        private string GenerateTextFromPdf(string filePath)
        {
            var textContent = string.Empty;
            var textBuilder = new StringBuilder();

            using (var reader = new PdfReader(filePath))
            {
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    textBuilder.Append(PdfTextExtractor.GetTextFromPage(reader, i));
                }
            }

            textContent = textBuilder.ToString();

            return textContent;
        }
    }
}
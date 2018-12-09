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
            document.Add(new Field("Id", book.Id.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("Title", book.Title, Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("Filename", book.Filename, Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("Language", book.LanguageId.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("Category", book.CategoryId.ToString(), Field.Store.YES, Field.Index.ANALYZED));

            //Check non-required properties
            if (!string.IsNullOrEmpty(book.Author.Trim()))
            {
                document.Add(new Field("Author", book.Author, Field.Store.YES, Field.Index.ANALYZED));
            }
            if (!string.IsNullOrEmpty(book.PublicationYear.ToString()))
            {
                document.Add(new Field("Year", book.PublicationYear.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            }

            if (!string.IsNullOrEmpty(book.Keywords.Trim()))
            {
                string[] keywords = book.Keywords.Trim().Split(',');
                foreach (string keyword in keywords)
                {
                    if (!string.IsNullOrEmpty(keyword.Trim()))
                    {
                        document.Add(new Field("Keyword", keyword.Trim(), Field.Store.YES, Field.Index.ANALYZED));
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
                document.Add(new Field("content", content, Field.Store.YES, Field.Index.ANALYZED));
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

            using(var reader = new PdfReader(filePath))
            {
                var textBuilder = new StringBuilder();

                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    textBuilder.Append(PdfTextExtractor.GetTextFromPage(reader, i));
                }
            }

            return textContent;
        }
    }
}
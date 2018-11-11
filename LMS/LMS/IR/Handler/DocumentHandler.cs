using LMS.DomainModel.DomainObject;
using Lucene.Net.Documents;

namespace LMS.IR.Handler
{
    public abstract class DocumentHandler
    {
        public abstract Document GetDocument(BookData book, string filePath);
    }
}
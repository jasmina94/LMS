using Lucene.Net.Documents;

namespace LMS.IR.Indexer
{
    public interface IEBookIndexer
    {
        Document[] Get();

        bool Add(Document document);

        bool Update(Document document, Field[] fields);

        bool Delete(Document document);

        bool Delete(string fieldValue);

        bool Delete(string fieldName, string fieldValue);
    }
}

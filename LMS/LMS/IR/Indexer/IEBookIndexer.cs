using Lucene.Net.Documents;

namespace LMS.IR.Indexer
{
    public interface IEBookIndexer
    {
        Document[] Get();

        bool Add(Document document, IndexerType type);

        bool Update(Document document, Field[] fields, IndexerType type);

        bool DeleteByDocument(Document document, IndexerType type);

        bool DeleteById(string fieldValue, IndexerType type);
    }
}

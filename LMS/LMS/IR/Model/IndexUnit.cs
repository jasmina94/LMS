using Lucene.Net.Documents;
using System.Collections.Generic;

namespace LMS.IR.Model
{
    public class IndexUnit
    {
        public string Text { set; get; }

        public string Title { set; get; }

        public List<string> Keywords { set; get; }

        public string FileName { set; get; }

        public string FileDate { set; get; }


        public IndexUnit()
        {

        }

        public IndexUnit(string text, string title, List<string> keywords, string filename, string filedate)
        {
            Text = text;
            Title = title;
            Keywords = keywords;
            FileName = filename;
            FileDate = filedate;
        }

        public Document getLuceneDocument()
        {
            Document retVal = new Document();

            retVal.Add(new Field("Text", Text, Field.Store.YES, Field.Index.ANALYZED));
            retVal.Add(new Field("Title", Title, Field.Store.YES, Field.Index.ANALYZED));
            foreach (string keyword in Keywords)
            {
                retVal.Add(new Field("Keyword", keyword, Field.Store.YES, Field.Index.ANALYZED));
            }
            retVal.Add(new Field("Filename", FileName, Field.Store.YES, Field.Index.ANALYZED));
            retVal.Add(new Field("Filedate", FileDate, Field.Store.YES, Field.Index.ANALYZED));

            return retVal;
        }
    }
}
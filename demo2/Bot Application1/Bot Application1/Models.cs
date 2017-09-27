using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bot_Application1 {

    public class WikiSearchResponse {
        public string batchcomplete { get; set; }
        public ContinueObject Continue { get; set; }
        public QueryObject query { get; set; }

    }

    public class QueryObject {
        public SearchInfoObject searchinfo { get; set; }
        public ICollection<SearchObject> search { get; set; }
    }

    public class SearchObject {
        public int ns { get; set; }
        public string title { get; set; }

        public int pageid { get; set; }
        public int size { get; set; }
        public int wordcount { get; set; }
        public string snippet { get; set; }
        public DateTime timestamp { get; set; }
    }

    public class SearchInfoObject {
        public int totalhits { get; set; }
    }

    public class ContinueObject {
        public int sroffset { get; set; }
        public string Continue { get; set; }
    }

    public class Models {
    }
}
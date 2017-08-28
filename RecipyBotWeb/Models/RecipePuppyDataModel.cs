using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipyBotWeb.Models
{
    public class RecipePuppyDataModel
    {
        public class RecipePuppyResult
        {
            public string title { get; set; }
            public string href { get; set; }
            public string ingredients { get; set; }
            public string thumbnail { get; set; }
        }

        public class RecipePuppyRootObject
        {
            public string title { get; set; }
            public double version { get; set; }
            public string href { get; set; }
            public List<RecipePuppyResult> results { get; set; }
        }
    }
}
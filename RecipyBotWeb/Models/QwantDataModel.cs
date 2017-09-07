using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipyBotWeb.Models
{
    public class QwantDataModel
    {
        public string status { get; set; }
        public QwantDataModelData data { get; set; }
    }

    public class Query
    {
        public string locale { get; set; }
        public string query { get; set; }
        public int offset { get; set; }
    }

    public class Item
    {
        public string title { get; set; }
        public string type { get; set; }
        public string media { get; set; }
        public string desc { get; set; }
        public string thumbnail { get; set; }
        public int thumb_width { get; set; }
        public int thumb_height { get; set; }
        public string width { get; set; }
        public string height { get; set; }
        public string size { get; set; }
        public string url { get; set; }
        public string _id { get; set; }
        public string b_id { get; set; }
        public string media_fullsize { get; set; }
        public string thumb_type { get; set; }
        public int count { get; set; }
    }

    public class Value
    {
        public string value { get; set; }
        public string label { get; set; }
        public bool translate { get; set; }
    }

    public class Size
    {
        public string label { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string selected { get; set; }
        public List<Value> values { get; set; }
    }

    public class Value2
    {
        public string value { get; set; }
        public string label { get; set; }
        public bool translate { get; set; }
    }

    public class License
    {
        public string label { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string selected { get; set; }
        public List<Value2> values { get; set; }
    }

    public class Value3
    {
        public string value { get; set; }
        public string label { get; set; }
        public bool translate { get; set; }
    }

    public class QwantDataModelSource
    {
        public string label { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string selected { get; set; }
        public List<Value3> values { get; set; }
    }

    public class Filters
    {
        public Size size { get; set; }
        public License license { get; set; }
        public QwantDataModelSource source { get; set; }
    }

    public class Result
    {
        public List<Item> items { get; set; }
        public Filters filters { get; set; }
        public string domain { get; set; }
        public string version { get; set; }
        public bool last { get; set; }
    }

    public class Cache
    {
        public string key { get; set; }
        public int created { get; set; }
        public int expiration { get; set; }
        public string status { get; set; }
        public int age { get; set; }
    }

    public class QwantDataModelData
    {
        public Query query { get; set; }
        public Result result { get; set; }
        public Cache cache { get; set; }
    }

}
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace phipgn_csharp_selenium_demo.TestDataAccess
{
    class JsonDataAccess
    {
        public static object[] GetDataSet()
        {
            JObject o = JObject.Parse(File.ReadAllText(ConfigurationManager.AppSettings["JsonDataFile"]));
            JArray testData = (JArray)o["TestData"];
            IList<JsonTestData> data = testData.ToObject<IList<JsonTestData>>();

            object[] DataSet = new object[data.Count];
            int i = 0;
            foreach (JsonTestData d in data)
            {
                DataSet[i++] = new object[] { d.Query, d.Expected };
            }              
            return DataSet;
        }
    }
}

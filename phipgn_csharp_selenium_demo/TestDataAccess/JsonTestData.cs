using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Configuration;

namespace phipgn_csharp_selenium_demo.TestDataAccess
{
    class JsonTestData
    {
        public static object[] GetDataSet()
        {
            JObject o = JObject.Parse(File.ReadAllText(ConfigurationManager.AppSettings["JsonDataFile"]));
            JArray testData = (JArray)o["TestData"];
            IList<TestData> data = testData.ToObject<IList<TestData>>();

            object[] DataSet = new object[data.Count];
            int i = 0;
            foreach (TestData d in data)
            {
                DataSet[i] = new object[] { data[i].Query, data[i].Expected };
                i++;
            }

            return DataSet;
        }
    }
}

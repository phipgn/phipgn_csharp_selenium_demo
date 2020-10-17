using phipgn_csharp_selenium_demo.Utilities;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace phipgn_csharp_selenium_demo.TestCases_HybridDriven
{
    /**
     * @author phipgn
     * 
     */
    class BaseTest
    {
        protected ActionKeywords actionKeywords;
        private Dictionary<string, string> data;

        private Dictionary<string, string> NormalizeData(object[] args)
        {
            Dictionary<string, string> dataDict = new Dictionary<string, string>();
            foreach (object arg in args)
            {
                string[] v = arg.ToString().Split('=');
                dataDict.Add(v[0], v[1]);
            }
            return dataDict;
        }

        protected void ExecuteTestCase(object[] args) 
        {
            data = NormalizeData(args);

            int nTestCases = ExcelUtil.GetRowCount(Constants.SHEET_TESTCASES) - 1;    // number of test cases
            for (int i = 1; i <= nTestCases; i++)
            {
                string testcaseId = ExcelUtil.GetCellData(i, Constants.COL_TC_ID, Constants.SHEET_TESTCASES);
                bool run = Boolean.Parse(ExcelUtil.GetCellData(i, Constants.COL_RUN, Constants.SHEET_TESTCASES));   // run test case or not
                if (run)
                {
                    LogUtil.StartTestCase(testcaseId);
                    for (int j = 1; j < ExcelUtil.GetRowCount(Constants.SHEET_TESTSTEPS); j++)
                    {
                        string testcaseId2 = ExcelUtil.GetCellData(j, Constants.COL_TC_ID, Constants.SHEET_TESTSTEPS);
                        if (testcaseId.Equals(testcaseId2))
                        {
                            string locatorId = ExcelUtil.GetCellData(j, Constants.COL_LOCATOR_ID, Constants.SHEET_TESTSTEPS);
                            string actionKeyword = ExcelUtil.GetCellData(j, Constants.COL_KEYWORD, Constants.SHEET_TESTSTEPS);
                            string dataset = ExcelUtil.GetCellData(j, Constants.COL_DATASET, Constants.SHEET_TESTSTEPS);
                            string parameter = (dataset == null) ? "" : data[dataset];
                            ExecuteKeyword(actionKeyword, locatorId, parameter);
                        }
                    }
                    LogUtil.EndTestCase();
                }
            }
        }

        private void ExecuteKeyword(string actionKeyword, string locatorId, string parameter)
        {
            if (actionKeyword == null || actionKeyword.Equals(""))
                throw new Exception("Action Keyword is NULL or empty!");

            MethodInfo mi = actionKeywords.GetType().GetMethod(actionKeyword);

            List<string> args = new List<string>();
            switch (actionKeyword)
            {
                case "OpenBrowser":
                case "CloseBrowser":
                case "Navigate":
                    args.Add(parameter);
                    break;
                case "ClickElement":
                    args.Add(locatorId);
                    break;
                case "InputText":
                case "VerifyProductTitle":
                case "SelectOption":
                    args.Add(locatorId);
                    args.Add(parameter);
                    break;
            }

            try
            {
                mi.Invoke(actionKeyword, args.ToArray());
            }
            catch (Exception e)
            {
                LogUtil.LogFail($"Failed at '{actionKeyword}': {e.Message}");
            }
        }
    }
}

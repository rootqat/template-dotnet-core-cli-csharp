using LambdaTest.Config;
using Microsoft.Playwright;
using Newtonsoft.Json;
using NUnit.Framework;
using Sentry.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace LambdaTest.Base
{
    internal class BasePage
    {
        public static IPage _page;
        public static IBrowser _browser;

        public static string GetCdpUrl(string browserType)
        {
            string user, accessKey;
            user = configSettings.LT_User;
            accessKey = configSettings.LT_AccessKey;

            Dictionary<string, object> capabilities = new Dictionary<string, object>();
            Dictionary<string, string> ltOptions = new Dictionary<string, string>();

            ltOptions.Add("name", "Playwright Test 101");
            ltOptions.Add("build", "Playwright C-Sharp NUnit tests");
            ltOptions.Add("platform", "Windows 10");
            ltOptions.Add("user", user);
            ltOptions.Add("accessKey", accessKey);

            capabilities.Add("browserName", browserType);
            capabilities.Add("browserVersion", "latest");
            capabilities.Add("LT:Options", ltOptions);

            string capabilitiesJson = JsonConvert.SerializeObject(capabilities);
            return "wss://cdp.lambdatest.com/playwright?capabilities=" + Uri.EscapeDataString(capabilitiesJson);
        }

        public static async Task SetTestStatus(string status, string remark, IPage page)
        {
            await page.EvaluateAsync("_ => {}", "lambdatest_action: {\"action\": \"setTestStatus\", \"arguments\": {\"status\":\"" + status + "\", \"remark\": \"" + remark + "\"}}");
        }
    }
}

using LambdaTest.Base;
using LambdaTest.Config;
using Microsoft.Playwright;
using NUnit.Framework;

namespace LambdaTest.Test
{
    internal class PlaywrightTestCases : BasePage
    {

        [Test, Order(1)]
        public async Task ScenarioTest1Async()
        {
            string URL = configSettings.URL;
            using var playwright = await Playwright.CreateAsync();
            _browser = await playwright.Chromium.ConnectAsync(GetCdpUrl("Chrome"));
            _page = await _browser.NewPageAsync();
            _page.GotoAsync(URL);
            try
            {
                string txt;
                string message = "Welcome to LambdaTest";

                txt = await _page.Locator("//a[contains(.,'Simple Form Demo')]").TextContentAsync();
                Assert.That(txt, Is.EqualTo("Simple Form Demo"));

                if (txt.Contains("Simple Form Demo"))
                {
                    // Use the following code to mark the test status.
                    await SetTestStatus("passed", "Title matched", _page);
                }
                else
                {
                    await SetTestStatus("failed", "Title not matched", _page);
                }

                await _page.Locator("//a[contains(.,'Simple Form Demo')]").ClickAsync();
                await _page.Locator("//input[@id='user-message']").FillAsync(message);
                Thread.Sleep(2000);
                await _page.Locator("//button[@id='showInput']").ClickAsync();
                Thread.Sleep(2000);
                string actualMessage = _page.Locator("//p[@id='message']").TextContentAsync().Result;
                Assert.That(message, Is.EqualTo(actualMessage));

                if (message.Contains(actualMessage))
                {
                    // Use the following code to mark the test status.
                    await SetTestStatus("passed", "Message matched", _page);
                }
                else
                {
                    await SetTestStatus("failed", "Message not matched", _page);
                }
            }
            catch (Exception ex) { Console.WriteLine(ex); }

        }

        [Test, Order(2)]
        public async Task ScenarioTest2Async()
        {
            string URL = configSettings.URL;
            using var playwright = await Playwright.CreateAsync();
            _browser = await playwright.Chromium.ConnectAsync(GetCdpUrl("Chrome"));
            _page = await _browser.NewPageAsync();
            _page.GotoAsync(URL);
            try
            {
                await _page.Locator("//a[contains(.,'Drag & Drop Sliders')]").ClickAsync();
                Thread.Sleep(2000);
                var slider = await _page.QuerySelectorAsync("(//input[@value='15'])[1]");
                var rangeValue = _page.Locator("//output[@id='rangeSuccess']");
                var initialText = rangeValue.InnerTextAsync().Result;
                Console.WriteLine("Initial text: " + initialText);
                var targetAmount = "95";
                var isCompleted = false;

                var srcBound = await slider.BoundingBoxAsync();
                if (srcBound != null)
                {
                    await _page.Mouse.MoveAsync(srcBound.X + srcBound.Width / 2, srcBound.Y + srcBound.Height / 2);
                    await _page.Mouse.DownAsync();
                    await _page.Mouse.MoveAsync(srcBound.X + 462, srcBound.Y + srcBound.Height / 2);
                    await _page.Mouse.UpAsync();
                    var text = rangeValue.InnerTextAsync().Result;
                    Console.WriteLine("Expected text: " + text);
                    if (text.Contains(targetAmount))
                    {
                        // Use the following code to mark the test status.
                        await SetTestStatus("passed", "Slider target matched", _page);
                    }
                    else
                    {
                        await SetTestStatus("failed", "Slider target not matched", _page);
                    }

                }

            }
            catch (Exception ex) { Console.WriteLine(ex); }
        }


        [Test, Order(3)]
        public async Task ScenarioTest3Async()
        {
            string URL = configSettings.URL;
            using var playwright = await Playwright.CreateAsync();
            _browser = await playwright.Chromium.ConnectAsync(GetCdpUrl("Chrome"));
            _page = await _browser.NewPageAsync();
            _page.GotoAsync(URL);

            try
            {
                string actualErrorMsg = "Please fill in this field.";
                await _page.Locator("//a[contains(.,'Input Form Submit')]").ClickAsync();
                await _page.Locator("button:text('Submit')").ClickAsync();

                IElementHandle nameInput1 = await _page.QuerySelectorAsync("//input[@id='name']");

                var expectedErrorMsg = await _page.EvaluateAsync<string>("input => input.validationMessage", nameInput1);
                Console.WriteLine("Expected Validation message " + expectedErrorMsg);


                if (actualErrorMsg.Equals(expectedErrorMsg))
                {
                    await SetTestStatus("Failed", "Error message matched with expected", _page);
                }
                else
                {
                    await SetTestStatus("Passed", "Error message not matched with expected", _page);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            try
            {
                await _page.Locator("//input[@id='name']").FillAsync("TestUser");
                await _page.Locator("//input[@id='inputEmail4']").FillAsync("testUser@gmail.com");
                await _page.Locator("//input[@id='inputPassword4']").FillAsync("Test@123");
                await _page.Locator("//input[@id='company']").FillAsync("ABC");
                await _page.Locator("//input[@id='websitename']").FillAsync("http://www.abc.com");
                await _page.Locator("//select[@name='country']").SelectOptionAsync("US");
                await _page.Locator("//input[@id='inputCity']").FillAsync("ABC");
                await _page.Locator("//input[@id='inputAddress1']").FillAsync("ABC");
                await _page.Locator("//input[@id='inputAddress2']").FillAsync("XYZ");
                await _page.Locator("//input[@id='inputState']").FillAsync("CO");
                await _page.Locator("//input[@id='inputZip']").FillAsync("4102");
                await _page.Locator("button:text('Submit')").ClickAsync();
                string submitSuccessMsg = "Thanks for contacting us, we will get back to you shortly.";

                Thread.Sleep(2000);
                string expectedMessge = await _page.Locator("//p[@class='success-msg hidden']").TextContentAsync();
                Assert.That(submitSuccessMsg, Is.EqualTo(expectedMessge));
                Console.WriteLine("Message after form submission " + expectedMessge);
                if (submitSuccessMsg.Equals(expectedMessge))
                {
                    await SetTestStatus("Passed", "Success message displayed", _page);
                }
                else
                {
                    await SetTestStatus("Failed", "Success message not displayed", _page);
                }

            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

    }
}

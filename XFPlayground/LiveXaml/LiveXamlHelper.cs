using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinFormsStarterKit.UserInterfaceBuilder.Helpers;
using XamarinFormsStarterKit.UserInterfaceBuilder.Models;

namespace LiveXaml
{
    public static class LiveXamlHelper
    {

        public static void XamlSync(string uriString, ContentPage contentPage)
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await RunXAMLChanges(uriString, contentPage);
                });
                return true;
            });
        }

        private static async Task RunXAMLChanges(string uriString, ContentPage contentPage)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(uriString);

                response.EnsureSuccessStatusCode();

                using (HttpContent content = response.Content)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    XamlPayload message = JsonConvert.DeserializeObject<XamlPayload>(responseBody, JsonSerializerSettingsConverter.Settings);

                    if (!string.IsNullOrEmpty(message.XAML) || !string.IsNullOrEmpty(message.PreserveXML))
                    {
                        await LiveXamlHelper.PreviewXaml(message.XAML, contentPage);
                    }

                }

            }
        }
        
        public static async Task PreviewXaml(string xaml, ContentPage contentPage)
        {
            try
            {
                if (string.IsNullOrEmpty(xaml))
                {
                    return;
                }

                await LiveXaml.UpdatePageFromXamlAsync(contentPage, xaml);
            }
            catch (Exception exception)
            {
                // Error 
                Debug.WriteLine(exception.Message);
                string xamlException = LiveXaml.GetXamlException(exception);
                await LiveXaml.UpdatePageFromXamlAsync(contentPage, xamlException);
            }

        }



    }
}

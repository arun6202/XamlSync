using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xamarin.Forms;
using XamarinFormsStarterKit.UserInterfaceBuilder;
using XamarinFormsStarterKit.UserInterfaceBuilder.Helpers;
using XamarinFormsStarterKit.UserInterfaceBuilder.Models;

namespace XamlFileWatcher
{

    internal class XamlSync
    {
        private const string PreserveXml = "XFPlayground/Preserve.xml";
        private const string Xaml = "XFPlayground/MainPage.xaml";
        private const string PreserveXaml = "XFPlayground/PreserveXaml.xml";

        private static readonly string XamarinFormsAssembly = "Xamarin.Forms";

        private static readonly string projectRoot = Path.GetFullPath(Path.Combine(typeof(XamlSync).Assembly.Location, appLocation));
        private static readonly string xmlFilePath = Path.Combine(projectRoot, PreserveXml);
        private static readonly string xamlFilePath = Path.Combine(projectRoot, Xaml);
        private static readonly string preserveXamlFilePath = Path.Combine(projectRoot, PreserveXaml);

        private static Type[] VisualElementTypes = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.FullName.Contains(XamarinFormsAssembly))
                .SelectMany(s => s.GetTypes())
                .Where(m => !m.IsAbstract)
                                    .Where(typeof(VisualElement).IsAssignableFrom).ToArray();


        private const string Url = "http://arun6202-001-site1.gtempurl.com";
        private static readonly string baseUri = Url + "/api/XamlSync6202";


        private const string appLocation = @"../../../../../../";

        public static async Task Start(string modifiedXamlPath)
        {

            string xamlPreserve = CreateAsISPreserveXaml(modifiedXamlPath);
            //var xamlPreserve = CreatePreserveXaml();  enable colors

            await Post(xamlPreserve, xamlPreserve);

            return;

        }

        private static async Task Post(string xml, string xaml)
        {
            try
            {
                await PostUpdatedXaml(xml, xaml);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        private static string CreateAsISPreserveXaml(string modifiedXamlPath)
        {

            XDocument xdoc = XDocument.Load(modifiedXamlPath);

            return xdoc.ToString();

        }

        private static string CreatePreserveXaml(string modifiedXamlPath)
        {

            XDocument xdoc = XDocument.Load(modifiedXamlPath);

            RemoveBackGroundColor(xdoc);

            ProcessText(xdoc);

            TraverseXMl(xdoc.Root);

            xdoc.Save(preserveXamlFilePath, SaveOptions.OmitDuplicateNamespaces);

            return xdoc.ToString();

        }

        private static void ProcessText(XDocument xdoc)
        {
            System.Collections.Generic.IEnumerable<XElement> attributesToDelete = from ele in xdoc.Descendants()
                                                                                  where ele != null && ele.Attribute("Text") != null
                                                                                  select ele;

            try
            {
                foreach (XAttribute attribute in attributesToDelete.Attributes())
                {

                    XElement parent = attribute.Parent;

                    System.Collections.Generic.IEnumerable<XElement> toDelete = from ele in xdoc.Descendants()
                                                                                where ele != null && ele.Attribute("Text") != null
                                                                                select ele;

                    foreach (XAttribute att in toDelete.Attributes())
                    {
                        att.Remove();

                    }

                    parent.SetAttributeValue("TextColor", $"#{LayoutBuilder.GetColor(true).ToHex()}");
                    parent.SetAttributeValue("Text", TextBuilder.GenerateLoremText(attribute.Value));

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static void RemoveBackGroundColor(XDocument xdoc)
        {
            System.Collections.Generic.IEnumerable<XElement> attributesToDelete = from ele in xdoc.Descendants()
                                                                                  where ele != null && ele.Attribute("BackgroundColor") != null
                                                                                  select ele;

            try
            {
                foreach (XAttribute attribute in attributesToDelete.Attributes())
                {
                    attribute.Remove();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static void TraverseXMl(XElement xdoc)
        {
            foreach (XElement element in xdoc.Descendants())
            {

                if (VisualElementTypes.Any(w => w.Name == element.Name.LocalName))
                {
                    element.SetAttributeValue("BackgroundColor", $"#{LayoutBuilder.GetColor(true).ToHex()}");

                }

                TraverseXMl(element);
            }
        }

        private static async Task PostUpdatedXaml(string preserveXML, string xAML)
        {
            XamlPayload xamlPayload = new XamlPayload { PreserveXML = preserveXML, XAML = xAML };

            string json = JsonConvert.SerializeObject(xamlPayload);

            Console.WriteLine($"XamlPlaygroundProduce sent: {xamlPayload.XAML} ,{xamlPayload.PreserveXML}");

            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(baseUri)
            };

            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            await client.PostAsync(baseUri, stringContent);

        }

    }
}

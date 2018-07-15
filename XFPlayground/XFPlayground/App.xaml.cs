using System;
using System.Net.Http;
using System.Threading.Tasks;
using LiveXaml;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinFormsStarterKit.UserInterfaceBuilder.Helpers;
using XamarinFormsStarterKit.UserInterfaceBuilder.Models;
 
[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XFPlayground
{
    public partial class App : Application
    {
        private const string UriString = "http://arun6202-001-site1.gtempurl.com/api/XamlSync6202";
       
        public App()
        {
            InitializeComponent();

            var playground = new Playground();

            MainPage = playground;

            LiveXamlHelper.XamlSync(UriString, playground);

         }

      
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

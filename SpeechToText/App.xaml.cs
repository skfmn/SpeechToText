using Xamarin.Forms;

namespace SpeechToText
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //The ISpeechToText interface is located in Settings.cs in the shared project
            DependencyService.Get<ISpeechToText>().Initialize();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

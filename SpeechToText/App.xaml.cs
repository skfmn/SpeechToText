using Xamarin.Forms;

namespace SpeechToText
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

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
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SpeechToText
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            //The ISpeechToText interface is located in Settings.cs in the shared project
            DependencyService.Get<ISpeechToText>();

            //Disable Button if no mic is present
            if (!Settings.IsMicPresent) { Micbutton.IsEnabled = false; }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var speechText = await WaitForSpeechToText();
            speechText = ToUpperFirstLetter(speechText);
            Speechtext.Text = speechText;
        }

        private async Task<string> WaitForSpeechToText()
        {
            return await DependencyService.Get<ISpeechToText>().SpeechToTextAsync();
        }

        //Capitalize the first letter
        public string ToUpperFirstLetter(string source)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;

            char[] letters = source.ToCharArray();

            letters[0] = char.ToUpper(letters[0]);

            return new string(letters);
        }
    }
}
using Android.Content;
using Android.Speech;
using SpeechToText.Droid;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(SpeechToTextDependency))]

namespace SpeechToText.Droid
{
    public class SpeechToTextDependency : ISpeechToText
    {
        private const int VOICE = 10;
        public static AutoResetEvent autoEvent = new AutoResetEvent(false);
        public static string SpeechText;

        public static SpeechToTextDependency Instance { get; private set; }

        public SpeechToTextDependency() => Initialize();

        public void Initialize()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        public async Task<string> SpeechToTextAsync()
        {
            var voiceIntent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
            voiceIntent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);
            voiceIntent.PutExtra(RecognizerIntent.ExtraPrompt, "Speek Now!\nSpeak Clearly!");
            voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputCompleteSilenceLengthMillis, 1500);             // If silent for 1.5 seconds assume they are done talking
            voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputPossiblyCompleteSilenceLengthMillis, 1500); // If silent for 1.5 seconds assume they are done talking
            voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputMinimumLengthMillis, 15000);                     // Wait 15 seconds for intial input 
            voiceIntent.PutExtra(RecognizerIntent.ExtraMaxResults, 1);

            SpeechText = "";
            autoEvent.Reset();

            //Starts the Activiy and sends the results to MainActivity OnActivityResult() which sends it back here...?
            MainActivity.Instance.StartActivityForResult(voiceIntent, VOICE);

            //give them enough time to say something
            await Task.Run(() => { autoEvent.WaitOne(new TimeSpan(0, 1, 0)); });

            //Sends the text to the WaitForSpeechToText() Method from the requesting page
            return SpeechText;
        }
    }
}

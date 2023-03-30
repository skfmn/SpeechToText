using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Speech;

namespace SpeechToText.Droid
{
    [Activity(Label = "SpeechToText", Icon = "@mipmap/stticon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private const int VOICE = 10;
        public static MainActivity Instance { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Instance = this;

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            //Use the Setting to Enable or Disable the Mic Button as needed
            string rec = PackageManager.FeatureMicrophone;
            if (rec != "android.hardware.microphone")
            {
                Settings.IsMicPresent = false;
            }
            else
            {
                Settings.IsMicPresent = true;
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        //From the Intent sent from the Voice Activity started in SpeechToTextDepndency.cs
        protected override void OnActivityResult(int requestCode, Result resultVal, Intent data)
        {
            if (requestCode == VOICE)
            {
                if (resultVal == Result.Ok)
                {
                    var matches = data.GetStringArrayListExtra(RecognizerIntent.ExtraResults);
                    if (matches.Count != 0)
                    {
                        var textInput = matches[0];

                        //Limit the Length of the text if you need to.
                        if (textInput.Length > 500) { textInput = textInput.Substring(0, 500); }

                        //Sends the results back to SpeechToTextDepndency.cs   
                        SpeechToTextDependency.SpeechText = textInput;
                    }
                }
                SpeechToTextDependency.autoEvent.Set();
            }
            base.OnActivityResult(requestCode, resultVal, data);
        }
    }
}
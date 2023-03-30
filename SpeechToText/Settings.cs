using System.Threading.Tasks;
using Xamarin.Essentials;

namespace SpeechToText
{
    public interface ISpeechToText
    {
        void Initialize();

        Task<string> SpeechToTextAsync();
    }

    public static class Settings
    {
        public static bool IsMicPresent
        {
            get => Preferences.Get(nameof(IsMicPresent), false);
            set => Preferences.Set(nameof(IsMicPresent), value);
        }
    }
}
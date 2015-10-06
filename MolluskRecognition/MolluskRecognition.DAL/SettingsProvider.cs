using System.ComponentModel.Composition;
using MolluskRecognition.Properties;

namespace MolluskRecognition.DAL
{
    public interface ISettingsProvider
    {
        string LocationsImagesLocation { get; set; }
        long LocationIndex { get; set; }
        void Save();
    }

    [Export(typeof(ISettingsProvider))]
    public class SettingsProvider : ISettingsProvider
    {
        public string LocationsImagesLocation
        {
            get { return Settings.Default.LocationsImagesLocation; }
            set { Settings.Default.LocationsImagesLocation = value; }
        }

        public long LocationIndex
        {
            get { return Settings.Default.LocationIndex; }
            set { Settings.Default.LocationIndex = value; }
        }

        public void Save()
        {
            Settings.Default.Save();
        }
    }
}

using ModBot;
using System;
using System.ComponentModel.Composition;
using System.Net;

namespace CoMaNdO.MultipleOutputs
{
    [Export(typeof(IExtension))]
    public class MultipleOutputs : IExtension
    {
        private string LatestVersion;

        public void Load()
        {
            //Events.UI.Loaded += Events_OnUILoaded;

            UI.AddWindow("Multiple Outputs", new MultipleOutputsWindow(this), false, false, false, false, "Multiple\r\nOutputs");
        }

        /*private void Events_OnUILoaded()
        {
            UI.AddWindow("Multiple Outputs", new MultipleOutputsWindow(this), false, false, false, false, "Multiple\r\nOutputs");
        }*/

        public string Name { get { return "Multiple Command Outputs"; } }
        public string FileName { get { return "CoMaNdO.MultipleOutputs.dll"; } }
        public string Author { get { return "CoMaNdO"; } }
        public string UniqueID { get { return "CoMaNdO.MultipleOutputs"; } }
        public string ContactInfo { get { return "CoMaNdO.ModBot@gmail.com"; } }
        public string Version { get { return "0.0.1"; } }
        public int ApiVersion { get { return 0; } }
        public int LoadPriority { get { return 2; } }

        public bool UpdateCheck()
        {
            using (WebClient w = new WebClient())
            {
                w.Proxy = null;
                try
                {
                    LatestVersion = w.DownloadString("https://dl.dropboxusercontent.com/u/60356733/ModBot/Extensions/Extensions.txt");
                    if (LatestVersion != "")
                    {
                        foreach (string Extension in LatestVersion.Split(Environment.NewLine.ToCharArray()))
                        {
                            string[] data = Extension.Split(';');
                            if (data.Length > 6)
                            {
                                if (data[3] == UniqueID)
                                {
                                    LatestVersion = data[5];

                                    string[] Latest = LatestVersion.Split('.'), Current = Version.Split('.');
                                    int LatestMajor = int.Parse(Latest[0]), LatestMinor = int.Parse(Latest[1]), LatestBuild = int.Parse(Latest[2]);
                                    int CurrentMajor = int.Parse(Current[0]), CurrentMinor = int.Parse(Current[1]), CurrentBuild = int.Parse(Current[2]);
                                    return (LatestMajor > CurrentMajor || LatestMajor == CurrentMajor && LatestMinor > CurrentMinor || LatestMajor == CurrentMajor && LatestMinor == CurrentMinor && LatestBuild > CurrentBuild);
                                }
                            }
                        }
                    }
                }
                catch
                {
                }
            }
            return false;
        }

        public string UpdateURL { get { return "https://dl.dropboxusercontent.com/u/60356733/ModBot/Extensions/" + UniqueID + "/" + LatestVersion + "/" + FileName; } }
    }
}

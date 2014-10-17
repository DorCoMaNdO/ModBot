using ModBot;
using System;
using System.ComponentModel.Composition;
using System.Net;
using System.Threading;

namespace Example
{
    // This is an example on how to use ModBot's API in 3rd-party extensions.
    // Any extension must be built under the x86 CPU architecture, building it under x64 will most likely NOT load and under Any CPU might cause issues or not load aswell.
    // Do not include any x64 only dependencies with your extensions, as building the project under x86 will not load x64 dependencies.
    [Export(typeof(IExtension))]
    public class ExampleExtension : IExtension
    {
        string LatestVersion;

        public void Load()
        {
            // Load what you want to do here.
            API.Settings settings = new API.Settings(this, "example.ini"); // Register a settings file.
            settings.SetValue("Example Section", "SuccessfulExample", "0"); // Set or create a value named "SuccessfulExample" under the section "Example Section", with the value "0".

            API.Events.Connected += Events_Connected; // Register an event handler to add our custom command.
            API.Events.OnUILoaded += Events_OnUILoaded;
            //MessageBox.Show("This is an example popup.", "Example Extension");

            settings.SetValue("Example Section", "SuccessfulExample", "1"); // Change the value of "SuccessfulExample" to "1".
            string Example = settings.GetValue("Example Section", "SuccessfulExample", "0"); // Check the value of "SuccessfulExample", if "SuccessfulExample" is missing, use a default value of "0".
            if(Example == "1") // Check if "SuccessfulExample" under "Example Section" is "1".
            {
                Console.WriteLine("Successful example, hurray!"); // Print this message to the console if "SuccessfulExample" has been "1".
            }
        }

        private void Events_Connected(string channel, string nick, bool partnered) // We register our commands after the bot is connected to the channel.
        {
            API.Commands.Add("!example", Command_Example); // We add the command with a handler that will perform the task we want it to.
        }

        private void Events_OnUILoaded()
        {
            API.UI.AddWindow("Example", new ExampleWindow());
        }

        private static void Command_Example(string user, string command, string[] args)
        {
            // Output
            API.Irc.sendMessage("My first command, YAY!");
        }

        public string Name { get { return "Example Extension"; } } // The name of the extension
        public string FileName { get { return "CoMaNdO.Example.dll"; } } // The name that the extension should have, try to make it unique, it should mostly be like the UniqueID below.
        public string Author { get { return "CoMaNdO"; } } // Your name/nickname.
        public string UniqueID { get { return "CoMaNdO.Example"; } } // Will be used for data storage, to keep a unique space for your extension, you'd normally want to put your name/nickname and the name of the extension.
        public string Version { get { return "0.0.1"; } } // The version of the extension.
        //public string MinVersion { get { return "1.6.5382.0"; } } // Min version of the bot that supports this extension, not used at the moment.
        public int ApiVersion { get { return 0; } } // The API version that it has been built with, changes to the API version will be posted on the blog. The use of ApiVersion 0 is if you believe that changes to the API won't affect your code, this is highly doubtable unless you're me. 

        public bool UpdateCheck()
        {
            using (WebClient w = new WebClient())
            {
                w.Proxy = null;
                try
                {
                    /*LatestVersion = w.DownloadString("https://dl.dropboxusercontent.com/u/60356733/ModBot/Extensions/" + UniqueID + "/Version.txt"); // The link to a text file that contains the latest version of the extension, should remain a static link, dropbox recommended.

                    if (LatestVersion != "")
                    {
                        string[] Latest = LatestVersion.Split('.'), Current = Version.Split('.');
                        int LatestMajor = int.Parse(Latest[0]), LatestMinor = int.Parse(Latest[1]), LatestBuild = int.Parse(Latest[2]);
                        int CurrentMajor = int.Parse(Current[0]), CurrentMinor = int.Parse(Current[1]), CurrentBuild = int.Parse(Current[2]);
                        return (LatestMajor > CurrentMajor || LatestMajor == CurrentMajor && LatestMinor > CurrentMinor || LatestMajor == CurrentMajor && LatestMinor == CurrentMinor && LatestBuild > CurrentBuild);
                    }*/

                    LatestVersion = w.DownloadString("https://dl.dropboxusercontent.com/u/60356733/ModBot/Extensions/Extensions.txt"); // The link to a text file that contains the latest version of the extension, should remain a static link, dropbox recommended.
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

        public string UpdateURL { get { return "https://dl.dropboxusercontent.com/u/60356733/ModBot/Extensions/" + UniqueID + "/" + LatestVersion + "/" + FileName; } } // The link that contains the update, should remain a static link, dropbox recommended.
    }
}

using ModBot;
using System;
using System.Net;

namespace CoMaNdO.Example
{
    // This is an example on how to use ModBot's API in 3rd-party extensions.
    // Any extension must be built under the x86 CPU architecture, building it under x64 will most likely NOT load and under Any CPU might cause issues or not load aswell.
    // Do not include any x64 only dependencies with your extensions, as building the project under x86 will not load x64 dependencies.
    public class Example : IExtension
    {
        private string LatestVersion;

        public void Load()
        {
            // Load what you want to do here.
            Settings settings = new Settings(this, "example.ini");  // Register a settings file.
            settings.SetValue("Example Section", "SuccessfulExample", "0"); // Set or create a value named "SuccessfulExample" under the section "Example Section", with the value "0".

            Events.Connected += Events_Connected; // Register an event handler to add our custom command.

            UI.AddWindow("Example", new ExampleWindow());

            settings.SetValue("Example Section", "SuccessfulExample", "1"); // Change the value of "SuccessfulExample" to "1".
            string Example = settings.GetValue("Example Section", "SuccessfulExample", "0"); // Check the value of "SuccessfulExample", if "SuccessfulExample" is missing, use a default value of "0".
            if (Example == "1") // Check if "SuccessfulExample" under "Example Section" is "1".
            {
                Console.WriteLine("Successful example, hurray!"); // Print this message to the console if "SuccessfulExample" has been "1".
            }
        }

        private void Events_Connected(string channel, string nick, bool partnered, bool subprogram) // We register our commands after the bot is connected to the channel.
        {
            Commands.Add(this, "!example", Command_Example); // We add the command with a handler that will perform the task we want it to.
        }

        private void Command_Example(string user, Command cmd, string[] args, string origin)
        {
            // Output
            Chat.SendMessage("My first command, YAY!");
        }

        public string Name { get { return "Example Extension"; } } // The name of the extension
        public string FileName { get { return "CoMaNdO.Example.dll"; } } // The name that the extension should have, try to make it unique, it should mostly be like the UniqueID below.
        public string Author { get { return "CoMaNdO"; } } // Your name/nickname.
        public string UniqueID { get { return "CoMaNdO.Example"; } } // Will be used for data storage, to keep a unique space for your extension, you'd normally want to put your name/nickname and the name of the extension.
        public string ContactInfo { get { return "CoMaNdO.ModBot@gmail.com"; } } // Will be used to refer people for suggestions, error reports and more.
        public string Version { get { return "0.0.5"; } } // The version of the extension.
        public int ApiVersion { get { return 5; } } // The API version that it has been built with, changes to the API version will be posted on the blog. The use of ApiVersion 0 is if you believe that changes to the API won't affect your code, this is highly doubtable unless you're me. 
        public int LoadPriority { get { return 3; } }

        public bool UpdateCheck()
        {
            using (WebClient w = new WebClient())
            {
                w.Proxy = null;
                try
                {
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

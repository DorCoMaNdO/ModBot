using ModBot;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace CoMaNdO.SongRequests
{
    [Export(typeof(IExtension))]
    public class SongRequests : IExtension
    {
        private string LatestVersion;

        public void Load()
        {
            SongRequest.Load(this);
        }

        public string Name { get { return "Song Requests System"; } }
        public string FileName { get { return "CoMaNdO.SongRequests.dll"; } }
        public string Author { get { return "CoMaNdO"; } }
        public string UniqueID { get { return "CoMaNdO.SongRequests"; } }
        public string ContactInfo { get { return "CoMaNdO.ModBot@gmail.com"; } }
        public string Version { get { return "0.0.1"; } }
        public int ApiVersion { get { return 0; } }
		public int LoadPriority { get { return 1; } }

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

    static class SongRequest
    {
        public class Song
        {
            public string id { get; private set; }
            public string title { get; private set; }
            public TimeSpan duration { get; private set; }
            public string requester = "";
            public Song(string id, string title, TimeSpan duration)
            {
                this.id = id;
                this.title = title;
                this.duration = duration;
            }
        }

        public static int VotesRequired = 2;
        public static List<string> Votes = new List<string>();
        public static Song CurrentSong = null;
        public static int MaxLength = 900, TimeStarted, EstimatedEnding;
        public static SongRequestsWindow Window;
        public static IExtension extension;
        public static Settings ini;
        //private static System.Threading.Timer NextSongTimer = new System.Threading.Timer(NextSong, null, Timeout.Infinite, Timeout.Infinite);

        public static void Load(IExtension sender)
        {
            extension = sender;
            ini = new Settings(extension, "Settings.ini");

            Window = new SongRequestsWindow(extension);
            Window.SongRequestPlayer.Dispose();

            UI.AddWindow("Song Request", Window, true, false, false, false, "Song\r\nRequest");

            Events.UI.Loaded += Events_OnUILoaded;
            Events.Connected += Events_Connected;
        }

        private static void Events_OnUILoaded()
        {
            //UI.AddWindow("Song Request", Window = new SongRequestsWindow(extension), true, false, false, false, "Song\r\nRequest");
            Window.SongRequestPlayer = new WebBrowser();
            Window.SongRequestPlayer.Location = new Point(692, 12);
            Window.SongRequestPlayer.MinimumSize = new Size(20, 20);
            Window.SongRequestPlayer.Name = "SongRequestPlayer";
            Window.SongRequestPlayer.ScrollBarsEnabled = false;
            Window.SongRequestPlayer.Size = new Size(320, 240);
            Window.SongRequestPlayer.TabIndex = 70;
            Window.SongRequestPlayer.Url = new Uri("about:blank");
            Window.SongRequestPlayer.WebBrowserShortcutsEnabled = false;
            Window.Controls.Add(Window.SongRequestPlayer);
        }

        private static void Events_Connected(string channel, string nick, bool partnered)
        {
            Commands.Add("!songrequest", Command_SongRequest, 0, 0);
            //Commands.Add("!testsong", Command_TestSong, 0, 0);
            //Commands.Add("!skipsong", Command_SkipSong, 0, 0);
            //Commands.Add("!stopsong", Command_StopSong, 0, 0);

            //YouTube.PlaySong();
        }

        private static void Command_SongRequest(string user, string cmd, string[] args)
        {
            if (args.Length > 0)
            {
                Song song = GetSong(args[0]);
                //song.requester = user;
                int response = AddSong(song, user);
                if (response == 0)
                {
                    Chat.SendMessage(user + ", the song \"" + song.title + "\" (Duration: " + song.duration + ") has been added to the queue.");
                }
                else if (response == 1)
                {
                    Chat.SendMessage(user + ", the song you requested is too long.");
                }
                else if (response == 2)
                {
                    Chat.SendMessage(user + ", the song you requested is invalid.");
                }
                else if (response == 3)
                {
                    Chat.SendMessage(user + ", the song you requested is already in the queue.");
                }
                else if (response == 4)
                {
                    Chat.SendMessage(user + ", you have reached the requests limit.");
                }
                else if (response == 5)
                {
                    Chat.SendMessage(user + ", you have insufficient " + Currency.Name + ".");
                }
            }
        }

        private static void Command_TestSong(string user, string cmd, string[] args)
        {
            PlaySong();
        }

        private static void Command_SkipSong(string user, string cmd, string[] args)
        {
            VoteSkip(user);
        }

        private static void Command_StopSong(string user, string cmd, string[] args)
        {
            StopSong();
        }

        public static Song GetSong(string id)
        {
            using (WebClient w = new WebClient())
            {
                w.Proxy = null;
                try
                {
                    JObject json = JObject.Parse(w.DownloadString("https://www.googleapis.com/youtube/v3/videos?id=" + id + "&key=AIzaSyAXkTQTVZmZo0Thi2bbIer9PZIvzMgdTsk&fields=items(id,snippet(title),status(uploadStatus,privacyStatus),contentDetails(duration))&part=snippet,contentDetails,status"));
                    foreach (JToken song in json["items"])
                    {
                        if (song["id"].ToString() == id)
                        {
                            if (song["status"]["uploadStatus"].ToString() == "processed" && (song["status"]["privacyStatus"].ToString() == "public" || song["status"]["privacyStatus"].ToString() == "unlisted"))
                            {
                                return new Song(id, song["snippet"]["title"].ToString(), System.Xml.XmlConvert.ToTimeSpan(song["contentDetails"]["duration"].ToString()));
                            }
                            break;
                        }
                    }
                }
                catch
                {
                }
            }
            return null;
        }

        public static int AddSong(Song song, string requester)
        {
            if (requester == null || requester == "") return -1;

            if (song != null)
            {
                song.requester = requester;

                if (song.duration.TotalSeconds <= MaxLength)
                {
                    List<string> songs = new List<string>();
                    if (File.Exists(Api.GetDataPath(extension) + "Songs.txt"))
                    {
                        songs = File.ReadAllLines(Api.GetDataPath(extension) + "Songs.txt").ToList();
                        int requests = 0;
                        foreach (string sSong in File.ReadAllLines(Api.GetDataPath(extension) + "Songs.txt"))
                        {
                            if (song.id.ToLower() == sSong.Split('|')[0].ToLower()) return 3;

                            if (Window.LimitRequests.Checked)
                            {
                                if (sSong.Substring(sSong.LastIndexOf('|') + 1).ToLower() == requester.ToLower()) requests++;

                                if (Window.RequestsLimit.Value <= requests) return 4;
                            }
                        }
                    }

                    if (Window.ChargeRequest.Checked)
                    {
                        if (Currency.Check(requester) < Window.RequestPrice.Value) return 5;

                        Currency.Remove(requester, (int)Window.RequestPrice.Value);
                    }

                    songs.Add(song.id + "|" + song.title + "|" + song.requester);
                    File.WriteAllLines(Api.GetDataPath(extension) + "Songs.txt", songs.ToArray());

                    return 0;
                }

                return 1;
            }

            return 2;
        }

        public static void VoteSkip(string user)
        {
            string name = user;
            user = user.ToLower();
            if (!Votes.Contains(user))
            {
                Votes.Add(user);
                if (Votes.Count < VotesRequired)
                {
                    Chat.SendMessage(name + " has voted to skip the current song (" + CurrentSong.title + (CurrentSong.requester != "" ? ", requested by " + CurrentSong.requester : "") + "), " + (VotesRequired - Votes.Count) + " more vote(s) required.");
                }
                else
                {
                    ReorderQueue();
                    PlaySong();
                }
            }
        }

        public static void ReorderQueue(bool Remove = false, int start = 1)
        {
            if (File.Exists(Api.GetDataPath(extension) + "Songs.txt"))
            {
                List<string> OldSongs = File.ReadAllLines(Api.GetDataPath(extension) + "Songs.txt").ToList(), songs = new List<string>();
                for (int i = start; i < OldSongs.Count; i++)
                {
                    songs.Add(OldSongs[i]);
                }

                if (!Remove)
                {
                    for (int i = start - 1; i >= 0; i--)
                    {
                        songs.Add(OldSongs[i]);
                    }
                }

                File.WriteAllLines(Api.GetDataPath(extension) + "Songs.txt", songs.ToArray());
            }
        }

        public static void PlaySong(Song song = null)
        {
            if (song == null && File.Exists(Api.GetDataPath(extension) + "Songs.txt"))
            {
                foreach (string sSong in File.ReadAllLines(Api.GetDataPath(extension) + "Songs.txt"))
                {
                    if (sSong != "" && (song = GetSong(sSong.Split('|')[0])) != null)
                    {
                        song.requester = sSong.Substring(sSong.LastIndexOf('|') + 1);
                        break;
                    }
                }
            }
            if (song != null)
            {
                Votes.Clear();
                //Window.SongRequestPlayer.Url = new Uri("https://www.youtube.com/embed/" + song.id + "?autoplay=1&enablejsapi=1");
                //Window.SongRequestPlayer.Url = new Uri("https://www.youtube.com/apiplayer?video_id=" + song.id + "&version=3&autoplay=1&enablejsapi=1&feature=player_embedded&controls=0&modestbranding=1&rel=0&showinfo=0&autohide=1&color=white&playerapiid=musicPlayer&iv_load_policy=3");
                //Window.YouTubePlayer.Movie = "https://www.youtube.com/v/" + song.id + "?autoplay=1";
                //Window.YouTubePlayer.Movie = "https://www.youtube.com/apiplayer?video_id=" + song.id + "&version=3&autoplay=1&enablejsapi=1&feature=player_embedded&controls=0&modestbranding=1&rel=0&showinfo=0&autohide=1&color=white&playerapiid=musicPlayer&iv_load_policy=3";
                CurrentSong = song;
                Window.BeginInvoke((MethodInvoker)delegate
                {
                    Window.SongRequestPlayer.Visible = true;
                    Window.SongRequestPlayer.BringToFront();
                    //Window.SongRequestPlayer.Document.Write("<html><head><script type=\"text/javascript\">function TestScript() { outputID.innerHTML = \"Test\" }</script></head><body><iframe id=\"musicPlayer\" type=\"text/html\" src=\"https://www.youtube.com/apiplayer?video_id=" + song.id + "&version=3&autoplay=1&enablejsapi=1&feature=player_embedded&controls=0&modestbranding=1&rel=0&showinfo=0&autohide=1&color=white&playerapiid=musicPlayer&iv_load_policy=3\" frameborder=\"0\" allowfullscreen><div id=\"outputID\" style=\"color:Red; font-size:16\">Hello from HTML document with script!</div></body></html>");
                    // Window.SongRequestPlayer.Document.Write("<html><head><script type=\"text/javascript\">function TestScript() { outputID.innerHTML = \"Test\" }</script></head><body><iframe id=\"musicPlayer\" type=\"text/html\" src=\"https://www.youtube.com/apiplayer?video_id=" + song.id + "&version=3&autoplay=1&enablejsapi=1&feature=player_embedded&controls=0&modestbranding=1&rel=0&showinfo=0&autohide=1&color=white&playerapiid=musicPlayer&iv_load_policy=3\" frameborder=\"0\" allowfullscreen><div id=\"outputID\" style=\"color:Red; font-size:16\">Hello from HTML document with script!</div></body></html>");
                    Window.SongRequestPlayer.DocumentText = Properties.Resources.SongRequest;
                    //Window.SongRequestPlayer.Refresh();
                });

                /*Program.Invoke(() =>
                {
                    Window.SongRequestPlayer.Url = new Uri("https://www.youtube.com/apiplayer?video_id=" + song.id + "&version=3&autoplay=1&enablejsapi=1&feature=player_embedded&controls=0&modestbranding=1&rel=0&showinfo=0&autohide=1&color=white&playerapiid=musicPlayer&iv_load_policy=3");
                    Window.SongRequestPlayer.Visible = true;
                    Window.SongRequestPlayer.BringToFront();
                    //Window.YouTubePlayer.CallFunction("pauseVideo()");
                });*/
                //Window.SongRequestPlayer.Navigated += (object sender, WebBrowserNavigatedEventArgs e) =>
                //{
                //    Console.WriteLine("Loaded");
                //    //File.WriteAllText("test.txt", Window.SongRequestPlayer, Encoding.UTF8);
                //    //Window.SongRequestPlayer.Document.InvokeScript("musicPlayer.pauseVideo();");
                //    new Thread(() =>
                //    {
                //        Thread.Sleep(10000);
                //        Program.Program.Invoke((MethodInvoker)delegate
                //        {
                //            /*bool found = false;
                //            foreach (HtmlElement elem in Window.SongRequestPlayer.Document.All)
                //            {
                //                if (elem.Id == "musicPlayer")
                //                {
                //                    found = true;
                //                    Console.WriteLine("JACKPOT");
                //                    elem.InvokeMember("pauseVideo()");
                //                    elem.InvokeMember("pauseVideo();");
                //                    elem.InvokeMember("player.pauseVideo()");
                //                    elem.InvokeMember("player.pauseVideo();");
                //                    elem.InvokeMember("musicPlayer.pauseVideo();");
                //                    elem.InvokeMember("musicPlayer.pauseVideo()");
                //                }
                //            }
                //            if (!found)
                //            {
                //                //Window.SongRequestPlayer.Document.Write("<object type=\"application/x-shockwave-flash\" data=\"https://www.youtube.com/apiplayer?video_id=" + song.id + "&version=3&feature=player_embedded&autoplay=1&controls=0&enablejsapi=1&modestbranding=1&rel=0&showinfo=0&autohide=1&color=white&playerapiid=musicPlayer&iv_load_policy=3\" width=\"100%\" height=\"100%\" id=\"musicPlayer\" style=\"visibility: visible;\"><param name=\"allowScriptAccess\" value=\"always\"><param name=\"wmode\" value=\"transparent\"></object>");
                //                Window.SongRequestPlayer.Document.Write("<iframe id=\"musicPlayer\" type=\"text/html\" width=\"640\" height=\"360\" src=\"https://www.youtube.com/apiplayer?video_id=" + song.id + "&version=3&autoplay=1&enablejsapi=1&feature=player_embedded&controls=0&modestbranding=1&rel=0&showinfo=0&autohide=1&color=white&playerapiid=musicPlayer&iv_load_policy=3\" frameborder=\"0\" allowfullscreen>");
                //                Window.SongRequestPlayer.Refresh();
                //            }*/
                //            //Window.SongRequestPlayer.Navigate("javascript:musicPlayer.pauseVideo()");
                //            //if (test) return;
                //            //test = true;
                //            //Window.SongRequestPlayer.Document.InvokeScript
                //            //Window.SongRequestPlayer.Document.Write("<object type=\"application/x-shockwave-flash\" data=\"https://www.youtube.com/apiplayer?video_id=" + song.id + "&version=3&feature=player_embedded&autoplay=1&controls=0&enablejsapi=1&modestbranding=1&rel=0&showinfo=0&autohide=1&color=white&playerapiid=musicPlayer&iv_load_policy=3\" width=\"100%\" height=\"100%\" id=\"musicPlayer\" style=\"visibility: visible;\"><param name=\"allowScriptAccess\" value=\"always\"><param name=\"wmode\" value=\"transparent\"></object>");
                //            //Window.SongRequestPlayer.Document.Write("<iframe id=\"musicPlayer\" type=\"text/html\" width=\"640\" height=\"360\" src=\"https://www.youtube.com/apiplayer?video_id=" + song.id + "&version=3&autoplay=1&enablejsapi=1&feature=player_embedded&controls=0&modestbranding=1&rel=0&showinfo=0&autohide=1&color=white&playerapiid=musicPlayer&iv_load_policy=3\" frameborder=\"0\" allowfullscreen>");
                //            //Console.WriteLine(Window.SongRequestPlayer.Document.ActiveElement.ToString());
                //            //Window.SongRequestPlayer.Document.InvokeScript("musicPlayer.pauseVideo();");
                //            //Window.SongRequestPlayer.Document.InvokeScript("musicPlayer.pauseVideo()");
                //            //Window.SongRequestPlayer.
                //            //Window.SongRequestPlayer.Url = new Uri("javascript:musicPlayer.pauseVideo()");
                //            //Window.SongRequestPlayer.Navigate("javascript:musicPlayer.pauseVideo()");
                //            //HtmlElement element = Window.SongRequestPlayer.Document.GetElementById("musicPlayer");
                //            //element.InvokeMember("playVideo");
                //            //File.WriteAllText("test.txt", Window.SongRequestPlayer.DocumentText, Encoding.UTF8);
                //            //Console.WriteLine(Window.SongRequestPlayer.Document.ActiveElement.Name);
                //            //Window.SongRequestPlayer.Document.GetElementById("musicPlayer").InvokeMember("playVideo()");
                //            /*File.WriteAllText("test.txt", Window.SongRequestPlayer.DocumentText, Encoding.UTF8);
                //            foreach (HtmlElement elem in Window.SongRequestPlayer.Document.All)
                //            {
                //                Console.WriteLine(elem + " " + elem.Name);
                //            }*/
                //        });
                //    }).Start();
                //};

                //Irc.sendMessage("[Song Request]: Now playing - " + song.title + (CurrentSong.requester != "" ? ", requested by " + CurrentSong.requester : ""));
            }
        }

        private static void NextSong(object state)
        {
            /*Program.Program.Invoke(() =>
            {
                foreach (System.Windows.Forms.HtmlElement elem in Window.SongRequestPlayer.Document.All)
                {
                    Console.WriteLine(elem.Id);
                    if (elem.Id == "musicPlayer")
                    {
                        Console.WriteLine("JACKPOT");
                        elem.InvokeMember("javascript:pauseVideo()");
                        elem.InvokeMember("javascript:pauseVideo();");
                        elem.InvokeMember("javascript:player.pauseVideo()");
                        elem.InvokeMember("javascript:player.pauseVideo();");
                        elem.InvokeMember("javascript:musicPlayer.pauseVideo()");
                        elem.InvokeMember("javascript:musicPlayer.pauseVideo();");
                        Window.SongRequestPlayer.Navigate("javascript:pauseVideo()");
                        Window.SongRequestPlayer.Navigate("javascript:pauseVideo();");
                        Window.SongRequestPlayer.Navigate("javascript:player.pauseVideo()");
                        Window.SongRequestPlayer.Navigate("javascript:player.pauseVideo();");
                        Window.SongRequestPlayer.Navigate("javascript:musicPlayer.pauseVideo()");
                        Window.SongRequestPlayer.Navigate("javascript:musicPlayer.pauseVideo();");
                    }
                }
            });*/
            ReorderQueue();
            PlaySong();
        }

        public static void SongRequestPlayer_Navigated(object sender, System.Windows.Forms.WebBrowserNavigatedEventArgs e)
        {
            Window.SongRequestPlayer.Navigated -= SongRequestPlayer_Navigated;
            if (CurrentSong == null && File.Exists(Api.GetDataPath(extension) + "Songs.txt"))
            {
                foreach (string sSong in File.ReadAllLines(Api.GetDataPath(extension) + "Songs.txt"))
                {
                    if (sSong != "" && (CurrentSong = GetSong(sSong.Split('|')[0])) != null)
                    {
                        CurrentSong.requester = sSong.Substring(sSong.LastIndexOf('|') + 1);
                        break;
                    }
                }
            }
            if (CurrentSong != null)
            {
                Console.WriteLine("test");
                //Window.SongRequestPlayer.Document.Write("<iframe id=\"musicPlayer\" type=\"text/html\" src=\"https://www.youtube.com/apiplayer?video_id=oOxPvmXNVtA&version=3&autoplay=1&enablejsapi=1&feature=player_embedded&controls=0&modestbranding=1&rel=0&showinfo=0&autohide=1&color=white&playerapiid=musicPlayer&iv_load_policy=3\" frameborder=\"0\" allowfullscreen>");
                //Window.SongRequestPlayer.Document.Write("<html><head><script type=\"text/javascript\">function TestScript() { outputID.innerHTML = \"Test\" } function Pause() { document.getElementById(\"popupVid\").getElementsByTagName(\"iframe\")[0].contentWindow;.postMessage(\"{\\\"event\\\":\\\"command\\\",\\\"func\\\":\\\"\"pauseVideo\"\\\",\\\"args\\\":\\\"\\\"}\", \"*\"); }</script></head><body><div id=\"outputID\" style=\"color:Red; font-size:16\">Hello from HTML document with script!</div><iframe id=\"musicPlayer\" type=\"text/html\" src=\"https://www.youtube.com/apiplayer?video_id=oOxPvmXNVtA&version=3&autoplay=1&enablejsapi=1&feature=player_embedded&controls=0&modestbranding=1&rel=0&showinfo=0&autohide=1&color=white&playerapiid=musicPlayer&iv_load_policy=3\" frameborder=\"0\" allowfullscreen></iframe></body></html>");
                // Window.SongRequestPlayer.Document.Write("<html><head><script type=\"text/javascript\">function TestScript() { outputID.innerHTML = \"Test\"; } function Pause() { outputID.innerHTML = \"Blah\"; func = 'pauseVideo'; document.getElementById(\"popupVid\").getElementsByTagName(\"iframe\")[0].contentWindow.postMessage('{\"event\":\"command\",\"func\":\"' + func + '\",\"args\":\"\"}','*'); }</script></head><body><div id=\"outputID\" style=\"color:Red; font-size:16\">Hello from HTML document with script!</div><div id=\"popupVid\" style=\"position:absolute;left:0px;top:87px;width:auto;background-color:#D05F27;height:auto;display:;z-index:200;\"><iframe width=\"640\" height=\"360\" src=\"https://www.youtube.com/embed/M7lc1UVf-VE?autoplay=1&controls=0&disablekb=1&enablejsapi=1&modestbranding=1&showinfo=0&autohide=1&color=white&iv_load_policy=3\" frameborder=\"0\" allowfullscreen></iframe><br><a href=\"javascript:void(0);\" onclick=\"Pause();\">Pause</a></div></body></html>");
                //Window.SongRequestPlayer.DocumentText = Properties.Resources.SongRequest.Replace("{VIDEOID}", CurrentSong.id);
                //Console.WriteLine(Properties.Resources.SongRequest);
                //Window.SongRequestPlayer.Document.Write(Properties.Resources.SongRequest.Replace("{VIDEOID}", CurrentSong.id));
                //Window.SongRequestPlayer.Document.Write("<html><head><script type=\"text/javascript\">function TestScript() { outputID.innerHTML = \"Test\" } function onYouTubePlayerReady(player) { outputID.innerHTML = \"Test\" } function Pause() { document.getElementById('ytplayer').pauseVideo(); }</script></head><body><div id=\"outputID\" style=\"color:Red; font-size:16\">Hello from HTML document with script!</div><iframe id=\"ytplayer\" type=\"text/html\" width=\"640\" height=\"360\" src=\"https://www.youtube.com/embed/M7lc1UVf-VE?autoplay=1&controls=0&disablekb=1&enablejsapi=1&modestbranding=1&showinfo=0&autohide=1&color=white&iv_load_policy=3\" frameborder=\"0\" allowfullscreen></iframe><a href=\"javascript:void(0);\" onclick=\"pause();\">Pause</a></body></html>");
                //Window.SongRequestPlayer.Document.Write("<object type=\"application/x-shockwave-flash\" data=\"https://www.youtube.com/apiplayer?video_id=lsE_J6yskQE&amp;version=3&amp;feature=player_embedded&amp;autoplay=1&amp;controls=0&amp;enablejsapi=1&amp;modestbranding=1&amp;rel=0&amp;showinfo=0&amp;autohide=1&amp;color=white&amp;playerapiid=musicPlayer&amp;iv_load_policy=3\" width=\"100%\" height=\"100%\" id=\"musicPlayer\" style=\"visibility: visible;\"><param name=\"allowScriptAccess\" value=\"always\"><param name=\"wmode\" value=\"transparent\"></object>");
                //Window.SongRequestPlayer.Refresh();
            }
            bool loaded = false;
            Window.SongRequestPlayer.DocumentCompleted += ((object s, System.Windows.Forms.WebBrowserDocumentCompletedEventArgs eargs) =>
            {
                if (loaded) return;
                loaded = true;
                Console.WriteLine("test2");
                Window.SongRequestPlayer.DocumentText = Properties.Resources.SongRequest.Replace("{VIDEOID}", CurrentSong.id);
                //Window.SongRequestPlayer.Document.Write(Properties.Resources.SongRequest.Replace("{VIDEOID}", CurrentSong.id));
                //Window.SongRequestPlayer.Refresh();
                new Thread(() =>
                {
                    Thread.Sleep(5000);
                    Window.BeginInvoke((MethodInvoker)delegate
                    {
                        //Window.SongRequestPlayer.Document.InvokeScript("TestScript");
                        Window.SongRequestPlayer.Document.InvokeScript("Pause");
                    });
                }).Start();
            });
            //EstimatedEnding = (TimeStarted = GetUnixTimeNow()) + (int)CurrentSong.duration.TotalSeconds + 1;
            //NextSongTimer.Change((int)CurrentSong.duration.TotalSeconds * 1000 + 1000, Timeout.Infinite);
            /*NextSongTimer.Change(5000, Timeout.Infinite);
            bool found = false;
            foreach (System.Windows.Forms.HtmlElement elem in Window.SongRequestPlayer.Document.All)
            {
                Console.WriteLine(C:elem.Id);
                if (elem.Id == "musicPlayer")
                {
                    found = true;
                    Console.WriteLine("JACKPOT");
                    elem.InvokeMember("javascript:pauseVideo()");
                    elem.InvokeMember("javascript:pauseVideo();");
                    elem.InvokeMember("javascript:player.pauseVideo()");
                    elem.InvokeMember("javascript:player.pauseVideo();");
                    elem.InvokeMember("javascript:musicPlayer.pauseVideo()");
                    elem.InvokeMember("javascript:musicPlayer.pauseVideo();");
                    Window.SongRequestPlayer.Navigate("javascript:pauseVideo()");
                    Window.SongRequestPlayer.Navigate("javascript:pauseVideo();");
                    Window.SongRequestPlayer.Navigate("javascript:player.pauseVideo()");
                    Window.SongRequestPlayer.Navigate("javascript:player.pauseVideo();");
                    Window.SongRequestPlayer.Navigate("javascript:musicPlayer.pauseVideo()");
                    Window.SongRequestPlayer.Navigate("javascript:musicPlayer.pauseVideo();");
                }
            }
            if (!found)
            {
                Window.SongRequestPlayer.Document.Write("<iframe id=\"musicPlayer\" type=\"text/html\" width=\"640\" height=\"360\" src=\"https://www.youtube.com/apiplayer?video_id=" + CurrentSong.id + "&version=3&autoplay=1&enablejsapi=1&feature=player_embedded&controls=0&modestbranding=1&rel=0&showinfo=0&autohide=1&color=white&playerapiid=musicPlayer&iv_load_policy=3\" frameborder=\"0\" allowfullscreen>");
                Window.SongRequestPlayer.Refresh();
            }*/

            //Irc.sendMessage("[Song Request]: Now playing - " + CurrentSong.title + (CurrentSong.requester != "" ? ", requested by " + CurrentSong.requester : ""));
        }

        public static void StopSong()
        {
            TimeStarted = EstimatedEnding = 0;
            //NextSongTimer.Change(Timeout.Infinite, Timeout.Infinite);
            Window.SongRequestPlayer.Url = null;
            CurrentSong = null;
        }
    }
}
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;

namespace ModBot
{
    public static class YouTube
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
        public static int TimeStarted, EstimatedEnding;
        private static Timer NextSongTimer = new Timer(NextSong, null, Timeout.Infinite, Timeout.Infinite);

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

        public static int AddSong(Song song)
        {
            if (song != null)
            {
                if (song.duration.TotalSeconds <= 900)
                {
                    List<string> songs = new List<string>();
                    if (File.Exists(@"Data\Songs.txt"))
                    {
                        songs = File.ReadAllLines(@"Data\Songs.txt").ToList();
                    }
                    songs.Add(song.id + "|" + song.title + "|" + song.requester);
                    File.WriteAllLines(@"Data\Songs.txt", songs.ToArray());
                    return 1;
                }
                return 0;
            }
            return -1;
        }

        public static void VoteSkip(string user)
        {
            string name = user;
            user = Api.capName(user);
            if(!Votes.Contains(user))
            {
                Votes.Add(user);
                if (Votes.Count < VotesRequired)
                {
                    Irc.sendMessage(name + " has voted to skip the current song (" + CurrentSong.title + (CurrentSong.requester != "" ? ", requested by " + CurrentSong.requester : "") +"), " + (VotesRequired - Votes.Count) + " more vote(s) required.");
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
            if (File.Exists(@"Data\Songs.txt"))
            {
                List<string> OldSongs = File.ReadAllLines(@"Data\Songs.txt").ToList(), songs = new List<string>();
                for (int i = start; i < OldSongs.Count; i++)
                {
                    songs.Add(OldSongs[i]);
                }

                if(!Remove)
                {
                    for (int i = start-1; i >= 0; i--)
                    {
                        songs.Add(OldSongs[i]);
                    }
                }

                File.WriteAllLines(@"Data\Songs.txt", songs.ToArray());
            }
        }

        public static void PlaySong(Song song = null)
        {
            if (song == null)
            {
                foreach (string sSong in File.ReadAllLines(@"Data\Songs.txt"))
                {
                    if (sSong != "" && (song = YouTube.GetSong(sSong.Split('|')[0])) != null)
                    {
                        song.requester = sSong.Substring(sSong.LastIndexOf('|')+1);
                        break;
                    }
                }
            }
            if (song != null)
            {
                Votes.Clear();
                //Program.MainForm.SongRequestPlayer.Url = new Uri("https://www.youtube.com/embed/" + song.id + "?autoplay=1&enablejsapi=1");
                //Program.MainForm.SongRequestPlayer.Url = new Uri("https://www.youtube.com/apiplayer?video_id=" + song.id + "&version=3&autoplay=1&enablejsapi=1&feature=player_embedded&controls=0&modestbranding=1&rel=0&showinfo=0&autohide=1&color=white&playerapiid=musicPlayer&iv_load_policy=3");
                //Program.MainForm.YouTubePlayer.Movie = "https://www.youtube.com/v/" + song.id + "?autoplay=1";
                //Program.MainForm.YouTubePlayer.Movie = "https://www.youtube.com/apiplayer?video_id=" + song.id + "&version=3&autoplay=1&enablejsapi=1&feature=player_embedded&controls=0&modestbranding=1&rel=0&showinfo=0&autohide=1&color=white&playerapiid=musicPlayer&iv_load_policy=3";
                CurrentSong = song;
                Program.Invoke((System.Windows.Forms.MethodInvoker)delegate
                {
                    Program.MainForm.SongRequestPlayer.Url = new Uri("https://www.youtube.com/apiplayer?video_id=" + song.id + "&version=3&autoplay=1&enablejsapi=1&feature=player_embedded&controls=0&modestbranding=1&rel=0&showinfo=0&autohide=1&color=white&playerapiid=musicPlayer&iv_load_policy=3");
                    Program.MainForm.SongRequestPlayer.Visible = true;
                    Program.MainForm.SongRequestPlayer.BringToFront();
                    //Program.MainForm.YouTubePlayer.CallFunction("pauseVideo()");
                });
                //Program.MainForm.SongRequestPlayer.Navigated += (object sender, WebBrowserNavigatedEventArgs e) =>
                //{
                //    Console.WriteLine("Loaded");
                //    //File.WriteAllText("test.txt", Program.MainForm.SongRequestPlayer);
                //    //Program.MainForm.SongRequestPlayer.Document.InvokeScript("musicPlayer.pauseVideo();");
                //    new Thread(() =>
                //    {
                //        Thread.Sleep(10000);
                //        Program.Program.Invoke((MethodInvoker)delegate
                //        {
                //            /*bool found = false;
                //            foreach (HtmlElement elem in Program.MainForm.SongRequestPlayer.Document.All)
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
                //                //Program.MainForm.SongRequestPlayer.Document.Write("<object type=\"application/x-shockwave-flash\" data=\"https://www.youtube.com/apiplayer?video_id=" + song.id + "&version=3&feature=player_embedded&autoplay=1&controls=0&enablejsapi=1&modestbranding=1&rel=0&showinfo=0&autohide=1&color=white&playerapiid=musicPlayer&iv_load_policy=3\" width=\"100%\" height=\"100%\" id=\"musicPlayer\" style=\"visibility: visible;\"><param name=\"allowScriptAccess\" value=\"always\"><param name=\"wmode\" value=\"transparent\"></object>");
                //                Program.MainForm.SongRequestPlayer.Document.Write("<iframe id=\"musicPlayer\" type=\"text/html\" width=\"640\" height=\"360\" src=\"https://www.youtube.com/apiplayer?video_id=" + song.id + "&version=3&autoplay=1&enablejsapi=1&feature=player_embedded&controls=0&modestbranding=1&rel=0&showinfo=0&autohide=1&color=white&playerapiid=musicPlayer&iv_load_policy=3\" frameborder=\"0\" allowfullscreen>");
                //                Program.MainForm.SongRequestPlayer.Refresh();
                //            }*/
                //            //Program.MainForm.SongRequestPlayer.Navigate("javascript:musicPlayer.pauseVideo()");
                //            //if (test) return;
                //            //test = true;
                //            //Program.MainForm.SongRequestPlayer.Document.InvokeScript
                //            //Program.MainForm.SongRequestPlayer.Document.Write("<html lang=\"en\"><head><meta charset=\"utf-8\"><link id=\"bootstrap-style\" href=\"/css/bootstrap.css\" rel=\"stylesheet\"><link href=\"/css/bootstrap-responsive.css\" rel=\"stylesheet\"><link id=\"base-style\" href=\"/css/style.css\" rel=\"stylesheet\"><link id=\"base-style-responsive\" href=\"/css/style-responsive.css\" rel=\"stylesheet\"><!--[if lt IE 7 ]><link id=\"ie-style\" href=\"/css/style-ie.css\" rel=\"stylesheet\"><![endif]--><!--[if IE 8 ]><link id=\"ie-style\" href=\"/css/style-ie.css\" rel=\"stylesheet\"><![endif]--><!--[if IE 9 ]><![endif]--><!--[if lt IE 9]><script src=\"https://html5shim.googlecode.com/svn/trunk/html5.js\"></script><![endif]--><link rel=\"shortcut icon\" href=\"/favicon.png\"><link rel=\"icon\" href=\"/favicon.png\" type=\"image/x-icon\"><script async=\"\" src=\"//www.google-analytics.com/analytics.js\"></script><script>(function(i,s,o,g,r,a,m){i['GoogleAnalyticsObject']=r;i[r]=i[r]||function(){(i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)})(window,document,'script','//www.google-analytics.com/analytics.js','ga');ga('create', 'UA-39733925-1', 'nightbot.tv');ga('send', 'pageview');</script><script type=\"text/javascript\" src=\"https://ajax.googleapis.com/ajax/libs/swfobject/2.1/swfobject.js\"></script><style type=\"text/css\"></style><script>window[\"_GOOG_TRANS_EXT_VER\"] = \"1\";</script></head><body data-spy=\"scroll\" data-target=\"#nbsidebar\"><div class=\"container-fluid\"><div class=\"row-fluid\"><noscript><div class=\"alert alert-block span10\"><h4 class=\"alert-heading\">Warning!</h4><p>You need to have <a href=\"http://en.wikipedia.org/wiki/JavaScript\" target=\"_blank\">JavaScript</a> enabled to use this site.</p></div></noscript><div id=\"content\" class=\"span10\" style=\"min-height: 526px;\"><div id=\"autodj\" class=\"row-fluid sortable\"><div class=\"box span8\"><div class=\"box-content\" id=\"musicHolder\" style=\"height: 0;padding-bottom: 55%;position: relative;background-color: #000;\"><div style=\"position: absolute;top: 0; left: 0; right: 0; bottom: 0;\"><object type=\"application/x-shockwave-flash\" data=\"https://www.youtube.com/apiplayer?video_id=" + song.id + "&version=3&feature=player_embedded&autoplay=1&controls=0&enablejsapi=1&modestbranding=1&rel=0&showinfo=0&autohide=1&color=white&playerapiid=musicPlayer&iv_load_policy=3\" width=\"100%\" height=\"100%\" id=\"musicPlayer\" style=\"visibility: visible;\"><param name=\"allowScriptAccess\" value=\"always\"><param name=\"wmode\" value=\"transparent\"></object></div></div></div><div class=\"box span4\"><div class=\"box-content\"><div style=\"font-size:12px;text-align:center;font-weight:bold;\" id=\"currentTitle\">Gorillaz - Sunshine In a Bag</div><div style=\"font-size:12px;text-align:center;\" id=\"currentUser\">Requested by Zephroskash</div><div style=\"width:100%;font-size:12px;\"><span id=\"time\" style=\"float:left;\">00:00:01</span><span id=\"duration\" style=\"float:right;font-size:12px;\">00:05:29</span></div><br><div style=\"margin-bottom: 9px;height:3px;\" class=\"progress\"><div id=\"progressbar\" style=\"width: 0.375817836029374%;\" class=\"bar\"></div></div><br><div style=\"width:100%;text-align:center;\"><a id=\"pause\" href=\"#\" onclick=\"javascript:document.getElementById('musicPlayer').pauseVideo();\" class=\"quick-button-small\" style=\"width: 75px; display: none; margin-bottom: 4px;\"><i class=\"fa fa-pause\"></i><p>Pause</p></a><a id=\"play\" href=\"#\" onclick=\"javascript:document.getElementById('musicPlayer').playVideo();\" class=\"quick-button-small\" style=\"display: inline-block; width: 75px; margin-bottom: 4px;\"><i class=\"fa fa-play\"></i><p>Play</p></a><a href=\"#\" class=\"quick-button-small\" style=\"width:75px;display:inline-block;margin-bottom:4px;\" onclick=\"javascript:skipSong();\"><i class=\"fa fa-fast-forward\"></i><p>Skip Song</p></a><a target=\"_blank\" href=\"songlist/dorcomando\" class=\"quick-button-small\" style=\"width:75px;display:inline-block;margin-bottom:4px;\"><i class=\"fa fa-list\"></i><p>Song List</p></a><a href=\"#\" onclick=\"javascript:changeVolume('decrease');\" class=\"quick-button-small\" style=\"width:75px;display:inline-block;margin-bottom:4px;\"><i class=\"fa fa-volume-down\"></i><p>Lower Volume</p></a><a href=\"#\" onclick=\"javascript:changeVolume('increase');\" class=\"quick-button-small\" style=\"width:75px;display:inline-block;margin-bottom:4px;\"><i class=\"fa fa-volume-up\"></i><p>Raise Volume</p></a></div><br><div style=\"font-size:12px;\"><h4 style=\"color:black;margin-bottom:5px;\">Hotkeys:</h4><span style=\"color:black;font-weight:bold;\">ALT + <i class=\"icon-arrow-up\"></i></span> Increase Volume<br><span style=\"color:black;font-weight:bold;\">ALT + <i class=\"icon-arrow-down\"></i></span> Decrease Volume<br><span style=\"color:black;font-weight:bold;\">ALT + <i class=\"icon-arrow-right\"></i></span> Skip Song<br><br><i class=\"icon-folder-close\"></i> <a href=\"http://www.nightbot.tv/windowshotkeys.zip\">Download External Hotkeys (Windows)</a><br><i class=\"icon-folder-close\"></i> <a href=\"http://www.nightbot.tv/currentsongwindows3.0.zip\">Download Current Song for XSplit/OBS (Windows) v3.0</a></div></div></div></div></div></div></div><script src=\"/js/jquery-1.9.1.min.js\"></script><script src=\"/js/jquery-migrate-1.0.0.min.js\"></script><script src=\"/js/jquery-ui-1.10.0.custom.min.js\"></script><script src=\"/js/bootstrap.min.js\"></script><script src=\"/js/jquery.cookie.js\"></script><script src=\"/js/jquery.dataTables.min.js\"></script><script src=\"/js/excanvas.js\"></script><script src=\"/js/jquery.flot.min.js\"></script><script src=\"/js/jquery.flot.resize.min.js\"></script><script src=\"/js/jquery.chosen.min.js\"></script><script src=\"/js/jquery.uniform.min.js\"></script><script src=\"/js/jquery.noty.js\"></script><script src=\"/js/jquery.gritter.min.js\"></script><script src=\"/js/jquery.knob.js\"></script><script src=\"/js/custom.js\"></script><script type=\"text/javascript\">var RumID = 396;</script><script type=\"text/javascript\" src=\"https://statuscake.com/App/Workfloor/RUM/Embed.js\"></script><script type=\"text/javascript\" src=\"/js/soundcloud.player.api.js\"></script><script type=\"text/javascript\" src=\"/js/jquery-cookies.js\"></script><script type=\"text/javascript\" src=\"/js/jquery.marquee.js\"></script><script type=\"text/javascript\">$(document).keydown(function(event){if(event.keyCode === 38 && event.altKey) {changeVolume('increase');}if(event.keyCode === 39 && event.altKey) {skipSong();}if(event.keyCode === 40 && event.altKey) {changeVolume('decrease');}});function updateHTML(elmId, value) {if (document.getElementById(elmId)) {document.getElementById(elmId).innerHTML = value;}}function calcTime(secs, num1, num2) {s = ((Math.floor(secs/num1))%num2).toString();if (s.length < 2) {s = '0' + s;}return s;}function updatePlayerInfo() {if(document.getElementById('musicPlayer') && currentlyLoaded === true) { if(currentPlayer === 'youtube') {if (document.getElementById('musicPlayer').getCurrentTime() && document.getElementById('musicPlayer').getCurrentTime() !== 0 && document.getElementById('musicPlayer').getCurrentTime() === document.getElementById('musicPlayer').getDuration()) { currentlyLoaded = false;next(currentSongID,false,false);}duration = document.getElementById('musicPlayer').getDuration()currentTime = document.getElementById('musicPlayer').getCurrentTime();if(document.getElementById('musicPlayer').getPlayerState() == 1) {$('#pause').attr('onclick','javascript:document.getElementById(\'musicPlayer\').pauseVideo();').css('display','inline-block');$('#play').css('display','none');}if(document.getElementById('musicPlayer').getPlayerState() !== 1) {$('#play').attr('onclick','javascript:document.getElementById(\'musicPlayer\').playVideo();').css('display','inline-block');$('#pause').css('display','none');}if(currentVolume != document.getElementById('musicPlayer').getVolume()) {document.getElementById('musicPlayer').setVolume(currentVolume);}timeLeft = duration-currentTime;percentage = (currentTime/duration)*100;hoursNow = calcTime(currentTime,3600,24);minsNow = calcTime(currentTime,60,60);secsNow = calcTime(currentTime,1,60);hours = calcTime(duration,3600,24);mins = calcTime(duration,60,60);secs = calcTime(duration,1,60);updateHTML('time', hoursNow+':'+minsNow+':'+secsNow);updateHTML('duration', hours+':'+mins+':'+secs);$('#progressbar').css('width',percentage+'%');} else {player = SC.Widget($('#musicPlayer iframe')[0]);duration = 0;currentTime = 0;vol = 0;player.getCurrentSoundIndex(function(i) {if(i !== 0) {currentlyLoaded = false;next(currentSongID,false,false);}});player.getDuration(function(duration) {duration = Math.round(duration/1000);player.getPosition(function(currentTime) {currentTime = Math.round(currentTime/1000);              player.getVolume(function(vol) {if(currentVolume != vol) {player.setVolume(currentVolume);}timeLeft = duration-currentTime;percentage = (currentTime/duration)*100;hoursNow = calcTime(currentTime,3600,24);minsNow = calcTime(currentTime,60,60);secsNow = calcTime(currentTime,1,60);hours = calcTime(duration,3600,24);mins = calcTime(duration,60,60);secs = calcTime(duration,1,60);updateHTML('time', hoursNow+':'+minsNow+':'+secsNow);updateHTML('duration', hours+':'+mins+':'+secs);$('#progressbar').css('width',percentage+'%');});});});}}}function skipSong() {if(ga) ga('send', 'event', 'AutoDJ', 'Skip');currentlyLoaded = false;next(currentSongID,currentUser,true);}function next(sid,user,skip) {if(SCplayer()) {SCplayer().unbind('play');SCplayer().unbind('pause');SCplayer().unbind('ready');SCplayer().unbind('finish');}if(runningSkip) {$.gritter.add({class_name: 'gritterfixed',title: 'Error',text: 'How about not spamming the button?',});return;}runningSkip = true;if(ga) ga('send', 'event', 'AutoDJ', 'Next');currentlyLoaded = false;$('#musicHolder').html('Loading..');$('#currentTitle').html('N/A');$('marquee').trigger('stop');currentUser = 'N/A';$('#currentUser').html('Requested by N/A');var query = '';if(sid) {query += 'sid='+encodeURIComponent(sid)+'&';}if(user) {query += 'user='+encodeURIComponent(user)+'&';}if(skip) {query += 'skip=true&';}$.ajax({dataType: 'json',url: '/autodj/next?'+query+'callback=?',success: function(data) {if(data.status === 'success') {if(data.next !== 'none') {var newSong = data.next;currentSongID = newSong.id;currentPlayer = newSong.type;currentTitle = newSong.title;currentUser = newSong.user;$('#currentTitle').html(currentTitle);$('#currentUser').html('Requested by '+currentUser);if(currentPlayer == 'youtube') {$('#musicHolder').html('<div style=\"position: absolute;top: 0; left: 0; right: 0; bottom: 0;\"><div id=\"musicPlayer\"></div></div>');if(data.noNewSong == true) {$('#musicPlayer').parent().after('<span style=\"position:absolute;top:10px;color:white;left:10px;right:10px;font-size:20px;text-shadow:1px 1px #000000;overflow:hidden;\"><marquee behavior=\"scroll\" scrollamount=\"2\" direction=\"left\">There are no more songs to play. Type !songs in chat for instructions on how to request a song.</marquee></span>')$('marquee').marquee();}var params = { allowScriptAccess: \"always\", wmode: \"transparent\" };swfobject.embedSWF(\"https://www.youtube.com/apiplayer?video_id=\"+newSong.vid+\"&version=3&feature=player_embedded&autoplay=1&controls=0&enablejsapi=1&modestbranding=1&rel=0&showinfo=0&autohide=1&color=white&playerapiid=musicPlayer&iv_load_policy=3\", \"musicPlayer\", \"100%\", \"100%\", \"8\", null, null, params);} else {$('#musicHolder').html('<div id=\"musicPlayer\"></div><div id=\"artwork\" style=\"position: absolute;top: 0; left: 0; right: 0; bottom: 0;text-align:center;background-color:#000;border-radius:0px 0px 5px 5px;\"><img style=\"height:100% !important;\" height=\"100%\" src=\"'+newSong.artwork+'\" /></div>');$('#musicPlayer').html('<iframe class=\"iframe\" width=\"10\" style=\"opacity: 0;\" height=\"10\" scrolling=\"no\" frameborder=\"no\" src=\"https://w.soundcloud.com/player/?url='+newSong.url+'&auto_play=true&auto_advance=true&buying=true&liking=true&download=true&sharing=true&show_artwork=false&show_comments=false&show_playcount=true&show_user=true&hide_related=false&start_track=0&callback=true\"></iframe>')if(data.noNewSong == true) {$('#musicPlayer').after('<span style=\"position:absolute;top:10px;color:white;left:10px;right:10px;font-size:20px;text-shadow:1px 1px #000000;overflow:hidden;\"><marquee behavior=\"scroll\" scrollamount=\"2\" direction=\"left\">There are no more songs to play. Type !songs in chat for instructions on how to request a song.</marquee></span>')$('marquee').marquee();}SCplayer().bind('ready', function() {$('#pause').attr('onclick', 'javascript:SCplayer().toggle();').css('display','inline-block');$('#play').css('display','none');});SCplayer().bind('finish', function() {currentlyLoaded = false;next(currentSongID,false,false);});SCplayer().bind('play', function() {$('#pause').attr('onclick', 'javascript:SCplayer().toggle();').css('display','inline-block');$('#play').css('display','none');});SCplayer().bind('pause', function() {$('#play').attr('onclick', 'javascript:SCplayer().toggle();').css('display','inline-block');$('#pause').css('display','none');});}currentlyLoaded = true;} else {$('#musicHolder').html('<span style=\"position:absolute;top:10px;color:white;left:10px;right:10px;font-size:20px;text-shadow:1px 1px #000000;overflow:hidden;\"><marquee behavior=\"scroll\" scrollamount=\"2\" direction=\"left\">There are no more songs to play. Type !songs in chat for instructions on how to request a song.</marquee></span>')$('marquee').marquee();setTimeout(next, 30000);}} else {$.gritter.add({title: 'Error',text: data.msg,});setTimeout(next, 30000);}runningSkip = false;},timeout: 10000}).fail(function(xhr, status) {if(status == 'timeout') {$.gritter.add({title: 'Error',text: 'There was an error loading the next song. Trying again in 10 seconds.',});setTimeout(function(){ next(sid,user,skip); }, 10000);runningSkip = false;}});}function check() {$.getJSON('/autodj/check?callback=?', function(data) { if(ga) ga('send', 'event', 'AutoDJ', 'Check');if (isNaN(data.volume) === false && data.volume != currentVolume) {currentVolume = parseInt(data.volume);$.cookie(\"volume\", currentVolume, { expires: 365 });$.gritter.add({title: 'Volume Changed',text: 'The volume has been changed to '+currentVolume+'.',});}if(data.skipid == currentSongID && currentUser !== 'N/A') {skipSong();}});}function changeVolume(kind) {if(kind === 'increase') {if(ga) ga('send', 'event', 'AutoDJ', 'Increase Volume');currentVolume = parseInt(currentVolume)+10;if(currentVolume >= 100) currentVolume = 100;$.cookie(\"volume\", currentVolume, { expires: 365 });$.getJSON('/autodj/updatevolume?volume='+currentVolume+'&callback=?');$.gritter.add({class_name: 'gritterfixed',title: 'Volume Changed',text: 'The volume has been changed to '+currentVolume+'.',});} else {if(ga) ga('send', 'event', 'AutoDJ', 'Decrease Volume');currentVolume = parseInt(currentVolume)-10;if(currentVolume <= 0) currentVolume = 0;$.cookie(\"volume\", currentVolume, { expires: 365 });$.getJSON('/autodj/updatevolume?volume='+currentVolume+'&callback=?');$.gritter.add({class_name: 'gritterfixed',title: 'Volume Changed',text: 'The volume has been changed to '+currentVolume+'.',});}}runningSkip = false;currentSongID = 0;currentPlayer = 'youtube';currentVolume = parseInt($.cookie(\"volume\"));currentlyLoaded = false;SCplayer = function() {try {SC.Widget($('#musicPlayer iframe')[0]);} catch(e) {return false}return SC.Widget($('#musicPlayer iframe')[0]);};next();setInterval(check, 10000);setInterval(updatePlayerInfo, 600);</script></body></html>");
                //            //Program.MainForm.SongRequestPlayer.Document.Write("<object type=\"application/x-shockwave-flash\" data=\"https://www.youtube.com/apiplayer?video_id=" + song.id + "&version=3&feature=player_embedded&autoplay=1&controls=0&enablejsapi=1&modestbranding=1&rel=0&showinfo=0&autohide=1&color=white&playerapiid=musicPlayer&iv_load_policy=3\" width=\"100%\" height=\"100%\" id=\"musicPlayer\" style=\"visibility: visible;\"><param name=\"allowScriptAccess\" value=\"always\"><param name=\"wmode\" value=\"transparent\"></object>");
                //            //Program.MainForm.SongRequestPlayer.Document.Write("<iframe id=\"musicPlayer\" type=\"text/html\" width=\"640\" height=\"360\" src=\"https://www.youtube.com/apiplayer?video_id=" + song.id + "&version=3&autoplay=1&enablejsapi=1&feature=player_embedded&controls=0&modestbranding=1&rel=0&showinfo=0&autohide=1&color=white&playerapiid=musicPlayer&iv_load_policy=3\" frameborder=\"0\" allowfullscreen>");
                //            //Console.WriteLine(Program.MainForm.SongRequestPlayer.Document.ActiveElement.ToString());
                //            //Program.MainForm.SongRequestPlayer.Document.InvokeScript("musicPlayer.pauseVideo();");
                //            //Program.MainForm.SongRequestPlayer.Document.InvokeScript("musicPlayer.pauseVideo()");
                //            //Program.MainForm.SongRequestPlayer.
                //            //Program.MainForm.SongRequestPlayer.Url = new Uri("javascript:musicPlayer.pauseVideo()");
                //            //Program.MainForm.SongRequestPlayer.Navigate("javascript:musicPlayer.pauseVideo()");
                //            //HtmlElement element = Program.MainForm.SongRequestPlayer.Document.GetElementById("musicPlayer");
                //            //element.InvokeMember("playVideo");
                //            //File.WriteAllText("test.txt", Program.MainForm.SongRequestPlayer.DocumentText);
                //            //Console.WriteLine(Program.MainForm.SongRequestPlayer.Document.ActiveElement.Name);
                //            //Program.MainForm.SongRequestPlayer.Document.GetElementById("musicPlayer").InvokeMember("playVideo()");
                //            /*File.WriteAllText("test.txt", Program.MainForm.SongRequestPlayer.DocumentText);
                //            foreach (HtmlElement elem in Program.MainForm.SongRequestPlayer.Document.All)
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
            /*Program.Program.Invoke((System.Windows.Forms.MethodInvoker)delegate
            {
                foreach (System.Windows.Forms.HtmlElement elem in Program.MainForm.SongRequestPlayer.Document.All)
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
                        Program.MainForm.SongRequestPlayer.Navigate("javascript:pauseVideo()");
                        Program.MainForm.SongRequestPlayer.Navigate("javascript:pauseVideo();");
                        Program.MainForm.SongRequestPlayer.Navigate("javascript:player.pauseVideo()");
                        Program.MainForm.SongRequestPlayer.Navigate("javascript:player.pauseVideo();");
                        Program.MainForm.SongRequestPlayer.Navigate("javascript:musicPlayer.pauseVideo()");
                        Program.MainForm.SongRequestPlayer.Navigate("javascript:musicPlayer.pauseVideo();");
                    }
                }
            });*/
            ReorderQueue();
            PlaySong();
        }

        public static void SongRequestPlayer_Navigated(object sender, System.Windows.Forms.WebBrowserNavigatedEventArgs e)
        {
            EstimatedEnding = (TimeStarted = Api.GetUnixTimeNow()) + (int)CurrentSong.duration.TotalSeconds + 1;
            NextSongTimer.Change((int)CurrentSong.duration.TotalSeconds * 1000 + 1000, Timeout.Infinite);
            /*NextSongTimer.Change(5000, Timeout.Infinite);
            bool found = false;
            foreach (System.Windows.Forms.HtmlElement elem in Program.MainForm.SongRequestPlayer.Document.All)
            {
                Console.WriteLine(elem.Id);
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
                    Program.MainForm.SongRequestPlayer.Navigate("javascript:pauseVideo()");
                    Program.MainForm.SongRequestPlayer.Navigate("javascript:pauseVideo();");
                    Program.MainForm.SongRequestPlayer.Navigate("javascript:player.pauseVideo()");
                    Program.MainForm.SongRequestPlayer.Navigate("javascript:player.pauseVideo();");
                    Program.MainForm.SongRequestPlayer.Navigate("javascript:musicPlayer.pauseVideo()");
                    Program.MainForm.SongRequestPlayer.Navigate("javascript:musicPlayer.pauseVideo();");
                }
            }
            if (!found)
            {
                Program.MainForm.SongRequestPlayer.Document.Write("<iframe id=\"musicPlayer\" type=\"text/html\" width=\"640\" height=\"360\" src=\"https://www.youtube.com/apiplayer?video_id=" + CurrentSong.id + "&version=3&autoplay=1&enablejsapi=1&feature=player_embedded&controls=0&modestbranding=1&rel=0&showinfo=0&autohide=1&color=white&playerapiid=musicPlayer&iv_load_policy=3\" frameborder=\"0\" allowfullscreen>");
                Program.MainForm.SongRequestPlayer.Refresh();
            }*/
            Irc.sendMessage("[Song Request]: Now playing - " + CurrentSong.title + (CurrentSong.requester != "" ? ", requested by " + CurrentSong.requester : ""));
        }

        public static void StopSong()
        {
            TimeStarted = EstimatedEnding = 0;
            NextSongTimer.Change(Timeout.Infinite, Timeout.Infinite);
            Program.MainForm.SongRequestPlayer.Url = null;
            CurrentSong = null;
        }
    }
}

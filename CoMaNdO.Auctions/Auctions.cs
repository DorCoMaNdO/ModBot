using ModBot;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Net;
using System.Threading;

namespace CoMaNdO.Auctions
{
    [Export(typeof(IExtension))]
    public class Auctions : IExtension
    {
        private string LatestVersion;

        public void Load()
        {
            Events.Connected += Events_Connected;
            Events.Currency.OnQueue += Events_OnCurrencyQueue;
            Events.OnDisconnect += Events_OnDisconnect;
        }

        private void Events_Connected(string channel, string nick, bool partnered)
        {
            Commands.Add("!auction", Command_Auction, 2, 0);
            Commands.Add("!bid", Command_Bid, 0, 0);

            if (Auction.Loop == null) Auction.Loop = new Timer(auctionLoopHandler, null, Timeout.Infinite, Timeout.Infinite);
            Auction.Loop.Change(Timeout.Infinite, Timeout.Infinite);
        }

        private void Command_Auction(string user, string cmd, string[] args)
        {
            if (args.Length > 0)
            {
                if (args[0].ToLower() == "open")
                {
                    if (!Auction.Open)
                    {
                        Auction.Start();
                        Chat.SendMessage("Auction open! Bid by typing \"!bid {amount}\", etc.");
                    }
                    else Chat.SendMessage("Auction already open. Close or cancel the previous one first.");
                }
                else if (args[0].ToLower() == "close")
                {
                    if (Auction.Open)
                    {
                        KeyValuePair<string, int> winner = Auction.Close();
                        Chat.SendMessage("Auction closed!  Winner is: " + Users.GetBTag(winner.Key) + " (" + winner.Value + ")");
                    }
                    else Chat.SendMessage("No auction open.");
                }
                else if (args[0].ToLower() == "cancel")
                {
                    if (Auction.Open)
                    {
                        Auction.Cancel();
                        Chat.SendMessage("Auction cancelled. Bids refunded.");
                    }
                    else Chat.SendMessage("No auction open.");
                }
            }
        }

        private void Command_Bid(string user, string cmd, string[] args)
        {
            if (args.Length > 0)
            {
                int amount;
                if (int.TryParse(args[0], out amount))
                {
                    if (Auction.Open)
                    {
                        if (Auction.Bid(user, amount))
                        {
                            Auction.Loop.Change(0, 30000);
                        }
                    }
                }
            }
        }

        private void auctionLoopHandler(Object state)
        {
            if (Auction.Open && Auction.highBidder != "")
            {
                Chat.SendMessage(Users.GetDisplayName(Auction.highBidder) + " is currently winning, with a bid of " + Auction.highBid + "!");
            }
        }

        private void Events_OnCurrencyQueue(string user, ref string output, bool showtime)
        {
            if (Auction.Open && Auction.highBidder.ToLower() == user.ToLower()) output += " {" + Auction.highBid + "}";
        }

        private void Events_OnDisconnect()
        {
            Auction.Cancel();
        }

        public string Name { get { return "Auctions System"; } }
        public string FileName { get { return "CoMaNdO.Auctions.dll"; } }
        public string Author { get { return "CoMaNdO"; } }
        public string UniqueID { get { return "CoMaNdO.Auctions"; } }
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

    static class Auction
    {
        public static Timer Loop;
        public static string highBidder = "";
        public static int highBid = 0;
        public static bool Open = false;

        public static void Start()
        {
            Open = true;
            highBidder = "";
            highBid = 0;
        }

        public static bool Bid(string nick, int amount)
        {
            if (Open && amount > 0 && Currency.Check(nick) >= amount)
            {
                if (amount > highBid)
                {
                    if (highBidder != "")
                    {
                        Currency.Add(highBidder, highBid);
                    }
                    highBid = amount;
                    highBidder = nick;
                    Currency.Remove(nick, amount);
                    return true;
                }
            }
            return false;
        }

        public static KeyValuePair<string, int> Close()
        {
            Open = false;
            string winner = highBidder;
            int bid = highBid;
            highBidder = "";
            highBid = 0;
            return new KeyValuePair<string, int>(winner, bid);
        }

        public static void Cancel()
        {
            if (highBidder != "") Currency.Add(highBidder, highBid);
            Open = false;
            highBidder = "";
            highBid = 0;
        }
    }

    public static class Api
    {
        public static Timer Loop { get { return Auction.Loop; } }
        public static string highBidder { get { return Auction.highBidder; } }
        public static int highBid { get { return Auction.highBid; } }
        public static bool Open { get { return Auction.Open; } }

        public static void Start() { Auction.Start(); }

        public static bool Bid(string nick, int amount) { return Auction.Bid(nick, amount); }

        public static KeyValuePair<string, int> Close() { return Auction.Close(); }

        public static void Cancel() { Auction.Cancel(); }
    }
}

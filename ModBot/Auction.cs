namespace ModBot
{
    static class Auction
    {
        public static string highBidder { get; private set; }
        public static int highBid { get; private set; }
        public static bool Open { get; private set; }

        public static void Start()
        {
            Open = true;
            highBidder = "";
            highBid = 0;
        }

        public static bool placeBid(string nick, int amount)
        {
            if (Open && amount > 0 && Database.checkCurrency(nick) >= amount)
            {
                if (amount > highBid)
                {
                    if (highBidder != "")
                    {
                        Database.addCurrency(highBidder, highBid);
                    }
                    highBid = amount;
                    highBidder = nick;
                    Database.removeCurrency(nick, amount);
                    return true;
                }
            }
            return false;
        }

        public static System.Tuple<string, int> Close()
        {
            Open = false;
            string winner = highBidder;
            int bid = highBid;
            highBidder = "";
            highBid = 0;
            return new System.Tuple<string, int>(winner, bid);
        }

        public static void Cancel()
        {
            if (highBidder != "")
            {
                Database.addCurrency(highBidder, highBid);
            }
            Open = false;
            highBidder = "";
            highBid = 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModBot
{
    public static class Auction
    {
        public static string highBidder { get; private set; }
        public static int highBid { get; private set; }

        public static void Start()
        {
            highBidder = "";
            highBid = 0;
        }

        public static bool placeBid(string nick, int amount)
        {
            if (Database.checkCurrency(nick) >= amount)
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

        public static void Cancel()
        {
            if (highBidder != "")
            {
                Database.addCurrency(highBidder, highBid);
            }
            highBidder = "";
            highBid = 0;
        }
    }
}

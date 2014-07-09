using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModBot
{
    public class Auction
    {
        public string highBidder {get; private set;}
        public int highBid { get; private set; }

        public Auction()
        {
            highBidder = "";
            highBid = 0;
        }

        public bool placeBid(string nick, int amount)
        {
            if (Database.checkCurrency(nick) >= amount)
            {
                if (amount > highBid)
                {
                    Database.addCurrency(highBidder, highBid);
                    highBid = amount;
                    highBidder = nick;
                    Database.removeCurrency(highBidder, highBid);
                    return true;
                }
                return false;
            }
            return false;
        }

        public void Cancel()
        {
            Database.addCurrency(highBidder, highBid);
            highBidder = "";
            highBid = 0;
        }
    }
}

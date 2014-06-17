using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModBot
{
    class Auction
    {
        private Database db;
        public String highBidder {get; private set;}
        public int highBid { get; private set; }

        public Auction(Database d)
        {
            db = d;
            highBidder = "";
            highBid = 0;
        }

        public bool placeBid(String nick, int amount)
        {
            if (db.checkCurrency(nick) >= amount)
            {
                if (amount > highBid)
                {
                    db.addCurrency(highBidder, highBid);
                    highBid = amount;
                    highBidder = nick;
                    db.removeCurrency(highBidder, highBid);
                    return true;
                }
                return false;
            }
            return false;
        }

        public void Cancel()
        {
            db.addCurrency(highBidder, highBid);
            highBidder = "";
            highBid = 0;
        }
    }
}

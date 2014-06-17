using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModBot
{
    public class Pool
    {
        private Dictionary<string, int> winners;
        private Dictionary<string, PoolUser> bets = new Dictionary<string, PoolUser>();
        private Database db;
        private int maxBet, totalBets;
        private String[] options;

        public Pool(Database db, int maxBet, String[] options)
        {
            this.db = db;
            this.maxBet = maxBet;
            this.options = options;
            totalBets = 0;
        }

        public void placeBet(String nick, int option, int amount)
        {
            if (option < options.Length && amount <= maxBet)
            {
                if (bets.ContainsKey(nick))
                {
                    if (db.checkCurrency(nick) + bets[nick].betAmount >= amount)
                    {
                        if (bets[nick].betAmount < amount)
                        {
                            db.addCurrency(nick, bets[nick].betAmount - amount);
                            bets[nick].betAmount = amount;
                            bets[nick].betOn = option;
                        }
                        else
                        {
                            db.removeCurrency(nick, amount - bets[nick].betAmount);
                            bets[nick].betAmount = amount;
                            bets[nick].betOn = option;
                        }
                    }
                }
                else
                {
                    if (db.checkCurrency(nick) >= amount)
                    {
                        db.removeCurrency(nick, amount);
                        bets.Add(nick, new PoolUser(option, amount));
                    }
                }
            }
        }

        public void closePool(int winIndex)
        {
            buildTotalBets();
            buildWinners(winIndex);            
        }

        private void buildWinners(int winIndex)
        {
            winners = new Dictionary<string, int>();
            foreach (String nick in bets.Keys)
            {
                if (bets[nick].betOn == winIndex)
                {
                    double temp = (double)(totalBets - getTotalBetsOn(winIndex)) * ((double)bets[nick].betAmount / getTotalBetsOn(winIndex));
                    int payout = (int)Math.Round(temp) + bets[nick].betAmount;
                    //Console.WriteLine("Pre-Round: " + temp + " Post Round and addition = " + payout);
                    if (winners.ContainsKey(nick))
                    {
                        Console.WriteLine(winners[nick].ToString());
                    }
                    else
                    {
                        winners.Add(nick, payout);
                        db.addCurrency(nick, payout);
                    }
                }
            }
        }

        private void buildTotalBets()
        {
            totalBets = 0;
            foreach (String nick in bets.Keys)
            {
                totalBets += bets[nick].betAmount;
            }
        }

        public int getTotalBets()
        {
            return totalBets;
        }

        public int getNumberOfBets(int index)
        {
            int numberOfBets = 0;
            foreach (String nick in bets.Keys)
            {
                if (bets[nick].betOn == index)
                {
                    numberOfBets++;
                }
            }
            return numberOfBets;
        }

        public int getTotalBetsOn(int index)
        {
            int totalBetsOnOption = 0;
            foreach (String nick in bets.Keys)
            {
                if (bets[nick].betOn == index)
                {
                    totalBetsOnOption += bets[nick].betAmount;
                }
            }

            return totalBetsOnOption;
        }

        public Dictionary<string, int> getWinners()
        {
            return winners;
        }

        public int getBetAmount(String nick)
        {
            return bets[nick].betAmount;
        }

        public int getBetOn(String nick)
        {
            return bets[nick].betOn;
        }

        public void cancel()
        {
            foreach (String nick in bets.Keys)
            {
                db.addCurrency(nick, bets[nick].betAmount);
            }
            bets.Clear();
        }

        public bool isInPool(String nick)
        {
            return bets.ContainsKey(nick);
        }
    }

    /// <summary>
    /// UTILITY OBJECT CLASS
    /// </summary>
    class PoolUser
    {
        public int betOn {get;set;}
        public int betAmount {get;set;}

        public PoolUser(int betOn, int betAmount) {
            this.betOn = betOn;
            this.betAmount = betAmount;
        }
    }
}

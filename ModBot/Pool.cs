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
        private int maxBet, totalBets;
        private List<string> options;

        public Pool(int maxBet, List<string> options)
        {
            this.maxBet = maxBet;
            this.options = options;
            totalBets = 0;
        }

        public void placeBet(string nick, string option, int amount)
        {
            if (options.Contains(option) && amount <= maxBet)
            {
                if (bets.ContainsKey(nick))
                {
                    if (Database.checkCurrency(nick) + bets[nick].betAmount >= amount)
                    {
                        if (bets[nick].betAmount < amount)
                        {
                            Database.addCurrency(nick, bets[nick].betAmount - amount);
                            bets[nick].betAmount = amount;
                            bets[nick].betOn = option;
                        }
                        else
                        {
                            Database.removeCurrency(nick, amount - bets[nick].betAmount);
                            bets[nick].betAmount = amount;
                            bets[nick].betOn = option;
                        }
                    }
                }
                else
                {
                    if (Database.checkCurrency(nick) >= amount)
                    {
                        Database.removeCurrency(nick, amount);
                        bets.Add(nick, new PoolUser(option, amount));
                    }
                }
            }
        }

        public void closePool(string winBet)
        {
            buildTotalBets();
            buildWinners(winBet);            
        }

        private void buildWinners(string winBet)
        {
            winners = new Dictionary<string, int>();
            foreach (string nick in bets.Keys)
            {
                if (bets[nick].betOn == winBet)
                {
                    double temp = (double)(totalBets - getTotalBetsOn(winBet)) * ((double)bets[nick].betAmount / getTotalBetsOn(winBet));
                    int payout = (int)Math.Round(temp) + bets[nick].betAmount;
                    //Console.WriteLine("Pre-Round: " + temp + " Post Round and addition = " + payout);
                    if (winners.ContainsKey(nick))
                    {
                        Console.WriteLine(winners[nick].ToString());
                    }
                    else
                    {
                        winners.Add(nick, payout);
                        Database.addCurrency(nick, payout);
                    }
                }
            }
        }

        private void buildTotalBets()
        {
            totalBets = 0;
            foreach (string nick in bets.Keys)
            {
                totalBets += bets[nick].betAmount;
            }
        }

        public int getTotalBets()
        {
            return totalBets;
        }

        public int getNumberOfBets(string bet)
        {
            int numberOfBets = 0;
            foreach (string nick in bets.Keys)
            {
                if (bets[nick].betOn == bet)
                {
                    numberOfBets++;
                }
            }
            return numberOfBets;
        }

        public int getTotalBetsOn(string bet)
        {
            int totalBetsOnOption = 0;
            foreach (string nick in bets.Keys)
            {
                if (bets[nick].betOn == bet)
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

        public int getBetAmount(string nick)
        {
            return bets[nick].betAmount;
        }

        public string getBetOn(string nick)
        {
            return bets[nick].betOn;
        }

        public void cancel()
        {
            foreach (string nick in bets.Keys)
            {
                Database.addCurrency(nick, bets[nick].betAmount);
            }
            bets.Clear();
        }

        public bool isInPool(string nick)
        {
            return bets.ContainsKey(nick);
        }
    }

    /// <summary>
    /// UTILITY OBJECT CLASS
    /// </summary>
    class PoolUser
    {
        public string betOn { get; set; }
        public int betAmount {get;set;}

        public PoolUser(string betOn, int betAmount) {
            this.betOn = betOn;
            this.betAmount = betAmount;
        }
    }
}

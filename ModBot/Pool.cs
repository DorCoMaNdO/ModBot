using System;
using System.Collections.Generic;

namespace ModBot
{
    static class Pool
    {
        private static Dictionary<string, int> winners = new Dictionary<string, int>();
        private static Dictionary<string, PoolUser> bets = new Dictionary<string, PoolUser>();
        private static int maxBet, totalBets;
        public static List<string> options { get; private set; }
        public static bool Running { get; private set; }
        public static bool Locked = false;

        public static void CreatePool(int max, List<string> lOptions)
        {
            maxBet = max;
            options = lOptions;
            totalBets = 0;
            bets.Clear();
            winners.Clear();
            Locked = false;
            Running = true;
        }

        public static bool placeBet(string nick, string option, int amount)
        {
            if (Running && options.Contains(option) && amount > 0 && amount <= maxBet)
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
                        return true;
                    }
                }
                else
                {
                    if (Database.checkCurrency(nick) >= amount)
                    {
                        Database.removeCurrency(nick, amount);
                        bets.Add(nick, new PoolUser(option, amount));
                        return true;
                    }
                }
            }
            return false;
        }

        public static void closePool(string winBet)
        {
            buildTotalBets();
            buildWinners(winBet);
            Locked = false;
            Running = false;
        }

        public static void buildWinners(string winBet)
        {
            winners.Clear();
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
            Running = false;
        }

        public static void buildTotalBets()
        {
            totalBets = 0;
            foreach (string nick in bets.Keys)
            {
                totalBets += bets[nick].betAmount;
            }
        }

        public static int getTotalBets()
        {
            return totalBets;
        }

        public static int getNumberOfBets(string bet)
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

        public static int getTotalBetsOn(string bet)
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

        public static Dictionary<string, int> getWinners()
        {
            return winners;
        }

        public static int getBetAmount(string nick)
        {
            return bets[nick].betAmount;
        }

        public static string getBetOn(string nick)
        {
            return bets[nick].betOn;
        }

        public static void cancel()
        {
            foreach (string nick in bets.Keys)
            {
                Database.addCurrency(nick, bets[nick].betAmount);
            }
            Clear();
            Running = false;
            Locked = false;
        }

        public static void Clear()
        {
            if(options != null) options.Clear();
            winners.Clear();
            bets.Clear();
        }

        public static bool isInPool(string nick)
        {
            return bets.ContainsKey(nick);
        }

        public static string GetOptionFromNumber(int optionnumber)
        {
            optionnumber--;
            if (optionnumber >= 0 && optionnumber < options.Count)
            {
                return options[optionnumber];
            }
            return "";
        }

        public static List<string> buildBetOptions(string[] temp)
        {
            List<string> betOptions = new List<string>();
            try
            {
                lock (betOptions)
                {
                    bool inQuote = false;
                    string option = "";
                    for (int i = 2; i < temp.Length; i++)
                    {
                        if (temp[i].StartsWith("\""))
                        {
                            inQuote = true;
                        }
                        if (!inQuote)
                        {
                            option = temp[i];
                        }
                        if (inQuote)
                        {
                            option += temp[i] + " ";
                        }
                        if (temp[i].EndsWith("\""))
                        {
                            option = option.Substring(1, option.Length - 3);
                            inQuote = false;
                        }
                        if (!inQuote && !option.StartsWith("#") && !betOptions.Contains(option))
                        {
                            betOptions.Add(option);
                            option = "";
                        }
                    }
                }

                //print(sb.ToString());
            }
            //catch (Exception e)
            catch
            {
                //Console.WriteLine(e.ToString());
                //Api.LogError("*************Error Message (via buildBetOptions()): " + DateTime.Now + "*********************************\r\n" + e + "\r\n");
            }
            return betOptions;
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

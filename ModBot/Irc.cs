using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Concurrent;

namespace ModBot
{
    class Irc
    {
        private String nick, password, channel, currency, admin, user = "";
        private int interval, payout = 0;
        private int[] intervals = {1,2,3,4,5,6,10,12,15,20,30,60};
        private TcpClient irc;
        private StreamReader read;
        private StreamWriter write;
        private bool raffleOpen, bettingOpen, auctionOpen, poolLocked = false;
        private Auction auction;
        private Raffle raffle;
        private Pool pool;
        private Database db;
        private List<string> users = new List<string>();
        private DateTime time;
        private bool handoutGiven = false;
        private String[] betOptions;
        private Timer currencyQueue;
        private List<string> usersToLookup = new List<string>();
        private ConcurrentQueue<string> highPriority = new ConcurrentQueue<string>();
        private ConcurrentQueue<string> normalPriority = new ConcurrentQueue<string>();
        private ConcurrentQueue<string> lowPriority = new ConcurrentQueue<string>();
        private Thread listener;
        private Thread timerThread;
        private Thread KAthread;
        private Timer auctionLooper;
        private Timer messageQueue;
        private Commands commands;
        private String greeting;
        private bool greetingOn = false;
        private int attempt;


        public Irc(String nick, String password, String channel, String currency, int interval, int payout) {
            setNick(nick);
            setPassword(password);
            setChannel(channel);
            setAdmin(channel);
            setCurrency(currency);
            setInterval(interval);
            setPayout(payout);

            Initialize();
        }

        private void Initialize()
        {
            db = new Database();
            db.newUser(capName(admin));
            db.setUserLevel(capName(admin), 3);

            commands = new Commands();

            greeting = ModBot.Properties.Settings.Default.greeting;

            Connect();   
        }

        private void Connect()
        {
            if (irc != null)
            {
                //Console.WriteLine("Irc connection already exists.  Closing it and opening a new one.");
                irc.Close();
            }

            irc = new TcpClient();

            int count = 1;
            while (!irc.Connected)
            {
                Console.WriteLine("Connect attempt " + count);
                try
                {
                    irc.Connect("199.9.250.229", 443);

                    read = new StreamReader(irc.GetStream());
                    write = new StreamWriter(irc.GetStream());

                    write.AutoFlush = true;

                    sendRaw("PASS " + password);
                    sendRaw("NICK " + nick);
                    sendRaw("USER " + nick + " 8 * :" + nick);
                    sendRaw("JOIN " + channel);
                }
                catch (SocketException e)
                {
                    Console.WriteLine("Unable to connect. Retrying in 5 seconds");
                }
                catch (Exception e)
                {
                    StreamWriter errorLog = new StreamWriter("Error_Log.log", true);
                    errorLog.WriteLine("*************Error Message (via Connect()): " + DateTime.Now + "*********************************");
                    errorLog.WriteLine(e);
                    errorLog.WriteLine("");
                    errorLog.Close();
                }
                count++;
                // Console.WriteLine("Connection failed.  Retrying in 5 seconds.");
                Thread.Sleep(5000);
            }
            StartThreads();            
        }

        private void StartThreads()
        {
            listener = new Thread(new ThreadStart(Listen));
            listener.Start();

            timerThread = new Thread(new ThreadStart(doWork));
            timerThread.Start();

            KAthread = new Thread(new ThreadStart(KeepAlive));
            KAthread.Start();

            messageQueue = new Timer(handleMessageQueue, null, 0, 4000);

            currencyQueue = new Timer(handleCurrencyQueue, null, Timeout.Infinite, Timeout.Infinite);

            auctionLooper = new Timer(auctionLoop, null, Timeout.Infinite, Timeout.Infinite);
        }

        /*private void Flush()
        {
            try
            {
                timerThread.Abort();
                KAthread.Abort();
                messageQueue.Dispose();
                currencyQueue.Dispose();
                auctionLooper.Dispose();
                listener.Abort();
            }
            catch (Exception e)
            {

            }
        }*/

        private void Listen()
        {
            try
            {
                while (irc.Connected)
                {
                    parseMessage(read.ReadLine());
                }
            }
            catch (IOException e)
            {

            }
            catch (Exception e)
            {
                Console.WriteLine("Uh oh, there was an error!  Everything should keep running, but if you keep seeing this message, email your Error_log.log file to twitch.tv.modbot@gmail.com");
                StreamWriter errorLog = new StreamWriter("Error_Log.log", true);
                errorLog.WriteLine("*************Error Message (via Listen()): " + DateTime.Now + "*********************************");
                errorLog.WriteLine(e);
                errorLog.WriteLine("");
                errorLog.Close();
            }
        }

        private void KeepAlive()
        {
            while (true)
            {
                Thread.Sleep(30000);
                sendRaw("PING 1245");
            }          
        }

        private void parseMessage(String message)
        {
            //print(message);            
            String[] msg = message.Split(' ');
            
            
            if (msg[0].Equals("PING"))
            {
                sendRaw("PONG " + msg[1]);
                //print("PONG " + msg[1]);
            }
            else if (msg[1].Equals("PRIVMSG"))
            {
                user = capName(getUser(message));
                addUserToList(user);
                //print(message);
                String temp = message.Substring(message.IndexOf(":", 1)+1);
                print(user + ": " + temp);
                handleMessage(temp);
            }
            else if (msg[1].Equals("JOIN"))
            {
                user = capName(getUser(message));
                addUserToList(user);
                print(user + " joined");
                if (greeting != "" && greetingOn)
                {
                    sendMessage(greeting.Replace("@user", user), 3);
                }
                if (!db.userExists(user))
                {
                    db.newUser(user);
                    db.addCurrency(user, payout);
                }
            }
            else if (msg[1].Equals("PART"))
            {
                removeUserFromList(capName(getUser(message)));
                print(user + " left");
            }
            else if (msg[1].Equals("352"))
            {
                //print(message);
                addUserToList(capName(msg[4]));
            }
            else
            {
                //print(message);
            }
        }

        private void handleMessage(String message)
        {
            String[] msg = message.Split(' ');

            //////////////RAFFLE COMMANDS/////////////////
            #region raffle
            if (msg[0].Equals("!raffle") && msg.Length >= 2) {
                //ADMIN RAFFLE COMMANDS: !raffle open <TicketCost> <MaxTickets>, !raffle close, !raffle draw, !raffle cancel//
                if (db.getUserLevel(user) >= 1) 
                {
                    if (msg[1].Equals("open") && msg.Length >= 4)
                    {
                        if (!raffleOpen)
                        {
                            int cost, max;
                            if (int.TryParse(msg[2], out cost) && int.TryParse(msg[3], out max))
                            {
                                if (cost >= 0 && max > 0)
                                {
                                    raffleOpen = true;
                                    raffle = new Raffle(db, cost, max);
                                    sendMessage("Raffle open!  Each ticket costs " + cost + " " + currency + " , and you can buy a maximum of " + max + " tickets.", 2);
                                }
                            }
                        }
                        else
                        {
                            sendMessage("There is already a raffle open.  Close or cancel the previous one first.", 1);
                        }
                    }
                    else if (msg[1].Equals("close"))
                    {
                        if (!raffleOpen)
                        {
                            sendMessage("No raffle running.  Start one first!", 1);
                        }
                        else
                        {
                            int numWinners = 0;
                            raffleOpen = false;
                            if (msg.Length > 2 && int.TryParse(msg[2], out numWinners) && numWinners > 0 && numWinners < raffle.getTotalTicketsPurchased())
                            {
                                if (numWinners <= raffle.maxDraw())
                                {
                                    sendMessage("Closing the raffle!  Total of " + raffle.getTotalTicketsPurchased() + " tickets purchased.", 2);
                                    if (raffle.getTotalTicketsPurchased() == 0)
                                    {
                                        sendMessage("No winners!", 2);
                                    }
                                    else
                                    {
                                        sendMessage("And the winners are...", 2);
                                        StringBuilder winners = new StringBuilder();
                                        bool addComma = false;
                                        for (int i = 0; i < numWinners; i++)
                                        {
                                            raffle.endRaffle();
                                            if (addComma)
                                            {
                                                winners.Append(", ");
                                            }
                                            winners.Append(checkBtag(raffle.getWinner()) + "! (" + raffle.getPersonalTicketsPurchased(raffle.getWinner()) + " tickets purchased)");
                                            addComma = true;
                                        }
                                        sendMessage(winners.ToString(), 2);
                                    }
                                }
                                else
                                {
                                    sendMessage("There are only " + raffle.maxDraw() + " people in the raffle who haven't won yet.  Your amount of winners to draw must be less than " + raffle.maxDraw(), 1);
                                }
                            }
                            else
                            {
                                raffle.endRaffle();
                                sendMessage("Closing the raffle!  Total of " + raffle.getTotalTicketsPurchased() + " tickets purchased.", 2);
                                if (raffle.getTotalTicketsPurchased() == 0)
                                {
                                    sendMessage("No winners!", 2);
                                }
                                else
                                {
                                    sendMessage("And the winner is....", 2);
                                    sendMessage(checkBtag(raffle.getWinner()) + "! (" + raffle.getPersonalTicketsPurchased(raffle.getWinner()) + " tickets purchased)", 2);
                                }
                            }
                        }
                    }
                    else if (msg[1].Equals("cancel"))
                    {
                        if (!raffleOpen)
                        {
                            sendMessage("No raffle running.", 1);
                        }
                        else
                        {
                            raffleOpen = false;
                            raffle.cancel();
                            sendMessage("Raffle canceled.  All tickets refunded.", 1);
                        }
                    }
                    else if (msg[1].Equals("draw"))
                    {
                        if (raffleOpen)
                        {
                            sendMessage("Close the raffle to draw a new winner!", 1);
                        }
                        else
                        {
                            int numWinners = 0;
                            if (msg.Length > 2 && int.TryParse(msg[2], out numWinners) && numWinners > 0 && numWinners < raffle.getTotalTicketsPurchased())
                            {
                                if (numWinners <= raffle.maxDraw())
                                {
                                    sendMessage("Drawing " + numWinners + " more winners!", 2);
                                    sendMessage("And the winners are...", 2);
                                    StringBuilder winners = new StringBuilder();
                                    bool addComma = false;
                                    for (int i = 0; i < numWinners; i++)
                                    {
                                        raffle.endRaffle();
                                        if (addComma)
                                        {
                                            winners.Append(", ");
                                        }
                                        winners.Append(checkBtag(raffle.getWinner()) + "!! (" + raffle.getPersonalTicketsPurchased(raffle.getWinner()) + " tickets purchased)");
                                        addComma = true;
                                    }
                                    sendMessage(winners.ToString(), 2);
                                }
                                else
                                {
                                    sendMessage("There are only " + raffle.maxDraw() + " people in the raffle who haven't won yet.  Your amount of winners to draw must be less than " + raffle.maxDraw(), 1);
                                }
                            }
                            else
                            {
                                raffle.endRaffle();
                                sendMessage("Drawing another winner!", 2);
                                sendMessage("And the winner is...", 2);
                                sendMessage(checkBtag(raffle.getWinner()) + "!! (" + raffle.getPersonalTicketsPurchased(raffle.getWinner()) + " tickets purchased)", 2);
                            }
                        }
                    }
                }
                //REGULAR USER COMMANDS: !raffle help
                if (msg[1].Equals("help"))
                {
                    if (raffleOpen)
                    {
                        sendMessage("Raffle currently open.  Each ticket costs " + raffle.getTicketCost() + " tokens, and you can buy a maximum of " + raffle.getMaxTickets() + " tickets. Buy tickets by type !ticket <amount>", 3);
                    }
                }
            }
            #endregion
            //////////////////END !RAFFLE COMMANDS//////////////


            //////////////////TICKET COMMANDS///////////////////
            #region ticket
            else if (msg[0].Equals("!ticket") || msg[0].Equals("!tickets")) {
                if (raffleOpen && msg.Length >= 2)
                {
                    int i;
                    if (msg[1] != null && int.TryParse(msg[1], out i) && i>= 0)
                    {
                        raffle.buyEntries(user, i);
                    }
                }
            }
            #endregion
            //////////////////END TICKET COMMANDS///////////////


            /////////////////Currency Commands/////////////////////
            #region currency
            else if (msg[0].Equals("!" + currency, StringComparison.OrdinalIgnoreCase))
            {
                ///////////Check your Currency////////////
                if (msg.Length == 1)
                {
                    addToLookups(user);
                }

                else if (msg.Length == 2 && db.getUserLevel(user) == 3)
                {
                    if (db.userExists(capName(msg[1])))
                    {
                        sendMessage("Admin check: " + capName(msg[1]) + " has " + db.checkCurrency(capName(msg[1])) + " " + currency, 2);
                    }
                    else sendMessage("Admin check: " + capName(msg[1]) + " is not a valid user.", 2);
                }
                else if (msg.Length >= 3)
                {
                    /////////////MOD ADD CURRENCY//////////////
                    if (msg[1].Equals("add") && db.getUserLevel(user) >= 2)
                    {
                        int amount;
                        if (int.TryParse(msg[2], out amount) && msg.Length >= 4)
                        {
                            if (msg[3].Equals("all"))
                            {
                                foreach (String nick in users)
                                {
                                    db.addCurrency(capName(nick), amount);
                                }
                                sendMessage("Added " + amount + " " + currency + " to everyone.", 1);
                                Log(user + " added " + amount + " " + currency + " to everyone.");
                            }
                            else
                            {
                                db.addCurrency(capName(msg[3]), amount);
                                sendMessage("Added " + amount + " " + currency + " to " + capName(msg[3]), 1);
                                Log(user + " added " + amount + " " + currency + " to " + capName(msg[3]));
                            }
                        }
                    }

                    ////////////MOD REMOVE CURRENCY////////////////
                    if (msg[1].Equals("remove") && db.getUserLevel(user) >= 2)
                    {
                        int amount;
                        if (msg[2] != null && int.TryParse(msg[2], out amount) && msg.Length > -4)
                        {

                            if (msg[3].Equals("all"))
                            {
                                foreach (String nick in users)
                                {
                                    db.removeCurrency(nick, amount);
                                }
                                sendMessage("Removed " + amount + " " + currency + " from everyone.", 1);
                                Log(user + " removed " + amount + " " + currency + " from everyone.");
                            }
                            else
                            {
                                db.removeCurrency(capName(msg[3]), amount);
                                sendMessage("Removed " + amount + " " + currency + " from " + capName(msg[3]), 1);
                                Log(user + " removed " + amount + " " + currency + " from " + capName(msg[3]));
                            }

                        }
                    }
                }
            }
            #endregion
            /////////////////////END CURRENCY COMMANDS/////////////////////

            ///////////////////BETTING COMMANDS//////////////////////////
            #region betting
            //////////////////ADMIN BET COMMANDS/////////////////////////
            else if (msg[0].Equals("!gamble") && msg.Length >= 2)
            {
                if (db.getUserLevel(user) >= 1)
                {
                    if (msg[1].Equals("open") && msg.Length >= 5)
                    {
                        if (!bettingOpen)
                        {
                            int maxBet;
                            if (int.TryParse(msg[2], out maxBet))
                            {
                                buildBetOptions(msg);
                                pool = new Pool(db, maxBet, betOptions);
                                bettingOpen = true;
                                sendMessage("New Betting Pool opened!  Max bet = " + maxBet + " " + currency, 2);
                                String temp = "Betting open for: ";
                                for (int i = 0; i < betOptions.Length; i++)
                                {
                                    temp += "(" + (i+1).ToString() + ") " + betOptions[i] + " ";
                                }
                                sendMessage(temp, 2);
                                sendMessage("Bet by typing \"!bet 50 1\" to bet 50 " + currency + " on option 1,  \"!bet 25 2\" to bet 25 on option 2, etc", 2);
                            }
                            else sendMessage("Invalid syntax.  Open a betting pool with: !gamble open <maxBet> <option1>, <option2>, .... <optionN> (comma delimited options)", 2);
                        }
                        else sendMessage("Betting Pool already opened.  Close or cancel the current one before starting a new one.", 1);
                    }
                    else if (msg[1].Equals("close"))
                    {
                        if (bettingOpen)
                        {
                            poolLocked = true;
                            sendMessage("Bets locked in.  Good luck everyone!", 2);
                        }
                        else sendMessage("No pool currently open.", 2);
                    }
                    else if (msg[1].Equals("winner") && msg.Length == 3)
                    {
                        if (bettingOpen && poolLocked)
                        {
                            int winIndex;
                            if (int.TryParse(msg[2], out winIndex) && winIndex > 0)
                            {
                                winIndex = winIndex - 1;
                                if (winIndex < betOptions.Length)
                                {
                                    pool.closePool(winIndex);
                                    bettingOpen = false;
                                    poolLocked = false;
                                    sendMessage("Betting Pool closed! A total of " + pool.getTotalBets() + " " + currency + " were bet.", 2);
                                    String output = "Bets for:";
                                    for (int i = 0; i < betOptions.Length; i++)
                                    {
                                        double x = ((double)pool.getTotalBetsOn(i) / pool.getTotalBets()) * 100;
                                        output += " " + betOptions[i] + " - " + pool.getNumberOfBets(i) + " (" + Math.Round(x) + "%);";
                                        //Console.WriteLine("TESTING: getTotalBetsOn(" + i + ") = " + pool.getTotalBetsOn(i) + " --- getTotalBets() = " + pool.getTotalBets() + " ---  (double)betsOn(i)/totalBets() = " + (double)(pool.getTotalBetsOn(i) / pool.getTotalBets()) + " --- *100 = " + (double)(pool.getTotalBetsOn(i) / pool.getTotalBets()) * 100 + " --- Converted to a double = " + (double)((pool.getTotalBetsOn(i) / pool.getTotalBets()) * 100) + " --- Rounded double = " + Math.Round((double)((pool.getTotalBetsOn(i) / pool.getTotalBets()) * 100)));
                                    }
                                    sendMessage(output, 2);
                                    Dictionary<string, int> winners = pool.getWinners();
                                    output = "Winners:";
                                    if (winners.Count == 0)
                                    {
                                        sendMessage(output + " No One!", 2);
                                    }
                                    for (int i = 0; i < winners.Count; i++)
                                    {
                                        output += " " + winners.ElementAt(i).Key + " - " + winners.ElementAt(i).Value + " (Bet " + pool.getBetAmount(winners.ElementAt(i).Key) + ")";
                                        if (i == 0 && i == winners.Count - 1)
                                        {
                                            sendMessage(output, 2);
                                            output = "";
                                        }
                                        else if ((i != 0 && i % 10 == 0) || i == winners.Count - 1)
                                        {
                                            sendMessage(output, 2);
                                            output = "";
                                        }
                                    }

                                }
                                else
                                {
                                    sendMessage("Close the betting pool by typing \"!gamble winner 1\" if option 1 won, \"!gamble winner 2\" for option 2, etc.", 1);
                                    sendMessage("You can type !bet help to get a list of the options for a reminder of which is each number if needed", 1);
                                }
                            }
                            else
                            {
                                sendMessage("Close the betting pool by typing \"!gamble winner 1\" if option 1 won, \"!gamble winner 2\" for option 2, etc.", 1);
                                sendMessage("You can type !bet help to get a list of the options for a reminder of which option is each number if needed", 1);
                            }
                        }
                        else sendMessage("Betting pool must be open and bets must be locked before you can specify a winner.", 2);
                    }
                    else if (msg[1].Equals("cancel"))
                    {
                        if (pool != null)
                        {
                            pool.cancel();
                            bettingOpen = false;
                            poolLocked = false;
                            sendMessage("Betting Pool canceled.  All bets refunded", 2);
                        }
                    }
                }
            }
            ////////////////USER BET COMMANDS////////////////////////
            if (msg[0].Equals("!bet") && bettingOpen)
            {
                if (msg.Length >= 2)
                {
                    int betAmount;
                    int betOn;
                    if (msg[1].Equals("help"))
                    {
                        if (bettingOpen)
                        {
                            String temp = "Betting open for: ";
                            for (int i = 0; i < betOptions.Length; i++)
                            {
                                temp += "(" + (i + 1).ToString() + ") " + betOptions[i] + " ";
                            }
                            sendMessage(temp, 3);
                            sendMessage("Bet by typing \"!bet 50 1\" to bet 50 " + currency + " on option 1,  \"bet 25 2\" to bet 25 on option 2, etc", 3);
                        }
                    }
                    else if (int.TryParse(msg[1], out betAmount) && msg.Length >= 3 && bettingOpen && !poolLocked)
                    {
                        if (int.TryParse(msg[2], out betOn) && betOn > 0 && betOn <= betOptions.Length)
                        {
                            pool.placeBet(user, betOn - 1, betAmount);
                        }
                    }
                }
                else
                {
                    if (pool.isInPool(user))
                    {
                        sendMessage(user + ": " + betOptions[pool.getBetOn(user)] + " (" + pool.getBetAmount(user) + ")", 2);
                    }
                }
            }
            #endregion
            ////////////////END BET COMMANDS/////////////////////////

            ////////////////AUCTION COMMANDS/////////////////////////
            #region auction
            else if (msg[0].Equals("!auction") && msg.Length >= 2)
            {
                if (db.getUserLevel(user) >= 1)
                {
                    if (msg[1].Equals("open"))
                    {
                        if (!auctionOpen)
                        {
                            auctionOpen = true;
                            auction = new Auction(db);
                            sendMessage("Auction open!  Bid by typing \"!bid 50\", etc.", 2);
                        }
                        else sendMessage("Auction already open.  Close or cancel the previous one first.", 1);
                    }
                    else if (msg[1].Equals("close"))
                    {
                        if (auctionOpen)
                        {
                            auctionOpen = false;
                            sendMessage("Auction closed!  Winner is: " + checkBtag(auction.highBidder) + " (" + auction.highBid + ")", 2);
                        }
                        else sendMessage("No auction open.", 1);
                    }
                    else if (msg[1].Equals("cancel"))
                    {
                        if (auctionOpen)
                        {
                            auctionOpen = false;
                            auction.Cancel();
                            sendMessage("Auction cancelled.  Bids refunded.", 2);
                        }
                        else sendMessage("No auction open.", 1);
                    }
                }
            }

            if (msg[0].Equals("!bid") && msg.Length >= 2)
            {
                int amount;
                if (int.TryParse(msg[1], out amount))
                {
                    if (auctionOpen)
                    {
                        if (auction.placeBid(user, amount))
                        {
                            auctionLooper.Change(0, 30000);
                        }
                    }
                }
            }
            #endregion
            ///////////////END AUCTION COMMANDS////////////////////////

            ////////////////ADMIN COMMANDS//////////////////////////////
            #region admin
            else if (msg[0].Equals("!admin") && db.getUserLevel(user) >= 3 && msg.Length >= 2)
            {
                if (msg[1].Equals("payout") && msg.Length >= 3)
                {
                    int amount = 0;
                    if (int.TryParse(msg[2], out amount) && amount > 0)
                    {
                        setPayout(amount);
                        sendMessage("New Payout Amount: " + amount, 1);
                    }
                    else sendMessage("Can't change payout amount.  Must be a valid integer greater than 0", 1);
                }
                if (msg[1].Equals("interval") && msg.Length >= 3)
                {
                    int tempInterval = -1;
                    if (int.TryParse(msg[2], out tempInterval) && Array.IndexOf(intervals, tempInterval) > -1)
                    {
                        setInterval(tempInterval);
                        sendMessage("New Payout Interval: " + tempInterval, 1);
                    }
                    else
                    {
                        //Console.WriteLine(tempInterval + " " + Array.IndexOf(intervals, tempInterval));
                        String output = "Can't change payout interval.  Accepted values: ";
                        bool addComma = false;
                        foreach (int x in intervals)
                        {
                            if (addComma)
                            {
                                output += ", ";
                            }
                            output += x;
                            addComma = true;
                        }
                        output += " minutes.";
                        sendMessage(output, 1);
                    }
                }
                if (msg[1].Equals("addmod") && msg.Length >= 3)
                {
                    string tNick = capName(msg[2]);
                    if (db.userExists(tNick))
                    {
                        if (!tNick.Equals(admin, StringComparison.OrdinalIgnoreCase))
                        {
                            db.setUserLevel(tNick, 1);
                            sendMessage(tNick + " added as a bot moderator.", 1);
                        }
                        else sendMessage("Cannot change broadcaster access level.", 1);
                    }
                    else sendMessage(tNick + " does not exist in the database.  Have them type !<currency>, then try to add them again.", 1);
                }
                if (msg[1].Equals("addsuper") && msg.Length >= 3)
                {
                    String tNick = capName(msg[2]);
                    if (db.userExists(tNick))
                    {
                        if (!tNick.Equals(admin, StringComparison.OrdinalIgnoreCase))
                        {
                            db.setUserLevel(tNick, 2);
                            sendMessage(tNick + " added as a bot Super Mod.", 1);
                        }
                        else sendMessage("Cannot change Broadcaster access level.", 1);
                    }
                    else sendMessage(tNick + " does not exist in the database.  Have them type !<currency>, then try to add them again.", 1);
                }
                if (msg[1].Equals("demote") && msg.Length >= 3)
                {
                    string tNick = capName(msg[2]);
                    if (db.userExists(tNick))
                    {
                        if (db.getUserLevel(tNick) > 0)
                        {
                            if (!tNick.Equals(admin, StringComparison.OrdinalIgnoreCase))
                            {
                                db.setUserLevel(tNick, db.getUserLevel(tNick) - 1);
                                sendMessage(tNick + " demoted.", 1);
                            }
                            else sendMessage("Cannot change Broadcaster access level.", 1);
                        }
                        else sendMessage("User is already Access Level 0.  Cannot demote further.", 1);
                    }
                    else sendMessage(tNick + " does not exist in the database.  Have them type !<currency>, then try again.", 1);
                }
                if (msg[1].Equals("setlevel") && msg.Length >= 4)
                {
                    string tNick = capName(msg[2]);
                    if (db.userExists(tNick))
                    {
                        if (!tNick.Equals(admin, StringComparison.OrdinalIgnoreCase))
                        {
                            int level;
                            if (int.TryParse(msg[3], out level) && level >= 0 && level < 3)
                            {
                                db.setUserLevel(tNick, level);
                                sendMessage(tNick + " set to Access Level " + level, 1);
                            }
                            else sendMessage("Level must be greater than or equal to 0, and less than 3 (0>=Level<3)", 1);
                        }
                        else sendMessage("Cannot change broadcaster access level.", 1);
                    }
                    else sendMessage(tNick + " does not exist in the database.  Have them type !currency, then try again.", 1);
                }
                if (msg[1].Equals("greeting") && msg.Length >= 3)
                {
                    print(db.getUserLevel(user) + "test 1");
                    if (msg[2].Equals("on"))
                    {
                        greetingOn = true;
                        sendMessage("Greetings turned on.", 2);
                    }
                    if (msg[2].Equals("off"))
                    {
                        greetingOn = false;
                        sendMessage("Greetings turned off.", 2);
                    }
                    if (msg[2].Equals("set") && msg.Length >= 4)
                    {
                        StringBuilder sb = new StringBuilder();
                        for (int i = 3; i < msg.Length; i++)
                        {
                            if (i == 3 && msg[i].StartsWith("/"))
                            {
                                sb.Append(msg[i].Substring(1, msg[i].Length-1) + " ");
                            }
                            else sb.Append(msg[i] + " ");
                        }
                       
                        greeting = sb.ToString();
                        ModBot.Properties.Settings.Default.greeting = greeting;
                        ModBot.Properties.Settings.Default.Save();
                        sendMessage("Your new greeting is: " + greeting, 2);
                        
                    }
                }
                if (msg[1].Equals("addsub") && msg.Length >= 3)
                {
                    if (db.addSub(capName(msg[2])))
                    {
                        sendMessage(capName(msg[2]) + " added as a subscriber.", 2);
                    }
                    else sendMessage(capName(msg[2]) + " does not exist in the database.  Have them type !<currency> then try again.", 1);
                }
                if (msg[1].Equals("removesub") && msg.Length >= 3)
                {
                    if (db.removeSub(capName(msg[2])))
                    {
                        sendMessage(capName(msg[2]) + " removed from subscribers.", 2);
                    }
                    else sendMessage(capName(msg[2]) + " does not exist in the database.", 1);
                }
            }
            #endregion
            ////////////////END ADMIN COMMANDS///////////////////////////

            ////////////////MOD COMMANDS//////////////////////////////
            #region modcommands
            else if (msg[0].Equals("!mod") && db.getUserLevel(user) >= 1 && msg.Length >= 2)
            {
                if (msg[1].Equals("addcommand") && msg.Length >= 5) {
                    int level;
                    if (int.TryParse(msg[2], out level) && level >= 0 && level <= 3){
                        String command = msg[3].ToLower();
                        if (!commands.cmdExists(command))
                        {
                            StringBuilder sb = new StringBuilder();
                            for (int i = 4; i < msg.Length; i++)
                            {
                                if (msg[i].StartsWith("/") && i == 4)
                                {
                                    sb.Append(msg[i].Substring(1, msg[i].Length-1));
                                }
                                else sb.Append(msg[i]);
                                if (i != msg.Length - 1)
                                {
                                    sb.Append(" ");
                                }
                            }
                            commands.addCommand(command, level, sb.ToString());
                            sendMessage(command + " command added.", 2);
                        }
                        else sendMessage(command + " is already a command.", 1);
                    }
                    else sendMessage("Invalid syntax.  Correct syntax is \"!mod addcom <access level> <command> <text you want to output>", 1);
                }
                if (msg[1].Equals("removecommand") && msg.Length >= 3)
                {
                    String command = msg[2].ToLower();
                    if (commands.cmdExists(command))
                    {
                        commands.removeCommand(command);
                        sendMessage(command + " removed.", 2);
                    }
                    else sendMessage(command + " command does not exist.", 1);
                }
                if (msg[1].Equals("commmandlist"))
                {
                    String temp = commands.getList();
                    if (temp != "")
                    {
                        sendMessage("Current commands: " + temp.Substring(0, temp.Length - 2), 2);
                    }
                    else sendMessage("Currently no custom commands", 2);
                }
            }
            #endregion
            ///////////////END MOD COMMANDS//////////////////////////

            ///////////////CUSTOM COMMANDS//////////////////////////
            #region custom
            if (commands.cmdExists(msg[0].ToLower()) && db.getUserLevel(user) >= commands.LevelRequired(msg[0]))
            {
                if (msg.Length > 1 && db.getUserLevel(user) > 0)
                {
                    sendMessage(commands.getOutput(msg[0]).Replace("@user", capName(msg[1])), 2);
                }
                else
                {
                    sendMessage(commands.getOutput(msg[0]).Replace("@user", user), 2);
                }
            }
            #endregion
            //////////////END CUSTOM COMMANDS///////////////////////

            ////////////////ADD BATTLETAG///////////////////////////
            if ((msg[0].Equals("!btag")|| msg[0].Equals("!battletag")) && msg.Length == 2 && msg[1].Contains("#"))
            {
                db.setBtag(user, msg[1]);
            }

           /* if (msg[0].Equals("!restart") && db.getUserLevel(user) == 3)
            {
                irc.Close();
                //Flush();
                Connect();
            }*/
            
        }



        private void addUserToList(String nick)
        {
            lock (users)
            {
                if (!users.Contains(nick))
                {
                    users.Add(nick);
                }
            }
        }

        private void removeUserFromList(String nick)
        {
            lock (users)
            {
                if (users.Contains(nick))
                {
                    users.Remove(nick);
                }
            }
        }

        private void buildUserList()
        {
            sendRaw("WHO " + channel);
        }

        private String capName(String user)
        {
            return char.ToUpper(user[0]) + user.Substring(1);
        }

        private String getUser(String message)
        {
            String[] temp = message.Split('!');
            user = temp[0].Substring(1);
            return capName(user);
        }

        private void setNick(String tNick)
        {
            nick = tNick.ToLower();
        }

        private void setPassword(String tPassword)
        {
            password = tPassword;
        }

        private void setChannel(String tChannel)
        {
            if (tChannel.StartsWith("#")) {
                channel = tChannel;
            }
            else {
                channel = "#" + tChannel;
            }
        }

        private void setAdmin(String tChannel)
        {
            if (tChannel.StartsWith("#"))
            {
                admin = tChannel.Substring(1);
            }
            else
            {
                admin = tChannel;
            }
        }

        private void setCurrency(String tCurrency)
        {
            if (tCurrency.StartsWith("!"))
            {
                currency = tCurrency.Substring(1);
            }
            else
            {
                currency = tCurrency;
            }
        }

        private void setInterval(int tInterval)
        {
            interval = tInterval;
        }

        private void setPayout(int tPayout)
        {
            payout = tPayout;
        }

        private void print(String message)
        {
            Console.WriteLine(message);
        }

        private void sendRaw(String message)
        {
            
            try
            {
                write.WriteLine(message);
                attempt = 0;
            }
            catch (Exception e)
            {
                attempt++;
                //Console.WriteLine("Can't send data.  Attempt: " + attempt);
                if (attempt >= 5)
                {
                    Console.WriteLine("Disconnected.  Attempting to reconnect.");
                    irc.Close();
                    //Flush();
                    Connect();
                    attempt = 0;
                }
            }

        }

        private void sendMessage(String message, int priority)
        {
            if (priority == 1)
            {
                highPriority.Enqueue(message);
            }
            else if (priority == 2)
            {
                normalPriority.Enqueue(message);
            }
            else lowPriority.Enqueue(message);
        }

        private String checkBtag(String person)
        {
            //DB Lookup person to see if they have a battletag set
            String btag = db.getBtag(person);
            //print(btag);
            if (btag == null)
            {
                return person;
            }
            else return person + " (" + btag + ") ";
        }

        private bool checkTime()
        {
            time = DateTime.Now;
            int x = time.Minute;

            //Console.WriteLine(x);
            if (x % interval == 0)
            {
                //print("HANDOUT TIME!!");
                handoutGiven = true;
                return handoutGiven;
            }

            //print("Time doesn't match :(");
            handoutGiven = false;
            return handoutGiven;
        }

        private bool checkStream()
        {
            if (irc.Connected)
            {
                using (var w = new WebClient())
                {
                    String json_data = "";
                    try
                    {
                        w.Proxy = null;
                        json_data = w.DownloadString("https://api.twitch.tv/kraken/streams/" + channel.Substring(1));
                        JObject stream = JObject.Parse(json_data);
                        if (stream["stream"].HasValues)
                        {
                            //print("STREAM ONLINE");
                            return true;
                        }
                    }
                    catch (SocketException e)
                    {
                        Console.WriteLine("Unable to connect to twitch API to check stream status. Skipping.");
                    }
                    catch (Exception e)
                    {
                        StreamWriter errorLog = new StreamWriter("Error_Log.log", true);
                        errorLog.WriteLine("*************Error Message (via checkStream()): " + DateTime.Now + "*********************************");
                        errorLog.WriteLine(e);
                        errorLog.WriteLine("");
                        errorLog.Close();
                    }
                }

                //print("STREAM OFFLINE");
                return false;
            }
            return false;
        }

        private String checkSubList()
        {
            if (Properties.Settings.Default.subUrl != "")
            {
                string url = Properties.Settings.Default.subUrl;
                String json_data = "";
                StringBuilder sb = new StringBuilder();
                sb.Append("Subscribers from Google Doc: ");
                using (var w = new WebClient())
                {
                    try
                    {
                        w.Proxy = null;
                        json_data = w.DownloadString(url);
                        JObject list = JObject.Parse(json_data);
                        bool addComma = false;
                        foreach (var x in list["feed"]["entry"])
                        {
                            if (addComma)
                            {
                                sb.Append(", ");
                            }
                            sb.Append(capName(x["title"]["$t"].ToString()));
                            addComma = true;
                        }
                    }
                    catch (SocketException e)
                    {
                        Console.WriteLine("Unable to read from Google Drive. Skipping.");
                    }
                    catch (Exception e)
                    {
                        StreamWriter errorLog = new StreamWriter("Error_Log.log", true);
                        errorLog.WriteLine("*************Error Message (via checkSubList()): " + DateTime.Now + "*********************************");
                        errorLog.WriteLine(e);
                        errorLog.WriteLine("");
                        errorLog.Close();
                    }
                }
                return sb.ToString();
            }
            else return "No valid Sub link supplied.  Skipping.";
        }

        private void handoutCurrency()
        {
            try
            {
                buildUserList();
                String temp = checkSubList();
                Thread.Sleep(5000);
                Console.WriteLine(temp);
                lock (users)
                {
                    foreach (String person in users)
                    {
                        if (db.isSubscriber(person) || temp.Contains(person))
                        {
                            db.addCurrency(person, payout * 2);
                        }
                        else
                        {
                            db.addCurrency(person, payout);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Problem reading sub list.  Skipping");
                StreamWriter errorLog = new StreamWriter("Error_Log.log", true);
                errorLog.WriteLine("*************Error Message (via handoutCurrency()): " + DateTime.Now + "*********************************");
                errorLog.WriteLine(e);
                errorLog.WriteLine("");
                errorLog.Close();
            }
        }

        private void buildBetOptions(String[] temp)
        {
            try
            {
                betOptions = new String[temp.Length - 3];
                StringBuilder sb = new StringBuilder();
                for (int i = 3; i < temp.Length; i++)
                {
                    sb.Append(temp[i]);
                }
                betOptions = sb.ToString().Split(',');
                //print(sb.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                StreamWriter errorLog = new StreamWriter("Error_Log.log", true);
                errorLog.WriteLine("*************Error Message: " + DateTime.Now + "*********************************");
                errorLog.WriteLine(e);
                errorLog.WriteLine("");
                errorLog.Close();
            }
        }

        private void doWork()
        {
            while (true)
            {
                try
                {
                    if (checkTime() && checkStream())
                    {
                        print("Handout happening now! Paying everyone " + payout + " " + currency);
                        handoutCurrency();
                    }
                    Thread.Sleep(60000);
                }
                catch (SocketException e)
                {
                    Console.WriteLine("No response from twitch.  Skipping handout.");
                }
                catch (Exception e)
                {
                    StreamWriter errorLog = new StreamWriter("Error_Log.log", true);
                    errorLog.WriteLine("*************Error Message (via doWork()): " + DateTime.Now + "*********************************");
                    errorLog.WriteLine(e);
                    errorLog.WriteLine("");
                    errorLog.Close();
                }
            }
        }

        private void addToLookups(String nick)
        {
            if (usersToLookup.Count == 0)
            {
                currencyQueue.Change(4000, Timeout.Infinite);
            }
            if (!usersToLookup.Contains(nick))
            {
                usersToLookup.Add(nick);
            }
        }

        private void handleCurrencyQueue(Object state)
        {
            if (usersToLookup.Count != 0)
            {
                String output = currency + ":";
                bool addComma = false;
                foreach (String person in usersToLookup)
                {
                    if (addComma){
                        output += ", ";
                    }

                    output += " " + person + " - " + db.checkCurrency(person);
                    if (raffleOpen)
                    {
                        if (raffle.isInRaffle(person))
                        {
                            output += "(" + raffle.getPersonalTicketsPurchased(person) + ")";
                        }
                    }
                    if (bettingOpen)
                    {
                        if (pool.isInPool(person))
                        {
                            output += "[" + pool.getBetAmount(person) + "]";
                        }
                    }
                    if (auctionOpen)
                    {
                        if (auction.highBidder.Equals(person))
                        {
                            output += "{" + auction.highBid + "}";
                        }
                    }
                    addComma = true;
                }
                sendRaw("PRIVMSG " + channel + " :" + output);
                usersToLookup.Clear();
            }

        }

        private void handleMessageQueue(Object state)
        {
            String message;
            //Console.WriteLine("Entering Message Queue.  Time: " + DateTime.Now);
            if (highPriority.TryDequeue(out message))
            {
                print(nick + ": " + message);
                sendRaw("PRIVMSG " + channel + " :" + message);
                messageQueue.Change(4000, Timeout.Infinite);
            }
            else if (normalPriority.TryDequeue(out message))
            {
                print(nick + ": " + message);
                sendRaw("PRIVMSG " + channel + " :" + message);
                messageQueue.Change(4000, Timeout.Infinite);
            }
            else if (lowPriority.TryDequeue(out message))
            {
                print(nick + ": " + message);
                sendRaw("PRIVMSG " + channel + " :" + message);
                messageQueue.Change(4000, Timeout.Infinite);
            }
            else messageQueue.Change(4000, Timeout.Infinite);
        }

        private void auctionLoop(Object state)
        {
            if (auctionOpen)
            {
                sendMessage(auction.highBidder + " is currently winning, with a bid of " + auction.highBid + "!", 1);
            }
        }

        private void Log(String output)
        {
            try
            {
                StreamWriter log = new StreamWriter("CommandLog.log", true);
                log.WriteLine("<" + DateTime.Now + "> " + output);
                log.Close();
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                StreamWriter errorLog = new StreamWriter("Error_Log.log", true);
                errorLog.WriteLine("*************Error Message (via Log()): " + DateTime.Now + "*********************************");
                errorLog.WriteLine(e);
                errorLog.WriteLine("");
                errorLog.Close();
            }

        }
    }
}

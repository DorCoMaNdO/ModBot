Updated December 26th, 2014, up-to-date to version 1.7.5466.20465.

ModBot is designed to be a user-friendly bot for Twitch.TV, based heavily on LoyaltyBot.

This modification of ModBot directly uses the base code of the original ModBot available at http://twitchmodbot.sourceforge.net.
Even though most of the original code has been modified, this project would probably have never been started without it.

Any questions/comments/suggestions can be emailed to me @ CoMaNdO.ModBot@gmail.com

Requires .NET Framework 4. Most Windows users should have it already, but if not: http://www.microsoft.com/en-us/download/details.aspx?id=17718



************************INSTALLATION************************

Head to http://ModBot.wordpress.com/installation



************************SUBSCRIBERS (Regulars)**************

ModBot features 2 ways to handle subscribers: a google docs spreadsheet, or adding someone as a sub via the database. They do not conflict with each other, so if you happen to have someones name in both they will not have the subscriber effect twice or more. Just pick whichever one is more convenient for you to use, or a combination. Really doesn't matter, although it is recommended to use the database method.

	Google Docs (Support not provided, if you encounter any issues you're on your own, use the database method instead):
		If you already use LoyaltyBot, you can use the exact same subscribers link.
		If you don't use LoyaltyBot and want to make a google docs spreadsheet to more easily manage your subscribers list:
			1) Create a new spreadsheet at http://docs.google.com/
			2) Set cell A1 as the header "Username"
			3) Subscriber Names will then be placed in Column A cells, starting with A2. (Fig. 1)
			4) Set the Subscriber list to Public, and change the type to json. (Fig. 2)
				*Note: Even if you don't have a Sub button, you can still add people to your sub list so that they receive double currency income.
		Figure 1: [Column Setup] - http://i.imgur.com/eyQOwGz.jpg
		Figure 2: [Changing Doc Settings] - http://i.imgur.com/jDU9xOR.jpg



	Database:
		Use the following command to add a subscriber:
		!modbot addsub <username>

		Use the following command to remove a subscriber:
		!modbot removesub <username>



************************USER ACCESS LEVELS******************

Something that you need to understand for this bot are the user levels. 
	You, the broadcaster, are always Level 5. You have access to every command.
	Normal users in the channel are level 0. They have access to all commands that are set to level 0.
	Level 1 users are Helpers. They have basic access.
	Level 2 users are Moderators. They have more advanced access.
	Level 3 users are Super mods. They have access to all commands except the ones that affect all users or the channel.
	Level 4 users are Trusted mods. They have access that is equal to the streamer, add only people you trust to this level.

Access of each level is described below.



************************COMMANDS****************************

Currency:
	!currency/!<currency> - (Anyone or Access Level 1 if the command is disabled) Checks your current amount of currency on the channel.
	!currency/!<currency> top5 - (Anyone) Provides a list of the 5 users with the most currency (5 minutes cooldown, mods not affected).
	!currency/!<currency> <username> - (Access Level 1) Manually check the currency of a specific person.
	!currency/!<currency> enable/disable - (Access Level 3) Enable or disable the currency command.
	!currency/!<currency> add <username/online/all> <amount> - (Access Level 3) Adds the specified amount of currency to the username. Using "online" as the username will give the specified of currency to all online users in the channel. Using "all" (Access Level 4) as the username will give the specified of currency to all users that have ever visited the channel.
	!currency/!<currency> set <username/online/all> <amount> - (Access Level 3) Sets the specified amount of currency to the username. Using "online" as the username will set the specified of currency to all online users in the channel. Using "all" (Access Level 4) as the username will set the specified of currency to all users that have ever visited the channel.
	!currency/!<currency> remove <username/online/all> <amount> - (Access Level 3) Removes the specified amount of currency from the username. Using "online" as the username will remove the specified of currency from all online users in the channel. Using "all" (Access Level 4) as the username will remove the specified of currency from all users that have ever visited the channel.
	!currency/!<currency> clear - (Access Level 3) Removes the currency from all the users who have ever visited the channel.



Time watched:
	!time - (Anyone or Access Level 1 if the command is disabled) Checks for how long you've watched the stream.
	!time top5 - (Anyone) Provides a list of the 5 users with the most time watched (5 minutes cooldown, mods not affected).
	!time <username> - (Access Level 1) Manually check the time watched of a specific person.
	!time enable/disable - (Access Level 3) Enable or disable the time command.



Custom commands:
	!modbot addcmd <AccessLevelRequired> <command> <output> - (Access Level 2) Access level must be between 0 and 4. Command and output can be any text. Quick example: "!mod addcom 0 !ts Come hang out with on on teamspeak" would add a !ts command that anyone in the channel could use. If the first character of the <output> is a /, it will be stripped.
	!modbot delcmd <command> - (Access Level 2) Deletes the command. Currently if you want to edit a command, you must delete it and re-add it.
	!modbot cmdlist - (Access Level 1) Lists all of the custom commands currently available to the channel.

	* The commands will always trigger if they're the first word in the sentence, so you might want to have a "command identifier" such as ! to prevent "accidental triggers" from occuring.
	* You can also use {NUMBER} as parameters where NUMBER represents the parameter index that will be copied.
	  Example: !modbot addcmd 0 !welcome Welcome to the stream {1}!
		       !welcome DorCoMaNdO
	  Output:  Welcome to the stream DorCoMaNdO!
	  * All of the parameters are required so a message will NOT be sent if you'd try to do something such as using just "!welcome" from the example above.



User management:
	!modbot addhelper <username> - (Access Level 3) Changes the person to a Helper (Access Level 1).
	!modbot addmod <username> - (Access Level 3) Changes the person to a Moderator (Access Level 2).
	!modbot addsuper <username> - (Access Level 4) Changes the person to a Super mod (Access Level 3).
	!modbot demote <username> - (Access Level 3) Moves the person down 1 Access Level. Only works if the person's access level is above 0.
	!modbot setlevel <username> <number> - (Access Level 3) Sets the person to the specified Access Level. Can be used instead of addhelper, addmod, addsuper, or demote. Cannot change someones access level to yours or above or below 0.
	!modbot addsub <username> - (Access Level 3) Add someone to the internal Sub List. Doesn't cause conflicts with a spreadsheet Sub List.
	!modbot removesub <username> - (Access Level 3) Remove someone from the internal Sub List.



Channel metadata:
	!modbot title <title> - (Access Level 4) Changes the channel's title.
	!modbot game <game> - (Access Level 4) Changes the channel's game.



Battletag:
	!btag/!battletag <YourBtag> - (Anyone) Sets your battletag in the database. If you win an auction or raffle, your battletag is shown in the winner output.



Bot:
	!modbot/!bot/!botinfo - (Anyone) Provides brief information about the bot, the current version and the blog's link (5 minutes cooldown, mods not affected). Please use it to support me.



************************EXTENSIONS**************************

* Extensions can be downloaded and updated through the updater, although updates to extensions will possibly be performed automatically if configured right by the extension.


Giveaway:
	Run giveaways in your channel, the UI provides many options to configure such as if the winner must be a follower and/or a subscriber and more.


	UI:
		Types:
			Active users - "Last active less than X minutes ago", this will include all the people who joined/said anything withing the last X minutes.
			Keyword - Users enter the giveaway by entering one of the keywords (!ticket/!tickets/!raffle/!giveaway).
					  If a custom keyword is specified, it will be announced to the chat and it will be the only one that will add them to the giveaway.
			Tickets - Users enter with !ticket/!tickets <amount>, you define the max amount of tickets and the ticket count.

		Settings:
			Must be a follower - Followers only.
			Must be a subscriber - Subscribers only.
			Subscribers' win multiplier X - Subscribers' "luck", this will multiply the subscribers' entries, even in tickets (will go above max tickets).
			Must have at least X currency - Only users with a certain amount of currency and above.
			Has watched the stream for at least X hours and Y minutes - Only users who watched the stream for a certain length and longer.
			Automatically ban winner - Automatically disqualify a user for the next roll.
			Announce false entries - In ticket/keyword giveaway, if a user doesn't answer the requirements, tell him in a message.
			Warn and timeout false entries - Give up to 3 warnings to a user for false entries, then time out.
			Announce timeouts - Announce timeouts made for false entries.

			NOTE: When the bot allows (eg. when the checkbox is checkable), features and filters will work together (eg. followers only and subscribers only will only allow followers that are subscribed).

		Bans:
			A blacklist from the giveaway, these users will not roll.

		Users:
			The users that are in the giveaway (in the active users giveaway this is slightly in-accurate as it does NOT eliminate names that are not following if "Must be a follower" is checked).

		Winner chat:
			See the chat messages of the winner for easy interaction.

		Buttons:
			Start - Start a giveaway with the settings specified above.
			Roll - Roll for a winner (if a ticket or a keyword giveaway is selected, closing the giveaway first is required).
			Open - Open a closed giveaway.
			Close - Close an opened giveaway.
			Announce - Announce the winner to the chat.
			Stop - End the giveaway.
			Cancel - Cancel the giveaway, refund entries in a ticket giveaway.

		Other:
			Timer above chat - How long has it been since the winner's last message.
			Timer above buttons, below chat - How long has it been since the last roll.


	Commands:
		!giveaway/!raffle start <Type> [Price] [MaxTickets] - (Access Level 2) Starts a new giveaway.
			Type - The giveaway type, these are the possible options:
				1 = Active users.
				2 = Keyword.
				3 = Ticket.
			If Ticket giveaway is selected, a price and max tickets can be provided too, default price is 5 and max tickets 1.
				Price - The price for a single ticket.
				MaxTickets - The max amount of tickets for a single user to buy.
		!giveaway/!raffle close/lock - (Access Level 2) Declines new entries.
		!giveaway/!raffle open/unlock - (Access Level 2) Accepts new entries.
		!giveaway/!raffle stop - (Access Level 2) Ends the giveaway.
		!giveaway/!raffle roll [optional <Amount>] - (Access Level 2) Rolls for a winner, in ticket/keyword giveaway the giveaway must be closed first.
		!giveaway/!raffle cancel - (Access Level 2) Cancels the current giveaway (and refunds everyone's entries).

		!ticket <amount> - (Anyone) In a ticket giveaway, purchases the specified number of tickets. The amount of tickets must be equal or greater than 0. If you buy tickets and wish to get out of the raffle, use "!ticket 0" to have your coins refunded.


	Planned:
		More options to control giveaways through the chat.



Auction:
	Lets users bid currency over something provided by (probably) the streamer.


	Commands:
		!auction open - (Access Level 2) Opens a new auction. Users can bid freely until you close the auction. Current winner is shown in the channel each time there's a new High Bid, and every 30 seconds afterwards.
		!auction close - (Access Level 2) Closes the auction and announces the final winner.
		!auction cancel - (Access Level 2) Cancels the auction and refunds the highest bid.
		!bid <Amount> - (Anyone) Bid <Amount>.


	Planned:
		UI.



Gambling:
	Lets users gamble currency points over different options, that's another way to earn currency.
	This is commonly used for win/lose over games/matches in the stream.


	Commands:
		!gamble open <MinBet> <MaxBet> <WinReward> <option1> <option2> <option3> ...  <optionN> - (Access Level 2) Opens a new betting pool.
			MinBet - Specifies the minimum amount of currency required to make a bet, minimum value is 1.
			MaxBet - Specifies the maximum amount of currency that a user can bet, minimum value is MinBet.
			WinReward - Additional currency reward to give to each winner, minimum value is 0.
			Options - Space separated, use quotation marks to add an option with spaces.
		!gamble close - (Access Level 2) Locks the bets so that no more bets can be made.
		!gamble winner <Option> - (Access Level 2) Closes the bet pool, and pays out people who bet on the correct option.
			Option - The winning option, provide the name or the option id with a hashtag first (ex: #1).
		!gamble cancel - (Access Level 2) Cancels the bet pool, and refunds all bets.

		!bet help - (Anyone) Provides information about the availble betting options and how to bet.
		!bet <Amount> <Option> - (Anyone) Bet <amount> on <option>
			Amount - The amount of currency to bet.
			Option - The option to bet on, can be the full option name or the id with a hashtag first (ex: #1).


	Planned:
		UI.



Greetings:
	Greets new users joining the channel.
	This is really not recommended for use, many people find greeting bots offensive.


	Commands:
		!modbot greeting set <Greeting Text> - (Access Level 3) Sets your greeting message that will be sent everytime someone joins your channel. Using '@user' in your greeting will put the person who joined's username in the greeting.
		!modbot greeting on - (Access Level 3) Turns on your greeting messages.
		!modbot greeting off - (Access Level 3) Turns off your greeting messages.



Polls:
	Start polls that cost currency.
	Contains optional automated goals to end the poll.


	Commands:
		!poll create/start <VoteCost> <VotesGoal> <TotalVotesGoal> <Title> <Option1> <Option2> ... <OptionN> - (Access Level 2) Start a poll.
			VoteCost - The cost to vote.
			VotesGoal - The goal that a single option must reach to automatically end the poll and declare that option as the winner, 0 to disable.
			TotalVotesGoal - The goal that all options together must reach (ex: if set to 8, 3 from option 1, 5 from option 2) to automatically end the poll and declare the leading option as the winner, 0 to disable.
			Title - The title of the poll.
			Options - Space separated options, use quotation marks to add options with spaces (ex: "Option 1").
		!poll close - (Access Level 2) Decline new votes.
		!poll open - (Access Level 2) Accept new votes.
		!poll stop/end - (Access Level 2) Ends the poll, declaring the leading option as the winner.
		!poll cancel - (Access Level 2) Cancels the poll, refunds all the votes.
		!poll votes - (Access Level 1) Shows the current state of the votes.
		!poll votes top <number> - (Access Level 1) Shows the current state of the top <number> votes.
		!vote <Option> - (Anyone) Vote on <option>.
			Option - The option, must be the full name or the id with a hashtag first (ex: #1).


	Planned:
		UI.



User Ranks (Work in progress):
	Provides ranks to your users. Fully configurable.
	

	Commands:
		!ranks add <Rank> - (Access Level 3) Add a rank.
		!ranks remove <Rank> - (Access Level 3) Remove a rank.
		!ranks list - (Access Level 3) List all ranks.


	Planned:
		UI.



Multiple Command Outputs:
	Adds the option to add custom commands with multiple outputs.
	An optional option is data storage, once the command is used and a certain output has been granted to a user, it'll be saved an whenever the user will use the command again, he'll get the same output every time.


	UI:
		[EXPLAIN]


	Planned:
		Better UI, commands.
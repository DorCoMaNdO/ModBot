namespace ModBot
{
    public static class CommandDemo
    {
        /*static CommandDemo()
        {
            Console.WriteLine("Test");
        }*/

        public static void OnRegistering(InitializationStep step) // This handler has been registered in the Modifications class. It's being called after the main window has loaded.
        {
            if(step == InitializationStep.CommandRegistration) // We register our commands at the same time the bot registers it's commands.
            {
                Commands.Add("!demo", Command_Demo); // We add the command with a handler that will perform the task we want it to.
            }
        }

        private static void Command_Demo(string user, string command, string[] args)
        {
            // Output
            Irc.sendMessage("This is my simple demo command!");
        }
    }
}

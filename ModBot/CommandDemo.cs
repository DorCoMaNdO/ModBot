using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModBot
{
    public static class CommandDemo
    {
        /*private class Dummy // We need a dummy class that we can call without editing the bot's code.
        {
            public Dummy()
            {
                Irc.OnInitialize += OnRegistering; // We use the dummy class to register our event.
                Console.WriteLine("Test1");
            }
        }
        private static Dummy dummy = new Dummy();*/

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

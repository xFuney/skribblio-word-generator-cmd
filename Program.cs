using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Skribblio_Words_Generator
{
    class Program
    {
        private const int ERROR_NO_FILE = 0x1;
        private const int ERROR_USER_DENIED_OVERWRITE = 0x25;
        private const int ERROR_FILE_CREATION_EXCEPTION = 0x2A;
        private const int ERROR_IO_EXCEPTION = 0x2;
        private const int ERROR_USER_REQUESTED = 0x45;
        private const int SUCCESS = 0x0;

        static void Main(string[] args)
        {
            // Check if the user passed an argument.

            if (args.Length == 0)
            {
                Console.WriteLine("You must provide a file that contains words to generate a CSV from.");
                Environment.Exit(ERROR_NO_FILE);   
            }

            // Let us just pretend that this works. It doesn't.
            // This WAS meant to check if a valid path was parsed, but that didn't happen.
           
            string fullPath = System.IO.Path.GetFullPath(args[0]);

            // Get the amount of lines in the selected text file.

            try
            {
                int i_lines = System.IO.File.ReadLines(fullPath).Count();
            }
            catch (IOException)
            {
                Console.WriteLine("IO Exception has occurred - did you provide a valid path?");
                Environment.Exit(ERROR_IO_EXCEPTION);
            }

            // Probably a horrible idea double-declaring but it works now.
            int lines = System.IO.File.ReadLines(fullPath).Count();

            // Open a stream so we can read from the file.
            System.IO.StreamReader reader = new System.IO.StreamReader(fullPath);

            // This code is practically a copy/paste from the source of the Windows Forms version.

            string outputText = "";
            string currentStr = "";

            int randomValue = 0; // We initialise it out here so we aren't initialising it constantly in the loop.
            int selectedAmount = 0;

            Random random = new Random();

            for (int i = 0; i < lines; i++)
            {
                // This'll recurse for each line that exists.
                // Don't bother throwing it into an array as that'll take up too much memory.

                // For every line we'll generate a number between 0 and 1.
                // IF 1, WE WILL NOT ADD THE NUMBER TO THE SET
                // IF 0, WE WILL ADD THE NUMBER TO THE SET

                // Once we've reached 29, we'll break out of this loop and produce the output to the user.

                currentStr = reader.ReadLine();
                //Console.WriteLine(currentStr);

                if (currentStr[0] != '-')
                {
                    randomValue = random.Next(0, 2) - 1;

                    if (randomValue != 0)
                    {
                        // Add one to the selected amount, and change the output text to reflect this.
                        selectedAmount++;
                        //Console.WriteLine("SelectedAmount is now " + selectedAmount + ".");

                        outputText = outputText + currentStr + ",";

                        //Console.WriteLine("OutputText is now " + outputText);
                    };

                    if (selectedAmount > 28)
                    {
                        // Break the FOR loop, as we have enough custom words now.
                        //Console.WriteLine("29 SELECTED, BREAKING LOOP");
                        break;
                    }
                }
                else
                {
                    lines--;
                }
            };

            // Close the file because we are finished with it.

            reader.Close();
            // Quickly remove the last character from the output to prevent a blank value being presented.

            outputText = outputText.Substring(0, outputText.Length - 1);

            // This code is added to create the file.

            //Console.WriteLine(outputText);
            string currentWorkingDirectory = System.IO.Directory.GetCurrentDirectory();
            string csvPath = currentWorkingDirectory + "\\processed.csv";

            if (System.IO.File.Exists(csvPath))
            {
                // Ask the user before we overwrite it?
                Console.Write("You've already got a file called 'processed.csv', overwrite? [y/N] ");
                string userInput = Console.ReadLine();
                userInput = userInput.ToLower();

                if (userInput != "y")
                {
                    // User said no or some other value, which means no.
                    Console.WriteLine("Exiting...");
                    Environment.Exit(ERROR_USER_DENIED_OVERWRITE);
                }
            }

            try
            {
                // Create the file, or overwrite if the file exists.
                using (FileStream fs = File.Create(csvPath))
                {
                    byte[] outputProcessed = new UTF8Encoding(true).GetBytes(outputText);
                    // Add some information to the file.
                    fs.Write(outputProcessed, 0, outputProcessed.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("FILE CREATION EXCEPTION");
                Console.WriteLine(ex.ToString());
                Environment.Exit(ERROR_FILE_CREATION_EXCEPTION);
            }
            
            Console.WriteLine("Creation complete, used " + selectedAmount + " out of " + lines + " words.");
            Environment.Exit(SUCCESS);

        }
    }
}

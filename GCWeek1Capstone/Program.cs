using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace GCWeek1Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(@"Welcome to the Pig Latin Translator!
            ");

            bool killswitch = true;
            while (killswitch)
            {
                string userInputStringORIG;
                string userInputString;
                Console.WriteLine("Enter a line to be translated:");
                userInputString = userInputStringORIG = Console.ReadLine();

                if (userInputStringORIG.Substring(userInputStringORIG.Length - 1) == "." ||
                    userInputStringORIG.Substring(userInputStringORIG.Length - 1) == "!" ||
                    userInputStringORIG.Substring(userInputStringORIG.Length - 1) == "?") //scrubs punctuation off end 
                {
                    userInputString = userInputString.Remove(userInputString.Length - 1);
                }

                List<string> wordList = new List<string>(Regex.Split(userInputString, @"[ ]+"));
                List<string> latinedWordList = new List<string>();

                foreach(string word in wordList)
                {

                    if (Regex.IsMatch(word, @"[\@\.1-9 ]"))
                    {
                        latinedWordList.Add(word);
                    }

                    else

                    {
                        string latinword = ReturnLatined(word.ToLower());
                        latinword = CapCheck(latinword, word); // capitalization method
                        latinedWordList.Add(latinword);
                    }
                }

                //ReturnLatined(userInputString);
                string outputSentence = string.Join(" " , latinedWordList.ToArray());

                if (userInputStringORIG.Substring(userInputStringORIG.Length - 1) == "." ||
                    userInputStringORIG.Substring(userInputStringORIG.Length - 1) == "!" ||
                    userInputStringORIG.Substring(userInputStringORIG.Length - 1) == "?")  
                {
                    Console.WriteLine(outputSentence + userInputStringORIG.Substring(userInputStringORIG.Length - 1)); //puts punctuation back
                }

                else //no punctuation originally
                {
                    Console.WriteLine(outputSentence);
                }

                killswitch = ContinueLoop("Translate another line?");

            }
        }


        static string ReturnLatined(string inputString1)
        {

            string inputString = inputString1;

            // vowel
            if (Regex.IsMatch(inputString.ToLower(), @"^[aeiou]"))
            {
                return (inputString.ToLower() + "way");
            }

          
            // consonant
            else
            {
                //creates a list of every consonant block by separating by vowels and removing them
                List<string> regexList = new List<string>(Regex.Split(inputString.ToLower(), @"[aeiou]+"));

                //removes the first consonant block from the beginning
                //does this via assigning the first block from the list as the string to be removed
                //makes an integer that finds the starting place in the index where the first instance of the removalString is
                //makes an output string by using those to find the place where the string is, remove however many subsequent characters the block is long 
                
                string removalString = regexList[0];
                int i = inputString.IndexOf(removalString); 
                string outputString2 = inputString.Remove(i, removalString.Length);

                return (outputString2 + regexList[0] + "ay");

                /* Indexing the position is more versatile in GENERAL but not as necessary for this specific process
                 * since we don't need to find the consonant block in the middle of the string ever.
                 * The bit to be removed is always going to start at the beginning and go the length of that block.
                 * I could take it out and simplify, especially since the indexing is case-sensitive and I ran into issues there,
                 * but it works right now, no reason to.
                 */

            }
        }

        static bool ContinueLoop(string prompt)
        {
            while (true)
            {
                bool userContinue;
                Console.WriteLine(prompt);
                Console.WriteLine("Y/N?");
                string userContinueInput = Console.ReadLine();

                if (userContinueInput.ToLower() == "y")
                {
                    userContinue = true;
                }

                else if (userContinueInput.ToLower() == "n")
                {
                    userContinue = false;
                }

                else
                {
                    Console.WriteLine("I'm not sure what you mean.");
                    continue;
                }


                return userContinue;
            }
        }

        static string CapCheck(string inputWord, string origWord)
        {

            
            if (Regex.IsMatch((" " + origWord + " "), @"\s[A-Z']+\s"))
            {
                inputWord = inputWord.ToUpper();
                return inputWord;
            }


            else if (Regex.IsMatch(origWord, @"^[A-Z]"))
            {
                //separates each char in the string into a char array, replaces first letter with capitalized one
                char[] inputCharacteredArray = inputWord.ToCharArray();
                char firstChar = inputCharacteredArray[0];
                firstChar = char.ToUpper(firstChar);
                inputCharacteredArray[0] = firstChar;

                //turn the array back into a string
                string outputString = new string(inputCharacteredArray);
                return outputString;
            }

            else
            {
                return inputWord;
            }

        }

    }
}

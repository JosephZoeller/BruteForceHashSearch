using System;
using System.Linq;

namespace BruteForceHashSearch
{
    class Program
    {
        static int count = 0;
        static char[] UpperCaseFrequency = { '_', 'E', 'T', 'A', 'I', 'N', 'O', 'S', 'H', 'R', 'D', 'L', 'U', 'C', 'M', 'F', 'W', 'Y', 'G', 'P', 'B', 'V', 'K', 'Q', 'J', 'X', 'Z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        static char[] LowerCaseFrequency = { '_', 'e', 't', 'a', 'i', 'n', 'o', 's', 'h', 'r', 'd', 'l', 'u', 'c', 'm', 'f', 'w', 'y', 'g', 'p', 'b', 'v', 'k', 'q', 'j', 'x', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        static char[] allCharactersFrequency = { '_', 'E', 'e', 'T', 't', 'A', 'a', 'I', 'i', 'N', 'n', 'O', 'o', 'S', 's', 'H', 'h', 'R', 'r', 'D', 'd', 'L', 'l', 'U', 'u', 'C', 'c', 'M', 'm', 'F', 'f', 'W', 'w', 'Y', 'v', 'G', 'g', 'P', 'p', 'B', 'b', 'V', 'v', 'K', 'k', 'Q', 'q', 'J', 'j', 'X', 'x', 'Z', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        static string[] guessWords;

        static string[] hashesToSeek = { "2d51f5fdf967", "abf2e64923af", "3da307098c9d", "24617427e7a", "ff2c0c1307d1", "9030677652fa", "ddbca5100242", "51c1cc6f8622", "b741f2949e26", "8d5895232237", "60db807ad6c6" }; // TENSION_NECK preincluded
        static char[][] parrallelCases = { allCharactersFrequency };

        static void Main(string[] args)
        {
            guessWords = Properties.Resources.SINGLE.Split('\n');

            int depth = 2;
            Console.WriteLine("Beginning Search, Depth: " + depth + "...");
            guessWords = ToUpperCaseAll(guessWords);
            Console.WriteLine("Guess words have been uppercased");
            StartWordsComboSearch(depth, "", "");
            //StartCharactersComboSearch(depth, "", "");

            /*
            Parallel.ForEach(parrallelCases, obj => {
                StartSearch(depth, searchPrefix, obj);
            });
            */

            Console.WriteLine("End Reached, Count: " + count);
            Console.ReadLine();
        }

        static void StartWordsComboSearch(int maxDepth, string prefix, string postfix)
        {
            Console.WriteLine("Search: " + prefix + "<...>" + postfix);
            if (!(prefix + postfix == ""))
                CheckString(prefix, postfix);

            if (maxDepth >= 1)
            {
                foreach (string word in guessWords)
                {
                    CheckString(prefix + word, postfix);

                    if (maxDepth > 1)
                        WordsRecurse(1, maxDepth, prefix + word, postfix);
                }
            }
        }

        static void WordsRecurse(int currentDepth, int maxDepth, string baseString, string postfix)
        {
            foreach (string word in guessWords)
            {
                CheckString(baseString + "_" + word, postfix);
                if (currentDepth + 1 < maxDepth)
                {
                    WordsRecurse(currentDepth + 1, maxDepth, baseString + "_" + word, postfix);
                }
            }
        }

        static void StartCharactersComboSearch(int maxDepth, string prefix, string postfix)
        {
            Console.WriteLine("Search: " + prefix + "<...>" + postfix);
            if (!(prefix + postfix == ""))
                CheckString(prefix, postfix);

            if (maxDepth >= 1)
            {
                foreach (char character in allCharactersFrequency)
                {
                    CheckString(prefix + character, postfix);

                    if (maxDepth > 1)
                        CharacterRecurse(1, maxDepth, prefix + character, postfix);
                }
            }
        }

        static void CharacterRecurse(int currentDepth, int maxDepth, string baseString, string postfix)
        {
            foreach (char character in allCharactersFrequency)
            {
                CheckString(baseString + character, postfix);
                if (currentDepth + 1 < maxDepth)
                {
                    CharacterRecurse(currentDepth + 1, maxDepth, baseString + character, postfix);
                }

            }

        }

        static void CheckString(string candidateString, string postfix = "")
        {
            string checkString = (candidateString + postfix);
            string tryHash = (CityHash.CityHash.CityHash64WithSeeds(checkString + "\0", 0x9ae16a3b2f90404f, (uint)((checkString[0]) << 16) + (uint)checkString.Length) & 0xFFFFFFFFFFFF).ToString("x");

            //Console.WriteLine(checkString);
            //Console.ReadKey();

            count++;
            if (hashesToSeek.Contains(tryHash))
            {
                Console.WriteLine("Found: " + candidateString);
                string writeToFile = string.Format("Found: [{0}] \n", candidateString);
                System.IO.File.AppendAllText("FoundHashes.txt", writeToFile);
            }
        }

        static string[] ToUpperCaseAll(string[] words)
        {

            for (int i = 0; i < words.Length; i++)
            {
                string word = words[i];
                if (word != null)
                {
                    words[i] = word.First().ToString().ToUpper() + word.Substring(1);
                }
            }
            return words;

        }

        static string[] FirstLetterToUpperCaseAll(string[] words)
        {

            for (int i = 0; i < words.Length; i++)
            {
                string word = words[i];
                if (word != null)
                {
                    words[i] = word.ToUpper();
                }
            }
            return words;

        }
    }
}

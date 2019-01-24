using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BruteForceHashSearch
{
    class Program
    {
        static int count = 0;
        static char[] UpperCaseFrequency = { '_', 'E', 'T', 'A', 'I', 'N', 'O', 'S', 'H', 'R', 'D', 'L', 'U', 'C', 'M', 'F', 'W', 'Y', 'G', 'P', 'B', 'V', 'K', 'Q', 'J', 'X', 'Z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        static char[] LowerCaseFrequency = { '_', 'e', 't', 'a', 'i', 'n', 'o', 's', 'h', 'r', 'd', 'l', 'u', 'c', 'm', 'f', 'w', 'y', 'g', 'p', 'b', 'v', 'k', 'q', 'j', 'x', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        static char[] allCharactersFrequency = { '_', 'E', 'e', 'T', 't', 'A', 'a', 'I', 'i', 'N', 'n', 'O', 'o', 'S', 's', 'H', 'h', 'R', 'r', 'D', 'd', 'L', 'l', 'U', 'u', 'C', 'c', 'M', 'm', 'F', 'f', 'W', 'w', 'Y', 'v', 'G', 'g', 'P', 'p', 'B', 'b', 'V', 'v', 'K', 'k', 'Q', 'q', 'J', 'j', 'X', 'x', 'Z', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        static string[] guessWords = Properties.Resources.GuessWords.Split('\n');

        static string[] hashesToSeek = { "2d51f5fdf967", "abf2e64923af", "3da307098c9d", "24617427e7a", "ff2c0c1307d1", "9030677652fa", "ddbca5100242", "51c1cc6f8622", "b741f2949e26", "8d5895232237"}; // "60db807ad6c6" TENSION_NECK
        static char[][] parrallelCases = { allCharactersFrequency };

        static void Main(string[] args)
        {
            int depth = 5;

            Console.WriteLine("Beginning Search, Depth: " + depth + "...");
            Console.WriteLine("# of all words: " + guessWords.Length);

            /*
            List<string> guessWordList = new List<string>(guessWords.ToList());
            for (int i = guessWordList.Count - 1; i >= 0; i--)
            {
                if (guessWordList[i].Length > 6)
                    guessWordList.RemoveAt(i);
            }
            guessWords = guessWordList.ToArray();
            Console.WriteLine("# of words with less than 6 characters: " + guessWords.Length);
            */
            

            StartWordsComboSearch(depth, "", "");
            guessWords = FirstLetterToUpperCaseAll(guessWords);
            StartWordsComboSearch(depth, "", "");
            guessWords = ToUpperCaseAll(guessWords);
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
            StringBuilder sb = new StringBuilder("Search structure: "); sb.Append(prefix);
            for (int i = 0; i < maxDepth - 1; i++) sb.Append("<...>_");
            sb.Append("<...>"); sb.Append(postfix);
            Console.WriteLine(sb);

            if (!(prefix + postfix == ""))
                CheckString(prefix, postfix);

            if (maxDepth >= 1)
            {
                for (int i = 0; i < guessWords.Length; i++)
                {
                    CheckString(prefix + guessWords[i], postfix);
                    
                    if (maxDepth > 1)
                        WordsRecurse(1, maxDepth, prefix + guessWords[i], postfix, i);
                }
            }
        }

        static void WordsRecurse(int currentDepth, int maxDepth, string baseString, string postfix, int wordIndex)
        {
            for (int i = 0; i < guessWords.Length; i++)
            {
                //if (i == wordIndex) continue;
                string combined = String.Join(string.Empty, guessWords[i], baseString);
                CheckString(combined, postfix);
                if (currentDepth + 1 < maxDepth)
                {
                    WordsRecurse(currentDepth + 1, maxDepth, combined, postfix, i);
                }
            }
        }

        static void StartCharactersComboSearch(int maxDepth, string prefix, string postfix)
        {
            StringBuilder sb = new StringBuilder("Search: "); sb.Append(prefix); sb.Append("<");
            for (int i = 0; i < maxDepth; i++) sb.Append("#");
            sb.Append(">"); sb.Append(postfix);
            Console.WriteLine(sb);

            if (!(prefix + postfix == ""))
                CheckString(prefix, postfix);

            if (maxDepth >= 1)
            {
                foreach (char character in allCharactersFrequency)
                {
                    string combined = string.Join(string.Empty, prefix, character);

                    CheckString(combined, postfix);
                    if (maxDepth > 1)
                        CharacterRecurse(1, maxDepth, combined, postfix);
                }
            }
        }

        static void CharacterRecurse(int currentDepth, int maxDepth, string baseString, string postfix)
        {
            foreach (char character in allCharactersFrequency)
            {
                string combined = string.Join(string.Empty, baseString, character);

                CheckString(combined, postfix);
                if (currentDepth + 1 < maxDepth)
                {
                    CharacterRecurse(currentDepth + 1, maxDepth, combined, postfix);
                }

            }

        }

        static void CheckString(string candidateString, string postfix = "")
        {
            string checkString = string.Join(string.Empty, candidateString, postfix);
            string tryHash = (CityHash.CityHash.CityHash64WithSeeds(checkString + "\0", 0x9ae16a3b2f90404f, (uint)((checkString[0]) << 16) + (uint)checkString.Length) & 0xFFFFFFFFFFFF).ToString("x");

            //Console.WriteLine(checkString);
            //Console.ReadKey();

            count++;
            if (hashesToSeek.Contains(tryHash))
            {
                string foundHash = "Found: " + tryHash + " | " + checkString + "\n";
                Console.WriteLine(foundHash);
                string writeToFile = foundHash;
                System.IO.File.AppendAllText("FoundHashes.txt", writeToFile);
            }
        }

        static string[] FirstLetterToUpperCaseAll(string[] words)
        {

            for (int i = 0; i < words.Length; i++)
            {
                string word = words[i];
                if (word != null)
                {
                    words[i] = word.First().ToString().ToUpper() + word.Substring(1).ToLower();
                }
            }
            return words;

        }

        static string[] ToUpperCaseAll(string[] words)
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

        static string[] ToLowerCaseAll(string[] words)
        {

            for (int i = 0; i < words.Length; i++)
            {
                string word = words[i];
                if (word != null)
                {
                    words[i] = word.ToLower();
                }
            }
            return words;

        }
    }
}

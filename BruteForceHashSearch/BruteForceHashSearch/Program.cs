using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace BruteForceHashSearch
{
    class Program
    {

        static char[] UpperCaseFrequency = { '_', 'E', 'T', 'A', 'I', 'N', 'O', 'S', 'H', 'R', 'D', 'L', 'U', 'C', 'M', 'F', 'W', 'Y', 'G', 'P', 'B', 'V', 'K', 'Q', 'J', 'X', 'Z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        static char[] LowerCaseFrequency = { '_', 'e', 't', 'a', 'i', 'n', 'o', 's', 'h', 'r', 'd', 'l', 'u', 'c', 'm', 'f', 'w', 'y', 'g', 'p', 'b', 'v', 'k', 'q', '0', '1', 'j', 'x', 'z', '2', '3', '4', '5', '6', '7', '8', '9' };
        static char[] allCharactersFrequency = { '_', 'E', 'e', 'T', 't', 'A', 'a', 'I', 'i', 'N', 'n', 'O', 'o', 'S', 's', 'H', 'h', 'R', 'r', 'D', 'd', 'L', 'l', 'U', 'u', 'C', 'c', 'M', 'm', 'F', 'f', 'W', 'w', 'Y', 'v', 'G', 'g', 'P', 'p', 'B', 'b', 'V', 'v', 'K', 'k', 'Q', 'q', 'J', 'j', 'X', 'x', 'Z', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        static void Main(string[] args)
        {
            Stopwatch stopWatch = new Stopwatch();

            int depth = 5;
            string[] dictionaryWords = Properties.Resources.Dictionary.Replace('\r','\n').Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            string[] guessWords = Properties.Resources.GuessWords.Replace('\r', '\n').Split(new char[]{'\n'}, StringSplitOptions.RemoveEmptyEntries);
            
            HashComparer.SetHashesToSeek(new string[]{ "86da1dadaf37" }); // "60db807ad6c6" TENSION_NECK //  other MESH_ for quiet rain scene"6597da72c376", "a92e1cb18e44", "ab91044b7ba0", <- muscles
            stopWatch.Start();
            Console.WriteLine("Beginning Search, Depth: " + depth + "...");

            CharacterCombiner CC = new CharacterCombiner(depth, "MESH_", "");
            CC.StartCharactersComboSearch(LowerCaseFrequency);

            /*
            CC.StartCharactersComboSearchMustContain("_", LowerCaseFrequency);
            foreach (string guessword in guessWords)
            {
                Console.WriteLine("Guessing for: " + guessword);
                CC.StartCharactersComboSearchMustContain(guessword, LowerCaseFrequency);
            }
            */

            /*
            Console.WriteLine("Lowercase search...");
            WordCombiner WC = new WordCombiner(3, "qui_body", "");
            string[] combinationWords = dictionaryWords;
            WC.StartWordsComboSearch(combinationWords, "_");
            Console.WriteLine("First letter Uppercase search...");
            combinationWords = WordCombiner.FirstLetterToUpperCaseAll(combinationWords);
            WC.StartWordsComboSearch(combinationWords, "_");
            Console.WriteLine("Uppercase search...");
            combinationWords = WordCombiner.ToUpperCaseAll(combinationWords);
            WC.StartWordsComboSearch(combinationWords, "_");
            */

            //string[] combinationWords = dictionaryWords;
            //WordCombiner WC = new WordCombiner(36, "", "");
            //Console.WriteLine("Sai's suggested collision search...");
            //WC.StartWordsComboSearch(combinationWords, "");

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            Console.WriteLine("End Reached, Count: " + HashComparer.getCount());
            Console.WriteLine("Elapsed time: " + elapsedTime);
            Console.ReadLine();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace BruteForceHashSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopWatch = new Stopwatch();

            int depth = 6; // 6 ~ 1 hour for MustContain
            string[] dictionaryWords = Properties.Resources.Dictionary.Replace('\r','\n').Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            string[] guessWords = Properties.Resources.GuessWords.Replace('\r', '\n').Split(new char[]{'\n'}, StringSplitOptions.RemoveEmptyEntries);
            
            HashComparer.SetHashesToSeek(new string[]{ "3da307098c9d", "ddbca5100242", "51c1cc6f8622"}); // "60db807ad6c6" TENSION_NECK
            stopWatch.Start();
            Console.WriteLine("Beginning Search, Depth: " + depth + "...");
            
            CharacterCombiner CC = new CharacterCombiner(depth, "", "");
            //CC.StartCharactersComboSearch();

            foreach (string guessword in guessWords)
            {
                Console.WriteLine("Guessing for: " + guessword);
                CC.StartCharactersComboSearchMustContain(guessword);
            }

            CC = new CharacterCombiner(depth, "qui", "");
            //CC.StartCharactersComboSearch();

            foreach (string guessword in guessWords)
            {
                Console.WriteLine("Guessing for: " + guessword);
                CC.StartCharactersComboSearchMustContain(guessword);
            }

            /*
            Console.WriteLine("Lowercase search...");
            WordCombiner WC = new WordCombiner(depth, "", "");
            WC.StartWordsComboSearch(guessWords, "_");
            Console.WriteLine("First letter Uppercase search...");
            guessWords = WordCombiner.FirstLetterToUpperCaseAll(guessWords);
            WC.StartWordsComboSearch(guessWords, "_");
            Console.WriteLine("Uppercase search...");
            guessWords = WordCombiner.ToUpperCaseAll(guessWords);
            WC.StartWordsComboSearch(guessWords, "_");
            */

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

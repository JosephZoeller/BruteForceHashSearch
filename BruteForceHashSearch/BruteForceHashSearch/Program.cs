using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BruteForceHashSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            int depth = 4;
            string[] dictionaryWords = Properties.Resources.Dictionary.Split('\n');
            string[] guessWords = Properties.Resources.GuessWords.Split('\n');
            HashComparer.SetHashesToSeek(new string[]{ "3da307098c9d", "ddbca5100242", "51c1cc6f8622" }); // "60db807ad6c6" TENSION_NECK

            Console.WriteLine("Beginning Search, Depth: " + depth + "...");
            CharacterCombiner CC = new CharacterCombiner(depth, "qui", "");
            CC.StartCharactersComboSearchMustContain("equip");

            WordCombiner WC = new WordCombiner(depth, "", "");
            WC.StartWordsComboSearch(guessWords, "_");
            guessWords = WordCombiner.FirstLetterToUpperCaseAll(guessWords);
            WC.StartWordsComboSearch(guessWords, "_");
            guessWords = WordCombiner.ToUpperCaseAll(guessWords);
            WC.StartWordsComboSearch(guessWords, "_");
            
            Console.WriteLine("End Reached, Count: " + HashComparer.getCount());
            Console.ReadLine();
        }
    }
}

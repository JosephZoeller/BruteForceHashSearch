using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BruteForceHashSearch
{
    class WordCombiner
    {
        string[] guessWords;
        int maxDepth;
        string prefix;
        string postfix;
        string wordSeperator;

        public WordCombiner(int _maxDepth, string _prefix, string _postfix)
        {
            maxDepth = _maxDepth; prefix = _prefix; postfix = _postfix;
        }

        public void StartWordsComboSearch(string[] _guessWords, string _wordSeperator = "")
        {
            guessWords = _guessWords; wordSeperator = _wordSeperator;

            StringBuilder sb = new StringBuilder("Search structure: ");
            sb.Append(prefix);
            for (int i = 0; i < maxDepth - 1; i++)
            {
                sb.Append("<...>");
                sb.Append(wordSeperator);
            }
            sb.Append("<...>");
            sb.Append(postfix);
            Console.WriteLine(sb);

            if (prefix + postfix != string.Empty)
                HashComparer.CheckString("", prefix, postfix);

            if (maxDepth >= 1)
            {
                for (int i = 0; i < guessWords.Length; i++)
                {
                    HashComparer.CheckString(guessWords[i], prefix, postfix);

                    if (maxDepth > 1)
                        WordsRecurse(1, maxDepth, prefix + guessWords[i], i);
                }
            }
        }

        private void WordsRecurse(int currentDepth, int maxDepth, string baseString, int wordIndex)
        {
            for (int i = 0; i < guessWords.Length; i++)
            {
                if (i == wordIndex) continue;

                string combined = String.Join(wordSeperator, baseString, guessWords[i]);
                HashComparer.CheckString(combined, prefix, postfix);

                if (currentDepth + 1 < maxDepth)
                {
                    WordsRecurse(currentDepth + 1, maxDepth, combined, i);
                }
            }
        }

        public static string[] FirstLetterToUpperCaseAll(string[] words)
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

        public static string[] ToUpperCaseAll(string[] words)
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

        public static string[] ToLowerCaseAll(string[] words)
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

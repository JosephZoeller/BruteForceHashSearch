using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BruteForceHashSearch
{
    class CharacterCombiner
    {
        string prefix;
        string postfix;
        string mustContain;
        int maxDepth;

        public CharacterCombiner(int _maxDepth, string _prefix = "", string _postfix = "")
        {
            maxDepth = _maxDepth; prefix = _prefix; postfix = _postfix; 
        }

        public void StartCharactersComboSearch(char[] characters)
        {
            if (prefix + postfix != string.Empty)
                HashComparer.CheckString("", prefix, postfix);

            if (maxDepth > 0)
            {
                foreach (char character in characters)
                {
                    string charToStr = character.ToString();
                    HashComparer.CheckString(charToStr, prefix, postfix);
                    if (maxDepth > 1)
                        CharacterRecurse(1, charToStr, characters);
                }
            }
        }

        private void CharacterRecurse(int currentDepth, string baseString, char[] characters)
        {
            foreach (char character in characters)
            {
                string combined = string.Join(string.Empty, baseString, character);
                HashComparer.CheckString(combined, prefix, postfix);

                if (currentDepth + 1 < maxDepth)
                {
                    CharacterRecurse(currentDepth + 1, combined, characters);
                }
            }
        }

        public void StartCharactersComboSearchMustContain(string _mustContain, char[] characters)
        {
            mustContain = _mustContain;

            if (prefix + mustContain + postfix != string.Empty)
                HashComparer.CheckString(mustContain, prefix, postfix);

            if (maxDepth > 0)
            {
                foreach (char character in characters)
                {
                    string charToStr = character.ToString();
                    HashComparer.CheckStringMustContain(charToStr, mustContain, prefix, postfix);
                    if (maxDepth > 1)
                        CharacterRecurseMustContain(1, charToStr, characters);
                }
            }
        }

        private void CharacterRecurseMustContain(int currentDepth, string baseString, char[] characters)
        {
            foreach (char character in characters)
            {
                string combined = string.Join(string.Empty, baseString, character);
                HashComparer.CheckStringMustContain(combined, mustContain, prefix, postfix);

                if (currentDepth + 1 < maxDepth)
                {
                    CharacterRecurseMustContain(currentDepth + 1, combined, characters);
                }
            }
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BruteForceHashSearch
{
    class CharacterCombiner
    {
        static char[] UpperCaseFrequency = { '_', 'E', 'T', 'A', 'I', 'N', 'O', 'S', 'H', 'R', 'D', 'L', 'U', 'C', 'M', 'F', 'W', 'Y', 'G', 'P', 'B', 'V', 'K', 'Q', 'J', 'X', 'Z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        static char[] LowerCaseFrequency = { '_', 'e', 't', 'a', 'i', 'n', 'o', 's', 'h', 'r', 'd', 'l', 'u', 'c', 'm', 'f', 'w', 'y', 'g', 'p', 'b', 'v', 'k', 'q', '0', '1', 'j', 'x', 'z', '2', '3', '4', '5', '6', '7', '8', '9' };
        static char[] allCharactersFrequency = { '_', 'E', 'e', 'T', 't', 'A', 'a', 'I', 'i', 'N', 'n', 'O', 'o', 'S', 's', 'H', 'h', 'R', 'r', 'D', 'd', 'L', 'l', 'U', 'u', 'C', 'c', 'M', 'm', 'F', 'f', 'W', 'w', 'Y', 'v', 'G', 'g', 'P', 'p', 'B', 'b', 'V', 'v', 'K', 'k', 'Q', 'q', 'J', 'j', 'X', 'x', 'Z', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        
        string prefix;
        string postfix;
        string mustContain;
        int maxDepth;

        public CharacterCombiner(int _maxDepth, string _prefix = "", string _postfix = "")
        {
            maxDepth = _maxDepth; prefix = _prefix; postfix = _postfix; 
        }

        public void StartCharactersComboSearch()
        {
            if (prefix + postfix != string.Empty)
                HashComparer.CheckString("", prefix, postfix);

            if (maxDepth > 0)
            {
                foreach (char character in LowerCaseFrequency)
                {
                    string charToStr = character.ToString();
                    HashComparer.CheckString(charToStr, prefix, postfix);
                    if (maxDepth > 1)
                        CharacterRecurse(1, charToStr);
                }
            }
        }

        private void CharacterRecurse(int currentDepth, string baseString)
        {
            foreach (char character in LowerCaseFrequency)
            {
                string combined = string.Join(string.Empty, baseString, character);
                HashComparer.CheckString(combined, prefix, postfix);

                if (currentDepth + 1 < maxDepth)
                {
                    CharacterRecurse(currentDepth + 1, combined);
                }
            }
        }

        public void StartCharactersComboSearchMustContain(string _mustContain)
        {
            mustContain = _mustContain;

            if (prefix + mustContain + postfix != string.Empty)
                HashComparer.CheckString(mustContain, prefix, postfix);

            if (maxDepth > 0)
            {
                foreach (char character in LowerCaseFrequency)
                {
                    string charToStr = character.ToString();
                    HashComparer.CheckStringMustContain(charToStr, mustContain, prefix, postfix);
                    if (maxDepth > 1)
                        CharacterRecurseMustContain(1, charToStr);
                }
            }
        }

        private void CharacterRecurseMustContain(int currentDepth, string baseString)
        {
            foreach (char character in LowerCaseFrequency)
            {
                string combined = string.Join(string.Empty, baseString, character);
                HashComparer.CheckStringMustContain(combined, mustContain, prefix, postfix);

                if (currentDepth + 1 < maxDepth)
                {
                    CharacterRecurseMustContain(currentDepth + 1, combined);
                }
            }
        }
        
    }
}

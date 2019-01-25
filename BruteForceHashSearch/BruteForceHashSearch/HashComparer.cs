using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BruteForceHashSearch
{
    static class HashComparer
    {
        public static string[] hashesToSeek;
        private static int count = 0;
        
        public static void SetHashesToSeek(string[] hashes)
        {
            hashesToSeek = hashes;
        }

        private static string TryHash(string checkString)
        {
            return (CityHash.CityHash.CityHash64WithSeeds(checkString + "\0", 0x9ae16a3b2f90404f, (uint)((checkString[0]) << 16) + (uint)checkString.Length) & 0xFFFFFFFFFFFF).ToString("x");
        }

        public static void CheckString(string candidateString = "", string prefix = "", string postfix = "")
        {
            string checkString = string.Join(string.Empty, prefix, candidateString, postfix);
            string tryHash = TryHash(checkString);

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

        public static void CheckStringMustContain(string candidateString = "", string mustContain = "", string prefix = "", string postfix = "")
        {
            for (int i = 0; i <= candidateString.Length; i++)
            {
                string checkString = string.Join(string.Empty, prefix, candidateString.Insert(i, mustContain), postfix);
                string tryHash = TryHash(checkString);

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
        }

        public static void ResetCount()
        {
            count = 0;
        }

        public static int getCount()
        {
            return count;
        }
    }
}

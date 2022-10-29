using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading;

namespace CountUnique
{
   public static class Process
    {
       public static int counter = 0;
        public static void ReadTextFile(List<string> P)
        {
            string? text = null;
            foreach (var file in P)
            {
                text = System.IO.File.ReadAllText(file);
                var SplitW = SplitWords(text);
                counter += CountOccurrences(SplitW);
            }
        }

        public static IEnumerable<string> SplitWords(string text)
        {
            string toLowerCase = text.ToLower();
            Regex wordMatcher = new Regex(@"[\p{L}']+");
            return wordMatcher.Matches(toLowerCase).Select(l => l.Value);
        }

        public static int CountOccurrences(IEnumerable<string> items)
        {
            int c = 0;
            var dictionary = new Dictionary<string, bool>();

            foreach (string word in items)
            {
                bool Flag = true;
                if (dictionary.TryGetValue(word, out Flag) == true)
                {
                    if (dictionary[word])
                        dictionary[word] = false;
                }
                else
                    dictionary[word] = true;
            }

            foreach (var entry in dictionary)
            {
                if (entry.Value.Equals(true))
                {
                    c++;
                }
            }
            return c;
        }

        public static FileInfo[] GetDocPathes(string Path)
        {
            DirectoryInfo P = new DirectoryInfo(Path);
            FileInfo[] Docs = P.GetFiles("*.txt");
            return Docs;
        }

        public static void Execute(string FolderPath)
        {
            var path = GetDocPathes(FolderPath);
            List<string> P1 = new List<string>();
            List<string> P2 = new List<string>();

            for (int i = 0; i < path.Count() / 2; i++)
            {
                P1.Add(path[i].ToString());
            }

            for (int i = path.Count() / 2; i < path.Count(); i++)
            {
                P2.Add(path[i].ToString());
            }

            //Thread Thread1 = new Thread(() => ReadTextFile(P1));
            //Thread Thread2 = new Thread(() => ReadTextFile(P2));
            //Thread1.Start();
            //Thread2.Start();

            ReadTextFile(P1);
            ReadTextFile(P2);
        }
    }
}

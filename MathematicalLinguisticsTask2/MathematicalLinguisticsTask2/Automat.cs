using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicalLinguisticsTask2
{
    public class Automat
    {
        public Dictionary<string, bool> ReadedWords { get; set; }
        public readonly HashSet<char> Alphabet = new HashSet<char>() { '0','1','2','3','4','5','6','7','8','9' }; 

        public Automat()
        {
            ReadedWords = new Dictionary<string, bool>();
            using (StreamReader sr = new StreamReader($"{Directory.GetCurrentDirectory().Replace("bin\\Debug","")}Data.txt"))
            {
                foreach (var word in sr.ReadToEnd().Split('#'))
                    ReadedWords.Add(word, false);
            }
        }
    }
}

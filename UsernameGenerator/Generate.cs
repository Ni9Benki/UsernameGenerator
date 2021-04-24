using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace UsernameGenerator
{
    class Generate
    {
        private List<string> englishWords = new List<string>();
        private List<string> swedishWords = new List<string>();

        public string GenerateUsername(string language)
        {
            var username = string.Empty;

            if (language == "English")
            {
                username = GetUsername(englishWords);
            }
            else if (language == "Swedish")
            {
                username = GetUsername(swedishWords);
            }

            return username.ToString();
        }

        private string GetUsername(List<string> words)
        {
            StringBuilder username = new StringBuilder();
            int numberOfWords = 2;
            Random random = new Random();

            for (int i = 0; i < numberOfWords; i++)
            {
                int index = random.Next(0, words.Count);

                username.Append(words[index].Substring(0, 1).ToUpper() + words[index].Substring(1).ToLower());
            }

            return username.ToString();
        }

        public void SetWordsInLists()
        {
            englishWords = ReadAllResourceLines(Properties.Resources.englishWords).ToList();
            swedishWords = ReadAllResourceLines(Properties.Resources.swedishWords).ToList();
        }

        string[] ReadAllResourceLines(string resourceText)
        {
            using (StringReader reader = new StringReader(resourceText))
            {
                return EnumerateLines(reader).ToArray();
            }
        }

        IEnumerable<string> EnumerateLines(TextReader reader)
        {
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                yield return line;
            }
        }
    }
}

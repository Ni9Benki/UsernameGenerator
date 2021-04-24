using System;
using System.Linq;
using System.IO;

namespace UsernameGenerator
{
    class Read
    {
        private string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            + @"\Usernames from Username Generator.txt";

        public bool CheckIfWordExists(string word)
        {
            var words = File.ReadAllLines(path).ToList();

            if (words.Contains(word))
            {
                return true;
            }

            return false;
        }

        public bool CheckIfFileExists()
        {
            if (File.Exists(path))
            {
                return true;
            }

            return false;
        }
    }
}

using System;

namespace UsernameGenerator
{
    class Save
    {
        public void WriteToFile(string generatedUsername)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                + @"\Usernames from Username Generator.txt";

            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(path, true))
            {
                file.WriteLine(generatedUsername);
            }
        }
    }
}
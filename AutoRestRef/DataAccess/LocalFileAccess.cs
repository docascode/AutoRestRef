using System;
using System.Collections.Generic;
using System.IO;

namespace AutoRestRef.DataAccess
{
    class LocalFileAccess
    {
        public static string ReadFile(string path)
        {
            try
            {
                using (var sr = new StreamReader(path))
                    return sr.ReadToEnd();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public static bool WriteFile(string path, string text)
        {
            try
            {
                using (var sw = new StreamWriter(path))
                    sw.WriteLine(text);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}

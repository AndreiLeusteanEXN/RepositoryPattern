using System;

namespace RepoPattern
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string path = @"..\..\..\albums.csv";
            Menu menu = new Menu(path);
        }
    }
}

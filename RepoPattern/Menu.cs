using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoPattern
{
    public class Menu
    {
        Repository repo;
        MenuSupport ms;
        string saveLocation = @"..\..\..\salvation.csv";

        public Menu(string path)
        {
            bool validFile = false;
            while (validFile == false)
            {
                Console.WriteLine("Insert filename:");
                string fileName = Console.ReadLine();
                string filePath = path + fileName;

                if (fileName == "exit")
                {
                    System.Environment.Exit(0);
                }

                if (filePath.EndsWith(".csv"))
                {
                    repo = new RepositoryCSV(filePath);
                    validFile = true;
                }

                else if (filePath.EndsWith(".xml"))
                {
                    repo = new RepositoryXML(filePath);
                    validFile = true;
                }

                else
                {
                    Console.WriteLine("Unsupported file extension!\nInsert a valid filename, or type \"exit\" to quit the program");
                }
            }

            ms = new MenuSupport(repo);
            saveLocation = path;
            MenuController();
        }

        public void MenuController()
        {
            bool run = true;

            while (run)
            {
                run = Show();
            }

        }

        public bool Show()
        {
            Console.Clear();
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Show albums");
            Console.WriteLine("2. Add an album");
            Console.WriteLine("3. Change an album");
            Console.WriteLine("4. Delete an album");
            Console.WriteLine("5. Save");
            Console.WriteLine("6. Quit");
            Console.Write("\r\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    ms.ShowAlbums();
                    return true;

                case "2":
                    ms.AddAlbum();
                    return true;

                case "3":
                    ms.UpdateAlbum();
                    return true;

                case "4":
                    ms.DeleteAlbum();
                    return true;

                case "5":
                    ms.SaveAlbum(saveLocation);
                    return true;

                default:
                    return false;
            }
        }
    }
}

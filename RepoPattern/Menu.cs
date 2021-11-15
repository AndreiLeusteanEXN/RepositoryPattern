using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoPattern
{
    public class Menu
    {
        IRepository repo;
        MenuSupport ms;
        string saveLocation = @"..\..\..\salvation.csv";
        //IRepository repo = new Repository(this.path);
        //repo.Save(path);
        public Menu(string path)
        {
            repo = new Repository(path);
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
                    //TODO: Validate input
                    ms.AddAlbum();
                    return true;
                case "3":
                    //TODO: Validate input
                    ms.UpdateAlbum();
                    return true;
                case "4":
                    ms.DeleteAlbum();
                    return true;
                case "5":
                    ms.SaveAlbum(saveLocation);
                    return true;
                default:
                    //save upon exit
                    repo.Save(saveLocation);
                    return false;
            }

        }

    }
}

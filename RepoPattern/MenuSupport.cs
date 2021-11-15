using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoPattern
{
    public class MenuSupport
    {
        IRepository repo;
        public MenuSupport(IRepository repo)
        {
            this.repo = repo;
        }
        public void ShowAlbums()
        {
            bool showAlbums = true;
            while (showAlbums)
            {
                Console.Clear();
                Console.WriteLine("1. Show all albums");
                Console.WriteLine("2. Show albums belonging to an artist");
                Console.WriteLine("3. Show albums from a certain year");
                Console.WriteLine("4. Show all albums from a genre");
                Console.WriteLine("5. Show albums from a record label");
                Console.WriteLine("6. Show albums by ownership status");
                Console.WriteLine("7. Back to main menu");
                Console.Write("\r\nSelect an option: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        repo.GetAll();
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("Insert the artist of interest:");
                        string artist = Console.ReadLine();
                        repo.GetByArtist(artist);
                        break;
                    case "3":
                        Console.WriteLine("Insert the year of interest:");
                        int year = int.Parse(Console.ReadLine());
                        repo.GetByYear(year);
                        break;
                    case "4":
                        Console.WriteLine("Insert the genre of interest:");
                        string genre = Console.ReadLine();
                        repo.GetByGenre(genre);
                        break;
                    case "5":
                        Console.WriteLine("Insert the record label of interest:");
                        string rl = Console.ReadLine();
                        repo.GetByRecordLabel(rl);
                        break;
                    case "6":
                        Console.WriteLine("Insert ownership status (true/false):");
                        bool status = bool.Parse(Console.ReadLine());
                        repo.GetByOwned(status);
                        break;
                    default:
                        Console.WriteLine("Returning to main menu");
                        showAlbums = false;
                        break;
                }
                //Inform the user to press a key
                Console.Write("\r\nPress any key to continue..");
                //Wait for user to input anything, otherwise the current screen would not be visible to the user due to the way the menu works
                Console.ReadLine();
            }
        }

        bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
        public void AddAlbum()
        {
            Console.Clear();
            bool insertionCheck = false, status = false ;
            string title = "";
            string artist = "";
            string genre = "";
            string rl = "";
            string userInput = "";
            int year = 0;
            int sales = 0;
            while (!insertionCheck)
            {
                //id,title,artist,year,genre,recordLabel,owned,sales
                Console.WriteLine("Album title:");
                title = Console.ReadLine();
                Console.WriteLine("Artist name:");
                artist = Console.ReadLine();
                Console.WriteLine("Year of release:");
                userInput = Console.ReadLine();
                while (IsDigitsOnly(userInput) == false && userInput!="")
                {
                    Console.WriteLine("Year of release:");
                    userInput = Console.ReadLine();
                }
                if (userInput!="")
                    year = int.Parse(userInput);
                Console.WriteLine("Genre:");
                genre = Console.ReadLine();
                Console.WriteLine("RecordLabel:");
                rl = Console.ReadLine();
                Console.WriteLine("Ownership status (true/false):");
                userInput = Console.ReadLine();
                while (userInput.ToLower()!="true" && userInput.ToLower()!="false" && userInput!="")
                {
                    Console.WriteLine("Ownership status (true/false):");
                    userInput = Console.ReadLine();
                }
                if (userInput!="")
                    status = bool.Parse(userInput);
                
                Console.WriteLine("Number of sales:");
                userInput = Console.ReadLine();
                while (IsDigitsOnly(userInput) == false && userInput != "")
                {
                    Console.WriteLine("Number of sales:");
                    userInput = Console.ReadLine();
                }
                if (userInput!="")
                    sales = int.Parse(userInput);

                Console.WriteLine($"Is this correct?(y/n)\n{title},{artist},{year},{genre},{rl},{status},{sales}");
                string check = Console.ReadLine();
                //while (check.ToLower()!="y" && check.ToLower()!="n")
                //{
                //    Console.WriteLine($"Is this correct?(y/n)\n{title},{artist},{year},{genre},{rl},{status},{sales}");
                //    check = Console.ReadLine();
                //}
                if (check == "y")
                {
                    insertionCheck = true;
                }
                else Console.WriteLine("Retriggering album insertion. Please add the information again\n\n\n");
            }

            repo.Insert(title, artist, year, genre, rl, status, sales);

            Console.WriteLine("Insertion completed successfully!");
            //Inform the user to press a key
            Console.Write("\r\nPress any key to continue..");
            //Wait for user to input anything, otherwise the current screen would not be visible to the user due to the way the menu works
            Console.ReadLine();
        }

        public void UpdateAlbum()
        {
            Console.Clear();
            int idToUpdate = 0;
            string userInput = "";///will be used to validate user input
            Console.WriteLine("ID of the album to be updated:");
            idToUpdate = int.Parse(Console.ReadLine());
            repo.GetById(idToUpdate);
            Console.WriteLine("Insert the value to update, or press enter to proceed to the next value");
            Console.WriteLine("Title:");
            string title = Console.ReadLine(); // no validation to do here
            Console.WriteLine("Artist:");
            string artist = Console.ReadLine(); // no validation to do here
            Console.WriteLine("Year:");
            userInput = Console.ReadLine();
            while (IsDigitsOnly(userInput) == false && userInput !="")
            {
                Console.WriteLine("Year:");
                userInput = Console.ReadLine();
            }
            int year = -1; // -1 means the year will not be modified
            if (userInput!="")
                year = int.Parse(userInput);
            Console.WriteLine("Genre:");
            string genre = Console.ReadLine(); // no validation to do here
            Console.WriteLine("Record Label:");
            string rl = Console.ReadLine(); // no validation to do here
            Console.WriteLine("Ownership status (true/false):");
            userInput = Console.ReadLine();
            while (userInput.ToLower()!="true" && userInput.ToLower()!="false")
            {
                Console.WriteLine("Ownership status (true/false):");
                userInput = Console.ReadLine();
            }
            bool status = bool.Parse(userInput);
            Console.WriteLine("Sales:");
            userInput = Console.ReadLine();
            while (IsDigitsOnly(userInput) == false && userInput != "")
            {
                Console.WriteLine("Year:");
                userInput = Console.ReadLine();
            }
            int sales = -1;
            if (userInput != "")
                sales = int.Parse(userInput);
            repo.Update(idToUpdate, title, artist, year, genre, rl, status, sales);
        }
        public void DeleteAlbum()
        {
            Console.Clear();
            int idToDelete = 0;
            Console.WriteLine("ID of the album to be deleted:");
            idToDelete = int.Parse(Console.ReadLine());
            repo.Delete(idToDelete);
            Console.WriteLine("The album has been deleted\nPress any key to return to the main menu...");
            Console.ReadLine();
        }

        public void SaveAlbum(string path)
        {
            Console.Clear();
            repo.Save(path);
            Console.WriteLine("Save completed successfully!\nPress any key to return to main menu..");
            Console.ReadLine();

        }
    }
}

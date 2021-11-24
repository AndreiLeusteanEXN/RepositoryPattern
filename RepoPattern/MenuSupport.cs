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
        public MenuSupport(Repository repo)
        {
            this.repo = repo;
        }

        void IterateQueryAndPrintAlbums(IEnumerable<Album> query)
        {
            foreach (var album in query)
            {
                Console.WriteLine(album.PrintString());
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

        int getValidNumberFromUserInput(string userInput, string inputRequestMessage)
        {
            int validNumber = -1;

            while (IsDigitsOnly(userInput) == false && userInput != "")
            {
                Console.WriteLine(inputRequestMessage);
                userInput = Console.ReadLine();
            }

            if (userInput == "")
            {
                return validNumber;
            }

            validNumber = int.Parse(userInput);
            return validNumber;
        }

        bool getValidBoolFromUserInput(string userInput, string inputRequestMessage)
        {
            bool validBool = false;

            //It is mandatory to add the ownership status
            while (userInput.ToLower() != "true" && userInput.ToLower() != "false")
            {
                Console.WriteLine("Ownership status (true/false):");
                userInput = Console.ReadLine();
            }

            validBool = bool.Parse(userInput);

            return validBool;
        }

        public void ShowAlbums()
        {
            bool showAlbums = true;

            while (showAlbums)
            {
                if (repo.GetAll().Count() == 0)
                {
                    showAlbums = false;
                    Console.WriteLine("There are no albums to show. Please add an album!\nPress enter to return to menu");
                    Console.ReadLine();
                    continue;
                }

                Console.Clear();
                Console.WriteLine("1. Show all albums");
                Console.WriteLine("2. Show albums belonging to an artist");
                Console.WriteLine("3. Show albums from a certain year");
                Console.WriteLine("4. Show all albums from a genre");
                Console.WriteLine("5. Show albums from a record label");
                Console.WriteLine("6. Show albums by ownership status");
                Console.WriteLine("7. Back to main menu");
                Console.Write("\r\nSelect an option: ");
                string userInput = "";

                switch (Console.ReadLine())
                {
                    case "1":
                        var queryAll = repo.GetAll();
                        IterateQueryAndPrintAlbums(queryAll);
                        break;

                    case "2":
                        Console.Clear();
                        Console.WriteLine("Insert the artist of interest:");
                        string artist = Console.ReadLine();
                        var queryArtist = repo.GetByArtist(artist);
                        IterateQueryAndPrintAlbums(queryArtist);
                        break;

                    case "3":
                        Console.WriteLine("Insert the year of interest:");
                        userInput = Console.ReadLine();
                        int year = getValidNumberFromUserInput(userInput, "Insert a valid year of interest:");
                        var queryYear = repo.GetByYear(year);
                        IterateQueryAndPrintAlbums(queryYear);
                        break;

                    case "4":
                        Console.WriteLine("Insert the genre of interest:");
                        string genre = Console.ReadLine();
                        var queryGenre = repo.GetByGenre(genre);
                        IterateQueryAndPrintAlbums(queryGenre);
                        break;

                    case "5":
                        Console.WriteLine("Insert the record label of interest:");
                        string recordLabel = Console.ReadLine();
                        var queryRecordLabel = repo.GetByRecordLabel(recordLabel);
                        IterateQueryAndPrintAlbums(queryRecordLabel);
                        break;

                    case "6":
                        Console.WriteLine("Insert ownership status (true/false):");
                        userInput = Console.ReadLine();
                        bool status = getValidBoolFromUserInput(userInput, "Insert ownership status(true / false):");
                        var queryOwned = repo.GetByOwned(status);
                        IterateQueryAndPrintAlbums(queryOwned);
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



        public void AddAlbum()
        {
            Console.Clear();
            var albumToInsert = new Album();
            bool insertionCheck = false;
            string userInput = "";

            while (!insertionCheck)
            {
                //id,title,artist,year,genre,recordLabel,owned,sales
                Console.WriteLine("Album title:");
                albumToInsert.Title = Console.ReadLine();

                Console.WriteLine("Artist name:");
                albumToInsert.Artist = Console.ReadLine();

                Console.WriteLine("Year of release:");
                userInput = Console.ReadLine();
                albumToInsert.Year = getValidNumberFromUserInput(userInput, "Year of release:");

                Console.WriteLine("Genre:");
                albumToInsert.Genre = Console.ReadLine();

                Console.WriteLine("RecordLabel:");
                albumToInsert.RecordLabel = Console.ReadLine();

                Console.WriteLine("Ownership status (true/false):");
                userInput = Console.ReadLine();
                albumToInsert.Owned = getValidBoolFromUserInput(userInput, "Ownership status (true/false):");

                Console.WriteLine("Number of sales:");
                userInput = Console.ReadLine();
                albumToInsert.Sales = getValidNumberFromUserInput(userInput, "Number of sales:");

                Console.WriteLine($"Is this correct?(y/n)\n{albumToInsert.NoIdString()}");
                string check = Console.ReadLine();


                if (check.ToLower() == "y")
                {
                    insertionCheck = true;
                }

                else
                {
                    Console.WriteLine("Retriggering album insertion. Please add the information again\n\n\n");
                }

            }

            repo.Insert(albumToInsert);

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
            userInput = Console.ReadLine();
            idToUpdate = getValidNumberFromUserInput(userInput, "ID of the album to be updated:");

            var idCheck = repo.GetById(idToUpdate);
            if (idCheck.Any())
            {
                var albumDTO = new AlbumDTO();

                Console.WriteLine("Insert the value to update, or press enter to proceed to the next value");
                Console.WriteLine("Title:");
                albumDTO.Title = Console.ReadLine(); // no validation to do here

                Console.WriteLine("Artist:");
                albumDTO.Artist = Console.ReadLine(); // no validation to do here

                Console.WriteLine("Year:");
                userInput = Console.ReadLine();
                albumDTO.Year = getValidNumberFromUserInput(userInput, "Year:");

                Console.WriteLine("Genre:");
                albumDTO.Genre = Console.ReadLine(); // no validation to do here
                
                Console.WriteLine("Record Label:");
                albumDTO.RecordLabel = Console.ReadLine(); // no validation to do here
                
                Console.WriteLine("Ownership status (true/false):");
                userInput = Console.ReadLine();
                albumDTO.Owned = getValidBoolFromUserInput(userInput, "Ownership status (true/false):");

                Console.WriteLine("Sales:");
                userInput = Console.ReadLine();
                albumDTO.Sales = getValidNumberFromUserInput(userInput, "Sales:");

                repo.Update(idToUpdate, albumDTO);
            }

            else
            {
                Console.WriteLine("No album found for given id!\nPress enter to return");
                Console.ReadLine();
            }
        }

        public void DeleteAlbum()
        {
            Console.Clear();
            int idToDelete = 0;
            Console.WriteLine("ID of the album to be deleted:");
            string userInput = Console.ReadLine();

            idToDelete = getValidNumberFromUserInput(userInput, "ID of the album to be updated:");
            var idCheck = repo.GetById(idToDelete);

            if (idCheck.Any())
            {
                idToDelete = int.Parse(Console.ReadLine());
                repo.Delete(idToDelete);
                Console.WriteLine("The album has been deleted\nPress any key to return to the main menu...");
                Console.ReadLine();
            }

            else
            {
                Console.WriteLine("No album found for given id!\nPress enter to return");
                Console.ReadLine();
            }
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

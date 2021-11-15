using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RepoPattern
{
    public class Repository : IRepository
    {

        private List<Album> albums;
        string header = "id,title,artist,year,genre,recordLabel,owned,sales";

        public Repository(string path)
        {
            albums = new List<Album>();
            //Warning: you are reading the code for the dumbest csv parser to ever exist
            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    StringBuilder sb = new();
                    string line=sr.ReadLine();
                    //the first line is the header, so we skip this one and move to the second line
                    while ((line=sr.ReadLine()) != null)
                    {
                        Album album = new();
                        int i = 0;
                        sb.Clear();
                        while (line[i]!=',')
                        {
                            sb.Append(line[i++]);
                        }
                        album.id = int.Parse(sb.ToString());
                        sb.Clear();

                        while (line[++i]!= ',')
                        {
                            sb.Append(line[i]);
                        }

                        album.title = sb.ToString();
                        sb.Clear();

                        while (line[++i] != ',')
                        {
                            sb.Append(line[i]);
                        }

                        album.artist = sb.ToString();
                        sb.Clear();

                        while (line[++i] != ',')
                        {
                            sb.Append(line[i]);
                        }

                        album.year = int.Parse(sb.ToString());
                        sb.Clear();

                        while (line[++i] != ',')
                        {
                            sb.Append(line[i]);
                        }

                        album.genre = sb.ToString();
                        sb.Clear();

                        while (line[++i] != ',')
                        {
                            sb.Append(line[i]);
                        }

                        album.recordLabel = sb.ToString();
                        sb.Clear();

                        while (line[++i] != ',')
                        {
                            sb.Append(line[i]);
                        }

                        album.owned = bool.Parse(sb.ToString());
                        sb.Clear();

                        //String indexing starts at 0, therefore this loop will get the entire number of sales
                        while (i<line.Length-1)
                        {
                            sb.Append(line[++i]);
                        }


                        album.sales = int.Parse(sb.ToString());

                        albums.Add(album);
                    }
                }
            }
        }

        void IRepository.Insert(string title, string artist, int year, string genre, string recordLabel, bool owned, int sales)
        {
            //insert a new song in the list - validation will be done at user input (MenuSupport class)
            //get the last id and increment it for the element to be inserted
            int id = albums.Last().id;
            Album a = new();
            a.id = ++id;
            a.title = title;
            a.artist = artist;
            a.year = year;
            a.genre = genre;
            a.recordLabel = recordLabel;
            a.owned = owned;
            a.sales = sales;
            albums.Add(a);
        }

        void IRepository.GetById(int id)
        {
            Album album = albums.Where(a => a.id == id).First();
            Console.WriteLine($"{album.id},{album.title},{album.artist},{album.year},{album.genre},{album.recordLabel},{album.owned},{album.sales}");
        }
        void IRepository.GetAll()
        {
            foreach (var album in albums)
            {
                Console.WriteLine($"{album.id},{album.title},{album.artist},{album.year},{album.genre},{album.recordLabel},{album.owned},{album.sales}");
            }
            //get. getby, get all. get shit and print to console
        }

        void IRepository.GetByArtist(string artist)
        {
            var query =
                from album in albums
                where album.artist == artist
                select album;
            foreach (var album in query)
            {
                Console.WriteLine($"{album.id},{album.title},{album.artist},{album.year},{album.genre},{album.recordLabel},{album.owned},{album.sales}");
            }
        }

        void IRepository.GetByYear(int year)
        {
            var query = albums.Where(a => a.year == year);
            foreach (var album in query)
            {
                Console.WriteLine($"{album.id},{album.title},{album.artist},{album.year},{album.genre},{album.recordLabel},{album.owned},{album.sales}");
            }
        }

        void IRepository.GetByGenre(string genre)
        {
            var query = albums.Where(a => a.genre == genre);
            foreach (var album in query)
            {
                Console.WriteLine($"{album.id},{album.title},{album.artist},{album.year},{album.genre},{album.recordLabel},{album.owned},{album.sales}");
            }
        }

        void IRepository.GetByRecordLabel(string rl)
        {
            var query = albums.Where(a => a.recordLabel == rl);
            foreach (var album in query)
            {
                Console.WriteLine($"{album.id},{album.title},{album.artist},{album.year},{album.genre},{album.recordLabel},{album.owned},{album.sales}");
            }
        }

        void IRepository.GetByOwned(bool owned)
        {
            var query = albums.Where(a => a.owned == owned);
            foreach (var album in query)
            {
                Console.WriteLine($"{album.id},{album.title},{album.artist},{album.year},{album.genre},{album.recordLabel},{album.owned},{album.sales}");
            }
        }
        void IRepository.Update(int id, string title, string artist, int year, string genre, string recordLabel, bool owned, int sales)
        {
            //modify an entry based on id
            Album albumReference = new();
            var query = albums.Where(a => a.id == id);
            foreach (var album in query)
            {
                albumReference = album;
                break;
            }
            if (title != "")
                albumReference.title = title;
            if (artist != "")
                albumReference.artist = artist;
            if (year != -1)
                albumReference.year = year;
            if (genre != "")
                albumReference.genre = genre;
            if (recordLabel != "")
            albumReference.recordLabel = recordLabel;
            albumReference.owned = owned;
            if (sales != -1)
                albumReference.sales = sales;
        }

        void IRepository.Delete(int id)
        {
            foreach (Album album in albums)
            {
                if (album.id==id)
                {
                    albums.Remove(album);
                    Console.WriteLine("Album deleted!");
                    break;
                }
            }
        }

        void IRepository.Save(string path)
        {
            //save memory content into the same file
            //StreamWriter constructor with 1 argument overwrites the file by default
            //string header = "id,title,artist,year,genre,recordLabel,owned,sales";
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(header);
                var lastAlbum = albums.Last();
                foreach (var album in albums)
                {
                    //There shall be no newline at the end of file
                    if (album == lastAlbum)
                    {
                        sw.Write($"{album.id},{album.title},{album.artist},{album.year},{album.genre},{album.recordLabel},{album.owned},{album.sales}");
                    }
                    else sw.WriteLine($"{album.id},{album.title},{album.artist},{album.year},{album.genre},{album.recordLabel},{album.owned},{album.sales}");
                }
            }
            Console.WriteLine(header);
            foreach (var album in albums)
            {
                Console.WriteLine($"{album.id},{album.title},{album.artist},{album.year},{album.genre},{album.recordLabel},{album.owned},{album.sales}");
            }

        }
    }
}

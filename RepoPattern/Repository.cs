using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoPattern
{
    public abstract class Repository : IRepository
    {
        //CRUD

        public List<Album> albums { get; set; }
        public const string header = "id,title,artist,year,genre,recordLabel,owned,sales";

        public void Insert(Album albumToInsert)
        {
            int id = albums.Last().Id;
            albumToInsert.Id = ++id;
            albums.Add(albumToInsert);
        }

        public IEnumerable<Album> GetById(int id)
        {
            var query = albums.Where(a => a.Id == id);
            return query;
        }

        public IEnumerable<Album> GetAll()
        {
            return albums.Where(x => x.Id%1==0);
        }

        public IEnumerable<Album> GetByArtist(string artist)
        {
            var query =
                from album in albums
                where album.Artist == artist
                select album;

            return query;
        }

        public IEnumerable<Album> GetByYear(int year)
        {
            var query = albums.Where(a => a.Year == year);
            return query;
        }

        public IEnumerable<Album> GetByGenre(string genre)
        {
            var query = albums.Where(a => a.Genre == genre);
            return query;
        }

        public IEnumerable<Album> GetByRecordLabel(string rl)
        {
            var query = albums.Where(a => a.RecordLabel == rl);
            return query;
        }

        public IEnumerable<Album> GetByOwned(bool owned)
        {
            var query = albums.Where(a => a.Owned == owned);
            return query;
        }

        //Existence of album is validated at user input
        public void Update(int id, AlbumDTO albumUpdateData)
        {
            Album albumReference = new();
            albumReference = albums.Where(a => a.Id == id).First();

            if (albumUpdateData.Title != "")
            {
                albumReference.Title = albumUpdateData.Title;
            }

            if (albumUpdateData.Artist != "")
            {
                albumReference.Artist = albumUpdateData.Artist;
            }

            if (albumUpdateData.Year != -1)
            {
                albumReference.Year = albumUpdateData.Year;
            }

            if (albumUpdateData.Genre != "")
            {
                albumReference.Genre = albumUpdateData.Genre;

            }

            if (albumUpdateData.RecordLabel != "")
            {
                albumReference.RecordLabel = albumUpdateData.RecordLabel;
            }

            albumReference.Owned = albumUpdateData.Owned;

            if (albumUpdateData.Sales != -1)
            {
                albumReference.Sales = albumUpdateData.Sales;
            }
        }

        //Validation is done at user input
        public void Delete(int id)
        {
            foreach (Album album in albums)
            {
                if (album.Id == id)
                {
                    albums.Remove(album);
                    Console.WriteLine("Album deleted!");
                    break;
                }
            }
        }

        public abstract void Save(string path);
    }
}

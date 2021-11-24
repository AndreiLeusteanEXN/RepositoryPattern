using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoPattern
{
    public interface IRepository
    {
        void Insert(Album albumToInsert);

        IEnumerable<Album> GetAll();

        IEnumerable<Album> GetById(int id);

        IEnumerable<Album> GetByArtist(string artist);

        IEnumerable<Album> GetByYear(int year);

        IEnumerable<Album> GetByGenre(string genre);

        IEnumerable<Album> GetByRecordLabel(string rl);

        IEnumerable<Album> GetByOwned(bool owned);

        void Update(int id, AlbumDTO albumUpdateData);

        void Delete(int id);

        public abstract void Save(string path);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoPattern
{
    public interface IRepository
    {
        //CRUD
        void Insert(string title, string artist, int year, string genre, string recordLabel, bool owned, int sales);
        void GetAll();

        void GetById(int id);
        void GetByArtist(string artist);
        void GetByYear(int year);

        void GetByGenre(string genre);

        void GetByRecordLabel(string rl);
        void GetByOwned(bool owned);


        void Update(int id, string title, string artist, int year, string genre, string recordLabel, bool owned, int sales);

        void Delete(int id);

        public void Save(string path);



    }
}

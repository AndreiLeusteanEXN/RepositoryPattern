using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoPattern
{
    public class AlbumDTO
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public string RecordLabel { get; set; }
        public bool Owned { get; set; }
        public int Sales { get; set; }
    }
}

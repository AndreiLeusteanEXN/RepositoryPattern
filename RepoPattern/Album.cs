using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RepoPattern
{
    [XmlType("album")]
    public class Album
    {
        [XmlElement("id")]
        public int Id { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("artist")]
        public string Artist { get; set; }

        [XmlElement("year")]
        public int Year { get; set; }

        [XmlElement("genre")]
        public string Genre { get; set; }

        [XmlElement("recordLabel")]
        public string RecordLabel { get; set; }

        [XmlElement("owned")]
        public bool Owned { get; set; }

        [XmlElement("sales")]
        public int Sales { get; set; }

        public  string NoIdString()
        {
            return $"{Title},{Artist},{Year},{Genre},{RecordLabel},{Owned},{Sales}";
        }

        public string PrintString()
        {
            return $"{Id},{Title},{Artist},{Year},{Genre},{RecordLabel},{Owned},{Sales}";
        }
    }
}

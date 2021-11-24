using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RepoPattern
{
    public class RepositoryXML :  Repository
    {
        public RepositoryXML(string path)
        {
            XmlRootAttribute xRoot = new XmlRootAttribute();
            var deserializer = new XmlSerializer(typeof(List<Album>), xRoot);
            xRoot.ElementName = "ArrayOfAlbum";
            xRoot.IsNullable = true;

            using var fs = new FileStream(path, FileMode.Open);
            {
                albums = (List<Album>)deserializer.Deserialize(fs);
            }

        }

        public override void Save(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Album>));
            StreamWriter sw = new StreamWriter(path);
            serializer.Serialize(sw, albums);
            sw.Close();
        }
    }
}

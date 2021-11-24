using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace RepoPattern
{
    public class RepositoryCSV :  Repository
    {

        string GetNextItemFromCsvLine(string csvLine, int itemStartOffset)
        {
            var sb = new StringBuilder();
            int offset = itemStartOffset;
            
            while (offset < csvLine.Length) 
            {
                if (csvLine[offset] == ',')
                {
                    break;
                }

                sb.Append(csvLine[offset++]);
            }

            string parsedItem = sb.ToString();
            return parsedItem;
        }
        public RepositoryCSV(string path)
        {
            albums = new List<Album>();

            //Warning: you are reading the code for the dumbest csv parser to ever exist
            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    //StringBuilder sb = new();
                    string line=sr.ReadLine();
                    string csvItem = "";

                    //the first line is the header, so we skip this one and move to the second line
                    while ((line=sr.ReadLine()) != null)
                    {
                        Album album = new();
                        int lineIndex = 0;

                        csvItem = GetNextItemFromCsvLine(line, lineIndex);
                        lineIndex += csvItem.Length + 1;
                        album.Id = int.Parse(csvItem);

                        csvItem = GetNextItemFromCsvLine(line, lineIndex);
                        lineIndex += csvItem.Length + 1;
                        album.Title = csvItem;

                        csvItem = GetNextItemFromCsvLine(line, lineIndex);
                        lineIndex += csvItem.Length + 1;
                        album.Artist = csvItem;

                        csvItem = GetNextItemFromCsvLine(line, lineIndex);
                        lineIndex += csvItem.Length + 1;
                        album.Year = int.Parse(csvItem);

                        csvItem = GetNextItemFromCsvLine(line, lineIndex);
                        lineIndex += csvItem.Length + 1;
                        album.Genre = csvItem;

                        csvItem = GetNextItemFromCsvLine(line, lineIndex);
                        lineIndex += csvItem.Length + 1;
                        album.RecordLabel = csvItem;

                        csvItem = GetNextItemFromCsvLine(line, lineIndex);
                        lineIndex += csvItem.Length + 1;
                        album.Owned = bool.Parse(csvItem);

                        csvItem = GetNextItemFromCsvLine(line, lineIndex);
                        lineIndex += csvItem.Length + 1;
                        album.Sales = int.Parse(csvItem);

                        albums.Add(album);
                    }
                }
            }
        }


        public override void Save(string path)
        {
            //save memory content into the same file
            //StreamWriter constructor with 1 argument overwrites the file by default
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(header);
                var lastAlbum = albums.Last();

                foreach (var album in albums)
                {

                    //There shall be no newline at the end of file
                    if (album == lastAlbum)
                    {
                        sw.Write(album.PrintString());
                    }

                    else
                    {
                        sw.WriteLine(album.PrintString());
                    }
                }
            }

            Console.WriteLine(header);
            foreach (var album in albums)
            {
                Console.WriteLine(album.PrintString());
            }

        }

    }
}

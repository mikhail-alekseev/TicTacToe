using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;

namespace TicTacToe
{
    public class Localization
    {
        private readonly Dictionary<string, Dictionary<string, string>> Loc = new Dictionary<string, Dictionary<string, string>>();

        public string Language { get; set; }


        public Localization(string filename, string language)
        {

            using (var reader = new StreamReader(filename))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                string[] header = csv.Context.HeaderRecord;

                for (int i = 1; i < header.Length; i++)
                {
                    string lang = header[i];
                    Loc.Add(lang, new Dictionary<string, string>());
                }

                while (csv.Read())
                {
                    string id = csv.GetField(header[0]);

                    for (int i = 1; i < header.Length; i++)
                    {
                        string lang = header[i];

                        string translation = csv.GetField(lang);

                        Loc[lang][id] = translation;
                    }
                }

            }
            Language = language;
        }

        public string GetString(string key)
        {
            return Loc[Language][key];
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ScrubScores
{
    public class Scrubber
    {
        private Dictionary<string, string> regExPatternMap;

        public Scrubber()
        {
            regExPatternMap = new Dictionary<string, string>
                                  {
                                      {"(<[^>]*Score>).*(</[^>]*Score>)", "$1** REMOVED **$2"},
                                      {"(Score)=\"[^\"]*\"", "$1=\"** REMOVED **\""}
                                  };
        }

        public bool ScrubDirectoryPath(string directoryPath)
        {
            bool success = true;
            var files = Directory.GetFiles(directoryPath);
            foreach (var file in files)
            {
                try
                {
                    this.ScrubFile(file);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error processing file {0}", file);
                    Console.WriteLine(ex);
                    success = false;
                    continue;
                }
            }
            return success;
        }

        private void ScrubFile(string filePath)
        {
            var fileContents = File.ReadAllText(filePath);
            regExPatternMap.Keys.ToList()
                .ForEach(key => fileContents = Regex.Replace(fileContents, key, regExPatternMap[key]));
            File.WriteAllText(filePath, fileContents);
        }
    }
}

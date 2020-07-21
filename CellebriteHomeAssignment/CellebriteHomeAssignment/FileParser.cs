using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CellebriteHomeAssignment
{
    public static class FileParser
    {
        private const int sectionFieldLength = 4;
        private const int lengthOfIDField = 4;
        private const int lengthOfValueFieldLength = 5;

        /// <summary>
        /// Returns dictionary from the "section" (which is the 4 chars at the begining of each line) to list of (id, value) pairs. 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<string, IEnumerable<KeyValuePair<string, string>>>> Parse(string filePath)
        {
            StreamReader reader = File.OpenText(filePath);
            var sectionTypeToValuePairs = new List<KeyValuePair<string, IEnumerable<KeyValuePair<string, string>>>>();
            SectionReader section;
            while ((section = SectionReader.NewSection(reader.ReadLine())) != null)
            {
                var newSectionTypeToValues = section.Read(sectionFieldLength, lengthOfIDField, lengthOfValueFieldLength);

                var sectionTypeToValues = sectionTypeToValuePairs.SingleOrDefault(sectionTypeToValues => sectionTypeToValues.Key.Equals(newSectionTypeToValues.Key));
                if (!sectionTypeToValues.Equals(default(KeyValuePair<string, IEnumerable<KeyValuePair<string, string>>>)))
                {
                    sectionTypeToValues.Value.ToList().AddRange(newSectionTypeToValues.Value);
                }
                else
                {
                    sectionTypeToValuePairs.Add(newSectionTypeToValues);
                }
            }

            return sectionTypeToValuePairs;
        }
    }
}
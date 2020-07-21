using System;
using System.Collections.Generic;

namespace CellebriteHomeAssignment
{
    public class SectionReader
    {
        private string line;
        private int index;
        private SectionReader(string line)
        {
            this.line = line;
            index = 0;
        }

        public KeyValuePair<string, IEnumerable<KeyValuePair<string, string>>> Read(int sectionFieldLength,int lengthOfIDField, int lengthOfValueFieldLength)
        {
            return new KeyValuePair<string, IEnumerable<KeyValuePair<string, string>>>(ReadChars(sectionFieldLength),ReadArray(lengthOfIDField, lengthOfValueFieldLength));
        }
        
        /// <summary>
        /// Reads array from line in the following format: ID(contant length),ValueFieldLength(contant length),Value(dynamic length) etc.
        /// </summary>
        /// <param name="lengthOfIDField"></param>
        /// <returns>(ID,Value) pairs</returns>
        private IEnumerable<KeyValuePair<string, string>> ReadArray(int lengthOfIDField, int lengthOfValueFieldLength)
        {
            var result = new List<KeyValuePair<string, string>>();
            while (index != line.Length)
                result.Add(new KeyValuePair<string, string>(
                    ReadChars(lengthOfIDField),
                    // Reads the ValueFieldLength field, converts hex to decimal, than reads the value field.
                    ReadChars(int.Parse(ReadChars(lengthOfValueFieldLength), System.Globalization.NumberStyles.HexNumber))
                ));

            return result;
        }

        private string ReadChars(int length)
        {
            var read = line.Substring(index, length);
            index += length;
            return read;
        }

        public static SectionReader NewSection(string line)
        {
            if (line == null)
            {
                return null;
            }
            else return new SectionReader(line);
        }
    }
}
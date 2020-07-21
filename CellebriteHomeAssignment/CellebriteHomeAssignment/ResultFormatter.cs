using System;
using System.Collections.Generic;
using System.Linq;

namespace CellebriteHomeAssignment
{
    public static class ParsedFileToContactsFormatter
    {
        private static Dictionary<string, string> sectionTranslation = new Dictionary<string, string>()
                        {
                            {"86B7", "first name"},
                            {"9E60", "last name" },
                            {"5159", "phone" },
                            {"D812", "time"},
                            {"6704", "photo" }
                        };
        
        /// <returns>List of contacts which every contact is a list of properties</returns>
        public static IEnumerable<IEnumerable<KeyValuePair<string, string>>> Format(IEnumerable<KeyValuePair<string, IEnumerable<KeyValuePair<string, string>>>> sectionTypeToValuePairs)
        {
            var ids = sectionTypeToValuePairs.Select(sectionTypeToValue => sectionTypeToValue.Value).SelectMany(x => x).Select(idToValue => idToValue.Key).Distinct();
            
            var contacts = ids.Select(id =>
                sectionTypeToValuePairs.Select(section =>
                new KeyValuePair<string, string> (sectionTranslation[section.Key], GetValues(section, id, sectionTranslation)))
            );

            return contacts;
        }

        /// <returns>The values of an ID in section </returns>
        private static string GetValues(KeyValuePair<string, IEnumerable<KeyValuePair<string, string>>> section, string id, Dictionary<string, string> fields)
        {
            var values = section.Value.ToList().Where(kv => kv.Key.Equals(id)).Select(kv => kv.Value);
            if (fields[section.Key].Equals("time"))
            {
                values = values.ToList().Select(time => DateTimeOffset.FromUnixTimeSeconds(long.Parse(time)).DateTime.AddHours(3).ToString(@"yyyy-MM-dd HH:mm:ss"));
            }

            return string.Join(", ", values);
        }
    }
}
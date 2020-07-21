using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CellebriteHomeAssignment
{
    public static class HtmlDispatcher
    {
        
        public static void Publish(string dispatchFilePath, IEnumerable<IEnumerable<KeyValuePair<string, string>>> contacts)
        {
            var contactsInHtml = contacts.Select(contact => WriteContactAsHtml(contact));

            var htmlResult =
                "<!DOCTYPE html>" +
                "<html>" +
                "<body>" +
                $"{string.Join("\n", contactsInHtml)}" +
                "</body>" +
                "</html>";

            File.WriteAllText(dispatchFilePath, htmlResult);
        }

        private static string WriteContactAsHtml(IEnumerable<KeyValuePair<string, string>> contact)
        {
            var contactPropertiesHtml = contact.ToList().Select(property =>
            {
                if (!property.Key.Equals("photo"))
                {
                    return $"<b>{property.Key} :</b> {property.Value} <br>";
                }
                else
                {
                    var propertyString = $"<b>{property.Key} :</b> <br>";
                    if (!property.Value.Equals(string.Empty))
                        propertyString += $"<img src=\"data: image/png; base64,{property.Value}\">";
                    return propertyString;
                }
            });

            return
                "<div style=\"background-color:lightgray;padding:15px;border:15px solid white;\"> \n" +
                $"{string.Join("\n", contactPropertiesHtml)}" +
                "</div>";
        }
    }
}
namespace CellebriteHomeAssignment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var ContactsFilePath = "/Users/yarintamam/Desktop/Home interview Cellebrite/ex_v8";
            var DispatchFilePath = "/Users/yarintamam/Desktop/result.html";
            var parsedFile = FileParser.Parse(ContactsFilePath);
            var contacts = ParsedFileToContactsFormatter.Format(parsedFile);
            HtmlDispatcher.Publish(DispatchFilePath, contacts);
        }
    }
}
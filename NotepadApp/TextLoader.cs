using System.IO;

namespace NotepadApp
{
    public class TextLoader
    {
        private readonly string text;
        
        public TextLoader(TextReader reader)
        {
            text = reader.ReadToEnd();
        }

        public string getText()
        {
            return text;
        }
    }
}
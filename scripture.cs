using System;
using System.Collections.Generic;
using System.Linq;

namespace ScriptureMemorizer
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a Scripture object
            Scripture scripture = new Scripture("John 3:16", "For God so loved the world that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life.");

            // Display the initial scripture
            DisplayScripture(scripture);

            // Prompt the user and hide words until all words are hidden or the user quits
            while (!scripture.IsFullyHidden())
            {
                Console.WriteLine("Press Enter to continue or type 'quit' to exit:");
                string input = Console.ReadLine();

                if (input.ToLower() == "quit")
                    break;

                // Hide a few random words in the scripture
                scripture.HideRandomWords(3);

                // Clear the console and display the updated scripture
                Console.Clear();
                DisplayScripture(scripture);
            }
        }

        static void DisplayScripture(Scripture scripture)
        {
            Console.WriteLine($"Scripture: {scripture.Reference}");
            Console.WriteLine(scripture.GetVisibleText());
        }
    }

    class Scripture
    {
        private string reference;
        private string text;
        private List<Word> words;

        public string Reference
        {
            get { return reference; }
        }

        public Scripture(string reference, string text)
        {
            this.reference = reference;
            this.text = text;
            this.words = text.Split(' ').Select(w => new Word(w)).ToList();
        }

        public void HideRandomWords(int count)
        {
            Random random = new Random();

            for (int i = 0; i < count; i++)
            {
                // Find a random visible word and hide it
                List<Word> visibleWords = words.Where(w => !w.IsHidden).ToList();

                if (visibleWords.Count > 0)
                {
                    int index = random.Next(visibleWords.Count);
                    visibleWords[index].Hide();
                }
                else
                {
                    // All words are already hidden, break the loop
                    break;
                }
            }
        }

        public string GetVisibleText()
        {
            return string.Join(" ", words.Select(w => w.IsHidden ? "_____" : w.Text));
        }

        public bool IsFullyHidden()
        {
            return words.All(w => w.IsHidden);
        }
    }

    class Word
    {
        private string text;
        private bool isHidden;

        public string Text
        {
            get { return text; }
        }

        public bool IsHidden
        {
            get { return isHidden; }
        }

        public Word(string text)
        {
            this.text = text;
            this.isHidden = false;
        }

        public void Hide()
        {
            isHidden = true;
        }
    }
}

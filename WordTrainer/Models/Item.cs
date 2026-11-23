using System;
using System.Collections.Generic;
using System.Text;

namespace WordTrainer.Models
{
    public class Item
    {
        private string _text = "";
        private string _translation = "";
        private string[] _categories = Array.Empty<string>();

        public string Text
        {
            get => _text;
            set
            {
                if(string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("value");
                _text = value.Trim();
            }
        }

        public string Translation
        {
            get => _translation;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("value");
                _translation = value.Trim();
            }
        }

        public string[] Categories
        {
            get => _categories;
            set
            {
                if (value == null || value.Length == 0)
                    throw new ArgumentNullException("value");
                _categories = value;
            }
        }

        public string Category => string.Join(" ", Categories);
        public Item() { }
        public override string ToString() => $"{Text} - {Translation}";

        public Item(string text, string translation, string[] categories)
        {
            Text = text ;
            Translation = translation ;
            Categories = categories ;
        }
    }
}

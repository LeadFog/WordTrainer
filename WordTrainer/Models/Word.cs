using System;
using System.Collections.Generic;
using System.Text;

namespace WordTrainer.Models
{
    public class Word
    {
        private string _text = "";
        private string _translation = "";
        private string _category = "";

        public string Text
        {
            get => _text;
            set
            {
                if(string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Word is white space");
                _text = value.Trim(); //trim убирает пробелы и символы в начале\конце строки
            }
        }

        public string Translation
        {
            get => _translation;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Word is white space");
                _translation = value.Trim(); 
            }
        }

        public string Category
        {
            get => _category;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Word is white space");
                _category = value.Trim(); 
            }
        }

        public Word() { }

        public Word(string text, string translation, string category)
        {
            Text = text;
            Translation = translation;
            Category = category;
        }

        public override string ToString() => $"{Text} - {Translation}";
    }
}

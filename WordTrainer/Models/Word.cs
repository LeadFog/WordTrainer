using System;
using System.Collections.Generic;
using System.Text;

namespace WordTrainer.Models
{
    public class Word : Item
    {
        public Word() { }
        
        public static string ValidateCategory(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
                throw new ArgumentNullException("sdsfd");
            return category.Trim();
        }

        public Word(string text, string translation, string category)
            : base(text, translation, new[] { ValidateCategory(category)}) { }

    }
}

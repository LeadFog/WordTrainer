using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace WordTrainer.Models
{
    public class Phrase : Item
    {
        public Phrase() { }

        public static string[] CombineCategory(string[] first, string[] second)
        {
            return first.Concat(second).Distinct().ToArray();
        }

        public Phrase(string text, string translation, string[] category)
            : base(text, translation, category) { }

        public Phrase(Word first, Word second)
            : base($"{first.Text} {second.Text}", $"{first.Translation} {second.Translation}", CombineCategory(first.Categories, second.Categories)) { }
    }
}

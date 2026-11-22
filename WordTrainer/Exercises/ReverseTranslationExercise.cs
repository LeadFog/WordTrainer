using System;
using System.Collections.Generic;
using System.Text;
using WordTrainer.Models;

namespace WordTrainer.Exercises
{
    internal class ReverseTranslationExercise : Exercise
    {
        public ReverseTranslationExercise(Word word, IEnumerable<string> options) : base(word, options) { }

        public override string Question => $"Translate to English: {Word.Translation}";
        protected override string CorrectAnswer => Word.Text;
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using WordTrainer.Models;

namespace WordTrainer.Exercises
{
    internal class TranslationExercise : Exercise
    {
        public TranslationExercise(Word word, IEnumerable<string> options) : base(word, options) { }

        public override string Question => $"Translate to Russian: {Word.Text}";
        protected override string CorrectAnswer => Word.Translation;
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using WordTrainer.Models;

namespace WordTrainer.Exercises
{
    public static class ExerciseFactory
    {
        private static readonly Random Rnd = new();

        public static Exercise Create(Item word, IReadOnlyList<Item> allWords, int optionsCount = 4)
        {
            bool reverse = Rnd.Next(2) == 0;

            var pool = allWords.Where(w => w != word && w.Category == word.Category).ToList();
            if (pool.Count < optionsCount - 1)
                pool = allWords.Where(w => w != word).ToList();

            var wrong = pool.OrderBy(_ => Rnd.Next()).Take(optionsCount - 1).Select(w => reverse ? w.Text : w.Translation).ToList();
            string correct = reverse ? word.Text : word.Translation;
            var options = wrong.Append(correct).Distinct().OrderBy(_ => Rnd.Next()).ToList();

            return reverse ? new ReverseTranslationExercise(word, options) : new TranslationExercise(word, options);
        }
    }
}

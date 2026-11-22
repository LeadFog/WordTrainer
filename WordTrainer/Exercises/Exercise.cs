using System;
using System.Collections.Generic;
using System.Text;
using WordTrainer.Models;

namespace WordTrainer.Exercises
{
    public abstract class Exercise
    {
        private readonly List<string> _options;

        public Word Word { get; }
        public IReadOnlyList<string> Options => _options;

        public abstract string Question { get; }
        protected abstract string CorrectAnswer { get; }

        protected Exercise(Word word, IEnumerable<string> options)
        {
            Word = word ?? throw new ArgumentNullException(nameof(word));
            _options = options.ToList();
            if (_options.Count < 2)
                throw new ArgumentException("Need 2 answers or more");
        }

        public bool CheckAnswer(string answer) => string.Equals(answer?.Trim(), CorrectAnswer, StringComparison.OrdinalIgnoreCase);

        public string GetCorrectAnswer() => CorrectAnswer;

        public virtual string Hint => $"Category: {Word.Category}";
    }
}

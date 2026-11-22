using WordTrainer.Data;
using WordTrainer.Exercises;
using WordTrainer.Models;

namespace WordTrainer.Logic
{
    public class Trainer
    {
        private readonly IWordRepository _repository; 
        private readonly Random _random = new();
        private Exercise? _current;
        private Word? _lastWord;      
        private bool _isAnswered;     

        public Statistics Stats { get; } = new();
        public string CurrentCategory { get; private set; } = "";
        public Exercise? CurrentExercise => _current;

        public event EventHandler<ExerciseChangedEventArgs>? ExerciseChanged;
        public event EventHandler<AnswerCheckedEventArgs>? AnswerChecked;
        public event EventHandler? StatisticsChanged;

        public Trainer(IWordRepository repository) => _repository = repository;

        public IReadOnlyList<string> Categories => _repository.Categories;

        public void NextExercise()
        {
            var words = _repository.Words.Where(w => w.Category == CurrentCategory).ToList();
            if (words.Count < 2)
            {
                _current = null;
                _lastWord = null;
                throw new InvalidOperationException("В категории должно быть минимум 2 слова.");
            }

            var candidates = _lastWord != null
                ? words.Where(w => w != _lastWord).ToList()
                : words;

            var word = candidates[_random.Next(candidates.Count)];
            _lastWord = word;
            _isAnswered = false; 

            _current = ExerciseFactory.Create(word, _repository.Words);
            ExerciseChanged?.Invoke(this, new ExerciseChangedEventArgs(_current));
        }

        public void SelectCategory(string category)
        {
            CurrentCategory = category;
            _lastWord = null; 
            NextExercise();
        }

        public void Answer(string userAnswer)
        {
            if (_current == null || _isAnswered) return;

            _isAnswered = true;
            bool isCorrect = _current.CheckAnswer(userAnswer);

            if (isCorrect) Stats.AddCorrect();
            else Stats.AddWrong();

            AnswerChecked?.Invoke(this, new AnswerCheckedEventArgs(isCorrect, userAnswer, _current.GetCorrectAnswer()));
            StatisticsChanged?.Invoke(this, EventArgs.Empty);
        }

        public void AddWord(Word word)
        {
            _repository.Add(word);
            _repository.Save();
        }

        public void SaveData() => _repository.Save();

        public void ResetStats()
        {
            Stats.Reset();
            StatisticsChanged?.Invoke(this, EventArgs.Empty);
        }
    }

}
}

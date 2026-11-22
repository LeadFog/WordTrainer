using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WordTrainer.Data;
using WordTrainer.Logic;
using WordTrainer.Models;

namespace WordTrainer.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private readonly Trainer _trainer;
    private string _question = "";
    private string _feedback = "";
    private string _stats = "";
    private string _selectedCategory = "";
    private bool _canAnswer = false;
    private bool _canNext = false;

    public ObservableCollection<string> Categories { get; } = new();
    public ObservableCollection<string> Options { get; } = new();

    public string Question
    {
        get => _question;
        set { _question = value; OnPropertyChanged(); }
    }

    public string Feedback
    {
        get => _feedback;
        set { _feedback = value; OnPropertyChanged(); }
    }

    public string Stats
    {
        get => _stats;
        set { _stats = value; OnPropertyChanged(); }
    }

    public string SelectedCategory
    {
        get => _selectedCategory;
        set
        {
            if (_selectedCategory != value)
            {
                _selectedCategory = value;
                OnPropertyChanged();
                _trainer.SelectCategory(value);
            }
        }
    }

    public bool CanAnswer
    {
        get => _canAnswer;
        set { _canAnswer = value; OnPropertyChanged(); AnswerCommand.RaiseCanExecuteChanged(); }
    }

    public bool CanNext
    {
        get => _canNext;
        set { _canNext = value; OnPropertyChanged(); NextCommand.RaiseCanExecuteChanged(); }
    }

    public RelayCommand AnswerCommand { get; }
    public RelayCommand NextCommand { get; }
    public RelayCommand AddWordCommand { get; }

    public event PropertyChangedEventHandler? PropertyChanged;

    public MainViewModel(Trainer trainer)
    {
        _trainer = trainer;

        _trainer.ExerciseChanged += OnExerciseChanged;
        _trainer.AnswerChecked += OnAnswerChecked;
        _trainer.StatisticsChanged += (s, e) => Stats = _trainer.Stats.ToString();

        AnswerCommand = new RelayCommand(OnAnswer, _ => CanAnswer);
        NextCommand = new RelayCommand(_ => _trainer.NextExercise(), _ => CanNext);
        AddWordCommand = new RelayCommand(OnAddWord);

        RefreshCategories();
        Stats = _trainer.Stats.ToString();
    }

    private void RefreshCategories()
    {
        Categories.Clear();
        foreach (var c in _trainer.Categories)
            Categories.Add(c);

        if (Categories.Count > 0)
            SelectedCategory = Categories[0];
    }

    private void OnExerciseChanged(object? sender, ExerciseChangedEventArgs e)
    {
        Question = e.Exercise.Question;
        Feedback = "";

        Options.Clear();
        foreach (var opt in e.Exercise.Options)
            Options.Add(opt);

        CanAnswer = false;
        CanNext = false;
    }

    private void OnAnswerChecked(object? sender, AnswerCheckedEventArgs e)
    {
        Feedback = e.IsCorrect
            ? $"✔ Верно!"
            : $"✘ Неверно. Правильный ответ: {e.CorrectAnswer}";

        CanAnswer = false;
        CanNext = true;
    }

    private string? _selectedOption;

    public void SelectOption(string option)
    {
        _selectedOption = option;
        CanAnswer = true;
    }

    private void OnAnswer(object? parameter)
    {
        if (_selectedOption != null)
            _trainer.Answer(_selectedOption);
    }

    private void OnAddWord(object? parameter)
    {
        System.Windows.MessageBox.Show("Диалог добавления слова (скоро)");
    }

    protected void OnPropertyChanged([CallerMemberName] string? name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public void SaveData() => _trainer.SaveData();
}
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WordTrainer.Models;

namespace WordTrainer.ViewModels;

public class AddWordViewModel : INotifyPropertyChanged
{
    private string _word = "";
    private string _translation = "";
    private string _selectedCategory = "";
    private string _selectedCategory2 = "";
    private bool _isPhrase;
    private bool _canOk;

    public bool IsConfirmed { get; private set; }

    public ObservableCollection<string> Categories { get; } = new();

    public string Word
    {
        get => _word;
        set { _word = value.Trim(); OnPropertyChanged(); UpdateCanOk(); }
    }

    public string Translation
    {
        get => _translation;
        set { _translation = value.Trim(); OnPropertyChanged(); UpdateCanOk(); }
    }

    public string SelectedCategory
    {
        get => _selectedCategory;
        set { _selectedCategory = value; OnPropertyChanged(); UpdateCanOk(); }
    }

    public string SelectedCategory2
    {
        get => _selectedCategory2;
        set { _selectedCategory2 = value; OnPropertyChanged(); UpdateCanOk(); }
    }

    public bool IsPhrase
    {
        get => _isPhrase;
        set { _isPhrase = value; OnPropertyChanged(); UpdateCanOk(); }
    }

    public bool CanOk
    {
        get => _canOk;
        private set { _canOk = value; OnPropertyChanged(); OkCommand.RaiseCanExecuteChanged(); }
    }

    public RelayCommand OkCommand { get; }
    public RelayCommand CancelCommand { get; }

    public event PropertyChangedEventHandler? PropertyChanged;
    public event EventHandler? RequestClose;

    public AddWordViewModel(IEnumerable<string> categories)
    {
        OkCommand = new RelayCommand(
            _ => { IsConfirmed = true; RequestClose?.Invoke(this, EventArgs.Empty); },
            _ => CanOk);

        CancelCommand = new RelayCommand(
            _ => { IsConfirmed = false; RequestClose?.Invoke(this, EventArgs.Empty); });

        foreach (var c in categories.Where(c => !c.Contains(' ')))
            Categories.Add(c);

        if (Categories.Count > 0)
        {
            SelectedCategory = Categories[0];
            SelectedCategory2 = Categories.Count > 1 ? Categories[1] : Categories[0];
        }
    }


    public Item CreateItem()
    {
        if (IsPhrase)
        {
            string[] combined = new[] { SelectedCategory.Trim(), SelectedCategory2.Trim() }
                .Distinct()
                .ToArray();

            return new Phrase(Word, Translation, combined);
        }

        return new Word(Word, Translation, SelectedCategory);
    }

    private void UpdateCanOk()
    {
        bool baseValid = !string.IsNullOrWhiteSpace(Word)
                      && !string.IsNullOrWhiteSpace(Translation)
                      && !string.IsNullOrWhiteSpace(SelectedCategory);

        if (IsPhrase)
            CanOk = baseValid && !string.IsNullOrWhiteSpace(SelectedCategory2);
        else
            CanOk = baseValid;
    }

    protected void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
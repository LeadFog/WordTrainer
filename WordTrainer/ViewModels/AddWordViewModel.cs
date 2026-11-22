using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace WordTrainer.ViewModels;

public class AddWordViewModel : INotifyPropertyChanged
{
    private string _word = "";
    private string _translation = "";
    private string _selectedCategory = "";
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

        foreach (var c in categories) Categories.Add(c);
        if (Categories.Count > 0) SelectedCategory = Categories[0];
    }

    private void UpdateCanOk()
    {
        CanOk = !string.IsNullOrWhiteSpace(Word)
             && !string.IsNullOrWhiteSpace(Translation)
             && !string.IsNullOrWhiteSpace(SelectedCategory);
    }

    protected void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
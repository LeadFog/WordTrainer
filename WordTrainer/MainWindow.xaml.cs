using System.Windows;
using WordTrainer.Data;
using WordTrainer.Logic;
using WordTrainer.ViewModels;

namespace WordTrainer;

public partial class MainWindow : Window
{
    private readonly MainViewModel _viewModel;

    public MainWindow()
    {
        InitializeComponent();

        string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "words.json");
        IWordRepository repository = new JsonWordRepository(path);
        repository.Load();

        var trainer = new Trainer(repository);
        _viewModel = new MainViewModel(trainer);
        DataContext = _viewModel;

        Closing += (s, e) => _viewModel.SaveData();
    }

    private void OnOptionClick(object sender, RoutedEventArgs e)
    {
        if (sender is System.Windows.Controls.Button btn)
        {
            _viewModel.SelectOption(btn.Content.ToString()!);
        }
    }
}
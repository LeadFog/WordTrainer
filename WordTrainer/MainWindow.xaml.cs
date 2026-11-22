using System.Windows;
using WordTrainer.Data;
using WordTrainer.Logic;
using WordTrainer.Models;
using WordTrainer.ViewModels;

namespace WordTrainer;

public partial class MainWindow : Window
{
    private readonly MainViewModel _viewModel;
    private readonly Trainer _trainer;

    public MainWindow()
    {
        InitializeComponent();

        string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "words.json");
        IWordRepository repository = new JsonWordRepository(path);
        repository.Load();

        _trainer = new Trainer(repository);
        _viewModel = new MainViewModel(_trainer);
        DataContext = _viewModel;
        _viewModel.RequestAddWord += OnRequestAddWord;
        _viewModel.RequestShowMessage += OnRequestShowMessage;
        Closing += (s, e) => _viewModel.SaveData();
    }


    private void OnRequestAddWord(object? sender, EventArgs e)
    {
        var dialogVm = new AddWordViewModel(_trainer.Categories);
        var dialog = new AddWordWindow(dialogVm) { Owner = this };

        dialog.ShowDialog();

        if (dialogVm.IsConfirmed)
        {
            try
            {
                var word = new Word(
                    dialogVm.Word,
                    dialogVm.Translation,
                    dialogVm.SelectedCategory);

                _trainer.AddWord(word);
                _viewModel.RefreshAfterAdd();

                if (dialogVm.SelectedCategory == _viewModel.SelectedCategory &&
                    _trainer.CanSelectCategory(dialogVm.SelectedCategory))
                {
                    _trainer.SelectCategory(dialogVm.SelectedCategory);
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }

    private void OnRequestShowMessage(object? sender, string message)
    {
        MessageBox.Show(message, "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
    }
    private void OnOptionClick(object sender, RoutedEventArgs e)
    {
        if (sender is System.Windows.Controls.Button btn)
            _viewModel.SelectOption(btn.Content.ToString()!);
    }
}
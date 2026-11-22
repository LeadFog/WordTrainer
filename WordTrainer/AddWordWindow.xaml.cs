using System.Windows;
using WordTrainer.ViewModels;

namespace WordTrainer;

public partial class AddWordWindow : Window
{
    public AddWordWindow(AddWordViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;

        viewModel.RequestClose += (s, e) => Close();
    }
}
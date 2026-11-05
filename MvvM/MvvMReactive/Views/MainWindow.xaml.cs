using System.Windows;
using MvvMReactive.ViewModels;

namespace MvvMReactive.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new CustomerViewModel();
    }
}

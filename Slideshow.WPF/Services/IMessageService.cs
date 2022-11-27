using System.Windows;

namespace Slideshow.WPF.Services
{
    public interface IMessageService
    {
        void ShowDialog(string message);
        void ShowDialog(string message, string caption, MessageBoxButton messageBoxButton, MessageBoxImage messageBoxImage);

        MessageBoxResult Question(string message);
    }
}

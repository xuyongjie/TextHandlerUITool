using ITextHandle;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using TextHandleUITool.Load;
using TextHandleUITool.Save;

namespace TextHandleUITool
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<IHandler> Handlers { get; set; }
        private IHandler _selectedHandler;
        private ISave _save;
        private ILoader _loader;
        public MainWindow()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            Handlers = new ObservableCollection<IHandler>();
            LoadExtensions();
        }

        private void LoadExtensions()
        {
            var directory = Environment.CurrentDirectory;
            var files = Directory.GetFiles(directory, "*.dll");
            foreach (var file in files)
            {
                Assembly assembly = Assembly.LoadFile(file);
                var extensions = (from m in assembly.ExportedTypes where typeof(IHandler).IsAssignableFrom(m)&&!m.IsInterface select m).ToList();
                foreach(var extension in extensions)
                {
                    Handlers.Add((IHandler)extension.Assembly.CreateInstance(extension.FullName));
                }
            }
        }
        private void ActionTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedHandler = (IHandler)(sender as ComboBox).SelectedItem;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ActionTypeComboBox.ItemsSource = Handlers;
            if(Handlers!=null&&Handlers.Count>0)
            {
                ActionTypeComboBox.SelectedIndex = 0;
            }
        }

        private void FileSelectButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.DefaultExt = ".txt";
            dialog.Filter = "Text documents(.txt)|*.txt";
            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                SelectedFileNameTextBlock.Text = dialog.FileName;
                _loader = new TextFileLoader(dialog.FileName);
                ResourceTextBox.Text = _loader.load();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string text = ResultTextBox.Text;
            if (!string.IsNullOrEmpty(text))
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.FileName = "result" + DateTime.Now.ToString("yyyyMMddHHmmss");
                dialog.DefaultExt = ".txt";
                dialog.Filter = "Text documents(.txt)|*.txt";
                bool? result = dialog.ShowDialog();
                if (result == true)
                {
                    _save = new SaveToFile(dialog.FileName);
                    _save.Save(ResultTextBox.Text);
                }
            }
        }

        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedHandler != null)
            {
                ResultTextBox.Text = _selectedHandler.Handle(ResourceTextBox.Text);
            }
        }
    }
}

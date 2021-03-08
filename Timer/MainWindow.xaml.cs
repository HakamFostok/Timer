using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Timer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null) =>
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));


        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args) =>
            PropertyChanged?.Invoke(this, args);

        #endregion

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            MidnightNotifier.DayChanged += (s, e) => RaisePropertyChanged(nameof(Days));
        }

        public double Days =>
            (new DateTime(2023, 1, 1).Date - DateTime.Now.Date).TotalDays;

        private void Button_Click(object sender, RoutedEventArgs e) =>
            RaisePropertyChanged(nameof(Days));

    }
}

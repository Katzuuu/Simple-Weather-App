using System.Windows;

namespace WpfApp1;

public partial class MainWindow : Window
{   
    public MainWindow()
    {
        InitializeComponent();
    }

    private async void DisplayData(string location)
    {
        if(string.IsNullOrWhiteSpace(location)) return;
        var data = await WeatherApi.GetWeatherDetails(location);
        Console.WriteLine(data.current.temp_c);
    }

    private void SearchBtn_OnClick(object sender, RoutedEventArgs e)
    {
        var inputValue = LocationInput.Text;
        DisplayData(LocationInput.Text);
    }
}
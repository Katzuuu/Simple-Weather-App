using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace WpfApp1;

public partial class MainWindow : Window
{   
    private bool _isAlertOpen;
    private string _previousLocation;
    public MainWindow()
    {
        InitializeComponent();
        _isAlertOpen = true;
        _previousLocation = "";
    }

    private void ShowXAMLElements()
    {
        if(!_isAlertOpen) return;
        
        AlertBorder.Visibility = Visibility.Hidden;
        InfoPanel.Visibility = Visibility.Visible;
        _isAlertOpen = false;
    }
    
    private async void DisplayData(WeatherDataSerializer data)
    {
        if(data == null) throw new Exception("data is null");
        ShowXAMLElements();
        
        LocationText.Text = data.location.name;
        TemperatureText.Text = Convert.ToString(data.current.temp_c);
        ConditionText.Text = data.current.condition.text;
        ConditionImg.Source = new BitmapImage(
            new Uri($"https:{data.current.condition.icon}", UriKind.Absolute)
        );
        
        Keyboard.ClearFocus();
    }
    
    private void LocationInput_OnGotFocus(object sender, RoutedEventArgs e)
    {
        if(LocationInput.Text != "Type your location") return;
        LocationInput.Text = "";
    }
    
    private void LocationInput_OnLostFocus(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(LocationInput.Text))
        {
            LocationInput.Text = "Type your location";
        }
    }

    private async Task<WeatherDataSerializer> GetData()
    {
        var location = LocationInput.Text;
        if(
            string.IsNullOrWhiteSpace(location) || 
            !Convert.ToBoolean(string.Compare(_previousLocation, location))
        ) return null;
        _previousLocation = location;
        var data = await WeatherApi.GetWeatherDetails(location);
        return data;
    }

    private async void SearchBtn_OnClick(object sender, RoutedEventArgs e)
    {
        WeatherDataSerializer data = await GetData();
        DisplayData(data);
    }

    private async void LocationInput_OnKeyDown(object sender, KeyEventArgs e)
    {
        if(e.Key != Key.Enter) return;
        WeatherDataSerializer data = await GetData();
        DisplayData(data);
    }
}
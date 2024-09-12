using MauiTestApp.Models;
using MauiTestApp.Services;

namespace MauiTestApp;

public partial class WeatherPage : ContentPage
{
    public List<Models.List> WeatherList;

    private double _latitude;
    private double _longitude;

    public WeatherPage()
    {
        InitializeComponent();
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await GetLocation();
        await GetWeatherDataByLocation(_latitude, _longitude);
    }

    public async Task GetLocation()
    {
        var location = await Geolocation.GetLocationAsync();
        _latitude = location.Latitude;
        _longitude = location.Longitude;
    }

    private async void TapLocation_Tapped(object sender, EventArgs e)
    {
        await GetLocation();
        await GetWeatherDataByLocation(_latitude, _longitude);
    }

    public async Task GetWeatherDataByLocation(double latitude, double longitude)
    {
        var result = await ApiService.GetWeather(_latitude, _longitude);

        UpdateUI(result);
    }

    public async Task GetWeatherDataByCityName(string cityName)
    {
        var result = await ApiService.GetWeatherByCity(cityName);

        UpdateUI(result);
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        var response = await DisplayPromptAsync(title: "", message: "", placeholder: "Search weather by city", accept: "Search", cancel: "Cancel");

        if (response != null)
        {
            await GetWeatherDataByCityName(response);
        }
    }

    public void UpdateUI(Root result)
    {
        WeatherList = new List<Models.List>();

        foreach (var item in result.List)
        {
            WeatherList.Add(item);
        }

        CvWeather.ItemsSource = WeatherList;

        LblCity.Text = result.City.Name;
        LblWeatherDescription.Text = result.List[0].Weather[0].Description;
        LblTemperature.Text = result.List[0].Main.Temperature + "°C";
        LblHumidity.Text = result.List[0].Main.Humidity + "%";
        LblWind.Text = Math.Round(result.List[0].Wind.Speed) + " m/s";
        ImgWeatherIcon.Source = result.List[0].Weather[0].CustomIcon;
    }

    private async void GetWeatherInRandomCityButton_Clicked(object sender, EventArgs e)
    {
        var result = await ApiService.GetWeatherByCity(RandomCity.PickRandomCity());

        UpdateUI(result);
    }
}
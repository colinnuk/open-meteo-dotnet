using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenMeteo;

namespace OpenMeteoTests
{
    [TestClass]
    public class UrlFactoryTests
    {
        [TestMethod]
        public void GetUrlWithOptions_WeatherForecastOptions_Test()
        {
            var factory = new UrlFactory();
            var url = factory.GetUrlWithOptions(GetWeatherForecastOptions());

            var expectedUrl = "https://api.open-meteo.com:443/v1/forecast?latitude=40.7128&longitude=-74.006&temperature_unit=celsius&windspeed_unit=kmh&precipitation_unit=mm&timezone=America/New_York&timeformat=iso8601&past_days=2&start_date=2023-01-01&end_date=2023-01-02&hourly=temperature_2m,windspeed_10m&daily=temperature_2m_max,temperature_2m_min&cell_selection=nearest&models=gfs_hrrr,gfs_global&current=temperature_2m&minutely_15=precipitation";
            Assert.AreEqual(expectedUrl, url);
        }

        [TestMethod]
        public void GetUrlWithOptions_GeocodingOptions_Test()
        {
            var factory = new UrlFactory();
            var url = factory.GetUrlWithOptions(GetGeocodingOptions());

            var expectedUrl = "https://geocoding-api.open-meteo.com:443/v1/search?name=New York&count=100&format=json&language=en";
            Assert.AreEqual(expectedUrl, url);
        }

        [TestMethod]
        public void GetUrlWithOptions_AirQualityOptions_Test()
        {
            var factory = new UrlFactory();
            var url = factory.GetUrlWithOptions(GetAirQualityOptions());

            var expectedUrl = "https://air-quality-api.open-meteo.com:443/v1/air-quality?latitude=40.7128&longitude=-74.006&domains=global&timeformat=iso8601&timezone=America/New_York&hourly=pm10,pm2_5";
            Assert.AreEqual(expectedUrl, url);
        }

        [TestMethod]
        public void GetUrlWithOptions_ElevationOptions_Test()
        {
            var factory = new UrlFactory();
            var url = factory.GetUrlWithOptions(GetElevationOptions());

            var expectedUrl = "https://api.open-meteo.com:443/v1/elevation?latitude=40.7128&longitude=-74.006";
            Assert.AreEqual(expectedUrl, url);
        }

        [TestMethod]
        public void GetUrlWithOptions_WeatherForecastOptions_WithApiKey_Test()
        {
            var factory = new UrlFactory("testApiKey");
            var url = factory.GetUrlWithOptions(GetWeatherForecastOptions());

            var expectedUrl = "https://customer-api.open-meteo.com:443/v1/forecast?latitude=40.7128&longitude=-74.006&temperature_unit=celsius&windspeed_unit=kmh&precipitation_unit=mm&timezone=America/New_York&timeformat=iso8601&past_days=2&start_date=2023-01-01&end_date=2023-01-02&hourly=temperature_2m,windspeed_10m&daily=temperature_2m_max,temperature_2m_min&cell_selection=nearest&models=gfs_hrrr,gfs_global&current=temperature_2m&minutely_15=precipitation&apikey=testApiKey";
            Assert.AreEqual(expectedUrl, url);
        }

        [TestMethod]
        public void GetUrlWithOptions_GeocodingOptions_WithApiKey_Test()
        {
            var factory = new UrlFactory("testApiKey");
            var url = factory.GetUrlWithOptions(GetGeocodingOptions());

            var expectedUrl = "https://customer-geocoding-api.open-meteo.com:443/v1/search?name=New York&count=100&format=json&language=en&apikey=testApiKey";
            Assert.AreEqual(expectedUrl, url);
        }

        [TestMethod]
        public void GetUrlWithOptions_AirQualityOptions_WithApiKey_Test()
        {
            var factory = new UrlFactory("testApiKey");
            var url = factory.GetUrlWithOptions(GetAirQualityOptions());

            var expectedUrl = "https://customer-air-quality-api.open-meteo.com:443/v1/air-quality?latitude=40.7128&longitude=-74.006&domains=global&timeformat=iso8601&timezone=America/New_York&hourly=pm10,pm2_5&apikey=testApiKey";
            Assert.AreEqual(expectedUrl, url);
        }

        [TestMethod]
        public void GetUrlWithOptions_ElevationOptions_WithApiKey_Test()
        {
            var factory = new UrlFactory("testApiKey");
            var url = factory.GetUrlWithOptions(GetElevationOptions());

            var expectedUrl = "https://customer-api.open-meteo.com:443/v1/elevation?latitude=40.7128&longitude=-74.006&apikey=testApiKey";
            Assert.AreEqual(expectedUrl, url);
        }

        private static WeatherForecastOptions GetWeatherForecastOptions() => new()
        {
            Latitude = 40.7128f,
            Longitude = -74.006f,
            Temperature_Unit = TemperatureUnitType.celsius,
            Windspeed_Unit = WindspeedUnitType.kmh,
            Precipitation_Unit = PrecipitationUnitType.mm,
            Timezone = "America/New_York",
            Timeformat = TimeformatType.iso8601,
            Past_Days = 2,
            Start_date = "2023-01-01",
            End_date = "2023-01-02",
            Hourly = new HourlyOptions([HourlyOptionsParameter.temperature_2m, HourlyOptionsParameter.windspeed_10m]),
            Daily = new DailyOptions([DailyOptionsParameter.temperature_2m_max, DailyOptionsParameter.temperature_2m_min]),
            Cell_Selection = CellSelectionType.nearest,
            Models = new WeatherModelOptions([WeatherModelOptionsParameter.gfs_hrrr, WeatherModelOptionsParameter.gfs_global]),
            Current = new CurrentOptions([CurrentOptionsParameter.temperature_2m]),
            Minutely15 = new Minutely15Options([Minutely15OptionsParameter.precipitation])
        };

        private static GeocodingOptions GetGeocodingOptions() => new("New York");

        private static AirQualityOptions GetAirQualityOptions() => new()
        {
            Latitude = 40.7128f,
            Longitude = -74.006f,
            Domains = "global",
            Timeformat = "iso8601",
            Timezone = "America/New_York",
            Hourly = new AirQualityOptions.HourlyOptions([AirQualityOptions.HourlyOptionsParameter.pm10, AirQualityOptions.HourlyOptionsParameter.pm2_5])
        };

        private static ElevationOptions GetElevationOptions() => new(40.7128f, -74.006f);
    }
}

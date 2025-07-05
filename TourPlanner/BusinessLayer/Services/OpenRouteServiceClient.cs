using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System;

namespace TourPlanner.BusinessLayer.Services;

public class OpenRouteServiceClient
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public OpenRouteServiceClient(IConfiguration config)
    {
        _httpClient = new HttpClient();
        _apiKey = config["OpenRouteService:ApiKey"] ?? throw new Exception("API-Key not found in config.");
    }

    public async Task<string> GetRouteAsync(string from, string to)
    {
        var url = $"https://api.openrouteservice.org/v2/directions/driving-car?api_key={_apiKey}&start={from}&end={to}";
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }
    public async Task<(double Lon, double Lat)> GetCoordinatesAsync(string address)
    {
        var geocodeUrl = $"https://api.openrouteservice.org/geocode/search?api_key={_apiKey}&text={Uri.EscapeDataString(address)}";
        var response = await _httpClient.GetAsync(geocodeUrl);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadFromJsonAsync<GeocodeResponse>();
        if (json?.Features?.Length > 0)
        {
            var coords = json.Features[0].Geometry.Coordinates;
            return (coords[0], coords[1]);
        }

        throw new Exception("No coordinates found for the address.");
    }

    public class GeocodeResponse
    {
        public Feature[] Features { get; set; } = Array.Empty<Feature>();
    }

    public class Feature
    {
        public Geometry Geometry { get; set; } = new();
    }

    public class Geometry
    {
        public double[] Coordinates { get; set; } = Array.Empty<double>();
    }

}
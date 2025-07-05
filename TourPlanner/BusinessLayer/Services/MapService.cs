using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace TourPlanner.BusinessLayer.Services
{
    public static class MapService
    {
        public static string GenerateMapHtml((double Lon, double Lat) from, (double Lon, double Lat) to)
        {
            string fromLat = from.Lat.ToString(CultureInfo.InvariantCulture);
            string fromLon = from.Lon.ToString(CultureInfo.InvariantCulture);
            string toLat = to.Lat.ToString(CultureInfo.InvariantCulture);
            string toLon = to.Lon.ToString(CultureInfo.InvariantCulture);

            return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'/>
    <title>Route Map</title>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <link rel='stylesheet' href='https://unpkg.com/leaflet@1.9.4/dist/leaflet.css'/>
    <script src='https://unpkg.com/leaflet@1.9.4/dist/leaflet.js'></script>
    <style>
        html, body, #map {{
            height: 100%;
            margin: 0;
        }}
    </style>
</head>
<body>
    <div id='map'></div>
    <script>
        var map = L.map('map').setView([{fromLat}, {fromLon}], 10);
        L.tileLayer('https://{{s}}.tile.openstreetmap.org/{{z}}/{{x}}/{{y}}.png', {{
            maxZoom: 19,
            attribution: '&copy; OpenStreetMap contributors'
        }}).addTo(map);

        var fromMarker = L.marker([{fromLat}, {fromLon}]).addTo(map).bindPopup('Start');
        var toMarker = L.marker([{toLat}, {toLon}]).addTo(map).bindPopup('Ziel');

        var latlngs = [
            [{fromLat}, {fromLon}],
            [{toLat}, {toLon}]
        ];
        var polyline = L.polyline(latlngs, {{color: 'blue'}}).addTo(map);
        map.fitBounds(polyline.getBounds());
    </script>
</body>
</html>";
        }

        public static async Task<string> SaveHtmlToTempFileAsync(string html)
        {
            var filePath = Path.Combine(Path.GetTempPath(), $"map_{Guid.NewGuid()}.html");
            await File.WriteAllTextAsync(filePath, html);
            return filePath;
        }

        public static async Task<string> GenerateErrorHtmlAsync(string message = "Ungültige Route.")
        {
            var html = $"<html><body><h3 style='font-family:sans-serif;color:red;text-align:center;'>{message}</h3></body></html>";
            return await SaveHtmlToTempFileAsync(html);
        }
    }
}

using System.IO;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using TourPlanner.DataAccessLayer.Models;
using TourPlanner.Infrastructure;

namespace TourPlanner.BusinessLayer.Services
{
    public class PdfExportService
    {
        public void ExportTour(Tour tour)
        {
            string fileName = $"Tour_{tour.Name}_{DateTime.Now:yyyyMMdd_HHmm}.pdf";
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);
            
            try
            {
                var document = new PdfDocument();
                var page = document.AddPage();
                var gfx = XGraphics.FromPdfPage(page);

                var titleFont = new XFont("Verdana", 20, XFontStyle.Bold);
                var bodyFont = new XFont("Verdana", 12, XFontStyle.Regular);

                var titleFormat = new XStringFormat
                {
                    Alignment = XStringAlignment.Center,
                    LineAlignment = XLineAlignment.Near
                };

                // Titel
                gfx.DrawString("Tour Report", titleFont, XBrushes.Black,
                    new XRect(0, 20, page.Width, 40), titleFormat);

                // Content
                int y = 80;
                gfx.DrawString($"Name: {tour.Name}", bodyFont, XBrushes.Black, new XPoint(40, y += 20));
                gfx.DrawString($"From: {tour.From}", bodyFont, XBrushes.Black, new XPoint(40, y += 20));
                gfx.DrawString($"To: {tour.To}", bodyFont, XBrushes.Black, new XPoint(40, y += 20));
                gfx.DrawString($"Distance: {tour.Distance}", bodyFont, XBrushes.Black, new XPoint(40, y += 20));
                gfx.DrawString($"Estimated Time: {tour.EstimatedTime}", bodyFont, XBrushes.Black, new XPoint(40, y += 20));
                gfx.DrawString($"Transport: {tour.TransportType}", bodyFont, XBrushes.Black, new XPoint(40, y += 20));

                gfx.DrawString("Description:", bodyFont, XBrushes.Black, new XPoint(40, y += 30));

                var descFormat = new XStringFormat
                {
                    Alignment = XStringAlignment.Near,
                    LineAlignment = XLineAlignment.Near
                };

                gfx.DrawString(tour.Description ?? "-", bodyFont, XBrushes.Black,
                    new XRect(40, y + 10, page.Width - 80, page.Height - y - 50), descFormat);

                using var stream = File.Create(filePath);
                document.Save(stream);
            }
            catch (Exception ex)
            {
                LoggerHelper.GetLogger(typeof(PdfExportService)).Error($"PDF export for the tour [{tour.Id}] failed: {ex.Message}");
            }
        }
    }
}

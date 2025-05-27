using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace CustomShirtStore.Controllers
{

    
    public class QrController : Controller
    {
        private readonly ILogger<QrController> _logger; 

        public QrController(ILogger<QrController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Generate(string text, int pixelsPerModule = 20, bool download = false)
        {
            if (string.IsNullOrEmpty(text))
            {
                return BadRequest("Text parameter is required.");
            }

            try
            {
                var qrGenerator = new QRCodeGenerator();
                var qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
                var qrCode = new PngByteQRCode(qrCodeData);
                var qrCodeBytes = qrCode.GetGraphic(pixelsPerModule);

                if (download)
                {
                    // Sanitize file name more robustly
                    var invalidChars = Path.GetInvalidFileNameChars();
                    var safeText = string.Join("_", text.Split(invalidChars, StringSplitOptions.RemoveEmptyEntries))
                        .Replace(":", "_").Replace("/", "_");
                    var maxLength = Math.Min(50, safeText.Length); // Use safeText.Length instead of text.Length
                    safeText = safeText.Substring(0, maxLength); // Apply substring based on safeText length
                    var fileName = $"QRCode_{safeText}.png";

                    return File(qrCodeBytes, "image/png", fileName);
                }

                return File(qrCodeBytes, "image/png");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating QR code for text: {Text}", text);
                return StatusCode(500, "Error generating QR code: " + ex.Message);
            }
        }
    }
}

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
//using static System.Net.Mime.MediaTypeNames;

namespace WeatherCSLib
{
    public class Utils
    {
        public static readonly JsonSerializerOptions SerializationOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic)
        };

        public static string? ApiKey()
        {
            return Environment.GetEnvironmentVariable("PIRATE_WEATHER_API_KEY");
        }

        /// <summary>
        /// Загружает изображение и масштабирует его с сохранением пропорций.
        /// </summary>
        /// <param name="imagePath">Путь к исходному изображению.</param>
        /// <param name="maxWidth">Максимальная желаемая ширина.</param>
        /// <param name="maxHeight">Максимальная желаемая высота.</param>
        /// <returns>Масштабированное изображение.</returns>
        public static Bitmap LoadAndScaleImage(string imagePath, int maxWidth, int maxHeight)
        {
            try
            {
                // 1. Загрузка исходного изображения
                using Image originalImage = Image.FromFile(imagePath);

                // 2. Вычисление новых размеров с сохранением пропорций
                int originalWidth = originalImage.Width;
                int originalHeight = originalImage.Height;

                float ratioX = (float)maxWidth / originalWidth;
                float ratioY = (float)maxHeight / originalHeight;

                // Выбираем наименьший коэффициент, чтобы изображение поместилось в заданные размеры
                float ratio = Math.Min(ratioX, ratioY);

                int newWidth = (int)(originalWidth * ratio);
                int newHeight = (int)(originalHeight * ratio);

                // 3. Создание нового битмапа с вычисленными размерами
                Bitmap scaledImage = new Bitmap(newWidth, newHeight);

                // 4. Отрисовка исходного изображения на новом битмапе с использованием высококачественной интерполяции
                using (Graphics graphics = Graphics.FromImage(scaledImage))
                {
                    graphics.CompositingMode = CompositingMode.SourceCopy;
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic; // Для лучшего качества масштабирования
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                    // Отрисовка изображения
                    using (var wrapMode = new ImageAttributes())
                    {
                        wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                        graphics.DrawImage(originalImage, new Rectangle(0, 0, newWidth, newHeight), 0, 0, originalWidth, originalHeight, GraphicsUnit.Pixel, wrapMode);
                    }
                }

                return scaledImage;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}

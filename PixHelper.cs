using System.Drawing;
using System.Drawing.Imaging;
using Tesseract;

public static class PixHelper
{
    public static Pix ConvertBitmapToPix(Bitmap bitmap)
    {
        using (var stream = new System.IO.MemoryStream())
        {
            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
            
            stream.Position = 0;
            return Pix.LoadFromMemory(stream.ToArray());
        }
    }
}

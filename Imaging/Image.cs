using System;
using System.Drawing;
using System.IO;

namespace JonathanPotts.Imaging
{
    /// <summary>
    /// An image that supports basic manipulation functionality.
    /// </summary>
    public abstract class Image : IDisposable
    {
        /// <summary>
        /// The width in pixels of the image being processed.
        /// </summary>
        public abstract int Width { get; }
        /// <summary>
        /// The height in pixels of the image being processed.
        /// </summary>
        public abstract int Height { get; }

        /// <summary>
        /// Creates a manipulatable image from a stream.
        /// </summary>
        /// <param name="stream">A stream containing the image data</param>
        /// <returns>A manipulatable image</returns>
        public static Image FromStream(Stream stream)
        {
            return ImageSkiaSharp.FromStream(stream);
        }

        /// <summary>
        /// Provides a stream containing encoded image data.
        /// </summary>
        /// <param name="format">The format to encode the image</param>
        /// <param name="quality">The quality (1-100) of the encoded the image</param>
        /// <returns>A stream containing encoded image data</returns>
        public abstract Stream AsEncodedStream(ImageEncodingFormat format, int quality = 100);
        /// <summary>
        /// Resizes the image to the specified width and height.
        /// </summary>
        /// <param name="width">The width in pixels to resize the image</param>
        /// <param name="height">The height in pixels to resize the image</param>
        public abstract void Resize(int width, int height);
        /// <summary>
        /// Crops the image using the specified aspect ratio.
        /// </summary>
        /// <param name="aspect">The aspect ratio to crop the image</param>
        public abstract void Crop(double aspect);
        /// <summary>
        /// Removes the transparency from the image.
        /// </summary>
        /// <param name="color">The color to make the background of the image</param>
        public abstract void RemoveTransparency(Color color);
        /// <summary>
        /// Disposes the image from memory.
        /// </summary>
        public abstract void Dispose();
    }

    /// <summary>
    /// Formats that the image data can be encoded with.
    /// </summary>
    public enum ImageEncodingFormat
    {
        /// <summary>
        /// Adapatable Scalable Texture Compression (ASTC)
        /// </summary>
        Astc,
        /// <summary>
        /// Bitmap (BMP)
        /// </summary>
        Bmp,
        /// <summary>
        /// Digital Negative (DNG)
        /// </summary>
        Dng,
        /// <summary>
        /// Graphics Interchange Format (GIF)
        /// </summary>
        Gif,
        /// <summary>
        /// Icon (ICO)
        /// </summary>
        Ico,
        /// <summary>
        /// Joint Photographic Experts Group (JPEG)
        /// </summary>
        Jpeg,
        /// <summary>
        /// Khronos Texture (KTX)
        /// </summary>
        Ktx,
        /// <summary>
        /// Ericsson Texture Compression - PKM (PKM)
        /// </summary>
        Pkm,
        /// <summary>
        /// Portable Network Graphics (PNG)
        /// </summary>
        Png,
        /// <summary>
        /// Wireless Application Protocol Bitmap (WBMP)
        /// </summary>
        Wbmp,
        /// <summary>
        /// WebP
        /// </summary>
        Webp
    }

    /// <summary>
    /// Commonly used aspect ratios for cropping images.
    /// </summary>
    public static class ImageCropFactors
    {
        /// <summary>
        /// Square 1:1
        /// </summary>
        public const double Square = 1.0;
        /// <summary>
        /// Standard 4:3
        /// </summary>
        public const double Standard4x3 = 4.0 / 3.0;
        /// <summary>
        /// Standard 3:2
        /// </summary>
        public const double Standard3x2 = 3.0 / 2.0;
        /// <summary>
        /// Widescreen 16:10
        /// </summary>
        public const double Widescreen16x10 = 16.0 / 10.0;
        /// <summary>
        /// Widescreen 16:9
        /// </summary>
        public const double Widescreen16x9 = 16.0 / 9.0;
    }
}

using System;
using System.Drawing;
using System.IO;
using SkiaSharp;

namespace JonathanPotts.Imaging
{
    /// <summary>
    /// An image that supports basic manipulation functionality using SkiaSharp.
    /// </summary>
    public class ImageSkiaSharp : Image
    {
        /// <summary>
        /// The width in pixels of the image being processed.
        /// </summary>
        public override int Width
        {
            get
            {
                return _bitmap.Width;
            }
        }

        /// <summary>
        /// The height in pixels of the image being processed.
        /// </summary>
        public override int Height
        {
            get
            {
                return _bitmap.Height;
            }
        }

        private SKBitmap _bitmap;

        private ImageSkiaSharp()
        {

        }

        /// <summary>
        /// Creates a manipulatable image from a stream.
        /// </summary>
        /// <param name="stream">A stream containing the image data</param>
        /// <returns>A manipulatable image</returns>
        public static new ImageSkiaSharp FromStream(Stream stream)
        {
            using (var managedStream = new SKManagedStream(stream))
            {
                return new ImageSkiaSharp()
                {
                    _bitmap = SKBitmap.Decode(managedStream)
                };
            }
        }

        /// <summary>
        /// Provides a stream containing encoded image data.
        /// </summary>
        /// <param name="format">The format to encode the image</param>
        /// <param name="quality">The quality (1-100) of the encoded the image</param>
        /// <returns>A stream containing encoded image data</returns>
        public override Stream AsEncodedStream(ImageEncodingFormat format, int quality = 100)
        {
            if (quality < 1 || quality > 100)
            {
                throw new ArgumentOutOfRangeException("quality", "The quality specified needs to be between 1 and 100.");
            }

            using (var image = SKImage.FromBitmap(_bitmap))
            {
                SKEncodedImageFormat imageFormat;

                if (format == ImageEncodingFormat.Astc)
                {
                    imageFormat = SKEncodedImageFormat.Astc;
                }
                else if (format == ImageEncodingFormat.Bmp)
                {
                    imageFormat = SKEncodedImageFormat.Bmp;
                }
                else if (format == ImageEncodingFormat.Dng)
                {
                    imageFormat = SKEncodedImageFormat.Dng;
                }
                else if (format == ImageEncodingFormat.Gif)
                {
                    imageFormat = SKEncodedImageFormat.Gif;
                }
                else if (format == ImageEncodingFormat.Ico)
                {
                    imageFormat = SKEncodedImageFormat.Ico;
                }
                else if (format == ImageEncodingFormat.Jpeg)
                {
                    imageFormat = SKEncodedImageFormat.Jpeg;
                }
                else if (format == ImageEncodingFormat.Ktx)
                {
                    imageFormat = SKEncodedImageFormat.Ktx;
                }
                else if (format == ImageEncodingFormat.Pkm)
                {
                    imageFormat = SKEncodedImageFormat.Pkm;
                }
                else if (format == ImageEncodingFormat.Png)
                {
                    imageFormat = SKEncodedImageFormat.Png;
                }
                else if (format == ImageEncodingFormat.Wbmp)
                {
                    imageFormat = SKEncodedImageFormat.Wbmp;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("format", "The image encoding format specified is not available when using SkiaSharp for processing.");
                }

                return image.Encode(imageFormat, quality).AsStream(true);
            }
        }

        /// <summary>
        /// Resizes the image to the specified width and height.
        /// </summary>
        /// <param name="width">The width in pixels to resize the image</param>
        /// <param name="height">The height in pixels to resize the image</param>
        public override void Resize(int width, int height)
        {
            var resizedBitmap = _bitmap.Resize(new SKImageInfo(width, height), SKBitmapResizeMethod.Lanczos3);

            _bitmap.Dispose();

            _bitmap = resizedBitmap;
        }

        /// <summary>
        /// Crops the image using the specified aspect ratio.
        /// </summary>
        /// <param name="aspect">The aspect ratio to crop the image</param>
        public override void Crop(double aspect)
        {
            var widthFactor = aspect;
            var heightFactor = 1.0 / aspect;

            SKRectI rect;

            var desiredHeight = (int)(_bitmap.Width * heightFactor);

            if (desiredHeight <= _bitmap.Height)
            {
                var difference = _bitmap.Height - desiredHeight;

                var offset = difference / 2;

                rect = new SKRectI(0, offset, _bitmap.Width, _bitmap.Height - offset);
            }
            else
            {
                var desiredWidth = (int)(_bitmap.Height * widthFactor);

                var difference = _bitmap.Width - desiredWidth;

                var offset = difference / 2;

                rect = new SKRectI(offset, 0, _bitmap.Width - offset, _bitmap.Height);
            }

            using (var image = SKImage.FromBitmap(_bitmap))
            {
                using (var croppedImage = image.Subset(rect))
                {
                    var croppedBitmap = SKBitmap.FromImage(croppedImage);

                    _bitmap.Dispose();

                    _bitmap = croppedBitmap;
                }
            }
        }

        /// <summary>
        /// Removes the transparency from the image.
        /// </summary>
        /// <param name="color">The color to make the background of the image</param>
        public override void RemoveTransparency(Color color)
        {
            var skiaColor = new SKColor(color.R, color.G, color.B);

            var coloredBitmap = new SKBitmap(_bitmap.Width, _bitmap.Height);

            using (var canvas = new SKCanvas(coloredBitmap))
            {
                canvas.DrawColor(skiaColor);

                canvas.DrawBitmap(_bitmap, 0, 0);
            }

            _bitmap.Dispose();

            _bitmap = coloredBitmap;
        }

        /// <summary>
        /// Disposes the image from memory.
        /// </summary>
        public override void Dispose()
        {
            _bitmap.Dispose();
        }
    }
}

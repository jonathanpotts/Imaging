# JonathanPotts.Imaging
A library offering basic image manipulation capabilities for cross-platform development (.NET Framework, .NET Core, ASP.NET, and Xamarin).

Uses [.NET Standard](https://github.com/dotnet/standard) and [SkiaSharp](https://github.com/mono/SkiaSharp).

This library should work on all .NET platforms that support .NET Standard 2.0 including:

* [.NET Framework](https://github.com/microsoft/dotnet) 4.6.1 (including [ASP.NET](https://www.asp.net))
* [.NET Core](https://github.com/dotnet/core) 2.0 (including [ASP.NET Core](https://github.com/aspnet/Home))
* [Mono](https://github.com/mono/mono) 5.4
* [Xamarin.iOS](https://github.com/xamarin/xamarin-macios) 10.14
* [Xamarin.Mac](https://github.com/xamarin/xamarin-macios) 3.8
* [Xamarin.Android](https://github.com/xamarin/xamarin-android) 8.0
* [Universal Windows Platform](https://docs.microsoft.com/en-us/windows/uwp/index) 10.0.16299

*Note*: Linux binaries are not included with the SkiaSharp NuGet package. However, they can be downloaded from the [SkiaSharp releases page](https://github.com/mono/SkiaSharp/releases). Make sure to get the binaries for the SkiaSharp version used by the version of this library being used.

## Example Usage

```csharp
  using (var image = Image.FromStream(input))
  {
      image.RemoveTransparency(Color.Black);
      image.Crop(ImageCropFactors.Square);
      image.Resize(256, 256);

      using (var output = image.AsEncodedStream(ImageEncodingFormat.Jpeg, 80))
      {
          // do something
      }
  }
```

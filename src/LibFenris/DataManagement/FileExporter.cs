using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using ImageMagick;
using LibFenris.FileFormats;

namespace LibFenris.DataManagement
{
    public static class FileExporter
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
        };

        public static void ExportStringListDict(Dictionary<string, StringList> stringListDict, string path)
        {
            // Write all strings to a single JSON file to make it easier to compare different versions.
            using (StreamWriter writer = new(Path.Combine(path, "StringList.json")))
            {
                Dictionary<string, Dictionary<string, string>> exportDict = new();

                foreach (var kvp in stringListDict)
                    exportDict.Add(kvp.Key, kvp.Value.StringDict);

                writer.Write(JsonSerializer.Serialize(exportDict, JsonOptions));
            }
        }

        public static void ExportTextureDict(Dictionary<string, Texture> textureDict, string path)
        {
            foreach (var kvp in textureDict)
            {
                try
                {
                    byte[] ddsTexture = TextureConverter.ConvertRawTexture(kvp.Value);

                    string imagePath = Path.Combine(path, kvp.Key);
                    if (Directory.Exists(imagePath) == false) Directory.CreateDirectory(imagePath);

                    try
                    {
                        // Try to process the image: slice into frames and convert to PNG
                        MagickImage image = new(ddsTexture);
                        var magickRects = kvp.Value.CalculateMagickRects();

                        for (int i = 0; i < kvp.Value.Frames.Length; i++)
                        {
                            MagickImage slice = (MagickImage)image.Clone();
                            slice.Crop(magickRects[i]);
                            slice.Format = MagickFormat.Png;
                            File.WriteAllBytes(Path.Combine(imagePath, $"{kvp.Value.Frames[i].ImageHandle}.png"), slice.ToByteArray());
                        }
                    }
                    catch (MagickException e)
                    {
                        // Fall back to DDS if processing failed
                        Console.WriteLine($"Failed to process {kvp.Key}, falling back to DDS");
                        //Console.WriteLine(e.ToString());
                        File.WriteAllBytes(Path.Combine(imagePath, $"{kvp.Key}.dds"), ddsTexture);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Failed to export {kvp.Key}");
                    Console.WriteLine(e.ToString());
                }
            }
        }
    }
}

using System.Reflection;
using LibFenris.FileFormats;

namespace LibFenris.DataManagement
{
    public class DataManager
    {
        private readonly Dictionary<string, StringList> _stringListDict = new();
        private readonly Dictionary<string, Texture> _textureDict = new();

        public void ParseFiles(string[] paths)
        {
            Console.WriteLine("Parsing...");

            foreach (string path in paths)
            {
                if ((Directory.Exists(path) || File.Exists(path)) == false) continue;   // Skip invalid paths

                if (File.GetAttributes(path).HasFlag(FileAttributes.Directory))
                {
                    foreach (string file in Directory.GetFiles(path))
                        ReadFile(file);
                }
                else
                {
                    ReadFile(path);
                }
            }
        }

        public void Export()
        {
            Console.WriteLine("Exporting...");

            string exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (_stringListDict.Count > 0)
            {
                string stringListPath = Path.Combine(exePath, "Output");
                if (Directory.Exists(stringListPath) == false) Directory.CreateDirectory(stringListPath);
                FileExporter.ExportStringListDict(_stringListDict, Path.Combine(exePath, "Output"));
            }

            if (_textureDict.Count > 0)
            {
                string texturePath = Path.Combine(exePath, "Output", "Texture");
                if (Directory.Exists(texturePath) == false) Directory.CreateDirectory(texturePath);
                FileExporter.ExportTextureDict(_textureDict, texturePath);
            }
        }

        private void ReadFile(string path)
        {
            using (FileStream fs = new(path, FileMode.Open))
            using (BinaryReader reader = new(fs))
            {
                try
                {
                    SnoFileHeader header = new(reader);  // Read SNO file header to determine if we can handle this file.

                    switch (header.FormatHash)
                    {
                        case SnoFormatDefinition.StringList: ReadStringList(reader, path); break;
                        case SnoFormatDefinition.Texture: ReadTexture(reader, path); break;
                        default: throw new NotImplementedException($"Unsupported SNO file type: {header.FormatHash}.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }

        private void ReadStringList(BinaryReader reader, string path)
        {
            string name = Path.GetFileNameWithoutExtension(path);
            StringList stringList = new(reader);
            _stringListDict.Add(name, stringList);
        }

        private void ReadTexture(BinaryReader reader, string path)
        {
            string name = Path.GetFileNameWithoutExtension(path);
            string payloadPath = Path.Combine(Path.GetDirectoryName(path), "..", "..", "payload", "Texture", $"{name}.tex");

            if (File.Exists(payloadPath) == false) throw new($"Payload for {name} not found.");

            Texture texture = new(reader);
            texture.PayloadPath = payloadPath;
            _textureDict.Add(name, texture);
        }
    }
}

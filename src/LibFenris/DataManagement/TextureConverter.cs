using LibFenris.FileFormats;

namespace LibFenris.DataManagement
{
    public static class TextureConverter
    {
        private static readonly Dictionary<int, TextureFormatDefinition> TextureFormatDict = new()
        {
            { 0,    new("DXGI_FORMAT_B8G8R8A8_UNORM",     "GL_RGBA8",                                     64) },
            { 7,    new("DXGI_FORMAT_A8_UNORM",           "GL_R8",                                        64) },
            { 9,    new("DXGI_FORMAT_BC1_UNORM",          "GL_COMPRESSED_RGB_S3TC_DXT1_EXT",              128) },
            { 10,   new("DXGI_FORMAT_BC1_UNORM",          "GL_COMPRESSED_RGB_S3TC_DXT1_EXT",              128) },
            { 12,   new("DXGI_FORMAT_BC3_UNORM",          "GL_COMPRESSED_RGBA_S3TC_DXT5_EXT",             64) },
            { 23,   new("DXGI_FORMAT_A8_UNORM",           "GL_R8",                                        128) },
            { 25,   new("DXGI_FORMAT_R16G16B16A16_FLOAT", "GL_RGBA16F",                                   32) },
            { 41,   new("DXGI_FORMAT_BC4_UNORM",          "GL_COMPRESSED_RED_RGTC1",                      64) },
            { 42,   new("DXGI_FORMAT_BC5_UNORM",          "GL_COMPRESSED_RG_RGTC2",                       64) },
            { 43,   new("DXGI_FORMAT_BC6H_SF16",          "GL_COMPRESSED_RGB_BPTC_SIGNED_FLOAT_ARB",      64) },
            { 44,   new("DXGI_FORMAT_BC7_UNORM",          "GL_COMPRESSED_RGBA_BPTC_UNORM_ARB",            64) },
            { 45,   new("DXGI_FORMAT_B8G8R8A8_UNORM",     "GL_RGBA8",                                     64) },
            { 46,   new("DXGI_FORMAT_BC1_UNORM",          "GL_COMPRESSED_RGB_S3TC_DXT1_EXT",              128) },
            { 47,   new("DXGI_FORMAT_BC1_UNORM",          "GL_COMPRESSED_RGBA_S3TC_DXT1_EXT",             128) },
            { 48,   new("DXGI_FORMAT_BC2_UNORM",          "GL_COMPRESSED_RGBA_S3TC_DXT3_EXT",             64) },
            { 49,   new("DXGI_FORMAT_BC3_UNORM",          "GL_COMPRESSED_RGBA_S3TC_DXT5_EXT",             64) },
            { 50,   new("DXGI_FORMAT_BC7_UNORM",          "GL_COMPRESSED_RGBA_BPTC_UNORM_ARB",            64) },
            { 51,   new("DXGI_FORMAT_BC6H_UF16",          "GL_COMPRESSED_RGB_BPTC_UNSIGNED_FLOAT_ARB",    64) },
        };

        private static readonly List<string> DxgiList = new()
        {
            "DXGI_FORMAT_UNKNOWN",
            "DXGI_FORMAT_R32G32B32A32_TYPELESS",
            "DXGI_FORMAT_R32G32B32A32_FLOAT",
            "DXGI_FORMAT_R32G32B32A32_UINT",
            "DXGI_FORMAT_R32G32B32A32_SINT",
            "DXGI_FORMAT_R32G32B32_TYPELESS",
            "DXGI_FORMAT_R32G32B32_FLOAT",
            "DXGI_FORMAT_R32G32B32_UINT",
            "DXGI_FORMAT_R32G32B32_SINT",
            "DXGI_FORMAT_R16G16B16A16_TYPELESS",
            "DXGI_FORMAT_R16G16B16A16_FLOAT",
            "DXGI_FORMAT_R16G16B16A16_UNORM",
            "DXGI_FORMAT_R16G16B16A16_UINT",
            "DXGI_FORMAT_R16G16B16A16_SNORM",
            "DXGI_FORMAT_R16G16B16A16_SINT",
            "DXGI_FORMAT_R32G32_TYPELESS",
            "DXGI_FORMAT_R32G32_FLOAT",
            "DXGI_FORMAT_R32G32_UINT",
            "DXGI_FORMAT_R32G32_SINT",
            "DXGI_FORMAT_R32G8X24_TYPELESS",
            "DXGI_FORMAT_D32_FLOAT_S8X24_UINT",
            "DXGI_FORMAT_R32_FLOAT_X8X24_TYPELESS",
            "DXGI_FORMAT_X32_TYPELESS_G8X24_UINT",
            "DXGI_FORMAT_R10G10B10A2_TYPELESS",
            "DXGI_FORMAT_R10G10B10A2_UNORM",
            "DXGI_FORMAT_R10G10B10A2_UINT",
            "DXGI_FORMAT_R11G11B10_FLOAT",
            "DXGI_FORMAT_R8G8B8A8_TYPELESS",
            "DXGI_FORMAT_R8G8B8A8_UNORM",
            "DXGI_FORMAT_R8G8B8A8_UNORM_SRGB",
            "DXGI_FORMAT_R8G8B8A8_UINT",
            "DXGI_FORMAT_R8G8B8A8_SNORM",
            "DXGI_FORMAT_R8G8B8A8_SINT",
            "DXGI_FORMAT_R16G16_TYPELESS",
            "DXGI_FORMAT_R16G16_FLOAT",
            "DXGI_FORMAT_R16G16_UNORM",
            "DXGI_FORMAT_R16G16_UINT",
            "DXGI_FORMAT_R16G16_SNORM",
            "DXGI_FORMAT_R16G16_SINT",
            "DXGI_FORMAT_R32_TYPELESS",
            "DXGI_FORMAT_D32_FLOAT",
            "DXGI_FORMAT_R32_FLOAT",
            "DXGI_FORMAT_R32_UINT",
            "DXGI_FORMAT_R32_SINT",
            "DXGI_FORMAT_R24G8_TYPELESS",
            "DXGI_FORMAT_D24_UNORM_S8_UINT",
            "DXGI_FORMAT_R24_UNORM_X8_TYPELESS",
            "DXGI_FORMAT_X24_TYPELESS_G8_UINT",
            "DXGI_FORMAT_R8G8_TYPELESS",
            "DXGI_FORMAT_R8G8_UNORM",
            "DXGI_FORMAT_R8G8_UINT",
            "DXGI_FORMAT_R8G8_SNORM",
            "DXGI_FORMAT_R8G8_SINT",
            "DXGI_FORMAT_R16_TYPELESS",
            "DXGI_FORMAT_R16_FLOAT",
            "DXGI_FORMAT_D16_UNORM",
            "DXGI_FORMAT_R16_UNORM",
            "DXGI_FORMAT_R16_UINT",
            "DXGI_FORMAT_R16_SNORM",
            "DXGI_FORMAT_R16_SINT",
            "DXGI_FORMAT_R8_TYPELESS",
            "DXGI_FORMAT_R8_UNORM",
            "DXGI_FORMAT_R8_UINT",
            "DXGI_FORMAT_R8_SNORM",
            "DXGI_FORMAT_R8_SINT",
            "DXGI_FORMAT_A8_UNORM",
            "DXGI_FORMAT_R1_UNORM",
            "DXGI_FORMAT_R9G9B9E5_SHAREDEXP",
            "DXGI_FORMAT_R8G8_B8G8_UNORM",
            "DXGI_FORMAT_G8R8_G8B8_UNORM",
            "DXGI_FORMAT_BC1_TYPELESS",
            "DXGI_FORMAT_BC1_UNORM",
            "DXGI_FORMAT_BC1_UNORM_SRGB",
            "DXGI_FORMAT_BC2_TYPELESS",
            "DXGI_FORMAT_BC2_UNORM",
            "DXGI_FORMAT_BC2_UNORM_SRGB",
            "DXGI_FORMAT_BC3_TYPELESS",
            "DXGI_FORMAT_BC3_UNORM",
            "DXGI_FORMAT_BC3_UNORM_SRGB",
            "DXGI_FORMAT_BC4_TYPELESS",
            "DXGI_FORMAT_BC4_UNORM",
            "DXGI_FORMAT_BC4_SNORM",
            "DXGI_FORMAT_BC5_TYPELESS",
            "DXGI_FORMAT_BC5_UNORM",
            "DXGI_FORMAT_BC5_SNORM",
            "DXGI_FORMAT_B5G6R5_UNORM",
            "DXGI_FORMAT_B5G5R5A1_UNORM",
            "DXGI_FORMAT_B8G8R8A8_UNORM",
            "DXGI_FORMAT_B8G8R8X8_UNORM",
            "DXGI_FORMAT_R10G10B10_XR_BIAS_A2_UNORM",
            "DXGI_FORMAT_B8G8R8A8_TYPELESS",
            "DXGI_FORMAT_B8G8R8A8_UNORM_SRGB",
            "DXGI_FORMAT_B8G8R8X8_TYPELESS",
            "DXGI_FORMAT_B8G8R8X8_UNORM_SRGB",
            "DXGI_FORMAT_BC6H_TYPELESS",
            "DXGI_FORMAT_BC6H_UF16",
            "DXGI_FORMAT_BC6H_SF16",
            "DXGI_FORMAT_BC7_TYPELESS",
            "DXGI_FORMAT_BC7_UNORM",
            "DXGI_FORMAT_BC7_UNORM_SRGB",
            "DXGI_FORMAT_AYUV",
            "DXGI_FORMAT_Y410",
            "DXGI_FORMAT_Y416",
            "DXGI_FORMAT_NV12",
            "DXGI_FORMAT_P010",
            "DXGI_FORMAT_P016",
            "DXGI_FORMAT_420_OPAQUE",
            "DXGI_FORMAT_YUY2",
            "DXGI_FORMAT_Y210",
            "DXGI_FORMAT_Y216",
            "DXGI_FORMAT_NV11",
            "DXGI_FORMAT_AI44",
            "DXGI_FORMAT_IA44",
            "DXGI_FORMAT_P8",
            "DXGI_FORMAT_A8P8",
            "DXGI_FORMAT_B4G4R4A4_UNORM",
            "DXGI_FORMAT_P208",
            "DXGI_FORMAT_V208",
            "DXGI_FORMAT_V408",
        };

        private static readonly byte[] BppCounts = new byte[]
        {
            0, 128, 128, 128, 128, 96, 96, 96, 96, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64, 64,
            64, 64, 64, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32,
            32, 32, 32, 32, 32, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 8, 8, 8, 8, 8, 8, 1,
            32, 32, 32, 4, 4, 4, 8, 8, 8, 8, 8, 8, 4, 4, 4, 8, 8, 8, 16, 16, 32, 32, 32, 32, 32, 32,
            32, 8, 8, 8, 8, 8, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 16
        };

        private enum Magic
        {
            DDS = 542327876,
            DX10 = 808540228,
            DXT1 = 827611204,
            DXT3 = 861165636,
            DXT5 = 894720068,
            ATI1 = 826889281,
            ATI2 = 843666497
        }

        public static byte[] ConvertRawTexture(Texture texture)
        {
            // Prepare DDS header information
            if (TextureFormatDict.TryGetValue(texture.TexFormat, out var format) == false)
            {
                Console.WriteLine($"Unknown texture format {texture.TexFormat}");
                return Array.Empty<byte>();
            }

            int index = DxgiList.IndexOf(format.Dxgi);

            if (index == -1)
            {
                Console.WriteLine($"Unknown texture DXGI {texture.TexFormat}");
                return Array.Empty<byte>();
            }

            byte[] payload = File.ReadAllBytes(texture.PayloadPath);

            // The width specified in the meta file is inaccurate and needs to be aligned based on the format
            int width = Align(texture.Width, format.Alignment);
            int height = texture.Height;

            var fourCC = index switch
            {
                71 => Magic.DXT1,    // DXGI_FORMAT_BC1_UNORM
                74 => Magic.DXT3,    // DXGI_FORMAT_BC2_UNORM
                77 => Magic.DXT5,    // DXGI_FORMAT_BC3_UNORM
                80 => Magic.ATI1,    // DXGI_FORMAT_BC4_UNORM
                83 => Magic.ATI2,    // DXGI_FORMAT_BC5_UNORM
                _ => Magic.DX10,
            };

            int count = (width * height * BppCounts[index]) / 8;

            // Write raw payload to a DDS file
            int headerSize = fourCC == Magic.DX10 ? 148 : 128;  // DX10 uses an extended header

            using (MemoryStream ms = new(new byte[headerSize + payload.Length]))
            using (BinaryWriter writer = new(ms))
            {
                // DDS header
                writer.Write((int)Magic.DDS);           // Magic

                writer.Write(124);                      // Size
                writer.Write(0x1 | 0x2 | 0x4 | 0x1000); // Flags
                writer.Write(height);                   // Height
                writer.Write(width);                    // Width
                writer.Write(count);                    // Pitch or linear size
                writer.Write(0);                        // Depth
                writer.Write(1);                        // Mip map count

                writer.BaseStream.Position = 76;
                writer.Write(32);                       // ddspf size
                writer.Write(4);                        // ddspf flags
                writer.Write((int)fourCC);              // ddspf fourCC

                // Extended DX10 header
                if (fourCC == Magic.DX10)
                {
                    writer.BaseStream.Position = 128;
                    writer.Write(index);                // DXGI
                    writer.Write(3);                    // Resource dimension
                    writer.Write(0);                    // Misc flag
                    writer.Write(1);                    // Array size
                    writer.Write(0);                    // Misc flag 2
                }

                // Payload
                writer.BaseStream.Position = headerSize;
                writer.Write(payload);

                return ms.ToArray();
            }
        }

        private static int Align(int number, int alignment)
        {
            int remainder = number % alignment;
            if (remainder == 0) return number;
            return number + (alignment - remainder);
        }
    }

    public class TextureFormatDefinition
    {
        public string Dxgi { get; }
        public string OpenGL { get; }
        public int Alignment { get; }

        public TextureFormatDefinition(string dxgi, string openGL, int alignment)
        {
            Dxgi = dxgi;
            OpenGL = openGL;
            Alignment = alignment;
        }
    }
}

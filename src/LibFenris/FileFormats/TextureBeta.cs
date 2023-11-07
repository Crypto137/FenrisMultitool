using System.Text;
using ImageMagick;

namespace LibFenris.FileFormats
{
    public class TextureBeta : ITexture
    {
        public int SnoId { get; }

        public int TexFormat { get; }               // enum
        public uint VolumeXSlices { get; }
        public uint VolumeYSlices { get; }
        public uint Width { get; }
        public uint Height { get; }
        public uint Depth { get; }
        public uint FaceCount { get; }
        public ushort MipMapLevelMin { get; }
        public ushort MipMapLevelMax { get; }
        public int ImportFlags { get; }
        public int TextureResourceType { get; }     // enum
        public Rgba AverageColor { get; }
        public UInt16Vector2 Hotspot { get; }

        public SerializeData Tex { get; }           // SerializeData for payload
        public ITexFrame[] Frames { get; }

        public string PayloadPath { get; set; }

        public TextureBeta(BinaryReader reader)
        {
            SnoId = reader.ReadInt32();
            reader.BaseStream.Position += 4;

            TexFormat = reader.ReadInt32();
            reader.BaseStream.Position += 4;
            VolumeXSlices = reader.ReadUInt32();
            VolumeYSlices = reader.ReadUInt32();
            Width = reader.ReadUInt32();
            Height = reader.ReadUInt32();
            Depth = reader.ReadUInt32();
            FaceCount = reader.ReadUInt32();
            MipMapLevelMin = reader.ReadUInt16();
            MipMapLevelMax = reader.ReadUInt16();
            ImportFlags = reader.ReadInt32();
            TextureResourceType = reader.ReadInt32();
            AverageColor = new(reader);
            Hotspot = new(reader);
            reader.BaseStream.Position += 16;

            // Read SerializeData that points to SerializeData for the payload
            SerializeData serTex = new(reader);
            long pos = reader.BaseStream.Position;
            reader.BaseStream.Position = SnoFileHeader.Size + serTex.Offset;
            Tex = new(reader);
            reader.BaseStream.Position = pos + 8;   // Return to previous position and skip padding

            // Read frames
            SerializeData serFrames = new(reader);
            reader.BaseStream.Position = SnoFileHeader.Size + serFrames.Offset;
            Frames = new ITexFrame[serFrames.SizeAndFlags / 100];   // Each frame is 100 bytes
            for (int i = 0; i < Frames.Length; i++)
                Frames[i] = new TexFrameBeta(reader);

            // unknown dword @ 0xb0
        }

        public MagickGeometry[] CalculateMagickRects()
        {
            // This is used for slicing textures into separate files with ImageMagick
            var magickRects = new MagickGeometry[Frames.Length];

            for (int i = 0; i < magickRects.Length; i++)
            {
                // The coordinates here are slightly inaccurate since we have to rely on UVs
                int x0 = (int)Math.Floor(Frames[i].U0 * Width);
                int y0 = (int)Math.Floor(Frames[i].V0 * Height);
                int x1 = (int)Math.Ceiling(Frames[i].U1 * Width);
                int y1 = (int)Math.Ceiling(Frames[i].V1 * Height);

                magickRects[i] = new(x0, y0, x1 - x0, y1 - y0);
            }

            return magickRects;
        }
    }

    public class TexFrameBeta : ITexFrame
    {
        public object ImageHandle { get; }
        public float U0 { get; }
        public float V0 { get; }
        public float U1 { get; }
        public float V1 { get; }
        public float TrimU0 { get; }
        public float TrimV0 { get; }
        public float TrimU1 { get; }
        public float TrimV1 { get; }

        public TexFrameBeta(BinaryReader reader)
        {
            // Image handle in the old format is a null terminated string with 68 bytes allocated to it.
            ImageHandle = Encoding.UTF8.GetString(reader.ReadBytes(68)).Trim('\0');

            U0 = reader.ReadSingle();
            V0 = reader.ReadSingle();
            U1 = reader.ReadSingle();
            V1 = reader.ReadSingle();
            TrimU0 = reader.ReadSingle();
            TrimV0 = reader.ReadSingle();
            TrimU1 = reader.ReadSingle();
            TrimV1 = reader.ReadSingle();
        }
    }
}

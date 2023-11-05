using ImageMagick;

namespace LibFenris.FileFormats
{
    public class Texture
    {
        public int SnoId { get; }

        public int TexFormat { get; }               // enum
        public ushort VolumeXSlices { get; }
        public ushort VolumeYSlices { get; }
        public ushort Width { get; }
        public ushort Height { get; }
        public uint Depth { get; }
        public byte FaceCount { get; }
        public byte MipMapLevelMin { get; }
        public byte MipMapLevelMax { get; }
        public int ImportFlags { get; }
        public int TextureResourceType { get; }     // enum
        public Rgba AverageColor { get; }
        public UInt16Vector2 Hotspot { get; }

        public SerializeData Tex { get; }           // SerializeData for payload
        public TexFrame[] Frames { get; }

        public string PayloadPath { get; set; }

        public Texture(BinaryReader reader)
        {
            SnoId = reader.ReadInt32();
            reader.BaseStream.Position += 4;

            TexFormat = reader.ReadInt32();
            VolumeXSlices = reader.ReadUInt16();
            VolumeYSlices = reader.ReadUInt16();
            Width = reader.ReadUInt16();
            Height = reader.ReadUInt16();
            Depth = reader.ReadUInt32();
            FaceCount = reader.ReadByte();
            MipMapLevelMin = reader.ReadByte();
            MipMapLevelMax = reader.ReadByte();
            reader.BaseStream.Position++;           // Padding byte
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
            Frames = new TexFrame[serFrames.SizeAndFlags / 36];   // Each frame is 36 bytes
            for (int i = 0; i < Frames.Length; i++)
                Frames[i] = new(reader);
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

    public class TexFrame
    {
        public uint ImageHandle { get; }    // Some kind of hash that replaced frame name.
        public float U0 { get; }
        public float V0 { get; }
        public float U1 { get; }
        public float V1 { get; }

        // UVs are repeated for some reason. In D3 there used to be pixel perfect coordinates here.
        public float TrimU0 { get; }
        public float TrimV0 { get; }
        public float TrimU1 { get; }
        public float TrimV1 { get; }

        public TexFrame(BinaryReader reader)
        {
            ImageHandle = reader.ReadUInt32();
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

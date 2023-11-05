using ImageMagick;

namespace LibFenris.FileFormats
{
    // Use interfaces for textures to support multiple texture formats
    public interface ITexture
    {
        public ITexFrame[] Frames { get; }
        public string PayloadPath { get; set; }
        public abstract MagickGeometry[] CalculateMagickRects();
    }

    public interface ITexFrame
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
    }
}

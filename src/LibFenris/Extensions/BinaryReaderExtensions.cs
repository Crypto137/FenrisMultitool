using System.Text;

namespace LibFenris.Extensions
{
    public static class BinaryReaderExtensions
    {
        public static string ReadFixedString(this BinaryReader reader, int length)
        {
            return Encoding.UTF8.GetString(reader.ReadBytes(length));
        }

        public static string ReadFixedStringAtOffset(this BinaryReader reader, long offset, int length)
        {
            long pos = reader.BaseStream.Position;
            reader.BaseStream.Seek(offset, 0);
            string result = reader.ReadFixedString(length);
            reader.BaseStream.Seek(pos, 0);
            return result;
        }
    }
}

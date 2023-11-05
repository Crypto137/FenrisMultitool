using LibFenris.Extensions;

namespace LibFenris.FileFormats
{
    public class StringList
    {
        public int SnoId { get; }

        public int HeaderSize { get; }  // Always 32. Used to be 40 in D3.
        public int EntriesSize { get; }

        public StringListEntry[] Entries { get; }
        public Dictionary<string, string> StringDict { get; } = new();

        public StringList(BinaryReader reader)
        {
            SnoId = reader.ReadInt32();
            reader.BaseStream.Position += 4;

            reader.BaseStream.Position += 8;
            HeaderSize = reader.ReadInt32();
            EntriesSize = reader.ReadInt32();
            reader.BaseStream.Position += 8;

            // Entries (40 bytes each)
            Entries = new StringListEntry[EntriesSize / 40];
            for (int i = 0; i < Entries.Length; i++)
                Entries[i] = new(reader);

            foreach (StringListEntry entry in Entries)
                StringDict.Add(entry.Label, entry.Text);
        }
    }

    public class StringListEntry
    {
        public string Label { get; }
        public string Text { get; }
        public uint LabelHash { get; }

        public StringListEntry(BinaryReader reader)
        {
            reader.BaseStream.Position += 8;

            // Strings have null at the end, same as D3. We subtract 1 from the length to ignore them.
            SerializeData serLabel = new(reader);
            Label = reader.ReadFixedStringAtOffset(SnoFileHeader.Size + serLabel.Offset, serLabel.SizeAndFlags - 1);
            reader.BaseStream.Position += 8;

            SerializeData serText = new(reader);
            Text = reader.ReadFixedStringAtOffset(SnoFileHeader.Size + serText.Offset, serText.SizeAndFlags - 1);
            LabelHash = reader.ReadUInt32();
            reader.BaseStream.Position += 4;
        }
    }
}

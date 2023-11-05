namespace LibFenris.FileFormats
{
    #region Enums

    public enum SnoGroup
    {
        Unknown = -3,
        Code = -2,
        None = -1,
        Actor = 1,
        NPCComponentSet = 2,
        AIBehavior = 3,
        AIState = 4,
        AmbientSound = 5,
        Anim = 6,
        Anim2D = 7,
        AnimSet = 8,
        Appearance = 9,
        Hero = 10,
        Cloth = 11,
        Conversation = 12,
        ConversationList = 13,
        EffectGroup = 14,
        Encounter = 15,
        Explosion = 17,
        FlagSet = 18,
        Font = 19,
        GameBalance = 20,
        Global = 21,
        LevelArea = 22,
        Light = 23,
        MarkerSet = 24,
        Observer = 26,
        Particle = 27,
        Physics = 28,
        Power = 29,
        Quest = 31,
        Rope = 32,
        Scene = 33,
        Script = 35,
        ShaderMap = 36,
        Shader = 37,
        Shake = 38,
        SkillKit = 39,
        Sound = 40,
        StringList = 42,
        Surface = 43,
        Texture = 44,
        Trail = 45,
        UI = 46,
        Weather = 47,
        World = 48,
        Recipe = 49,
        Condition = 51,
        TreasureClass = 52,
        Account = 53,
        Material = 57,
        Lore = 59,
        Reverb = 60,
        Music = 62,
        Tutorial = 63,
        AnimTree = 67,
        Vibration = 68,
        wWiseSoundBank = 71,
        Speaker = 72,
        Item = 73,
        PlayerClass = 74,
        FogVolume = 76,
        Biome = 77,
        Wall = 78,
        SoundTable = 79,
        Subzone = 80,
        MaterialValue = 81,
        MonsterFamily = 82,
        TileSet = 83,
        Population = 84,
        MaterialValueSet = 85,
        WorldState = 86,
        Schedule = 87,
        VectorField = 88,
        Storyboard = 90,
        Territory = 92,
        AudioContext = 93,
        VOProcess = 94,
        DemonScroll = 95,
        QuestChain = 96,
        LoudnessPreset = 97,
        ItemType = 98,
        Achievement = 99,
        Crafter = 100,
        HoudiniParticlesSim = 101,
        Movie = 102,
        TiledStyle = 103,
        Affix = 104,
        Reputation = 105,
        ParagonNode = 106,
        MonsterAffix = 107,
        ParagonBoard = 108,
        SetItemBonus = 109,
        StoreProduct = 110,
        ParagonGlyph = 111,
        ParagonGlyphAffix = 112,
        Challenge = 114,
        MarkingShape = 115,
        ItemRequirement = 116,
        Boost = 117,
        Emote = 118,
        Jewelry = 119,
        PlayerTitle = 120,
        Emblem = 121,
        Dye = 122,
        FogOfWar = 123,
        ParagonThreshold = 124,
        AIAwareness = 125,
        TrackedReward = 126,
        CollisionSettings = 127,
        Aspect = 128,
        ABTest = 129,
        Stagger = 130,
        EyeColor = 131,
        Makeup = 132,
        MarkingColor = 133,
        HairColor = 134,
        DungeonAffix = 135,
        Activity = 136,
        Season = 137,
        HairStyle = 138,
        FacialHair = 139,
        Face = 140,
        MercenaryClass = 141,
        PassivePowerContainer = 142,
        MountProfile = 143,
        AICoordinator = 144,
        CrafterTab = 145,
        TownPortalCosmetic = 146,
        AxeTest = 147,
        Wizard = 148,
        FootstepTable = 149,
        Modal = 150,
        CollectiblePower = 151,
        AppearanceSet = 152,
        Preset = 153,
        MAX_SNO_GROUPS = 154
    }

    public enum SnoFormatDefinition : uint
    {
        // 0.6 used a different texture meta format
        TextureBeta = 0x6d1931a9,

        // The first byte in StringList FormatHash seems to indicate version?
        StringList118 = 0x4fe48576,
        StringList119 = 0x4fe48577,
        StringList121 = 0x4fe48579,

        // Unsorted
        SoundTable = 0x75da10d,
        TiledStyle = 0xa1438466,
        Observer = 0x538aace1,
        Biome = 0xa5387e0f,
        World = 0xd4973eb0,
        Affix = 0xdf827c7d,
        SetItemBonus = 0x6c2a1b,
        Condition = 0xa2853ed1,
        FogVolume = 0x4e94600,
        Power = 0x6e9a02ac,
        Tutorial = 0xa0c4ae75,
        ParagonGlyphAffix = 0x760e665f,
        Makeup = 0x2d08ed12,
        MonsterAffix = 0x3580df4c,
        TownPortalCosmetic = 0xb4d290e9,
        MountProfile = 0x8394c753,
        ABTest = 0x662a2096,
        Trail = 0xc7bf6af1,
        Weather = 0x5065ac78,
        ParagonNode = 0x2168872,
        FootstepTable = 0x71e504c0,
        Lore = 0x3835f7e,
        Rope = 0xb4d0f45b,
        Animation = 0xb4893876,
        PassivePowerContainer = 0x2740e867,
        Shader = 0x94246cf5,
        Emote = 0x2cb54509,
        WorldState = 0xcfdede48,
        CollisionSettings = 0x2005626,
        TrackedReward = 0x36f8bfa3,
        Quest = 0x4d6f1b7b,
        Challenge = 0x949c3bfa,
        PlayerTitle = 0xc75809e0,
        ParagonThreshold = 0xf19d9f9c,
        Stagger = 0x9c60b334,
        FlagSet = 0x394e1220,
        Music = 0x626bea4,
        Cloth = 0xbd4a23e6,
        DungeonAffix = 0xfbe69fd5,
        Vibration = 0x9f9bebf3,
        DemonScroll = 0x7076d5d8,
        GameBalance = 0x88b4309d,
        ParagonGlyph = 0x74f841b,
        NPCComponentSet = 0x6936f46a,
        Account = 0x21f8dea,
        HoudiniParticlesSim = 0xa22b28e5,
        FogOfWar = 0xd9885328,
        Hero = 0xfc7ec2d9,
        MonsterFamily = 0x7d49175d,
        Boost = 0x59bb7738,
        MaterialValueSet = 0x2b792bbf,
        Speaker = 0xbf2c02d7,
        EffectGroup = 0x713f8993,
        Particle = 0x649ab876,
        Appearance = 0x5f6d9239,
        ItemType = 0xde8e910,
        Reputation = 0xd9702d7a,
        AnimTree = 0x67af3e51,
        Encounter = 0xf4fd7791,
        Reverb = 0x4a6678a,
        MarkingShape = 0x23e27af4,
        Scene = 0xc00dfcf6,
        Face = 0xd12ed4c3,
        Activity = 0x28d46a2c,
        Recipe = 0x237caff,
        Dye = 0x3303df37,
        FacialHair = 0x2c9cbc23,
        Jewelry = 0xcae08715,
        Territory = 0xa74ed120,
        Storyboard = 0x1272638d,
        CrafterTab = 0x5abbb020,
        AmbientSound = 0xef728e4d,
        MaterialValue = 0x3e39c656,
        EyeColor = 0xed73673,
        ParagonBoard = 0x652665a,
        Actor = 0x221f40b2,
        Material = 0x879de8c4,
        Item = 0x745320bf,
        UIDialog = 0x19a2605f,
        Subzone = 0x7b7eeb9a,
        Conversation = 0xb49dd346,
        Crafter = 0x4cd08e6b,
        Season = 0x8d795dbe,
        StoreProduct = 0x6c783d7a,
        Achievement = 0xe31b1a36,
        Light = 0x92e49ae9,
        Sound = 0xe1dd99ed,
        Physics = 0x4589a3c,
        ShaderMap = 0x83c748ab,
        Aspect = 0x903e859f,
        wWiseSoundBank = 0x183c321c,
        LevelArea = 0xa2e8593a,
        Emblem = 0xd93022f1,
        PlayerClass = 0x5e3975fa,
        Texture = 0xcfe7639c,
        Font = 0x25797ce1,
        Movie = 0x67efd8d0,
        AudioContext = 0x95b4c61f,
        QuestChain = 0x54bd74db,
        ItemRequirement = 0x494cd38c,
        Wall = 0x81fe2166,
        Surface = 0x35b8be2f,
        Animation2D = 0xa1c3db4b,
        HairColor = 0x53332438,
        Global = 0x9065437b,
        Shake = 0xb1778d32,
        Explosion = 0xbb9eb8a6,
        VectorField = 0x813ea8cc,
        MarkerSet = 0x458e1366,
        HairStyle = 0x4bc3f828,
        AnimSet = 0xec084b75,
        SkillKit = 0x83d037d,
        MarkingColor = 0x69e2ace4
    }

    #endregion

    #region Structs

    /// <summary>
    /// Generic header shared by all SNO files.
    /// </summary>
    public readonly struct SnoFileHeader
    {
        public const int Size = 16;

        public uint Signature { get; }   // 0xDEADBEEF, same as D3.
        public SnoFormatDefinition FormatHash { get; }
        public int Dummy { get; }      // 0
        public int XmlHash { get; }

        public SnoFileHeader(BinaryReader reader)
        {
            Signature = reader.ReadUInt32();
            if (Signature != 0xDEADBEEF) throw new("Invalid SNO file header.");

            FormatHash = (SnoFormatDefinition)reader.ReadUInt32();
            Dummy = reader.ReadInt32();
            XmlHash = reader.ReadInt32();
        }
    }

    /// <summary>
    /// A struct that points to serialized data.
    /// </summary>
    public readonly struct SerializeData
    {
        public int Offset { get; }
        public int SizeAndFlags { get; }

        public SerializeData(BinaryReader reader)
        {
            Offset = reader.ReadInt32();
            SizeAndFlags = reader.ReadInt32();
        }
    }

    public readonly struct Rgba
    {
        public float R { get; }
        public float G { get; }
        public float B { get; }
        public float A { get; }

        public Rgba(BinaryReader reader)
        {
            R = reader.ReadSingle();
            G = reader.ReadSingle();
            B = reader.ReadSingle();
            A = reader.ReadSingle();
        }
    }

    public readonly struct UInt16Vector2
    {
        public ushort X { get; }
        public ushort Y { get; }

        public UInt16Vector2(BinaryReader reader)
        {
            X = reader.ReadUInt16();
            Y = reader.ReadUInt16();
        }
    }

    #endregion
}

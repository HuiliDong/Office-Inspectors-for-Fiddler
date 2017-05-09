//-----------------------------------------------------------------------
// Copyright (c) 2013 Microsoft Corporation. All rights reserved.
// Use of this sample source code is subject to the terms of the Microsoft license 
// agreement under which you licensed this sample source code and is provided AS-IS.
// If you did not accept the terms of the license agreement, you are not authorized 
// to use this sample source code. For the terms of the license, please see the 
// license agreement between you and Microsoft.
//-----------------------------------------------------------------------

namespace FSSHTTPandWOPIInspector.Parsers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows.Forms;
    /// <summary>
    /// 2.2.1.1	Compact Unsigned 64-bit Integer
    /// </summary>
    public class CompactUnsigned64bitInteger : BaseStructure
    {
        /// <summary>
        /// Parse the CompactUnsigned64bitInteger structure.
        /// </summary>
        /// <param name="s">An stream containing CompactUnsigned64bitInteger structure.</param>
        public CompactUnsigned64bitInteger TryParse(Stream s)
        {
            base.Parse(s);
            byte temp = ReadByte();
            s.Position -= 1;
            CompactUnsigned64bitInteger compactUint64 = new CompactUnsigned64bitInteger();
            if (temp == 0x0)
            {
                compactUint64 = new CompactUintZero();
                compactUint64.Parse(s);
            }
            else if ((temp & 0x01) == 0x01)
            {
                compactUint64 = new CompactUint7bitvalues();
                compactUint64.Parse(s);
            }
            else if ((temp & 0x03) == 0x02)
            {
                compactUint64 = new CompactUint14bitvalues();
                compactUint64.Parse(s);
            }
            else if ((temp & 0x07) == 0x04)
            {
                compactUint64 = new CompactUint21bitvalues();
                compactUint64.Parse(s);
            }
            else if ((temp & 0x0F) == 0x08)
            {
                compactUint64 = new CompactUint28bitvalues();
                compactUint64.Parse(s);
            }
            else if ((temp & 0x1F) == 0x10)
            {
                compactUint64 = new CompactUint35bitvalues();
                compactUint64.Parse(s);
            }
            else if ((temp & 0x3F) == 0x20)
            {
                compactUint64 = new CompactUint42bitvalues();
                compactUint64.Parse(s);
            }
            else if ((temp & 0x7F) == 0x40)
            {
                compactUint64 = new CompactUint49bitvalues();
                compactUint64.Parse(s);
            }
            else if (temp == 0x80)
            {
                compactUint64 = new CompactUint64bitvalues();
                compactUint64.Parse(s);
            }
            return compactUint64;
        }

        /// <summary>
        /// get Uint feild in CompactUnsigned64bitInteger structure.
        /// </summary>
        /// <returns>The value of Uint feild</returns>
        public ulong GetUint(CompactUnsigned64bitInteger objectVal)
        {
            if (objectVal is CompactUintZero)
            {
                return (objectVal as CompactUintZero).Uint;
            }
            else if (objectVal is CompactUint7bitvalues)
            {
                return (objectVal as CompactUint7bitvalues).Uint;
            }
            else if (objectVal is CompactUint14bitvalues)
            {
                return (objectVal as CompactUint14bitvalues).Uint;
            }
            else if (objectVal is CompactUint21bitvalues)
            {
                return (objectVal as CompactUint21bitvalues).Uint;
            }
            else if (objectVal is CompactUint28bitvalues)
            {
                return (objectVal as CompactUint28bitvalues).Uint;
            }
            else if (objectVal is CompactUint35bitvalues)
            {
                return (objectVal as CompactUint35bitvalues).Uint;
            }
            else if (objectVal is CompactUint42bitvalues)
            {
                return (objectVal as CompactUint42bitvalues).Uint;
            }
            else if (objectVal is CompactUint49bitvalues)
            {
                return (objectVal as CompactUint49bitvalues).Uint;
            }
            else if (objectVal is CompactUint64bitvalues)
            {
                return (objectVal as CompactUint64bitvalues).Uint;
            }
            else
            {
                throw new Exception("The CompactUnsigned64bitInteger type is not right.");
            }
        }
    }

    /// <summary>
    /// 2.2.1.1.1	Compact Uint Zero
    /// </summary>
    public class CompactUintZero : CompactUnsigned64bitInteger
    {
        public byte Uint;

        /// <summary>
        /// Parse the CompactUintZero structure.
        /// </summary>
        /// <param name="s">An stream containing CompactUintZero structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            int Uint = ReadByte();
        }
    }

    /// <summary>
    /// 2.2.1.1.2	Compact Uint 7 bit values
    /// </summary>
    public class CompactUint7bitvalues : CompactUnsigned64bitInteger
    {
        [BitAttribute(1)]
        public byte A;
        [BitAttribute(7)]
        public byte Uint;

        /// <summary>
        /// Parse the CompactUint7bitvalues structure.
        /// </summary>
        /// <param name="s">An stream containing CompactUint7bitvalues structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            int index = 0;
            int temp = ReadByte();
            this.A = (byte)GetBits(temp, index, 1);
            index = index + 1;
            this.Uint = (byte)GetBits(temp, index, 7);
        }
    }

    /// <summary>
    /// 2.2.1.1.3	Compact Uint 14 bit values
    /// </summary>
    public class CompactUint14bitvalues : CompactUnsigned64bitInteger
    {
        [BitAttribute(2)]
        public byte A;
        [BitAttribute(14)]
        public ushort Uint;

        /// <summary>
        /// Parse the CompactUint14bitvalues structure.
        /// </summary>
        /// <param name="s">An stream containing CompactUint14bitvalues structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            int index = 0;
            int temp = ReadINT16();
            this.A = (byte)GetBits(temp, index, 2);
            index = index + 2;
            this.Uint = (ushort)GetBits(temp, index, 14);
        }
    }

    /// <summary>
    /// 2.2.1.1.4	Compact Uint 21 bit values
    /// </summary>
    public class CompactUint21bitvalues : CompactUnsigned64bitInteger
    {
        [BitAttribute(3)]
        public byte A;
        [BitAttribute(21)]
        public uint Uint;

        /// <summary>
        /// Parse the CompactUint21bitvalues structure.
        /// </summary>
        /// <param name="s">An stream containing CompactUint21bitvalues structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            int index = 0;
            int temp = Read3Bytes();
            this.A = (byte)GetBits(temp, index, 3);
            index = index + 3;
            this.Uint = (uint)GetBits(temp, index, 21);
        }
    }

    /// <summary>
    /// 2.2.1.1.5	Compact Uint 28 bit values
    /// </summary>
    public class CompactUint28bitvalues : CompactUnsigned64bitInteger
    {
        [BitAttribute(4)]
        public byte A;
        [BitAttribute(28)]
        public uint Uint;

        /// <summary>
        /// Parse the CompactUint28bitvalues structure.
        /// </summary>
        /// <param name="s">An stream containing CompactUint28bitvalues structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            int index = 0;
            uint temp = ReadUint();
            this.A = (byte)GetBits(temp, index, 4);
            index = index + 4;
            this.Uint = (uint)GetBits(temp, index, 28);
        }
    }

    /// <summary>
    /// 2.2.1.1.6	Compact Uint 35 bit values
    /// </summary>
    public class CompactUint35bitvalues : CompactUnsigned64bitInteger
    {
        [BitAttribute(5)]
        public byte A;
        [BitAttribute(35)]
        public ulong Uint;

        /// <summary>
        /// Parse the CompactUint35bitvalues structure.
        /// </summary>
        /// <param name="s">An stream containing CompactUint35bitvalues structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            int index = 0;
            long temp = Read5Bytes();
            this.A = (byte)GetBits(temp, index, 5);
            index = index + 5;
            this.Uint = (ulong)GetBits(temp, index, 35);
        }
    }

    /// <summary>
    /// 2.2.1.1.7	Compact Uint 42 bit values
    /// </summary>
    public class CompactUint42bitvalues : CompactUnsigned64bitInteger
    {
        [BitAttribute(6)]
        public byte A;
        [BitAttribute(42)]
        public ulong Uint;

        /// <summary>
        /// Parse the CompactUint42bitvalues structure.
        /// </summary>
        /// <param name="s">An stream containing CompactUint42bitvalues structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            int index = 0;
            long temp = Read6Bytes();
            this.A = (byte)GetBits(temp, index, 6);
            index = index + 6;
            this.Uint = (ulong)GetBits(temp, index, 42);
        }
    }

    /// <summary>
    /// 2.2.1.1.8	Compact Uint 49 bit values
    /// </summary>
    public class CompactUint49bitvalues : CompactUnsigned64bitInteger
    {
        [BitAttribute(7)]
        public byte A;
        [BitAttribute(49)]
        public ulong Uint;

        /// <summary>
        /// Parse the CompactUint49bitvalues structure.
        /// </summary>
        /// <param name="s">An stream containing CompactUint49bitvalues structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            int index = 0;
            long temp = Read7Bytes();
            this.A = (byte)GetBits(temp, index, 7);
            index = index + 7;
            this.Uint = (ulong)GetBits(temp, index, 49);
        }
    }

    /// <summary>
    /// 2.2.1.1.9	Compact Uint 64 bit values
    /// </summary>
    public class CompactUint64bitvalues : CompactUnsigned64bitInteger
    {
        public byte A;
        public ulong Uint;

        /// <summary>
        /// Parse the CompactUint64bitvalues structure.
        /// </summary>
        /// <param name="s">An stream containing CompactUint64bitvalues structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.A = ReadByte();
            this.Uint = ReadUlong();
        }
    }

    /// <summary>
    /// 2.2.1.2	File Chunk Reference
    /// </summary>
    public class FileChunkReference : BaseStructure
    {
        public CompactUnsigned64bitInteger Start;
        public CompactUnsigned64bitInteger Length;

        /// <summary>
        /// Parse the FileChunkReference structure.
        /// </summary>
        /// <param name="s">An stream containing FileChunkReference structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.Start = new CompactUnsigned64bitInteger();
            this.Start = this.Start.TryParse(s);
            this.Length = new CompactUnsigned64bitInteger();
            this.Length = this.Length.TryParse(s);
        }
    }

    /// <summary>
    /// 2.2.1.3	Binary Item
    /// </summary>
    public class BinaryItem : BaseStructure
    {
        public CompactUnsigned64bitInteger Length;
        public byte[] Content;

        /// <summary>
        /// Parse the StringItem structure.
        /// </summary>
        /// <param name="s">An stream containing StringItem structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.Length = new CompactUnsigned64bitInteger();
            this.Length = this.Length.TryParse(s);
            this.Content = ReadBytes((int)Length.GetUint(Length));
        }
    }

    /// <summary>
    /// Section 2.2.1.4   String Item
    /// </summary>
    public class StringItem : BaseStructure
    {
        public CompactUnsigned64bitInteger Count;
        public string Content;

        /// <summary>
        /// Parse the StringItem structure.
        /// </summary>
        /// <param name="s">An stream containing StringItem structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.Count = new CompactUnsigned64bitInteger();
            this.Count = this.Count.TryParse(s);
            this.Content = ReadString(System.Text.Encoding.Unicode, "", (int)Count.GetUint(Count));
        }
    }

    /// <summary>
    /// 2.2.1.5	Stream Object Header
    /// </summary>
    public class StreamObjectHeader : BaseStructure
    {
        /// <summary>
        /// Parse the StreamObjectHeader structure.
        /// </summary>
        /// <param name="s">An stream containing StreamObjectHeader structure.</param>
        public StreamObjectHeader TryParse(Stream s)
        {
            base.Parse(s);
            byte temp = ReadByte();
            s.Position -= 1;
            StreamObjectHeader streamObjectHeader = new StreamObjectHeader();
            if ((temp & 0x03) == 0x0)
            {
                streamObjectHeader = new bit16StreamObjectHeaderStart();
                streamObjectHeader.Parse(s);
            }
            else if ((temp & 0x03) == 0x02)
            {
                streamObjectHeader = new bit32StreamObjectHeaderStart();
                streamObjectHeader.Parse(s);
            }
            return streamObjectHeader;
        }

        /// <summary>
        /// get Uint feild in StreamObjectHeader structure.
        /// </summary>
        /// <returns>The value of Uint feild</returns>
        public ulong GetUint(CompactUnsigned64bitInteger objectVal)
        {
            if (objectVal is CompactUintZero)
            {
                return (objectVal as CompactUintZero).Uint;
            }
            else if (objectVal is CompactUint7bitvalues)
            {
                return (objectVal as CompactUint7bitvalues).Uint;
            }
            else if (objectVal is CompactUint14bitvalues)
            {
                return (objectVal as CompactUint14bitvalues).Uint;
            }
            else if (objectVal is CompactUint21bitvalues)
            {
                return (objectVal as CompactUint21bitvalues).Uint;
            }
            else if (objectVal is CompactUint28bitvalues)
            {
                return (objectVal as CompactUint28bitvalues).Uint;
            }
            else if (objectVal is CompactUint35bitvalues)
            {
                return (objectVal as CompactUint35bitvalues).Uint;
            }
            else if (objectVal is CompactUint42bitvalues)
            {
                return (objectVal as CompactUint42bitvalues).Uint;
            }
            else if (objectVal is CompactUint49bitvalues)
            {
                return (objectVal as CompactUint49bitvalues).Uint;
            }
            else if (objectVal is CompactUint64bitvalues)
            {
                return (objectVal as CompactUint64bitvalues).Uint;
            }
            else
            {
                throw new Exception("The CompactUnsigned64bitInteger type is not right.");
            }
        }
    }

    /// <summary>
    /// 2.2.1.5.1	16-bit Stream Object Header Start
    /// </summary>
    public class bit16StreamObjectHeaderStart : StreamObjectHeader
    {
        [BitAttribute(2)]
        public byte A;
        [BitAttribute(1)]
        public byte B;
        [BitAttribute(6)]
        public StreamObjectTypeHeaderStart Type;
        [BitAttribute(7)]
        public byte Length;

        /// <summary>
        /// Parse the bit16StreamObjectHeaderStart structure.
        /// </summary>
        /// <param name="s">An stream containing bit16StreamObjectHeaderStart structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            int index = 0;
            short temp = ReadINT16();
            this.A = (byte)GetBits(temp, index, 2);
            index = index + 2;
            this.B = (byte)GetBits(temp, index, 1);
            index = index + 1;
            this.Type = (StreamObjectTypeHeaderStart)GetBits(temp, index, 6);
            index = index + 6;
            this.Length = (byte)GetBits(temp, index, 7);
        }
    }

    /// <summary>
    /// 2.2.1.5.2	32-bit Stream Object Header Start
    /// </summary>
    public class bit32StreamObjectHeaderStart : StreamObjectHeader
    {
        [BitAttribute(2)]
        public byte A;
        [BitAttribute(1)]
        public byte B;
        [BitAttribute(14)]
        public StreamObjectTypeHeaderStart Type;
        [BitAttribute(15)]
        public short Length;
        public CompactUnsigned64bitInteger LargeLength;

        /// <summary>
        /// Parse the bit32StreamObjectHeaderStart structure.
        /// </summary>
        /// <param name="s">An stream containing bit32StreamObjectHeaderStart structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            int index = 0;
            uint temp = ReadUint();
            this.A = (byte)GetBits(temp, index, 2);
            index = index + 2;
            this.B = (byte)GetBits(temp, index, 1);
            index = index + 1;
            this.Type = (StreamObjectTypeHeaderStart)GetBits(temp, index, 14);
            index = index + 14;
            this.Length = (short)GetBits(temp, index, 15);

            if (this.Length == 32767)
            {
                this.LargeLength = new CompactUnsigned64bitInteger();
                this.LargeLength = this.LargeLength.TryParse(s);
            }
        }
        /// <summary>
        /// Get the Data length of the data that with bit32StreamObjectHeaderStart
        /// </summary>
        /// <returns>the length of data</returns>
        public int GetDataLength()
        {
            if (this.Length != 32767)
                return (int)this.Length;
            else
                return (int)this.LargeLength.GetUint(this.LargeLength);
        }
    }

    /// <summary>
    /// 2.2.1.5.3	8-bit Stream Object Header End
    /// </summary>
    public class bit8StreamObjectHeaderEnd : StreamObjectHeader
    {
        [BitAttribute(2)]
        public byte A;
        [BitAttribute(6)]
        public StreamObjectTypeHeaderEnd Type;

        /// <summary>
        /// Parse the bit8StreamObjectHeaderEnd structure.
        /// </summary>
        /// <param name="s">An stream containing bit8StreamObjectHeaderEnd structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            int index = 0;
            byte temp = ReadByte();
            this.A = (byte)GetBits(temp, index, 2);
            index = index + 2;
            this.Type = (StreamObjectTypeHeaderEnd)GetBits(temp, index, 6);
        }
    }

    /// <summary>
    /// 2.2.1.5.4	16-bit Stream Object Header End
    /// </summary>
    public class bit16StreamObjectHeaderEnd : StreamObjectHeader
    {
        [BitAttribute(2)]
        public byte A;
        [BitAttribute(14)]
        public StreamObjectTypeHeaderEnd Type;

        /// <summary>
        /// Parse the bit16StreamObjectHeaderEnd structure.
        /// </summary>
        /// <param name="s">An stream containing bit16StreamObjectHeaderEnd structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            int index = 0;
            short temp = ReadINT16();
            this.A = (byte)GetBits(temp, index, 2);
            index = index + 2;
            this.Type = (StreamObjectTypeHeaderEnd)GetBits(temp, index, 14);
        }
    }

    /// <summary>
    /// The enumeration of the stream object type header start
    /// </summary>
    public enum StreamObjectTypeHeaderStart
    {
        Unknown = 0x4E,
        DataElement = 0x01,
        ObjectDataBLOB = 0x02,
        ObjectGroupObjectExcludedData = 0x03,
        WaterlineKnowledgeEntry = 0x04,
        ObjectGroupObjectBLOBDataDeclaration = 0x05,
        DataElementHash = 0x06,
        StorageManifestRootDeclare = 0x07,
        RevisionManifestRootDeclare = 0x0A,
        CellManifestCurrentRevision = 0x0B,
        StorageManifestSchemaGUID = 0x0C,
        StorageIndexRevisionMapping = 0x0D,
        StorageIndexCellMapping = 0x0E,
        CellKnowledgeRange = 0x0F,
        Knowledge = 0x10,
        StorageIndexManifestMapping = 0x11,
        CellKnowledge = 0x14,
        DataElementPackage = 0x15,
        ObjectGroupObjectData = 0x16,
        CellKnowledgeEntry = 0x17,
        ObjectGroupObjectDeclare = 0x18,
        RevisionManifestObjectGroupReferences = 0x19,
        RevisionManifest = 0x1A,
        ObjectGroupObjectDataBLOBReference = 0x1C,
        ObjectGroupDeclarations = 0x1D,
        ObjectGroupData = 0x1E,
        LeafNodeObject = 0x1F, // Defined in MS-FSSHTTPD
        IntermediateNodeObject = 0x20, // Defined in MS-FSSHTTPD
        SignatureObject = 0x21, // Defined in MS-FSSHTTPD
        DataSizeObject = 0x22, // Defined in MS-FSSHTTPD
        WaterlineKnowledge = 0x29,
        ContentTagKnowledge = 0x2D,
        ContentTagKnowledgeEntry = 0x2E,
        Request = 0x040,
        FsshttpbSubResponse = 0x041,
        SubRequest = 0x042,
        ReadAccessResponse = 0x043,
        SpecializedKnowledge = 0x044,
        PutChangesResponseSerialNumberReassignAll = 0x045,
        WriteAccessResponse = 0x046,
        QueryChangesFilter = 0x047,
        Win32Error = 0x049,
        ProtocolError = 0x04B,
        ResponseError = 0x04D,
        UserAgentversion = 0x04F,
        QueryChangesFilterSchemaSpecific = 0x050,
        QueryChangesRequest = 0x051,
        HRESULTError = 0x052,
        PutChangesResponseSerialNumberReassign = 0x053,
        QueryChangesFilterDataElementIDs = 0x054,
        UserAgentGUID = 0x055,
        QueryChangesFilterDataElementType = 0x057,
        QueryChangesDataConstraint = 0x059,
        PutChangesRequest = 0x05A,
        QueryChangesRequestArguments = 0x05B,
        QueryChangesFilterCellID = 0x05C,
        UserAgent = 0x05D,
        QueryChangesResponse = 0x05F,
        QueryChangesFilterHierarchy = 0x060,
        FsshttpbResponse = 0x062,
        QueryDataElementRequest = 0x065,
        CellError = 0x066,
        QueryChangesFilterFlags = 0x068,
        DataElementFragment = 0x06A,
        FragmentKnowledge = 0x06B,
        FragmentKnowledgeEntry = 0x06C,
        ObjectGroupMetadataDeclarations = 0x79,
        ObjectGroupMetadata = 0x78,
        AllocateExtendedGUIDRangeRequest = 0x080,
        AllocateExtendedGUIDRangeResponse = 0x081,
        TargetPartitionId = 0x083,
        PutChangesLockId = 0x085,
        AdditionalFlags = 0x086,
        PutChangesResponse = 0x087,
        RequestHashOptions = 0x088,
        DiagnosticRequestOptionOutput = 0x089,
        DiagnosticRequestOptionInput = 0x08A,
        UserAgentClientandPlatform = 0x08B,
        UnknowKnowledge = 0x08C,
    }

    /// <summary>
    /// The enumeration of the stream object type header end
    /// </summary>
    public enum StreamObjectTypeHeaderEnd
    {
        DataElement = 0x01,
        Knowledge = 0x10,
        CellKnowledge = 0x14,
        DataElementPackage = 0x15,
        ObjectGroupDeclarations = 0x1D,
        ObjectGroupData = 0x1E,
        LeafNodeEnd = 0x1F, // Defined in MS-FSSHTTPD
        IntermediateNodeEnd = 0x20, // Defined in MS-FSSHTTPD
        WaterlineKnowledge = 0x29,
        ContentTagKnowledge = 0x2D,
        Request = 0x040,
        SubResponse = 0x041,
        SubRequest = 0x042,
        ReadAccessResponse = 0x043,
        SpecializedKnowledge = 0x044,
        WriteAccessResponse = 0x046,
        QueryChangesFilter = 0x047,
        Error = 0x04D,
        QueryChangesRequest = 0x051,
        UserAgent = 0x05D,
        Response = 0x062,
        FragmentKnowledge = 0x06B,
        ObjectGroupMetadataDeclarations = 0x79,
        TargetPartitionId = 0x083
    }

    /// <summary>
    /// The enumeration of request type.
    /// </summary>
    public enum RequestTypes
    {
        QueryAccess = 1,
        QueryChanges = 2,
        QueryKnowledge = 3,
        PutChanges = 5,
        QueryRawStorage = 6,
        PutRawStorage = 7,
        QueryDiagnosticStoreInfo = 8,
        AllocateExtendedGuidRange = 11
    }

    /// <summary>
    /// 2.2.1.7	Extended GUID
    /// </summary>
    public class ExtendedGUID : BaseStructure
    {
        /// <summary>
        /// Parse the ExtendedGUID structure.
        /// </summary>
        /// <param name="s">An stream containing ExtendedGUID structure.</param>
        public ExtendedGUID TryParse(Stream s)
        {
            base.Parse(s);
            byte temp = ReadByte();
            s.Position -= 1;
            ExtendedGUID extendedGUID = new ExtendedGUID();
            if (temp == 0x0)
            {
                extendedGUID = new ExtendedGUIDNullValue();
                extendedGUID.Parse(s);
            }
            else if ((temp & 0x07) == 0x4)
            {
                extendedGUID = new ExtendedGUID5BitUintValue();
                extendedGUID.Parse(s);
            }
            else if ((temp & 0x3F) == 0x20)
            {
                extendedGUID = new ExtendedGUID10BitUintValue();
                extendedGUID.Parse(s);
            }
            else if ((temp & 0x7F) == 0x40)
            {
                extendedGUID = new ExtendedGUID17BitUintValue();
                extendedGUID.Parse(s);
            }
            else if (temp == 0x80)
            {
                extendedGUID = new ExtendedGUID32BitUintValue();
                extendedGUID.Parse(s);
            }
            return extendedGUID;
        }
    }

    /// <summary>
    /// 2.2.1.7.1	Extended GUID Null Value
    /// </summary>
    public class ExtendedGUIDNullValue : ExtendedGUID
    {
        public byte Type;

        /// <summary>
        /// Parse the ExtendedGUIDNullValue structure.
        /// </summary>
        /// <param name="s">An stream containing ExtendedGUIDNullValue structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.Type = ReadByte();
        }
    }

    /// <summary>
    /// 2.2.1.7.2	Extended GUID 5 Bit Uint Value
    /// </summary>
    public class ExtendedGUID5BitUintValue : ExtendedGUID
    {
        [BitAttribute(3)]
        public byte Type;
        [BitAttribute(5)]
        public byte Value;
        public Guid GUID;

        /// <summary>
        /// Parse the ExtendedGUID5BitUintValue structure.
        /// </summary>
        /// <param name="s">An stream containing ExtendedGUID5BitUintValue structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            byte tempByte = ReadByte();
            int index = 0;
            this.Type = GetBits(tempByte, index, 3);
            index += 3;
            this.Value = GetBits(tempByte, index, 5);
            this.GUID = ReadGuid();
        }
    }

    /// <summary>
    /// 2.2.1.7.3	Extended GUID 10 Bit Uint Value
    /// </summary>
    public class ExtendedGUID10BitUintValue : ExtendedGUID
    {
        [BitAttribute(6)]
        public ushort Type;
        [BitAttribute(10)]
        public ushort Value;
        public Guid GUID;

        /// <summary>
        /// Parse the ExtendedGUID10BitUintValue structure.
        /// </summary>
        /// <param name="s">An stream containing ExtendedGUID10BitUintValue structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            short temp = ReadINT16();
            int index = 0;
            this.Type = GetBits(temp, index, 6);
            index += 6;
            this.Value = GetBits(temp, index, 10);
            this.GUID = ReadGuid();
        }
    }

    /// <summary>
    /// 2.2.1.7.4	Extended GUID 17 Bit Uint Value
    /// </summary>
    public class ExtendedGUID17BitUintValue : ExtendedGUID
    {
        [BitAttribute(7)]
        public uint Type;
        [BitAttribute(17)]
        public uint Value;
        public Guid GUID;

        /// <summary>
        /// Parse the ExtendedGUID17BitUintValue structure.
        /// </summary>
        /// <param name="s">An stream containing ExtendedGUID17BitUintValue structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            int temp = Read3Bytes();
            int index = 0;
            this.Type = GetBits(temp, index, 7);
            index += 7;
            this.Value = GetBits(temp, index, 17);
            this.GUID = ReadGuid();
        }
    }

    /// <summary>
    /// 2.2.1.7.5	Extended GUID 32 Bit Uint Value
    /// </summary>
    public class ExtendedGUID32BitUintValue : ExtendedGUID
    {
        public byte Type;
        public uint Value;
        public Guid GUID;

        /// <summary>
        /// Parse the ExtendedGUID32BitUintValue structure.
        /// </summary>
        /// <param name="s">An stream containing ExtendedGUID32BitUintValue structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.Type = ReadByte();
            this.Value = ReadUint();
            this.GUID = ReadGuid();
        }
    }

    /// <summary>
    /// 2.2.1.8	Extended GUID Array
    /// </summary>
    public class ExtendedGUIDArray : BaseStructure
    {
        public CompactUnsigned64bitInteger Count;
        public ExtendedGUID[] Content;

        /// <summary>
        /// Parse the ExtendedGUIDArray structure.
        /// </summary>
        /// <param name="s">An stream containing ExtendedGUIDArray structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.Count = new CompactUnsigned64bitInteger();
            this.Count = this.Count.TryParse(s);
            List<ExtendedGUID> tempContent = new List<ExtendedGUID>();
            if (this.Count.GetUint(Count) > 0)
            {
                ulong tempCount = this.Count.GetUint(Count);
                ExtendedGUID tempGuid = new ExtendedGUID();
                do
                {
                    tempGuid = tempGuid.TryParse(s);
                    tempContent.Add(tempGuid);
                    tempCount--;
                } while (tempCount > 0);
                this.Content = tempContent.ToArray();
            }
        }
    }

    /// <summary>
    /// 2.2.1.9	Serial Number
    /// </summary>
    public class SerialNumber : BaseStructure
    {
        /// <summary>
        /// Parse the SerialNumber structure.
        /// </summary>
        /// <param name="s">An stream containing SerialNumber structure.</param>
        public SerialNumber TryParse(Stream s)
        {
            base.Parse(s);
            byte temp = ReadByte();
            s.Position -= 1;
            SerialNumber serialNumber = new SerialNumber();
            if (temp == 0x0)
            {
                serialNumber = new SerialNumberNullValue();
                serialNumber.Parse(s);
            }
            else if (temp == 0x80)
            {
                serialNumber = new SerialNumber64BitUintValue();
                serialNumber.Parse(s);
            }
            return serialNumber;
        }
    }

    /// <summary>
    /// 2.2.1.9.1	Serial Number Null Value
    /// </summary>
    public class SerialNumberNullValue : SerialNumber
    {
        public byte Type;

        /// <summary>
        /// Parse the SerialNumberNullValue structure.
        /// </summary>
        /// <param name="s">An stream containing SerialNumberNullValue structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.Type = ReadByte();
        }
    }

    /// <summary>
    /// 2.2.1.9.2	Serial Number 64 Bit Uint Value
    /// </summary>
    public class SerialNumber64BitUintValue : SerialNumber
    {
        public byte Type;
        public Guid GUID;
        public ulong Value;

        /// <summary>
        /// Parse the SerialNumber64BitUintValue structure.
        /// </summary>
        /// <param name="s">An stream containing SerialNumber64BitUintValue structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.Type = ReadByte();
            this.GUID = ReadGuid();
            this.Value = ReadUlong();
        }
    }

    /// <summary>
    /// 2.2.1.10	Cell ID
    /// </summary>
    public class CellID : BaseStructure
    {
        public ExtendedGUID EXGUID1;
        public ExtendedGUID EXGUID2;

        /// <summary>
        /// Parse the CellID structure.
        /// </summary>
        /// <param name="s">An stream containing CellID structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.EXGUID1 = new ExtendedGUID();
            this.EXGUID1 = this.EXGUID1.TryParse(s);
            this.EXGUID2 = new ExtendedGUID();
            this.EXGUID2 = this.EXGUID2.TryParse(s);
        }
    }

    /// <summary>
    /// 2.2.1.11	Cell ID Array
    /// </summary>
    public class CellIDArray : BaseStructure
    {
        public CompactUnsigned64bitInteger Count;
        public CellID[] Content;

        /// <summary>
        /// Parse the CellIDArray structure.
        /// </summary>
        /// <param name="s">An stream containing CellIDArray structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.Count = new CompactUnsigned64bitInteger();
            this.Count = this.Count.TryParse(s);
            List<CellID> tempContent = new List<CellID>();
            if (this.Count.GetUint(Count) > 0)
            {
                ulong tempCount = this.Count.GetUint(Count);
                CellID tempGuid = new CellID();
                do
                {
                    tempGuid.Parse(s);
                    tempContent.Add(tempGuid);
                    tempCount--;
                } while (tempCount > 0);
                this.Content = tempContent.ToArray();
            }
        }
    }

    /// <summary>
    /// 2.2.1.12	Data Element Package
    /// </summary>
    public class DataElementPackage : BaseStructure
    {
        public bit16StreamObjectHeaderStart DataElementPackageStart;
        public byte Reserved;
        public object[] DataElements;
        public bit8StreamObjectHeaderEnd DataElementPackageEnd;

        /// <summary>
        /// Parse the DataElementPackage structure.
        /// </summary>
        /// <param name="s">An stream containing DataElementPackage structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.DataElementPackageStart = new bit16StreamObjectHeaderStart();
            this.DataElementPackageStart.Parse(s);
			ValidationChecker.ValidationCheckHeader(this.DataElementPackageStart, 0x15, "Data Element Package Start");
            this.Reserved = ReadByte();

            long DataElementPackageType = PreReadDataElementPackageType();
            List<object> DataElementsList = new List<object>();

            while (ContainsStreamObjectStart16BitHeader(0x01) && (DataElementPackageType == 0x01 || DataElementPackageType == 0x02 ||
                   DataElementPackageType == 0x03 || DataElementPackageType == 0x04 || DataElementPackageType == 0x05 ||
                   DataElementPackageType == 0x06 || DataElementPackageType == 0x0A))
            {

                switch (DataElementPackageType)
                {
                    case 0x01:
                        {
                            StorageIndexDataElement StorageIndex = new StorageIndexDataElement();
                            StorageIndex.Parse(s);
                            DataElementsList.Add(StorageIndex);
                            break;
                        }
                    case 0x02:
                        {
                            StorageManifestDataElement StorageManifest = new StorageManifestDataElement();
                            StorageManifest.Parse(s);
                            DataElementsList.Add(StorageManifest);
                            break;
                        }
                    case 0x03:
                        {
                            CellManifestDataElement CellManifest = new CellManifestDataElement();
                            CellManifest.Parse(s);
                            DataElementsList.Add(CellManifest);
                            break;
                        }
                    case 0x04:
                        {
                            RevisionManifestDataElement RevisionManifest = new RevisionManifestDataElement();
                            RevisionManifest.Parse(s);
                            DataElementsList.Add(RevisionManifest);
                            break;
                        }
                    case 0x05:
                        {
                            ObjectGroupDataElements ObjectGroup = new ObjectGroupDataElements();
                            ObjectGroup.Parse(s);
                            DataElementsList.Add(ObjectGroup);
                            break;
                        }
                    case 0x06:
                        {
                            DataElementFragmentDataElement DataElementFragment = new DataElementFragmentDataElement();
                            DataElementFragment.Parse(s);
                            DataElementsList.Add(DataElementFragment);
                            break;
                        }
                    case 0x0A:
                        {
                            ObjectDataBLOBDataElements ObjectDataBLOB = new ObjectDataBLOBDataElements();
                            ObjectDataBLOB.Parse(s);
                            DataElementsList.Add(ObjectDataBLOB);
                            break;
                        }
                    default:
                        throw new Exception("The DataElementPackageType is not right.");
                }
                DataElementPackageType = PreReadDataElementPackageType();
            }
            this.DataElements = DataElementsList.ToArray();
            this.DataElementPackageEnd = new bit8StreamObjectHeaderEnd();
            this.DataElementPackageEnd.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.DataElementPackageEnd, 0x15, "Data Element Package End");
        }
    }

    /// <summary>
    /// 2.2.1.12.1	Data Element Types
    /// </summary>
    public enum DataElementTypes
    {
        StorageIndex = 0x01,
        StorageManifest = 0x02,
        CellManifest = 0x03,
        RevisionManifest = 0x04,
        ObjectGroup = 0x05,
        DataElementFragment = 0x06,
        ObjectDataBLOB = 0x0A
    }

    /// <summary>
    /// 2.2.1.12.2	Storage Index Data Element
    /// </summary>
    public class StorageIndexDataElement : BaseStructure
    {
        public bit16StreamObjectHeaderStart DataElementStart;
        public ExtendedGUID DataElementExtendedGUID;
        public SerialNumber SerialNumber;
        public CompactUnsigned64bitInteger DataElementType;
        public object[] StorageIndexDataElementData;
        public bit8StreamObjectHeaderEnd DataElementEnd;

        /// <summary>
        /// Parse the StorageIndexDataElement structure.
        /// </summary>
        /// <param name="s">An stream containing StorageIndexDataElement structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.DataElementStart = new bit16StreamObjectHeaderStart();
            this.DataElementStart.Parse(s);
			ValidationChecker.ValidationCheckHeader(this.DataElementStart, 0x01, "Data Element Start");
            this.DataElementExtendedGUID = new ExtendedGUID();
            this.DataElementExtendedGUID = this.DataElementExtendedGUID.TryParse(s);
            this.SerialNumber = new SerialNumber();
            this.SerialNumber = this.SerialNumber.TryParse(s);
            this.DataElementType = new CompactUnsigned64bitInteger();
            this.DataElementType = this.DataElementType.TryParse(s);

            int StorageIndexType = (CurrentByte() >> 3) & 0x3F;
            List<object> DataList = new List<object>();
            while ((CurrentByte() & 0x03) == 0x0 && (StorageIndexType == 0x11 || StorageIndexType == 0x0E || StorageIndexType == 0x0D))
            {
                switch (StorageIndexType)
                {
                    case 0x11:
                        StorageIndexManifestMappingValues ManifestMappingValue = new StorageIndexManifestMappingValues();
                        ManifestMappingValue.Parse(s);
                        DataList.Add(ManifestMappingValue);
                        break;
                    case 0x0E:
                        StorageIndexCellMappingValues CellMappingValue = new StorageIndexCellMappingValues();
                        CellMappingValue.Parse(s);
                        DataList.Add(CellMappingValue);
                        break;
                    case 0x0D:
                        StorageIndexRevisionMappingValues RevisionMappingValue = new StorageIndexRevisionMappingValues();
                        RevisionMappingValue.Parse(s);
                        DataList.Add(RevisionMappingValue);
                        break;
                    default:
                        throw new Exception("The StorageIndexType is not right.");
                }
                StorageIndexType = (CurrentByte() >> 3) & 0x3F;
            }
            this.StorageIndexDataElementData = DataList.ToArray();
            this.DataElementEnd = new bit8StreamObjectHeaderEnd();
            this.DataElementEnd.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.DataElementEnd, 0x01, "Data Element End");
        }
    }

    /// <summary>
    /// This type is defined for section 2.2.1.12.2 Storage Index Data Element of manifest mapping values
    /// </summary>
    public class StorageIndexManifestMappingValues : BaseStructure
    {
        public bit16StreamObjectHeaderStart StorageIndexManifestMapping;
        public ExtendedGUID ManifestMappingExtendedGUID;
        public SerialNumber ManifestMappingSerialNumber;

        /// <summary>
        /// Parse the StorageIndexManifestMappingValues structure.
        /// </summary>
        /// <param name="s">An stream containing StorageIndexManifestMappingValues structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.StorageIndexManifestMapping = new bit16StreamObjectHeaderStart();
            this.StorageIndexManifestMapping.Parse(s);
			ValidationChecker.ValidationCheckHeader(this.StorageIndexManifestMapping, 0x11, "Storage Index Manifest Mapping");
            this.ManifestMappingExtendedGUID = new ExtendedGUID();
            this.ManifestMappingExtendedGUID = this.ManifestMappingExtendedGUID.TryParse(s);
            this.ManifestMappingSerialNumber = new SerialNumber();
            this.ManifestMappingSerialNumber = this.ManifestMappingSerialNumber.TryParse(s);
        }
    }

    /// <summary>
    /// This type is defined for section 2.2.1.12.2 Storage Index Data Element of cell mapping values
    /// </summary>
    public class StorageIndexCellMappingValues : BaseStructure
    {
        public bit16StreamObjectHeaderStart StorageIndexCellMapping;
        public CellID CellID;
        public ExtendedGUID CellMappingExtendedGUID;
        public SerialNumber CellMappingSerialNumber;

        /// <summary>
        /// Parse the StorageIndexCellMappingValues structure.
        /// </summary>
        /// <param name="s">An stream containing StorageIndexCellMappingValues structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.StorageIndexCellMapping = new bit16StreamObjectHeaderStart();
            this.StorageIndexCellMapping.Parse(s);
			ValidationChecker.ValidationCheckHeader(this.StorageIndexCellMapping, 0x0E, "Storage Index Cell Mapping");
            this.CellID = new CellID();
            this.CellID.Parse(s);
            this.CellMappingExtendedGUID = new ExtendedGUID();
            this.CellMappingExtendedGUID = this.CellMappingExtendedGUID.TryParse(s);
            this.CellMappingSerialNumber = new SerialNumber();
            this.CellMappingSerialNumber = this.CellMappingSerialNumber.TryParse(s);
        }
    }

    /// <summary>
    /// This type is defined for section 2.2.1.12.2 Storage Index Data Element of revising mapping values
    /// </summary>
    public class StorageIndexRevisionMappingValues : BaseStructure
    {
        public bit16StreamObjectHeaderStart StorageIndexRevisionMapping;
        public ExtendedGUID RevisionExtendedGUID;
        public ExtendedGUID RevisionMappingExtendedGUID;
        public SerialNumber RevisionMappingSerialNumber;

        /// <summary>
        /// Parse the StorageIndexRevisionMappingValues structure.
        /// </summary>
        /// <param name="s">An stream containing StorageIndexRevisionMappingValues structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.StorageIndexRevisionMapping = new bit16StreamObjectHeaderStart();
            this.StorageIndexRevisionMapping.Parse(s);
			ValidationChecker.ValidationCheckHeader(this.StorageIndexRevisionMapping, 0x0D, "Storage Index Revision Mapping");
            this.RevisionExtendedGUID = new ExtendedGUID();
            this.RevisionExtendedGUID = this.RevisionExtendedGUID.TryParse(s);
            this.RevisionMappingExtendedGUID = new ExtendedGUID();
            this.RevisionMappingExtendedGUID = this.RevisionMappingExtendedGUID.TryParse(s);
            this.RevisionMappingSerialNumber = new SerialNumber();
            this.RevisionMappingSerialNumber = this.RevisionMappingSerialNumber.TryParse(s);
        }
    }

    /// <summary>
    /// 2.2.1.12.3	Storage Manifest Data Element
    /// </summary>
    public class StorageManifestDataElement : BaseStructure
    {
        public bit16StreamObjectHeaderStart DataElementStart;
        public ExtendedGUID DataElementExtendedGUID;
        public SerialNumber SerialNumber;
        public CompactUnsigned64bitInteger DataElementType;
        public bit16StreamObjectHeaderStart StorageManifestSchemaGUID;
        public Guid GUID;
        public StorageManifestRootDeclareValues[] StorageManifestRootDeclare;
        public bit8StreamObjectHeaderEnd DataElementEnd;

        /// <summary>
        /// Parse the StorageManifestDataElement structure.
        /// </summary>
        /// <param name="s">An stream containing StorageManifestDataElement structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.DataElementStart = new bit16StreamObjectHeaderStart();
            this.DataElementStart.Parse(s);
			ValidationChecker.ValidationCheckHeader(this.DataElementStart, 0x01, "DataElement Start");
            this.DataElementExtendedGUID = new ExtendedGUID();
            this.DataElementExtendedGUID = this.DataElementExtendedGUID.TryParse(s);
            this.SerialNumber = new SerialNumber();
            this.SerialNumber = this.SerialNumber.TryParse(s);
            this.DataElementType = new CompactUnsigned64bitInteger();
            this.DataElementType = this.DataElementType.TryParse(s);
            this.StorageManifestSchemaGUID = new bit16StreamObjectHeaderStart();
            this.StorageManifestSchemaGUID.Parse(s);
			ValidationChecker.ValidationCheckHeader(this.StorageManifestSchemaGUID, 0x0C, "Storage Manifest Schema GUID");
            this.GUID = ReadGuid();
            List<StorageManifestRootDeclareValues> StorageManifestRootDeclareList = new List<StorageManifestRootDeclareValues>();
            while ((CurrentByte() & 0x03) == 0x0 && ((CurrentByte() >> 3) & 0x3F) == 0x07)
            {
                StorageManifestRootDeclareValues tempStorageManifestRootDeclare = new StorageManifestRootDeclareValues();
                tempStorageManifestRootDeclare.Parse(s);
                StorageManifestRootDeclareList.Add(tempStorageManifestRootDeclare);
            }
            this.StorageManifestRootDeclare = StorageManifestRootDeclareList.ToArray();
            this.DataElementEnd = new bit8StreamObjectHeaderEnd();
            this.DataElementEnd.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.DataElementEnd, 0x01, "DataElement End");
        }
    }

    /// <summary>
    /// This type is defined for section 2.2.1.12.3 Storage Manifest Data Element of root declare Values
    /// </summary>
    public class StorageManifestRootDeclareValues : BaseStructure
    {
        public bit16StreamObjectHeaderStart StorageManifestRootDeclare;
        public ExtendedGUID RootExtendedGUID;
        public CellID CellID;

        /// <summary>
        /// Parse the StorageManifestRootDeclareValues structure.
        /// </summary>
        /// <param name="s">An stream containing StorageManifestRootDeclareValues structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.StorageManifestRootDeclare = new bit16StreamObjectHeaderStart();
            this.StorageManifestRootDeclare.Parse(s);
			ValidationChecker.ValidationCheckHeader(this.StorageManifestRootDeclare, 0x07, "Storage Manifest Root Declare");
            this.RootExtendedGUID = new ExtendedGUID();
            this.RootExtendedGUID = this.RootExtendedGUID.TryParse(s);
            this.CellID = new CellID();
            this.CellID.Parse(s);
        }
    }

    /// <summary>
    /// 2.2.1.12.4	Cell Manifest Data Element
    /// </summary>
    public class CellManifestDataElement : BaseStructure
    {
        public bit16StreamObjectHeaderStart DataElementStart;
        public ExtendedGUID DataElementExtendedGUID;
        public SerialNumber SerialNumber;
        public CompactUnsigned64bitInteger DataElementType;
        public bit16StreamObjectHeaderStart CellManifestCurrentRevision;
        public ExtendedGUID CellManifestCurrentRevisionExtendedGUID;
        public bit8StreamObjectHeaderEnd DataElementEnd;

        /// <summary>
        /// Parse the CellManifestDataElement structure.
        /// </summary>
        /// <param name="s">An stream containing CellManifestDataElement structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.DataElementStart = new bit16StreamObjectHeaderStart();
            this.DataElementStart.Parse(s);
			ValidationChecker.ValidationCheckHeader(this.DataElementStart, 0x01, "DataElement Start");
            this.DataElementExtendedGUID = new ExtendedGUID();
            this.DataElementExtendedGUID = this.DataElementExtendedGUID.TryParse(s);
            this.SerialNumber = new SerialNumber();
            this.SerialNumber = this.SerialNumber.TryParse(s);
            this.DataElementType = new CompactUnsigned64bitInteger();
            this.DataElementType = this.DataElementType.TryParse(s);
            this.CellManifestCurrentRevision = new bit16StreamObjectHeaderStart();
            this.CellManifestCurrentRevision.Parse(s);
			ValidationChecker.ValidationCheckHeader(this.CellManifestCurrentRevision, 0x0B, "Cell Manifest Current Revision");
            this.CellManifestCurrentRevisionExtendedGUID = new ExtendedGUID();
            this.CellManifestCurrentRevisionExtendedGUID = this.CellManifestCurrentRevisionExtendedGUID.TryParse(s);
            this.DataElementEnd = new bit8StreamObjectHeaderEnd();
            this.DataElementEnd.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.DataElementEnd, 0x01, "DataElement End");
        }
    }

    /// <summary>
    /// 2.2.1.12.5	Revision Manifest Data Elements
    /// </summary>
    public class RevisionManifestDataElement : BaseStructure
    {
        public bit16StreamObjectHeaderStart DataElementStart;
        public ExtendedGUID DataElementExtendedGUID;
        public SerialNumber SerialNumber;
        public CompactUnsigned64bitInteger DataElementType;
        public bit16StreamObjectHeaderStart RevisionManifest;
        public ExtendedGUID RevisionID;
        public ExtendedGUID BaseRevisionID;
        public object[] RevisionManifestDataElementsData;
        public bit8StreamObjectHeaderEnd DataElementEnd;

        /// <summary>
        /// Parse the RevisionManifestDataElement structure.
        /// </summary>
        /// <param name="s">An stream containing RevisionManifestDataElement structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.DataElementStart = new bit16StreamObjectHeaderStart();
            this.DataElementStart.Parse(s);
			ValidationChecker.ValidationCheckHeader(this.DataElementStart, 0x01, "DataElement Start");
            this.DataElementExtendedGUID = new ExtendedGUID();
            this.DataElementExtendedGUID = this.DataElementExtendedGUID.TryParse(s);
            this.SerialNumber = new SerialNumber();
            this.SerialNumber = this.SerialNumber.TryParse(s);
            this.DataElementType = new CompactUnsigned64bitInteger();
            this.DataElementType = this.DataElementType.TryParse(s);
            this.RevisionManifest = new bit16StreamObjectHeaderStart();
            this.RevisionManifest.Parse(s);
			ValidationChecker.ValidationCheckHeader(this.RevisionManifest, 0x1A, "Revision Manifest");
            this.RevisionID = new ExtendedGUID();
            this.RevisionID = this.RevisionID.TryParse(s);
            this.BaseRevisionID = new ExtendedGUID();
            this.BaseRevisionID = this.BaseRevisionID.TryParse(s);

            int RevisionType = (CurrentByte() >> 3) & 0x3F;
            List<object> DataList = new List<object>();
            while ((CurrentByte() & 0x03) == 0x0 && (RevisionType == 0x0A || RevisionType == 0x19))
            {
                switch (RevisionType)
                {
                    case 0x0A:
                        {
                            RevisionManifestRootDeclareValues RootDeclareValue = new RevisionManifestRootDeclareValues();
                            RootDeclareValue.Parse(s);
                            DataList.Add(RootDeclareValue);
                            break;
                        }
                    case 0x19:
                        {
                            RevisionManifestObjectGroupReferencesValues ObjectGroupReferencesValue = new RevisionManifestObjectGroupReferencesValues();
                            ObjectGroupReferencesValue.Parse(s);
                            DataList.Add(ObjectGroupReferencesValue);
                            break;
                        }
                    default:
                        throw new Exception("The RevisionType is not right.");
                }
                RevisionType = (CurrentByte() >> 3) & 0x3F;
            }

            this.RevisionManifestDataElementsData = DataList.ToArray();
            this.DataElementEnd = new bit8StreamObjectHeaderEnd();
            this.DataElementEnd.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.DataElementEnd, 0x01, "DataElement End");
        }
    }

    /// <summary>
    /// This type is defined for section 2.2.1.12.5 Revision Manifest Data Element of root declare Values
    /// </summary>
    public class RevisionManifestRootDeclareValues : BaseStructure
    {
        public bit16StreamObjectHeaderStart RevisionManifestRootDeclare;
        public ExtendedGUID RootExtendedGUID;
        public ExtendedGUID ObjectExtendedGUID;

        /// <summary>
        /// Parse the RevisionManifestRootDeclareValues structure.
        /// </summary>
        /// <param name="s">An stream containing RevisionManifestRootDeclareValues structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.RevisionManifestRootDeclare = new bit16StreamObjectHeaderStart();
            this.RevisionManifestRootDeclare.Parse(s);
			ValidationChecker.ValidationCheckHeader(this.RevisionManifestRootDeclare, 0x0A, "Revision Manifest Root Declare");
            this.RootExtendedGUID = new ExtendedGUID();
            this.RootExtendedGUID = this.RootExtendedGUID.TryParse(s);
            this.ObjectExtendedGUID = new ExtendedGUID();
            this.ObjectExtendedGUID = this.ObjectExtendedGUID.TryParse(s);
        }
    }

    /// <summary>
    /// This type is defined for section 2.2.1.12.5 Revision Manifest Data Element of object group references values
    /// </summary>
    public class RevisionManifestObjectGroupReferencesValues : BaseStructure
    {
        public bit16StreamObjectHeaderStart RevisionManifestObjectGroupReferences;
        public ExtendedGUID ObjectGroupExtendedGUID;

        /// <summary>
        /// Parse the RevisionManifestObjectGroupReferencesValues structure.
        /// </summary>
        /// <param name="s">An stream containing RevisionManifestObjectGroupReferencesValues structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.RevisionManifestObjectGroupReferences = new bit16StreamObjectHeaderStart();
            this.RevisionManifestObjectGroupReferences.Parse(s);
			ValidationChecker.ValidationCheckHeader(this.RevisionManifestObjectGroupReferences, 0x19, "Revision Manifest Object Group References");
            this.ObjectGroupExtendedGUID = new ExtendedGUID();
            this.ObjectGroupExtendedGUID = this.ObjectGroupExtendedGUID.TryParse(s);
        }
    }

    /// <summary>
    /// 2.2.1.12.6	Object Group Data Elements
    /// </summary>
    public class ObjectGroupDataElements : BaseStructure
    {
        public bit16StreamObjectHeaderStart DataElementStart;
        public ExtendedGUID DataElementExtendedGUID;
        public SerialNumber SerialNumber;
        public CompactUnsigned64bitInteger DataElementType;
        public DataElementHash DataElementHash;
        public StreamObjectHeader ObjectGroupDeclarationsStart;
        public object[] ObjectDeclarationOrObjectDataBLOBDeclaration;
        public bit8StreamObjectHeaderEnd ObjectGroupDeclarationsEnd;
        public ObjectMetadataDeclaration ObjectMetadataDeclaration;
        public StreamObjectHeader ObjectGroupDataStart;
        public object[] ObjectDataOrObjectDataBLOBReference;
        public bit8StreamObjectHeaderEnd ObjectGroupDataEnd;
        public bit8StreamObjectHeaderEnd DataElementEnd;

        /// <summary>
        /// Parse the ObjectGroupDataElements structure.
        /// </summary>
        /// <param name="s">An stream containing ObjectGroupDataElements structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.DataElementStart = new bit16StreamObjectHeaderStart();
            this.DataElementStart.Parse(s);
			ValidationChecker.ValidationCheckHeader(this.DataElementStart, 0x01, "Data Element Start");
            this.DataElementExtendedGUID = new ExtendedGUID();
            this.DataElementExtendedGUID = this.DataElementExtendedGUID.TryParse(s);
            this.SerialNumber = new SerialNumber();
            this.SerialNumber = this.SerialNumber.TryParse(s);
            this.DataElementType = new CompactUnsigned64bitInteger();
            this.DataElementType = this.DataElementType.TryParse(s);

            if (ContainsStreamObjectHeader(0x06))
            {
                this.DataElementHash = new DataElementHash();
                this.DataElementHash.Parse(s);
            }
            this.ObjectGroupDeclarationsStart = new StreamObjectHeader();
            this.ObjectGroupDeclarationsStart = this.ObjectGroupDeclarationsStart.TryParse(s);
            ValidationChecker.ValidationCheckHeader(this.ObjectGroupDeclarationsStart, 0x1D, "Object Group Declarations Start");
            List<object> DeclarationList = new List<object>();
            while (ContainsStreamObjectHeader(0x18) || ContainsStreamObjectHeader(0x05))
            {
                if (ContainsStreamObjectHeader(0x18))
                {
                    ObjectDeclaration Declaration = new ObjectDeclaration();
                    Declaration.Parse(s);
                    DeclarationList.Add(Declaration);
                }
                else if (ContainsStreamObjectHeader(0x05))
                {
                    ObjectDataBLOBDeclaration DeclarationBLOB = new ObjectDataBLOBDeclaration();
                    DeclarationBLOB.Parse(s);
                    DeclarationList.Add(DeclarationBLOB);
                }
            }
            this.ObjectDeclarationOrObjectDataBLOBDeclaration = DeclarationList.ToArray();
            this.ObjectGroupDeclarationsEnd = new bit8StreamObjectHeaderEnd();
            this.ObjectGroupDeclarationsEnd.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.ObjectGroupDeclarationsEnd, 0x1D, "Object Group Declarations End");

            if (ContainsStreamObjectStart32BitHeader(0x79))
            {
                this.ObjectMetadataDeclaration = new ObjectMetadataDeclaration();
                this.ObjectMetadataDeclaration.Parse(s);
            }

            this.ObjectGroupDataStart = new StreamObjectHeader();
            this.ObjectGroupDataStart = this.ObjectGroupDataStart.TryParse(s);
            ValidationChecker.ValidationCheckHeader(this.ObjectGroupDataStart, 0x1E, "Object Group Data Start");
            List<object> ObjectDataList = new List<object>();
            while (ContainsStreamObjectHeader(0x16) || ContainsStreamObjectHeader(0x1C))
            {
                if (ContainsStreamObjectHeader(0x16))
                {
                    ObjectData data = new ObjectData();
                    data.Parse(s);
                    ObjectDataList.Add(data);
                }
                else if (ContainsStreamObjectHeader(0x1C))
                {
                    ObjectDataBLOBReference DataBLOB = new ObjectDataBLOBReference();
                    DataBLOB.Parse(s);
                    ObjectDataList.Add(DataBLOB);
                }
            }
            this.ObjectDataOrObjectDataBLOBReference = ObjectDataList.ToArray();

            this.ObjectGroupDataEnd = new bit8StreamObjectHeaderEnd();
            this.ObjectGroupDataEnd.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.ObjectGroupDataEnd, 0x1E, "Object Group Data End");
            this.DataElementEnd = new bit8StreamObjectHeaderEnd();
            this.DataElementEnd.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.DataElementEnd, 0x01, "Data Element End");
        }
    }

    /// <summary>
    /// 2.2.1.12.6.1	Object Declaration
    /// </summary>
    public class ObjectDeclaration : BaseStructure
    {
        public StreamObjectHeader ObjectGroupObjectDeclaration;
        public ExtendedGUID ObjectExtendedGUID;
        public CompactUnsigned64bitInteger ObjectPartitionID;
        public CompactUnsigned64bitInteger ObjectDataSize;
        public CompactUnsigned64bitInteger ObjectReferencesCount;
        public CompactUnsigned64bitInteger CellReferencesCount;

        /// <summary>
        /// Parse the ObjectDeclaration structure.
        /// </summary>
        /// <param name="s">An stream containing ObjectDeclaration structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.ObjectGroupObjectDeclaration = new StreamObjectHeader();
            this.ObjectGroupObjectDeclaration = this.ObjectGroupObjectDeclaration.TryParse(s);
            ValidationChecker.ValidationCheckHeader(this.ObjectGroupObjectDeclaration, 0x18, "Object Group Object Declaration");
            this.ObjectExtendedGUID = new ExtendedGUID();
            this.ObjectExtendedGUID = this.ObjectExtendedGUID.TryParse(s);
            this.ObjectPartitionID = new CompactUnsigned64bitInteger();
            this.ObjectPartitionID = this.ObjectPartitionID.TryParse(s);
            this.ObjectDataSize = new CompactUnsigned64bitInteger();
            this.ObjectDataSize = this.ObjectDataSize.TryParse(s);
            this.ObjectReferencesCount = new CompactUnsigned64bitInteger();
            this.ObjectReferencesCount = this.ObjectReferencesCount.TryParse(s);
            this.CellReferencesCount = new CompactUnsigned64bitInteger();
            this.CellReferencesCount = this.CellReferencesCount.TryParse(s);
        }
    }

    /// <summary>
    /// 2.2.1.12.6.2	ObjectDataBLOBDeclaration
    /// </summary>
    public class ObjectDataBLOBDeclaration : BaseStructure
    {
        public StreamObjectHeader ObjectGroupObjectDataBLOBDeclaration;
        public ExtendedGUID ObjectExtendedGUID;
        public ExtendedGUID ObjectDataBLOBEXGUID;
        public CompactUnsigned64bitInteger ObjectPartitionID;
        public CompactUnsigned64bitInteger ObjectReferencesCount;
        public CompactUnsigned64bitInteger CellReferencesCount;

        /// <summary>
        /// Parse the ObjectDataBLOBDeclaration structure.
        /// </summary>
        /// <param name="s">An stream containing ObjectDataBLOBDeclaration structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.ObjectGroupObjectDataBLOBDeclaration = new StreamObjectHeader();
            this.ObjectGroupObjectDataBLOBDeclaration = this.ObjectGroupObjectDataBLOBDeclaration.TryParse(s);
            ValidationChecker.ValidationCheckHeader(this.ObjectGroupObjectDataBLOBDeclaration, 0x05, "Object Group Object Data BLOB Declaration");
            this.ObjectExtendedGUID = new ExtendedGUID();
            this.ObjectExtendedGUID = this.ObjectExtendedGUID.TryParse(s);
            this.ObjectDataBLOBEXGUID = new ExtendedGUID();
            this.ObjectDataBLOBEXGUID = this.ObjectDataBLOBEXGUID.TryParse(s);
            this.ObjectPartitionID = new CompactUnsigned64bitInteger();
            this.ObjectPartitionID = this.ObjectPartitionID.TryParse(s);
            this.ObjectReferencesCount = new CompactUnsigned64bitInteger();
            this.ObjectReferencesCount = this.ObjectReferencesCount.TryParse(s);
            this.CellReferencesCount = new CompactUnsigned64bitInteger();
            this.CellReferencesCount = this.CellReferencesCount.TryParse(s);
        }
    }

    /// <summary>
    /// 2.2.1.12.6.3	Object Metadata Declaration
    /// </summary>
    public class ObjectMetadataDeclaration : BaseStructure
    {
        public bit32StreamObjectHeaderStart ObjectGroupMetadataDeclarations;
        public ObjectMetadata[] ObjectMetadata;
        public bit16StreamObjectHeaderEnd ObjectGroupMetadataDeclarationsEnd;

        /// <summary>
        /// Parse the ObjectMetadataDeclaration structure.
        /// </summary>
        /// <param name="s">An stream containing ObjectMetadataDeclaration structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.ObjectGroupMetadataDeclarations = new bit32StreamObjectHeaderStart();
            this.ObjectGroupMetadataDeclarations.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.ObjectGroupMetadataDeclarations, 0x79, "Object Group metadata declarations");
            List<ObjectMetadata> ObjectMetadataList = new List<ObjectMetadata>();
            while (ContainsStreamObjectStart32BitHeader(0x78))
            {
                ObjectMetadata tempObjectMetadata = new ObjectMetadata();
                tempObjectMetadata.Parse(s);
                ObjectMetadataList.Add(tempObjectMetadata);
            }
            this.ObjectMetadata = ObjectMetadataList.ToArray();
            this.ObjectGroupMetadataDeclarationsEnd = new bit16StreamObjectHeaderEnd();
            this.ObjectGroupMetadataDeclarationsEnd.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.ObjectGroupMetadataDeclarationsEnd, 0x79, "Object Group metadata declarations");
        }
    }

    /// <summary>
    /// 2.2.1.12.6.3.1	Object Metadata
    /// </summary>
    public class ObjectMetadata : BaseStructure
    {
        public bit32StreamObjectHeaderStart ObjectGroupMetadata;
        public CompactUnsigned64bitInteger ObjectChangeFrequency;

        /// <summary>
        /// Parse the ObjectMetadata structure.
        /// </summary>
        /// <param name="s">An stream containing ObjectMetadata structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.ObjectGroupMetadata = new bit32StreamObjectHeaderStart();
            this.ObjectGroupMetadata.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.ObjectGroupMetadata, 0x78, "Object Group metadata");
            this.ObjectChangeFrequency = new CompactUnsigned64bitInteger();
            this.ObjectChangeFrequency = this.ObjectChangeFrequency.TryParse(s);
        }
    }

    /// <summary>
    /// 2.2.1.12.6.4	Object Data
    /// </summary>
    public class ObjectData : BaseStructure
    {
        public StreamObjectHeader ObjectGroupObjectDataOrExcludedData;
        public ExtendedGUIDArray ObjectExtendedGUIDArray;
        public CellIDArray CellIDArray;

        public CompactUnsigned64bitInteger DataSize;
        public object Data;

        /// <summary>
        /// Parse the ObjectData structure.
        /// </summary>
        /// <param name="s">An stream containing ObjectData structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.ObjectGroupObjectDataOrExcludedData = new StreamObjectHeader();
            this.ObjectGroupObjectDataOrExcludedData = this.ObjectGroupObjectDataOrExcludedData.TryParse(s);
            this.ObjectExtendedGUIDArray = new ExtendedGUIDArray();
            this.ObjectExtendedGUIDArray.Parse(s);
            this.CellIDArray = new CellIDArray();
            this.CellIDArray.Parse(s);
            this.DataSize = new CompactUnsigned64bitInteger();
            this.DataSize = this.DataSize.TryParse(s);
            if (ContainsStreamObjectStart16BitHeader(0x20))
            {
                this.Data = new IntermediateNodeObjectData();
                ((IntermediateNodeObjectData)this.Data).Parse(s);

            }
            else if (ContainsStreamObjectStart16BitHeader(0x1F))
            {
                this.Data = new LeafNodeObjectData();
                ((LeafNodeObjectData)this.Data).Parse(s);
            }
            else
            {
            byte[] dataarray = ReadBytes((int)this.DataSize.GetUint(this.DataSize));
            this.Data = dataarray;
            }
        }
    }

    /// <summary>
    /// 2.2.1.12.6.5	Object Data BLOB Reference
    /// </summary>
    public class ObjectDataBLOBReference : BaseStructure
    {
        public StreamObjectHeader ObjectGroupObjectDataBLOBReference;
        public ExtendedGUIDArray ObjectExtendedGUIDArray;
        public CellIDArray CellIDArray;
        public ExtendedGUID BLOBExtendedGUID;

        /// <summary>
        /// Parse the ObjectDataBLOBReference structure.
        /// </summary>
        /// <param name="s">An stream containing ObjectDataBLOBReference structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.ObjectGroupObjectDataBLOBReference = new StreamObjectHeader();
            this.ObjectGroupObjectDataBLOBReference = this.ObjectGroupObjectDataBLOBReference.TryParse(s);
            ValidationChecker.ValidationCheckHeader(this.ObjectGroupObjectDataBLOBReference, 0x1C, "Object Group Object Data BLOB Reference");
            this.ObjectExtendedGUIDArray = new ExtendedGUIDArray();
            this.ObjectExtendedGUIDArray.Parse(s);
            this.CellIDArray = new CellIDArray();
            this.CellIDArray.Parse(s);
            this.BLOBExtendedGUID = new ExtendedGUID();
            this.BLOBExtendedGUID = this.BLOBExtendedGUID.TryParse(s);
        }
    }

    /// <summary>
    /// 2.2.1.12.6.6	Data Element Hash
    /// </summary>
    public class DataElementHash : BaseStructure
    {
        public StreamObjectHeader DataElementHashDeclaration;
        public CompactUnsigned64bitInteger DataElementHashScheme;
        public BinaryItem DataElementHashData;

        /// <summary>
        /// Parse the DataElementHash structure.
        /// </summary>
        /// <param name="s">An stream containing DataElementHash structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.DataElementHashDeclaration = new StreamObjectHeader();
            this.DataElementHashDeclaration = this.DataElementHashDeclaration.TryParse(s);
            this.DataElementHashScheme = new CompactUnsigned64bitInteger();
            this.DataElementHashScheme = this.DataElementHashScheme.TryParse(s);
            this.DataElementHashData = new BinaryItem();
            this.DataElementHashData.Parse(s);
        }
    }

    /// <summary>
    /// 2.2.1.12.7	Data Element Fragment Data Elements
    /// </summary>
    public class DataElementFragmentDataElement : BaseStructure
    {
        public bit16StreamObjectHeaderStart DataElementStart;
        public ExtendedGUID DataElementExtendedGUID;
        public SerialNumber SerialNumber;
        public CompactUnsigned64bitInteger DataElementType;
        public bit32StreamObjectHeaderStart DataElementFragment;
        public ExtendedGUID FragmentExtendedGUID;
        public CompactUnsigned64bitInteger FragmentDataElementSize;
        public FileChunkReference FragmentFileChunkReference;
        public BinaryItem FragmentData;
        public bit8StreamObjectHeaderEnd DataElementEnd;

        /// <summary>
        /// Parse the DataElementFragmentDataElement structure.
        /// </summary>
        /// <param name="s">An stream containing DataElementFragmentDataElement structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.DataElementStart = new bit16StreamObjectHeaderStart();
            this.DataElementStart.Parse(s);
			ValidationChecker.ValidationCheckHeader(this.DataElementStart, 0x01, "Data Element Start");
            this.DataElementExtendedGUID = new ExtendedGUID();
            this.DataElementExtendedGUID = this.DataElementExtendedGUID.TryParse(s);
            this.SerialNumber = new SerialNumber();
            this.SerialNumber = this.SerialNumber.TryParse(s);
            this.DataElementType = new CompactUnsigned64bitInteger();
            this.DataElementType = this.DataElementType.TryParse(s);
            this.DataElementFragment = new bit32StreamObjectHeaderStart();
            this.DataElementFragment.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.DataElementFragment, 0x6A, "Data Element Fragment");
            this.FragmentExtendedGUID = new ExtendedGUID();
            this.FragmentExtendedGUID = this.FragmentExtendedGUID.TryParse(s);
            this.FragmentDataElementSize = new CompactUnsigned64bitInteger();
            this.FragmentDataElementSize = this.FragmentDataElementSize.TryParse(s);
            this.FragmentFileChunkReference = new FileChunkReference();
            this.FragmentFileChunkReference.Parse(s);
            this.FragmentData = new BinaryItem();
            this.FragmentData.Parse(s);
            this.DataElementEnd = new bit8StreamObjectHeaderEnd();
            this.DataElementEnd.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.DataElementEnd, 0x01, "Data Element End");
        }
    }

    /// <summary>
    /// 2.2.1.12.8	Object Data BLOB Data Elements
    /// </summary>
    public class ObjectDataBLOBDataElements : BaseStructure
    {
        public bit16StreamObjectHeaderStart DataElementStart;
        public ExtendedGUID DataElementExtendedGUID;
        public SerialNumber SerialNumber;
        public CompactUnsigned64bitInteger DataElementType;
        public StreamObjectHeader ObjectDataBLOB;
        public BinaryItem Data;
        public bit8StreamObjectHeaderEnd DataElementEnd;

        /// <summary>
        /// Parse the ObjectDataBLOBDataElements structure.
        /// </summary>
        /// <param name="s">An stream containing ObjectDataBLOBDataElements structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.DataElementStart = new bit16StreamObjectHeaderStart();
            this.DataElementStart.Parse(s);
			ValidationChecker.ValidationCheckHeader(this.DataElementStart, 0x01, "Data Element Start");
            this.DataElementExtendedGUID = new ExtendedGUID();
            this.DataElementExtendedGUID = this.DataElementExtendedGUID.TryParse(s);
            this.SerialNumber = new SerialNumber();
            this.SerialNumber = this.SerialNumber.TryParse(s);
            this.DataElementType = new CompactUnsigned64bitInteger();
            this.DataElementType = this.DataElementType.TryParse(s);
            this.ObjectDataBLOB = new StreamObjectHeader();
            this.ObjectDataBLOB = this.ObjectDataBLOB.TryParse(s);
            ValidationChecker.ValidationCheckHeader(this.ObjectDataBLOB, 0x02, "Object Data BLOB");
            this.Data = new BinaryItem();
            this.Data.Parse(s);
            this.DataElementEnd = new bit8StreamObjectHeaderEnd();
            this.DataElementEnd.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.DataElementEnd, 0x01, "Data Element End");
        }
    }

    /// <summary>
    /// 2.2.1.13	Knowledge
    /// </summary>
    public class Knowledge : BaseStructure
    {
        public bit16StreamObjectHeaderStart KnowledgeStart;
        public SpecializedKnowledge[] SpecializedKnowledge;
        public bit8StreamObjectHeaderEnd KnowledgeEnd;

        /// <summary>
        /// Parse the Knowledge structure.
        /// </summary>
        /// <param name="s">An stream containing Knowledge structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.KnowledgeStart = new bit16StreamObjectHeaderStart();
            this.KnowledgeStart.Parse(s);
			ValidationChecker.ValidationCheckHeader(this.KnowledgeStart, 0x10, "Knowledge Start");

            List<SpecializedKnowledge> tempSpecializedKnowledge = new List<SpecializedKnowledge>();
            while (ContainsStreamObjectStart32BitHeader(0x44))
            {
                SpecializedKnowledge knowledge = new SpecializedKnowledge();
                knowledge.Parse(s);
                tempSpecializedKnowledge.Add(knowledge);
            };
            this.SpecializedKnowledge = tempSpecializedKnowledge.ToArray();
            this.KnowledgeEnd = new bit8StreamObjectHeaderEnd();
            this.KnowledgeEnd.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.KnowledgeEnd, 0x10, "Knowledge End");
        }
    }

    /// <summary>
    /// 2.2.1.13.1	Specialized Knowledge
    /// </summary>
    public class SpecializedKnowledge : BaseStructure
    {
        public bit32StreamObjectHeaderStart SpecializedKnowledgeStart;
        public Guid GUID;
        public object SpecializedKnowledgeData;
        public bit16StreamObjectHeaderEnd SpecializedKnowledgeEnd;

        /// <summary>
        /// Parse the SpecializedKnowledge structure.
        /// </summary>
        /// <param name="s">An stream containing SpecializedKnowledge structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.SpecializedKnowledgeStart = new bit32StreamObjectHeaderStart();
            this.SpecializedKnowledgeStart.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.SpecializedKnowledgeStart, 0x44, "Specialized Knowledge");
            this.GUID = ReadGuid();

            switch (this.GUID.ToString().ToUpper())
            {
                case "327A35F6-0761-4414-9686-51E900667A4D":
                    this.SpecializedKnowledgeData = new CellKnowLedge();
                    ((CellKnowLedge)this.SpecializedKnowledgeData).Parse(s);
                    break;
                case "3A76E90E-8032-4D0C-B9DD-F3C65029433E":
                    this.SpecializedKnowledgeData = new WaterlineKnowledge();
                    ((WaterlineKnowledge)this.SpecializedKnowledgeData).Parse(s);
                    break;
                case "0ABE4F35-01DF-4134-A24A-7C79F0859844":
                    this.SpecializedKnowledgeData = new FragmentKnowledge();
                    ((FragmentKnowledge)this.SpecializedKnowledgeData).Parse(s);
                    break;
                case "10091F13-C882-40FB-9886-6533F934C21D":
                    this.SpecializedKnowledgeData = new ContentTagKnowledge();
                    ((ContentTagKnowledge)this.SpecializedKnowledgeData).Parse(s);
                    break;
                case "BF12E2C1-E64F-4959-8282-73B9A24A7C44":
                    this.SpecializedKnowledgeData = new UnknowKnowledge();
                    ((UnknowKnowledge)this.SpecializedKnowledgeData).Parse(s);
                    break;
                default:
                    throw new Exception("The GUID is not right.");

            }
            this.SpecializedKnowledgeEnd = new bit16StreamObjectHeaderEnd();
            this.SpecializedKnowledgeEnd.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.SpecializedKnowledgeEnd, 0x44, "Specialized Knowledge End");
        }
    }


    /// <summary>
    /// 2.2.1.13.2	Cell Knowledge
    /// </summary>
    public class CellKnowLedge : BaseStructure
    {
        public bit16StreamObjectHeaderStart CellKnowledgeStart;
        public object[] CellKnowledgeData;
        public bit8StreamObjectHeaderEnd CellKnowledgeEnd;

        /// <summary>
        /// Parse the CellKnowLedge structure.
        /// </summary>
        /// <param name="s">An stream containing CellKnowLedge structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.CellKnowledgeStart = new bit16StreamObjectHeaderStart();
            this.CellKnowledgeStart.Parse(s);
			ValidationChecker.ValidationCheckHeader(this.CellKnowledgeStart, 0x14, "Cell Knowledge Start");
            List<object> tempCell = new List<object>();
            while (ContainsStreamObjectStart16BitHeader(0x0F) || ContainsStreamObjectStart16BitHeader(0x17))
            {
                if (ContainsStreamObjectStart16BitHeader(0x0F))
                {
                    CellKnowledgeRange cellknowledge = new CellKnowledgeRange();
                    cellknowledge.Parse(s);
                    tempCell.Add(cellknowledge);
                }
                else if (ContainsStreamObjectStart16BitHeader(0x17))
                {
                    CellKnowledgeEntry cellknowledge = new CellKnowledgeEntry();
                    cellknowledge.Parse(s);
                    tempCell.Add(cellknowledge);
                }
            }
            this.CellKnowledgeData = tempCell.ToArray();

            this.CellKnowledgeEnd = new bit8StreamObjectHeaderEnd();
            this.CellKnowledgeEnd.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.CellKnowledgeEnd, 0x14, "Cell Knowledge End");
        }
    }

    /// <summary>
    /// 2.2.1.13.2.1   Cell Knowledge Range
    /// </summary>
    public class CellKnowledgeRange : BaseStructure
    {
        public bit16StreamObjectHeaderStart cellKnowledgeRange;
        public Guid GUID;
        public CompactUnsigned64bitInteger From;
        public CompactUnsigned64bitInteger To;

        /// <summary>
        /// Parse the CellKnowledgeRange structure.
        /// </summary>
        /// <param name="s">An stream containing CellKnowledgeRange structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.cellKnowledgeRange = new bit16StreamObjectHeaderStart();
            this.cellKnowledgeRange.Parse(s);
			ValidationChecker.ValidationCheckHeader(this.cellKnowledgeRange, 0x0F, "cell Knowledge Range");
            this.GUID = ReadGuid();
            this.From = new CompactUnsigned64bitInteger();
            this.From = this.From.TryParse(s);
            this.To = new CompactUnsigned64bitInteger();
            this.To = this.To.TryParse(s);
        }
    }

    /// <summary>
    /// 2.2.1.13.2.2	Cell Knowledge Entry
    /// </summary>
    public class CellKnowledgeEntry : BaseStructure
    {
        public bit16StreamObjectHeaderStart cellKnowledgeEntry;
        public SerialNumber SerialNumber;

        /// <summary>
        /// Parse the CellKnowledgeEntry structure.
        /// </summary>
        /// <param name="s">An stream containing CellKnowledgeEntry structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.cellKnowledgeEntry = new bit16StreamObjectHeaderStart();
            this.cellKnowledgeEntry.Parse(s);
			ValidationChecker.ValidationCheckHeader(this.cellKnowledgeEntry, 0x17, "cell Knowledge Entry");
            this.SerialNumber = new SerialNumber();
            this.SerialNumber = this.SerialNumber.TryParse(s);
        }
    }

    /// <summary>
    /// 2.2.1.13.3	Fragment Knowledge
    /// </summary>
    public class FragmentKnowledge : BaseStructure
    {
        public bit32StreamObjectHeaderStart FragmentKnowledgeStart;
        public FragmentKnowledgeEntry[] FragmentKnowledgeEntries;
        public bit16StreamObjectHeaderEnd FragmentKnowledgeEnd;

        /// <summary>
        /// Parse the FragmentKnowledge structure.
        /// </summary>
        /// <param name="s">An stream containing FragmentKnowledge structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.FragmentKnowledgeStart = new bit32StreamObjectHeaderStart();
            this.FragmentKnowledgeStart.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.FragmentKnowledgeStart, 0x6B, "Fragment Knowledge Start");
            List<FragmentKnowledgeEntry> tempFragment = new List<FragmentKnowledgeEntry>();
            while (ContainsStreamObjectStart32BitHeader(0x06C))
            {
                FragmentKnowledgeEntry Fragmentknowledge = new FragmentKnowledgeEntry();
                Fragmentknowledge.Parse(s);
                tempFragment.Add(Fragmentknowledge);
            };
            this.FragmentKnowledgeEntries = tempFragment.ToArray();
            this.FragmentKnowledgeEnd = new bit16StreamObjectHeaderEnd();
            this.FragmentKnowledgeEnd.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.FragmentKnowledgeEnd, 0x6B, "Fragment Knowledge End");
        }
    }

    /// <summary>
    /// 2.2.1.13.3	Fragment Knowledge
    /// </summary>
    public class UnknowKnowledge : BaseStructure
    {
        public bit32StreamObjectHeaderStart UnknownKnowledgeStart;
        public byte[] UnknownData;

        /// <summary>
        /// Parse the FragmentKnowledge structure.
        /// </summary>
        /// <param name="s">An stream containing FragmentKnowledge structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.UnknownKnowledgeStart = new bit32StreamObjectHeaderStart();
            this.UnknownKnowledgeStart.Parse(s);
            this.UnknownData = new byte[this.UnknownKnowledgeStart.Length];
            this.UnknownData = ReadBytes(this.UnknownKnowledgeStart.Length);
        }
    }

    /// <summary>
    /// 2.2.1.13.3.1	Fragment Knowledge Entry
    /// </summary>
    public class FragmentKnowledgeEntry : BaseStructure
    {
        public bit32StreamObjectHeaderStart FragmentDescriptor;
        public ExtendedGUID ExtendedGUID;
        public CompactUnsigned64bitInteger DataElementSize;
        public FileChunkReference DataElementChunkReference;

        /// <summary>
        /// Parse the FragmentKnowledgeEntry structure.
        /// </summary>
        /// <param name="s">An stream containing FragmentKnowledgeEntry structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.FragmentDescriptor = new bit32StreamObjectHeaderStart();
            this.FragmentDescriptor.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.FragmentDescriptor, 0x6C, "Fragment Knowledge entry");
            this.ExtendedGUID = new ExtendedGUID();
            this.ExtendedGUID = this.ExtendedGUID.TryParse(s);

            this.DataElementSize = new CompactUnsigned64bitInteger();
            this.DataElementSize = this.DataElementSize.TryParse(s);
            this.DataElementChunkReference = new FileChunkReference();
            this.DataElementChunkReference.Parse(s);
        }
    }

    /// <summary>
    /// 2.2.1.13.4	Waterline Knowledge
    /// </summary>
    public class WaterlineKnowledge : BaseStructure
    {
        public bit16StreamObjectHeaderStart WaterlineKnowledgeStart;
        public WaterlineKnowledgeEntry[] WaterlineKnowledgeData;
        public bit8StreamObjectHeaderEnd WaterlineKnowledgeEnd;

        /// <summary>
        /// Parse the WaterlineKnowledge structure.
        /// </summary>
        /// <param name="s">An stream containing WaterlineKnowledge structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.WaterlineKnowledgeStart = new bit16StreamObjectHeaderStart();
            this.WaterlineKnowledgeStart.Parse(s);
			ValidationChecker.ValidationCheckHeader(this.WaterlineKnowledgeStart, 0x29, "Waterline Knowledge Start");
            List<WaterlineKnowledgeEntry> tempWaterline = new List<WaterlineKnowledgeEntry>();
            do
            {
                WaterlineKnowledgeEntry Waterlineknowledge = new WaterlineKnowledgeEntry();
                Waterlineknowledge.Parse(s);
                tempWaterline.Add(Waterlineknowledge);
            } while (ContainsStreamObjectStart16BitHeader(0x04));
            this.WaterlineKnowledgeData = tempWaterline.ToArray();
            this.WaterlineKnowledgeEnd = new bit8StreamObjectHeaderEnd();
            this.WaterlineKnowledgeEnd.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.WaterlineKnowledgeEnd, 0x29, "Waterline Knowledge Start");
        }
    }

    /// <summary>
    /// 2.2.1.13.4.1	Waterline Knowledge Entry
    /// </summary>
    public class WaterlineKnowledgeEntry : BaseStructure
    {
        public bit16StreamObjectHeaderStart waterlineKnowledgeEntry;
        public ExtendedGUID CellStorageExtendedGUID;
        public CompactUnsigned64bitInteger Waterline;
        public CompactUnsigned64bitInteger Reserved;

        /// <summary>
        /// Parse the WaterlineKnowledgeEntry structure.
        /// </summary>
        /// <param name="s">An stream containing WaterlineKnowledgeEntry structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.waterlineKnowledgeEntry = new bit16StreamObjectHeaderStart();
            this.waterlineKnowledgeEntry.Parse(s);
			ValidationChecker.ValidationCheckHeader(this.waterlineKnowledgeEntry, 0x04, "waterline Knowledge Entry");
            this.CellStorageExtendedGUID = new ExtendedGUID();
            this.CellStorageExtendedGUID = this.CellStorageExtendedGUID.TryParse(s);

            this.Waterline = new CompactUnsigned64bitInteger();
            this.Waterline = this.Waterline.TryParse(s);
            this.Reserved = new CompactUnsigned64bitInteger();
            this.Reserved = this.Reserved.TryParse(s);
        }
    }

    /// <summary>
    /// 2.2.1.13.5	Content Tag Knowledge
    /// </summary>
    public class ContentTagKnowledge : BaseStructure
    {
        public bit16StreamObjectHeaderStart ContentTagKnowledgeStart;
        public ContentTagKnowledgeEntry[] ContentTagKnowledgeEntryArray;
        public bit8StreamObjectHeaderEnd ContentTagKnowledgeEnd;

        /// <summary>
        /// Parse the WaterlineKnowledge structure.
        /// </summary>
        /// <param name="s">An stream containing WaterlineKnowledge structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.ContentTagKnowledgeStart = new bit16StreamObjectHeaderStart();
            this.ContentTagKnowledgeStart.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.ContentTagKnowledgeStart, 0x2D, "Content Tag Knowledge Start");
            List<ContentTagKnowledgeEntry> tempContentTag = new List<ContentTagKnowledgeEntry>();
            //TODO out conflict 246 or 249 frame TDI?
            while (ContainsStreamObjectHeader(0x2E))
            {
                ContentTagKnowledgeEntry ContentTagknowledge = new ContentTagKnowledgeEntry();
                ContentTagknowledge.Parse(s);
                tempContentTag.Add(ContentTagknowledge);
            };
            this.ContentTagKnowledgeEntryArray = tempContentTag.ToArray();
            this.ContentTagKnowledgeEnd = new bit8StreamObjectHeaderEnd();
            this.ContentTagKnowledgeEnd.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.ContentTagKnowledgeEnd, 0x2D, "Content Tag Knowledge End");
        }
    }

    /// <summary>
    /// 2.2.1.13.5.1	Content Tag Knowledge Entry
    /// </summary>
    public class ContentTagKnowledgeEntry : BaseStructure
    {
        public StreamObjectHeader ContentTagKnowledgeEntryStart;
        public ExtendedGUID BLOBExtendedGUID;
        public BinaryItem ClockData;

        /// <summary>
        /// Parse the ContentTagKnowledgeEntry structure.
        /// </summary>
        /// <param name="s">An stream containing ContentTagKnowledgeEntry structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.ContentTagKnowledgeEntryStart = new StreamObjectHeader();
            this.ContentTagKnowledgeEntryStart = this.ContentTagKnowledgeEntryStart.TryParse(s);
            ValidationChecker.ValidationCheckHeader(this.ContentTagKnowledgeEntryStart, 0x2E, "ContentTag Knowledge Entry Start");
            this.BLOBExtendedGUID = new ExtendedGUID();
            this.BLOBExtendedGUID = this.BLOBExtendedGUID.TryParse(s);
            this.ClockData = new BinaryItem();
            this.ClockData.Parse(s);
        }
    }

    /// <summary>
    /// 2.2.2	Request Message Syntax
    /// </summary>
    public class FsshttpbRequest : BaseStructure
    {
        public ushort ProtocolVersion;
        public ushort MinimumVersion;
        public ulong Signature;
        public bit32StreamObjectHeaderStart RequestStart;
        public bit32StreamObjectHeaderStart UserAgentStart;
        public bit32StreamObjectHeaderStart UserAgentGUID;
        public Guid? GUID;
        public bit32StreamObjectHeaderStart UserAgentClientAndPlatform;
        public CompactUnsigned64bitInteger ClientCount;
        public byte[] ClientByteArray;
        public CompactUnsigned64bitInteger PlatformCount;
        public byte[] PlatformByteArray;
        public bit32StreamObjectHeaderStart UserAgentVersion;
        public uint Version;
        public bit16StreamObjectHeaderEnd UserAgentEnd;
        /*TODO public bit32StreamObjectHeaderStart RequestHashingOptionsDeclaration;
        public CompactUnsigned64bitInteger RequestHasingSchema;

        [BitAttribute(1)]
        public byte? A;
        [BitAttribute(1)]
        public byte? B;
        [BitAttribute(1)]
        public byte? C;
        [BitAttribute(1)]
        public byte? D;
        [BitAttribute(4)]
        public byte? E;*/
        public RequestHash[] RequestHashPackage;
        public FsshttpbSubRequest[] SubRequest;
        public DataElementPackage DataElementPackage;
        public bit16StreamObjectHeaderEnd RequestEnd;

        /// <summary>
        /// Parse the FsshttpbRequest structure.
        /// </summary>
        /// <param name="s">An stream containing FsshttpbRequest structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.ProtocolVersion = ReadUshort();
            this.MinimumVersion = ReadUshort();
            this.Signature = ReadUlong();
            this.RequestStart = new bit32StreamObjectHeaderStart();
            this.RequestStart.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.RequestStart, 0x50, "Request");
            this.UserAgentStart = new bit32StreamObjectHeaderStart();
            this.UserAgentStart.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.UserAgentStart, 0x5D, "User Agent");
            if (ContainsStreamObjectStart32BitHeader(0x055))
            {
                this.UserAgentGUID = new bit32StreamObjectHeaderStart();
                this.UserAgentGUID.Parse(s);
            }
            if (this.UserAgentGUID != null)
            {
                this.GUID = ReadGuid();
            }
            if (ContainsStreamObjectStart32BitHeader(0x8B))
            {
                this.UserAgentClientAndPlatform = new bit32StreamObjectHeaderStart();
                this.UserAgentClientAndPlatform.Parse(s);
            }

            if (this.UserAgentClientAndPlatform != null)
            {
                this.ClientCount = new CompactUnsigned64bitInteger();
                this.ClientCount = this.ClientCount.TryParse(s);
                this.ClientByteArray = ReadBytes((int)this.ClientCount.GetUint(this.ClientCount));
                this.PlatformCount = new CompactUnsigned64bitInteger();
                this.PlatformCount = this.PlatformCount.TryParse(s);
                this.PlatformByteArray = ReadBytes((int)this.PlatformCount.GetUint(this.PlatformCount));
            }

            this.UserAgentVersion = new bit32StreamObjectHeaderStart();
            this.UserAgentVersion.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.UserAgentVersion, 0x4F, "User Agent version");
            this.Version = ReadUint();
            this.UserAgentEnd = new bit16StreamObjectHeaderEnd();
            this.UserAgentEnd.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.UserAgentEnd, 0x5D, "User Agent End");
            /*if (ContainsStreamObjectStart32BitHeader(0x8D))//TODO: This should be 0x88 accroding to TD, but in the capture files of sharepoint server 2013, it is 0x8D.
            {
                this.RequestHashingOptionsDeclaration = new bit32StreamObjectHeaderStart();
                this.RequestHashingOptionsDeclaration.Parse(s);
            }

            if (this.RequestHashingOptionsDeclaration != null)
            {
                // TODO: Missing fields (A B C D E) here, because it's seems these is not containd in capture files of sharepoint server 13. 
                this.RequestHasingSchema = new CompactUnsigned64bitInteger();
                this.RequestHasingSchema = this.RequestHasingSchema.TryParse(s);
            }*/
            if (ContainsStreamObjectStart32BitHeader(0x88) || ContainsStreamObjectStart32BitHeader(0x8D))
            {
                List<RequestHash> tempHashList = new List<RequestHash>();
                do
                {
                    RequestHash tempHash = new RequestHash();
                    tempHash.Parse(s);
                    tempHashList.Add(tempHash);
                    this.RequestHashPackage = tempHashList.ToArray();
                } while (ContainsStreamObjectStart32BitHeader(0x88) || ContainsStreamObjectStart32BitHeader(0x8D));
            }

            if (ContainsStreamObjectStart32BitHeader(0x042))
            {
                List<FsshttpbSubRequest> tempRequest = new List<FsshttpbSubRequest>();
                do
                {
                    FsshttpbSubRequest subRequest = new FsshttpbSubRequest();
                    subRequest.Parse(s);
                    tempRequest.Add(subRequest);
                    this.SubRequest = tempRequest.ToArray();
                } while (ContainsStreamObjectStart32BitHeader(0x042));
            }
            if (ContainsStreamObjectStart16BitHeader(0x15))
            {
                this.DataElementPackage = new DataElementPackage();
                this.DataElementPackage.Parse(s);
            }
            this.RequestEnd = new bit16StreamObjectHeaderEnd();
            this.RequestEnd.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.RequestEnd, 0x50, "Request End");
        }
    }

    /// <summary>
    /// 2.2.2	RequestHash in Request Message Syntax
    /// </summary>
    public class RequestHash : BaseStructure
    {
        public bit32StreamObjectHeaderStart RequestHashingOptionsDeclaration;
        public CompactUnsigned64bitInteger RequestHasingSchema;
        [BitAttribute(1)]
        public byte? A;
        [BitAttribute(1)]
        public byte? B;
        [BitAttribute(1)]
        public byte? C;
        [BitAttribute(1)]
        public byte? D;
        [BitAttribute(4)]
        public byte? E;

        /// <summary>
        /// Parse the RequestHash structure.
        /// </summary>
        /// <param name="s">An stream containing RequestHash structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            bool isValue88 = false;
            if (ContainsStreamObjectStart32BitHeader(0x88))
            {
                isValue88 = true;
            }
            this.RequestHashingOptionsDeclaration = new bit32StreamObjectHeaderStart();
            this.RequestHashingOptionsDeclaration.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.RequestHashingOptionsDeclaration, 0x88, "Request hashing options");
            if (this.RequestHashingOptionsDeclaration != null)
            {
                this.RequestHasingSchema = new CompactUnsigned64bitInteger();
                this.RequestHasingSchema = this.RequestHasingSchema.TryParse(s);
                if (isValue88)
                {
                    byte tempByte = ReadByte();
                    this.A = GetBits(tempByte, 0, 1);
                    this.B = GetBits(tempByte, 1, 1);
                    this.C = GetBits(tempByte, 2, 1);
                    this.D = GetBits(tempByte, 3, 1);
                    this.E = GetBits(tempByte, 4, 4);
                }
            }

        }
    }

    /// <summary>
    /// 2.2.2.1	Sub-Requests
    /// </summary>
    public class FsshttpbSubRequest : BaseStructure
    {
        public bit32StreamObjectHeaderStart SubRequestStart;
        public CompactUnsigned64bitInteger RequestID;
        public CompactUnsigned64bitInteger RequestType;
        public CompactUnsigned64bitInteger Priority;
        public TargetPartitionId TargetPartitionId;
        public object SubRequestData;
        public bit16StreamObjectHeaderEnd SubRequestEnd;

        /// <summary>
        /// Parse the FsshttpbSubRequests structure.
        /// </summary>
        /// <param name="s">An stream containing FsshttpbSubRequests structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.SubRequestStart = new bit32StreamObjectHeaderStart();
            this.SubRequestStart.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.SubRequestStart, 0x42, "Sub-request ");
            this.RequestID = new CompactUnsigned64bitInteger();
            this.RequestID = this.RequestID.TryParse(s);
            this.RequestType = new CompactUnsigned64bitInteger();
            this.RequestType = this.RequestType.TryParse(s);
            this.Priority = new CompactUnsigned64bitInteger();
            this.Priority = this.Priority.TryParse(s);
            if (ContainsStreamObjectStart32BitHeader(0x83))
            {
                this.TargetPartitionId = new TargetPartitionId();
                this.TargetPartitionId.Parse(s);
            }

            switch (RequestType.GetUint(RequestType))
            {
                case 0x01:
                    this.SubRequestData = new QueryAccessRequest();
                    break;
                case 0x02:
                    this.SubRequestData = new QueryChangesRequest();
                    ((QueryChangesRequest)this.SubRequestData).Parse(s);
                    break;
                case 0x05:
                    this.SubRequestData = new PutChangesRequest();
                    ((PutChangesRequest)this.SubRequestData).Parse(s);
                    break;
                case 0x0B:
                    this.SubRequestData = new AllocateExtendedGUIDRangeRequest();
                    ((AllocateExtendedGUIDRangeRequest)this.SubRequestData).Parse(s);
                    break;
                default:
                    throw new Exception("The RequestType is not right.");
            }
            this.SubRequestEnd = new bit16StreamObjectHeaderEnd();
            this.SubRequestEnd.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.SubRequestEnd, 0x42, "Sub-request end");
        }
    }

    /// <summary>
    /// 2.2.2.1.1	Target Partition Id
    /// </summary>
    public class TargetPartitionId : BaseStructure
    {
        public bit32StreamObjectHeaderStart TargetPartitionIdStart;
        public Guid PartitionIdGUID;
        public bit16StreamObjectHeaderEnd TargetPartitionIdEnd;// FSSHTTP2013.saz #41 have end NewFolder.saz #35 #41 don't have end ,

        /// <summary>
        /// Parse the TargetPartitionId structure.
        /// </summary>
        /// <param name="s">An stream containing TargetPartitionId structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.TargetPartitionIdStart = new bit32StreamObjectHeaderStart();
            this.TargetPartitionIdStart.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.TargetPartitionIdStart, 0x83, "Target Partition Id ");
            this.PartitionIdGUID = ReadGuid();
            short tempShrot = ReadINT16();
            s.Position -= 2;
            if (tempShrot == 0x020F)
            {
                this.TargetPartitionIdEnd = new bit16StreamObjectHeaderEnd();
                this.TargetPartitionIdEnd.Parse(s);
            }
        }
    }

    /// <summary>
    /// Section 2.2.2.1.2   Query Access
    /// </summary>
    public class QueryAccessRequest
    {
        // Query access does not have any sub-request data.
    }

    /// <summary>
    /// 2.2.2.1.3	Query Changes
    /// </summary>
    public class QueryChangesRequest : BaseStructure
    {
        public bit32StreamObjectHeaderStart queryChangesRequest;
        [BitAttribute(1)]
        public byte A;
        [BitAttribute(1)]
        public byte B;
        [BitAttribute(1)]
        public byte C;
        [BitAttribute(1)]
        public byte D;
        [BitAttribute(1)]
        public byte E;
        [BitAttribute(1)]
        public byte F;
        [BitAttribute(2)]
        public byte G;
        public byte? Other;
        public bit32StreamObjectHeaderStart queryChangesRequestArguments;
        [BitAttribute(1)]
        public byte? F2;
        [BitAttribute(1)]
        public byte? G2;
        [BitAttribute(6)]
        public byte? H;
        public CellID CellID;
        public bit32StreamObjectHeaderStart QueryChangesDataConstraints;
        public CompactUnsigned64bitInteger MaxDataElements;
        public CompactUnsigned64bitInteger MajorVersionNumber;
        public CompactUnsigned64bitInteger MinorVersionNumber;
        public Filter[] QueryChangesFilters;
        public Knowledge Knowledge;

        /// <summary>
        /// Parse the Filter structure.
        /// </summary>
        /// <param name="s">An stream containing Filter structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.queryChangesRequest = new bit32StreamObjectHeaderStart();
            this.queryChangesRequest.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.queryChangesRequest, 0x51, "Query Changes request");
            byte tempByte = ReadByte();
            this.A = GetBits(tempByte, 0, 1);
            this.B = GetBits(tempByte, 1, 1);
            this.C = GetBits(tempByte, 2, 1);
            this.D = GetBits(tempByte, 3, 1);
            this.E = GetBits(tempByte, 4, 1);
            this.F = GetBits(tempByte, 5, 1);
            this.G = GetBits(tempByte, 6, 2);
            s.Position += 1;
            short tempInt = ReadINT16();
            s.Position -= 3;
            if (tempInt == 0x02DA)
            {
                this.Other = ReadByte();
            }

            if (ContainsStreamObjectStart32BitHeader(0x05B))
            {
                this.queryChangesRequestArguments = new bit32StreamObjectHeaderStart();
                this.queryChangesRequestArguments.Parse(s);

                byte tempByte2 = ReadByte();
                this.F2 = GetBits(tempByte2, 0, 1);
                this.G2 = GetBits(tempByte2, 1, 1);
                this.H = GetBits(tempByte2, 2, 6);
            }

            this.CellID = new CellID();
            this.CellID.Parse(s);
            if (ContainsStreamObjectStart32BitHeader(0x059))
            {
                this.QueryChangesDataConstraints = new bit32StreamObjectHeaderStart();
                this.QueryChangesDataConstraints.Parse(s);
            }
            if (this.QueryChangesDataConstraints != null)
            {
                this.MaxDataElements = new CompactUnsigned64bitInteger();
                this.MaxDataElements = this.MaxDataElements.TryParse(s);
            }

            //TODO��The below two fields are newly add in TD, but they have not conatined in the capture data for sharepoint 2013 server
            //this.MajorVersionNumber = new CompactUnsigned64bitInteger();
            //this.MajorVersionNumber = this.MajorVersionNumber.TryParse(s);
            //this.MinorVersionNumber = new CompactUnsigned64bitInteger();
            //this.MinorVersionNumber = this.MinorVersionNumber.TryParse(s);
            List<Filter> FilterList = new List<Filter>();
            while (ContainsStreamObjectStart32BitHeader(0x47))
            {
                Filter tempFilter = new Filter();
                tempFilter.Parse(s);
                FilterList.Add(tempFilter);
            }
            this.QueryChangesFilters = FilterList.ToArray();

            if (ContainsStreamObjectStart16BitHeader(0x10))
            {
                this.Knowledge = new Knowledge();
                this.Knowledge.Parse(s);
            }
        }
    }

    /// <summary>
    /// 2.2.2.1.3.1	Filters
    /// </summary>
    public class Filter : BaseStructure
    {
        public bit32StreamObjectHeaderStart QueryChangesFilterStart;
        public FilterType FilterType;
        public byte FilterOperation;
        public object QueryChangesFilterData;
        public bit16StreamObjectHeaderEnd QueryChangesFilterEnd;
        public bit32StreamObjectHeaderStart QueryChangesFilterFlags;
        [BitAttribute(1)]
        public byte? F;
        [BitAttribute(7)]
        public byte? Reserved;

        /// <summary>
        /// Parse the Filter structure.
        /// </summary>
        /// <param name="s">An stream containing Filter structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.QueryChangesFilterStart = new bit32StreamObjectHeaderStart();
            this.QueryChangesFilterStart.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.QueryChangesFilterStart, 0x47, "Query Changes Filter");
            this.FilterType = (FilterType)ReadByte();
            this.FilterOperation = ReadByte();

            switch (this.FilterType)
            {
                case Parsers.FilterType.AllFilter:
                case Parsers.FilterType.StorageIndexReferencedDataElementsFilter:
                    break;
                case Parsers.FilterType.DataElementIDsFilter:
                    this.QueryChangesFilterData = new DataElementIDsFilter();
                    ((DataElementIDsFilter)this.QueryChangesFilterData).Parse(s);
                    break;
                case Parsers.FilterType.DataElementTypeFilter:
                    this.QueryChangesFilterData = new DataElementTypeFilter();
                    ((DataElementTypeFilter)this.QueryChangesFilterData).Parse(s);
                    break;
                case Parsers.FilterType.CellIDFilter:
                    this.QueryChangesFilterData = new CellIDFilter();
                    ((CellIDFilter)this.QueryChangesFilterData).Parse(s);
                    break;
                case Parsers.FilterType.CustomFilter:
                    this.QueryChangesFilterData = new CustomFilter();
                    ((CustomFilter)this.QueryChangesFilterData).Parse(s);
                    break;
                case Parsers.FilterType.HierarchyFilter:
                    this.QueryChangesFilterData = new HierarchyFilter();
                    ((HierarchyFilter)this.QueryChangesFilterData).Parse(s);
                    break;
                default:
                    throw new Exception("The FilterType is not right.");
            }

            this.QueryChangesFilterEnd = new bit16StreamObjectHeaderEnd();
            this.QueryChangesFilterEnd.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.QueryChangesFilterEnd, 0x47, "Query Changes Filter End");

            if (ContainsStreamObjectStart32BitHeader(0x87))
            {
                this.QueryChangesFilterFlags = new bit32StreamObjectHeaderStart();
                this.QueryChangesFilterFlags.Parse(s);
            }
            if (this.QueryChangesFilterFlags != null)
            {
                byte tempByte = ReadByte();
                this.F = GetBits(tempByte, 0, 1);
                this.Reserved = GetBits(tempByte, 1, 7);
            }
        }
    }

    /// <summary>
    /// The enumeration of filter type
    /// </summary>
    public enum FilterType : byte
    {
        AllFilter = 1,
        DataElementTypeFilter = 2,
        StorageIndexReferencedDataElementsFilter = 3,
        CellIDFilter = 4,
        CustomFilter = 5,
        DataElementIDsFilter = 6,
        HierarchyFilter = 7
    }

    /// <summary>
    /// Section 2.2.2.1.3.1.1   All Filter
    /// </summary>
    public class AllFilter
    {
        // The All filter specifies a filter that matches all data elements. This filter does not contain any data.
    }

    /// <summary>
    /// 2.2.2.1.3.1.2	Data Element Type Filter
    /// </summary>
    public class DataElementTypeFilter : BaseStructure
    {
        public bit32StreamObjectHeaderStart QueryChangesFilterDataElementType;
        public CompactUnsigned64bitInteger DataElementType;

        /// <summary>
        /// Parse the DataElementTypeFilter structure.
        /// </summary>
        /// <param name="s">An stream containing DataElementTypeFilter structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.QueryChangesFilterDataElementType = new bit32StreamObjectHeaderStart();
            this.QueryChangesFilterDataElementType.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.QueryChangesFilterDataElementType, 0x57, "Query Changes Filter Data Element type");
            this.DataElementType = new CompactUnsigned64bitInteger();
            this.DataElementType = this.DataElementType.TryParse(s);
        }
    }

    /// <summary>
    /// Section 2.2.2.1.3.1.3   Storage Index Referenced Data Elements Filter
    /// </summary>
    public class StorageIndexReferencedDataElementsFilter
    {
        // This filter is not currently supported by the server.
    }

    /// <summary>
    /// 2.2.2.1.3.1.4	Cell ID Filter
    /// </summary>
    public class CellIDFilter : BaseStructure
    {
        public bit32StreamObjectHeaderStart QueryChangesFilterCellID;
        public CellID CellID;

        /// <summary>
        /// Parse the CellIDFilter structure.
        /// </summary>
        /// <param name="s">An stream containing CellIDFilter structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.QueryChangesFilterCellID = new bit32StreamObjectHeaderStart();
            this.QueryChangesFilterCellID.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.QueryChangesFilterCellID, 0x5C, "Query Changes Filter Cell ID");
            this.CellID = new CellID();
            this.CellID.Parse(s);
        }
    }

    /// <summary>
    /// 2.2.2.1.3.1.5	Custom Filter
    /// </summary>
    public class CustomFilter : BaseStructure
    {
        public bit32StreamObjectHeaderStart QueryChangesFilterSchemaSpecific;
        public Guid SchemaGUID;
        public byte[] SchemaFilterData;

        /// <summary>
        /// Parse the CustomFilter structure.
        /// </summary>
        /// <param name="s">An stream containing CustomFilter structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.QueryChangesFilterSchemaSpecific = new bit32StreamObjectHeaderStart();
            this.QueryChangesFilterSchemaSpecific.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.QueryChangesFilterSchemaSpecific, 0x50, "Query Changes Filter schema specific");
            this.SchemaGUID = ReadGuid();
            this.SchemaFilterData = ReadBytes(QueryChangesFilterSchemaSpecific.GetDataLength() - 16);
        }
    }

    /// <summary>
    /// 2.2.2.1.3.1.6	Data Element IDs Filter
    /// </summary>
    public class DataElementIDsFilter : BaseStructure
    {
        public bit32StreamObjectHeaderStart QueryChangesFilterDataElementIDs;
        public ExtendedGUIDArray DataElementIDs;

        /// <summary>
        /// Parse the DataElementIDsFilter structure.
        /// </summary>
        /// <param name="s">An stream containing DataElementIDsFilter structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.QueryChangesFilterDataElementIDs = new bit32StreamObjectHeaderStart();
            this.QueryChangesFilterDataElementIDs.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.QueryChangesFilterDataElementIDs, 0x54, "Query Changes Filter Data Element IDs");
            this.DataElementIDs = new ExtendedGUIDArray();
            this.DataElementIDs.Parse(s);
        }
    }

    /// <summary>
    /// 2.2.2.1.3.1.7	Hierarchy Filter
    /// </summary>
    public class HierarchyFilter : BaseStructure
    {
        public bit32StreamObjectHeaderStart QueryChangesFilterHierarchy;
        public HierarchyFilterDepth Depth;
        public CompactUnsigned64bitInteger Count;
        public Byte[] RootIndexKeyByteArray;

        /// <summary>
        /// Parse the HierarchyFilter structure.
        /// </summary>
        /// <param name="s">An stream containing HierarchyFilter structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.QueryChangesFilterHierarchy = new bit32StreamObjectHeaderStart();
            this.QueryChangesFilterHierarchy.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.QueryChangesFilterHierarchy, 0x60, "Query Changes Filter hierarchy");
            this.Depth = (HierarchyFilterDepth)ReadByte();
            this.Count = new CompactUnsigned64bitInteger();
            this.Count = this.Count.TryParse(s);
            this.RootIndexKeyByteArray = ReadBytes((int)this.Count.GetUint(this.Count));
        }
    }

    /// <summary>
    /// The enumeration of the hierarchy filter depth
    /// </summary>
    public enum HierarchyFilterDepth : byte
    {
        // Index values corresponding to the specified keys only.
        IndexOnly = 0,

        // First data elements referenced by the storage index values corresponding to the specified keys only.
        FirstDataElement = 1,

        // Single level. All data elements under the sub-graphs rooted by the specified keys stopping at any storage index entries.
        SingleLevel = 2,

        // Deep. All data elements and storage index entries under the sub-graphs rooted by the specified keys.
        Deep = 3
    }

    /// <summary>
    /// 2.2.2.1.4	Put Changes
    /// </summary>
    public class PutChangesRequest : BaseStructure
    {
        public bit32StreamObjectHeaderStart putChangesRequest;
        public ExtendedGUID StorageIndexExtendedGUID;
        public ExtendedGUID ExpectedStorageIndexExtendedGUID;
        [BitAttribute(1)]
        public byte A;
        [BitAttribute(1)]
        public byte B;
        [BitAttribute(1)]
        public byte C;
        [BitAttribute(1)]
        public byte D;
        [BitAttribute(1)]
        public byte E;
        [BitAttribute(1)]
        public byte F;
        [BitAttribute(1)]
        public byte G;
        [BitAttribute(1)]
        public byte H;
        public AdditionalFlags AdditionalFlags;
        public LockId LockId;
        public Knowledge ClientKnowledge;
        public DiagnosticRequestOptionInput DiagnosticRequestOptionInput;

        /// <summary>
        /// Parse the PutChangesRequest structure.
        /// </summary>
        /// <param name="s">An stream containing PutChangesRequest structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.putChangesRequest = new bit32StreamObjectHeaderStart();
            this.putChangesRequest.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.putChangesRequest, 0x5A, "Put Changes request");
            this.StorageIndexExtendedGUID = new ExtendedGUID();
            this.StorageIndexExtendedGUID = this.StorageIndexExtendedGUID.TryParse(s);
            this.ExpectedStorageIndexExtendedGUID = new ExtendedGUID();
            this.ExpectedStorageIndexExtendedGUID = this.ExpectedStorageIndexExtendedGUID.TryParse(s);
            byte tempByte = ReadByte();
            this.A = (byte)GetBits(tempByte, 0, 1);
            this.B = (byte)GetBits(tempByte, 1, 1);
            this.C = (byte)GetBits(tempByte, 2, 1);
            this.D = (byte)GetBits(tempByte, 3, 1);
            this.E = (byte)GetBits(tempByte, 4, 1);
            this.F = (byte)GetBits(tempByte, 5, 1);
            this.G = (byte)GetBits(tempByte, 6, 1);
            this.H = (byte)GetBits(tempByte, 7, 1);
            if (ContainsStreamObjectStart32BitHeader(0x86))
            {
                this.AdditionalFlags = new AdditionalFlags();
                this.AdditionalFlags.Parse(s);
            }
            if (ContainsStreamObjectStart32BitHeader(0x85))
            {
                this.LockId = new LockId();
                this.LockId.Parse(s);
            }
            if (ContainsStreamObjectStart16BitHeader(0x10))
            {
                this.ClientKnowledge = new Knowledge();
                this.ClientKnowledge.Parse(s);
            }
            if (ContainsStreamObjectStart32BitHeader(0x8A))
            {
                this.DiagnosticRequestOptionInput = new DiagnosticRequestOptionInput();
                this.DiagnosticRequestOptionInput.Parse(s);
            }
        }
    }

    /// <summary>
    /// 2.2.2.1.4.1	Additional Flags
    /// </summary>
    public class AdditionalFlags : BaseStructure
    {
        public bit32StreamObjectHeaderStart AdditionalFlagsHeader;
        [BitAttribute(1)]
        public byte A;
        [BitAttribute(1)]
        public byte B;
        [BitAttribute(1)]
        public byte C;
        [BitAttribute(1)]
        public byte D;
        [BitAttribute(1)]
        public byte E;
        [BitAttribute(1)]
        public byte F;
        [BitAttribute(10)]
        public ushort Reserved;

        /// <summary>
        /// Parse the AdditionalFlags structure.
        /// </summary>
        /// <param name="s">An stream containing AdditionalFlags structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.AdditionalFlagsHeader = new bit32StreamObjectHeaderStart();
            this.AdditionalFlagsHeader.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.AdditionalFlagsHeader, 0x86, "Additional Flags");
            short tempUshort = ReadINT16();
            this.A = (byte)GetBits(tempUshort, 0, 1);
            this.B = (byte)GetBits(tempUshort, 1, 1);
            this.C = (byte)GetBits(tempUshort, 2, 1);
            this.D = (byte)GetBits(tempUshort, 3, 1);
            this.E = (byte)GetBits(tempUshort, 4, 1);
            this.F = (byte)GetBits(tempUshort, 5, 1);
            this.Reserved = (byte)GetBits(tempUshort, 6, 16);
        }
    }

    /// <summary>
    /// 2.2.2.1.4.2	Lock Id
    /// </summary>
    public class LockId : BaseStructure
    {
        public bit32StreamObjectHeaderStart LockIdHeader;
        public Guid LockIdGuid;

        /// <summary>
        /// Parse the LockId structure.
        /// </summary>
        /// <param name="s">An stream containing LockId structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.LockIdHeader = new bit32StreamObjectHeaderStart();
            this.LockIdHeader.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.LockIdHeader, 0x85, "Put Changes Lock Id");
            this.LockIdGuid = ReadGuid();
        }
    }

    /// <summary>
    /// 2.2.2.1.4.3	Diagnostic Request Option Input
    /// </summary>
    public class DiagnosticRequestOptionInput : BaseStructure
    {
        public bit32StreamObjectHeaderStart DiagnosticRequestOptionInputHeader;
        [BitAttribute(1)]
        public byte A;
        [BitAttribute(7)]
        public byte Reserved;

        /// <summary>
        /// Parse the DiagnosticRequestOptionInput structure.
        /// </summary>
        /// <param name="s">An stream containing DiagnosticRequestOptionInput structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.DiagnosticRequestOptionInputHeader = new bit32StreamObjectHeaderStart();
            this.DiagnosticRequestOptionInputHeader.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.DiagnosticRequestOptionInputHeader, 0x8A, "Diagnostic Request Option Input ");
            byte tempByte = ReadByte();
            this.A = GetBits(tempByte, 0, 1);
            this.Reserved = GetBits(tempByte, 1, 7);
        }
    }

    /// <summary>
    /// 2.2.2.1.5	Allocate Extended GUID Range
    /// </summary>
    public class AllocateExtendedGUIDRangeRequest : BaseStructure
    {
        public bit32StreamObjectHeaderStart allocateExtendedGUIDRangeRequest;
        public CompactUnsigned64bitInteger RequestIdCount;
        public byte Reserved;

        /// <summary>
        /// Parse the AllocateExtendedGUIDRangeRequest structure.
        /// </summary>
        /// <param name="s">An stream containing AllocateExtendedGUIDRangeRequest structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.allocateExtendedGUIDRangeRequest = new bit32StreamObjectHeaderStart();
            this.allocateExtendedGUIDRangeRequest.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.allocateExtendedGUIDRangeRequest, 0x80, "Allocate Extended GUID Range request");
            this.RequestIdCount = new CompactUnsigned64bitInteger();
            this.RequestIdCount = this.RequestIdCount.TryParse(s);
            this.Reserved = ReadByte();
        }
    }

    /// <summary>
    /// 2.2.3	Response Message Syntax
    /// </summary>
    public class FsshttpbResponse : BaseStructure
    {
        public ushort ProtocolVersion;
        public ushort MinimumVersion;
        public ulong Signature;
        public bit32StreamObjectHeaderStart ResponseStart;
        [BitAttribute(1)]
        public byte Status;
        [BitAttribute(7)]
        public byte Reserved;
        public ResponseError ResponseError;
        public DataElementPackage DataElementPackage;
        public FsshttpbSubResponse[] SubResponses;
        public bit16StreamObjectHeaderEnd ResponseEnd;

        /// <summary>
        /// Parse the FsshttpbResponse structure.
        /// </summary>
        /// <param name="s">An stream containing FsshttpbResponse structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.ProtocolVersion = ReadUshort();
            this.MinimumVersion = ReadUshort();
            this.Signature = ReadUlong();
            this.ResponseStart = new bit32StreamObjectHeaderStart();
            this.ResponseStart.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.ResponseStart, 0x62, "Response");
            byte tempByte = ReadByte();
            this.Status = GetBits(tempByte, 0, 1);
            this.Reserved = GetBits(tempByte, 1, 7);

            if (this.Status == 0x1)
            {
                this.ResponseError = new ResponseError();
                this.ResponseError.Parse(s);
            }
            else
            {
                if (ContainsStreamObjectStart16BitHeader(0x15))
                {
                    this.DataElementPackage = new DataElementPackage();
                    this.DataElementPackage.Parse(s);
                }

                if (ContainsStreamObjectStart32BitHeader(0x041))
                {
                    List<FsshttpbSubResponse> tempResponses = new List<FsshttpbSubResponse>();
                    do
                    {
                        FsshttpbSubResponse subResponse = new FsshttpbSubResponse();
                        subResponse.Parse(s);
                        tempResponses.Add(subResponse);
                        this.SubResponses = tempResponses.ToArray();
                    } while (ContainsStreamObjectStart32BitHeader(0x041));
                }
            }

            this.ResponseEnd = new bit16StreamObjectHeaderEnd();
            this.ResponseEnd.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.ResponseEnd, 0x62, "Response End");
        }
    }

    /// <summary>
    /// 2.2.3.1	Sub-Responses
    /// </summary>
    public class FsshttpbSubResponse : BaseStructure
    {
        public bit32StreamObjectHeaderStart SubResponseStart;
        public CompactUnsigned64bitInteger RequestID;
        public CompactUnsigned64bitInteger RequestType;
        [BitAttribute(1)]
        public byte Status;
        [BitAttribute(7)]
        public byte Reserved;
        public ResponseError ResponseError;
        public object SubResponseData;
        public bit16StreamObjectHeaderEnd SubResponseEnd;

        /// <summary>
        /// Parse the FsshttpbSubResponse structure.
        /// </summary>
        /// <param name="s">An stream containing FsshttpbSubResponse structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.SubResponseStart = new bit32StreamObjectHeaderStart();
            this.SubResponseStart.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.SubResponseStart, 0x41, "Sub-response");
            this.RequestID = new CompactUnsigned64bitInteger();
            this.RequestID = RequestID.TryParse(s);
            this.RequestType = new CompactUnsigned64bitInteger();
            this.RequestType = RequestType.TryParse(s);
            byte tempByte = ReadByte();
            this.Status = GetBits(tempByte, 0, 1);
            this.Reserved = GetBits(tempByte, 1, 7);
            if (this.Status == 0x1)
            {
                this.ResponseError = new ResponseError();
                this.ResponseError.Parse(s);
            }
            else
            {
                if (this.RequestType.GetUint(RequestType) == 0x01)
                {
                    this.SubResponseData = new QueryAccessResponse();
                    ((QueryAccessResponse)this.SubResponseData).Parse(s);
                }
                else if (this.RequestType.GetUint(RequestType) == 0x02)
                {
                    this.SubResponseData = new QueryChangesResponse();
                    ((QueryChangesResponse)this.SubResponseData).Parse(s);
                }
                else if (this.RequestType.GetUint(RequestType) == 0x05)
                {
                    this.SubResponseData = new PutChangesResponse();
                    ((PutChangesResponse)this.SubResponseData).Parse(s);
                }
                else if (this.RequestType.GetUint(RequestType) == 0x0B)
                {
                    this.SubResponseData = new AllocateExtendedGUIDRange();
                    ((AllocateExtendedGUIDRange)this.SubResponseData).Parse(s);
                }

            }
            this.SubResponseEnd = new bit16StreamObjectHeaderEnd();
            this.SubResponseEnd.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.SubResponseEnd, 0x41, "Sub-response End");
        }
    }

    /// <summary>
    /// 2.2.3.1.1	Query Access
    /// </summary>
    public class QueryAccessResponse : BaseStructure
    {
        public bit32StreamObjectHeaderStart ReadAccessResponseStart;
        public ResponseError ReadAccessResponseError;
        public bit16StreamObjectHeaderEnd ReadAccessResponseEnd;
        public bit32StreamObjectHeaderStart WriteAccessResponseStart;
        public ResponseError WriteAccessResponseError;
        public bit16StreamObjectHeaderEnd WriteAccessResponseEnd;

        /// <summary>
        /// Parse the QueryAccessResponse structure.
        /// </summary>
        /// <param name="s">An stream containing QueryAccessResponse structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.ReadAccessResponseStart = new bit32StreamObjectHeaderStart();
            this.ReadAccessResponseStart.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.ReadAccessResponseStart, 0x43, "Read access response start");
            this.ReadAccessResponseError = new ResponseError();
            this.ReadAccessResponseError.Parse(s);
            this.ReadAccessResponseEnd = new bit16StreamObjectHeaderEnd();
            this.ReadAccessResponseEnd.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.ReadAccessResponseEnd, 0x43, "Read access response end");
            this.WriteAccessResponseStart = new bit32StreamObjectHeaderStart();
            this.WriteAccessResponseStart.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.WriteAccessResponseStart, 0x46, "Write access response start");
            this.WriteAccessResponseError = new ResponseError();
            this.WriteAccessResponseError.Parse(s);
            this.WriteAccessResponseEnd = new bit16StreamObjectHeaderEnd();
            this.WriteAccessResponseEnd.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.WriteAccessResponseEnd, 0x46, "Write access response end");

        }
    }

    /// <summary>
    /// 2.2.3.1.2	Query Changes
    /// </summary>
    public class QueryChangesResponse : BaseStructure
    {
        public bit32StreamObjectHeaderStart queryChangesResponse;
        public ExtendedGUID StorageIndexExtendedGUID;
        [BitAttribute(1)]
        public byte P;
        [BitAttribute(7)]
        public byte Reserved;
        public Knowledge Knowledge;

        /// <summary>
        /// Parse the QueryChangesResponse structure.
        /// </summary>
        /// <param name="s">An stream containing QueryChangesResponse structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.queryChangesResponse = new bit32StreamObjectHeaderStart();
            this.queryChangesResponse.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.queryChangesResponse, 0x5F, "Query Changes response");
            this.StorageIndexExtendedGUID = new ExtendedGUID();
            this.StorageIndexExtendedGUID = this.StorageIndexExtendedGUID.TryParse(s);
            byte tempbyte = ReadByte();
            this.P = GetBits(tempbyte, 0, 1);
            this.Reserved = GetBits(tempbyte, 1, 7);
            this.Knowledge = new Knowledge();
            this.Knowledge.Parse(s);
        }
    }

    /// <summary>
    /// 2.2.3.1.3	Put Changes
    /// </summary>
    public class PutChangesResponse : BaseStructure
    {
        public bit32StreamObjectHeaderStart putChangesResponse;
        public ExtendedGUID AppliedStorageIndexId;
        public ExtendedGUIDArray DataElementsAdded;
        public Knowledge ResultantKnowledge;
        public DiagnosticRequesOptionOutput DiagnosticRequestOptionOutput;

        /// <summary>
        /// Parse the PutChangesResponse structure.
        /// </summary>
        /// <param name="s">An stream containing PutChangesResponse structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            if (ContainsStreamObjectStart32BitHeader(0x87))//TODO WOPI2016_HTTP_onlywopi.2saz.saz #1197 #1199
            {
                this.putChangesResponse = new bit32StreamObjectHeaderStart();
                this.putChangesResponse.Parse(s);
 				ValidationChecker.ValidationCheckHeader(this.putChangesResponse, 0x87, "Put Changes Response");
                this.AppliedStorageIndexId = new ExtendedGUID();
                this.AppliedStorageIndexId = this.AppliedStorageIndexId.TryParse(s);
                this.DataElementsAdded = new ExtendedGUIDArray();
                this.DataElementsAdded.Parse(s);
            }

            this.ResultantKnowledge = new Knowledge();
            this.ResultantKnowledge.Parse(s);
            if (ContainsStreamObjectStart32BitHeader(0x89))
            {
                this.DiagnosticRequestOptionOutput = new DiagnosticRequesOptionOutput();
                this.DiagnosticRequestOptionOutput.Parse(s);
            }
        }
    }

    /// <summary>
    /// 2.2.3.1.3.1	Diagnostic Request Option Output
    /// </summary>
    public class DiagnosticRequesOptionOutput : BaseStructure
    {
        public bit32StreamObjectHeaderStart diagnosticRequestOptionOutputHeader;
        [BitAttribute(1)]
        public byte Forced;
        [BitAttribute(7)]
        public byte Reserved;

        /// <summary>
        /// Parse the DiagnosticRequesOptionOutput structure.
        /// </summary>
        /// <param name="s">An stream containing DiagnosticRequesOptionOutput structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.diagnosticRequestOptionOutputHeader = new bit32StreamObjectHeaderStart();
            this.diagnosticRequestOptionOutputHeader.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.diagnosticRequestOptionOutputHeader, 0x89, "Diagnostic Request Option Output");
            byte tempByte = ReadByte();
            this.Forced = GetBits(tempByte, 0, 1);
            this.Reserved = GetBits(tempByte, 1, 7);
        }
    }

    /// <summary>
    /// 2.2.3.1.4	Allocate Extended GUID Range
    /// </summary>
    public class AllocateExtendedGUIDRange : BaseStructure
    {
        public bit32StreamObjectHeaderStart AllocateExtendedGUIDRangeResponse;
        public Guid GUIDComponent;
        public CompactUnsigned64bitInteger IntegerRangeMin;
        public CompactUnsigned64bitInteger IntegerRangeMax;

        /// <summary>
        /// Parse the AllocateExtendedGUIDRange structure.
        /// </summary>
        /// <param name="s">An stream containing AllocateExtendedGUIDRange structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.AllocateExtendedGUIDRangeResponse = new bit32StreamObjectHeaderStart();
            this.AllocateExtendedGUIDRangeResponse.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.AllocateExtendedGUIDRangeResponse, 0x81, "Allocate Extended GUID Range response");
            this.GUIDComponent = ReadGuid();
            this.IntegerRangeMin = new CompactUnsigned64bitInteger();
            this.IntegerRangeMin = this.IntegerRangeMin.TryParse(s);
            this.IntegerRangeMax = new CompactUnsigned64bitInteger();
            this.IntegerRangeMax = this.IntegerRangeMax.TryParse(s);
        }
    }

    /// <summary>
    /// Section 2.2.3.2   Response Error
    /// </summary>
    public class ResponseError : BaseStructure
    {
        public bit32StreamObjectHeaderStart ErrorStart;
        public Guid ErrorTypeGUID;
        public object ErrorData;
        public bit32StreamObjectHeaderStart ErrorStringSupplementalInfoStart;
        public StringItem ErrorStringSupplementalInfo;
        public ResponseError ChainedError;
        public bit16StreamObjectHeaderEnd ErrorEnd;

        /// <summary>
        /// Parse the ResponseError structure.
        /// </summary>
        /// <param name="s">An stream containing ResponseError structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.ErrorStart = new bit32StreamObjectHeaderStart();
            this.ErrorStart.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.ErrorStart, 0x4D, "Error");
            this.ErrorTypeGUID = ReadGuid();
            if (this.ErrorTypeGUID.ToString().ToUpper() == "5A66A756-87CE-4290-A38B-C61C5BA05A67")
            {
                this.ErrorData = new CellError();
                ((CellError)this.ErrorData).Parse(s);
            }
            else if (this.ErrorTypeGUID.ToString().ToUpper() == "7AFEAEBF-033D-4828-9C31-3977AFE58249")
            {
                this.ErrorData = new ProtocolError();
                ((ProtocolError)this.ErrorData).Parse(s);
            }
            else if (this.ErrorTypeGUID.ToString().ToUpper() == "32C39011-6E39-46C4-AB78-DB41929D679E")
            {
                this.ErrorData = new Win32Error();
                ((Win32Error)this.ErrorData).Parse(s);
            }
            else if (this.ErrorTypeGUID.ToString().ToUpper() == "8454C8F2-E401-405A-A198-A10B6991B56E")
            {
                this.ErrorData = new HRESULTError();
                ((HRESULTError)this.ErrorData).Parse(s);
            }

            if (ContainsStreamObjectStart32BitHeader(0x04E))
            {
                this.ErrorStringSupplementalInfoStart = new bit32StreamObjectHeaderStart();
                this.ErrorStringSupplementalInfoStart.Parse(s);
                this.ErrorStringSupplementalInfo = new StringItem();
                this.ErrorStringSupplementalInfo.Parse(s);
            }

            if (ContainsStreamObjectStart32BitHeader(0x04D))
            {
                this.ChainedError = new ResponseError();
                this.ChainedError.Parse(s);
            }
            this.ErrorEnd = new bit16StreamObjectHeaderEnd();
            this.ErrorEnd.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.ErrorEnd, 0x4D, "Error End");
        }
    }

    /// <summary>
    /// 2.2.3.2.1 Cell Error
    /// </summary>
    public class CellError : BaseStructure
    {
        public bit32StreamObjectHeaderStart ErrorCell;
        public CellErrorCode ErrorCode;

        /// <summary>
        /// Parse the CellError structure.
        /// </summary>
        /// <param name="s">An stream containing CellError structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.ErrorCell = new bit32StreamObjectHeaderStart();
            this.ErrorCell.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.ErrorCell, 0x66, "Cell Error");
            this.ErrorCode = (CellErrorCode)ReadUint();
        }
    }

    /// <summary>
    /// The enumeration of the cell error code. section 2.2.3.2.1
    /// </summary>
    public enum CellErrorCode : uint
    {
        /// <summary>
        /// Unknown error
        /// </summary>
        Unknownerror = 1,

        /// <summary>
        /// Invalid object
        /// </summary>
        InvalidObject = 2,

        /// <summary>
        /// Invalid partition
        /// </summary>
        Invalidpartition = 3,

        /// <summary>
        /// Request not supported
        /// </summary>
        Requestnotsupported = 4,

        /// <summary>
        /// Storage readonly
        /// </summary>
        Storagereadonly = 5,

        /// <summary>
        /// Revision ID not found
        /// </summary>
        RevisionIDnotfound = 6,

        /// <summary>
        /// The Bad token
        /// </summary>
        Badtoken = 7,

        /// <summary>
        /// Request not finished
        /// </summary>
        Requestnotfinished = 8,

        /// <summary>
        /// Incompatible token
        /// </summary>
        Incompatibletoken = 9,

        /// <summary>
        /// Scoped cell storage
        /// </summary>
        Scopedcellstorage = 11,

        /// <summary>
        /// Coherency failure
        /// </summary>
        Coherencyfailure = 12,

        /// <summary>
        /// Cell storage state deserialization failure
        /// </summary>
        Cellstoragestatedeserializationfailure = 13,

        /// <summary>
        /// Incompatible protocol version
        /// </summary>
        Incompatibleprotocolversion = 15,

        /// <summary>
        /// Referenced data element not found
        /// </summary>
        Referenceddataelementnotfound = 16,

        /// <summary>
        /// Request stream schema error
        /// </summary>
        Requeststreamschemaerror = 18,

        /// <summary>
        /// Response stream schema error
        /// </summary>
        Responsestreamschemaerror = 19,

        /// <summary>
        /// Unknown request
        /// </summary>
        Unknownrequest = 20,

        /// <summary>
        /// Storage failure
        /// </summary>
        Storagefailure = 21,

        /// <summary>
        /// Storage write only
        /// </summary>
        Storagewriteonly = 22,

        /// <summary>
        /// Invalid serialization
        /// </summary>
        Invalidserialization = 23,

        /// <summary>
        /// Data element not found
        /// </summary>
        Dataelementnotfound = 24,

        /// <summary>
        /// Invalid implementation
        /// </summary>
        Invalidimplementation = 25,

        /// <summary>
        /// Incompatible old storage
        /// </summary>
        Incompatibleoldstorage = 26,

        /// <summary>
        /// Incompatible new storage
        /// </summary>
        Incompatiblenewstorage = 27,

        /// <summary>
        /// Incorrect context for data element ID
        /// </summary>
        IncorrectcontextfordataelementID = 28,

        /// <summary>
        /// Object group duplicate objects
        /// </summary>
        Objectgroupduplicateobjects = 29,

        /// <summary>
        /// Object reference not founding revision
        /// </summary>
        Objectreferencenotfoundinrevision = 31,

        /// <summary>
        /// Merge cell storage state conflict
        /// </summary>
        Mergecellstoragestateconflict = 32,

        /// <summary>
        /// Unknown query changes filter
        /// </summary>
        Unknownquerychangesfilter = 33,

        /// <summary>
        /// Unsupported query changes filter
        /// </summary>
        Unsupportedquerychangesfilter = 34,

        /// <summary>
        /// Unable to provide knowledge
        /// </summary>
        Unabletoprovideknowledge = 35,

        /// <summary>
        /// Data element missing ID
        /// </summary>
        DataelementmissingID = 36,

        /// <summary>
        /// Data element missing serial number
        /// </summary>
        Dataelementmissingserialnumber = 37,

        /// <summary>
        /// Request argument invalid
        /// </summary>
        Requestargumentinvalid = 38,

        /// <summary>
        /// Partial changes not supported
        /// </summary>
        Partialchangesnotsupported = 39,

        /// <summary>
        /// Store busy retry later
        /// </summary>
        Storebusyretrylater = 40,

        /// <summary>
        /// GUIDID table not supported
        /// </summary>
        GUIDIDtablenotsupported = 41,

        /// <summary>
        /// Data element cycle
        /// </summary>
        Dataelementcycle = 42,

        /// <summary>
        /// Fragment knowledge error
        /// </summary>
        Fragmentknowledgeerror = 43,

        /// <summary>
        /// Fragment size mismatch
        /// </summary>
        Fragmentsizemismatch = 44,

        /// <summary>
        /// Fragments incomplete
        /// </summary>
        Fragmentsincomplete = 45,

        /// <summary>
        /// Fragment invalid
        /// </summary>
        Fragmentinvalid = 46,

        /// <summary>
        /// Aborted after failed put changes
        /// </summary>
        Abortedafterfailedputchanges = 47,

        /// <summary>
        /// Upgrade failed because there are no upgradeable contents.
        /// </summary>
        FailedNoUpgradeableContents = 79,

        /// <summary>
        /// Unable to allocate additional extended GUIDs.
        /// </summary>
        UnableAllocateAdditionalExtendedGuids = 106,

        /// <summary>
        /// Site is in read-only mode.
        /// </summary>
        SiteReadonlyMode = 108,

        /// <summary>
        /// Multi-Request partition reached quota.
        /// </summary>
        MultiRequestPartitionReachQutoa = 111,

        /// <summary>
        /// Extended GUID collision.
        /// </summary>
        ExtendedGuidCollision = 112,

        /// <summary>
        /// Upgrade failed because of insufficient permissions.
        /// </summary>
        InsufficientPermisssions = 113,

        /// <summary>
        /// Upgrade failed because of server throttling.
        /// </summary>
        ServerThrottling = 114,

        /// <summary>
        /// Upgrade failed because the upgraded file is too large.
        /// </summary>
        FileTooLarge = 115
    }

    /// <summary>
    /// 2.2.3.2.2 Protocol Error
    /// </summary>
    public class ProtocolError : BaseStructure
    {
        public bit32StreamObjectHeaderStart ErrorProtocol;
        public ProtocolErrorCode ErrorCode;

        /// <summary>
        /// Parse the ProtocolError structure.
        /// </summary>
        /// <param name="s">An stream containing ProtocolError structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.ErrorProtocol = new bit32StreamObjectHeaderStart();
            this.ErrorProtocol.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.ErrorProtocol, 0x4B, "Protocol Error");
            this.ErrorCode = (ProtocolErrorCode)ReadUint();
        }
    }

    /// <summary>
    /// The enumeration of the protocol error code, section 2.2.3.2.2
    /// </summary>
    public enum ProtocolErrorCode
    {
        /// <summary>
        /// Unknown error
        /// </summary>
        Unknownerror = 1,

        /// <summary>
        /// End of Stream
        /// </summary>
        EndofStream = 50,

        /// <summary>
        /// Unknown internal error
        /// </summary>
        Unknowninternalerror = 61,

        /// <summary>
        /// Input stream schema invalid
        /// </summary>
        Inputstreamschemainvalid = 108,

        /// <summary>
        /// Stream object invalid
        /// </summary>
        Streamobjectinvalid = 142,

        /// <summary>
        /// Stream object unexpected
        /// </summary>
        Streamobjectunexpected = 143,

        /// <summary>
        /// Server URL not found
        /// </summary>
        ServerURLnotfound = 144,

        /// <summary>
        /// Stream object serialization error
        /// </summary>
        Streamobjectserializationerror = 145,
    }

    /// <summary>
    /// 2.2.3.2.3 Win32 Error
    /// </summary>
    public class Win32Error : BaseStructure
    {
        public bit32StreamObjectHeaderStart ErrorWin32;
        public uint ErrorCode;

        /// <summary>
        /// Parse the Win32Error structure.
        /// </summary>
        /// <param name="s">An stream containing Win32Error structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.ErrorWin32 = new bit32StreamObjectHeaderStart();
            this.ErrorWin32.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.ErrorWin32, 0x49, "Win32 Error");
            this.ErrorCode = ReadUint();
        }
    }

    /// <summary>
    /// 2.2.3.2.4 HRESULT Error
    /// </summary>
    public class HRESULTError : BaseStructure
    {
        public bit32StreamObjectHeaderStart ErrorHRESULT;
        public uint ErrorCode;

        /// <summary>
        /// Parse the HRESULTError structure.
        /// </summary>
        /// <param name="s">An stream containing HRESULTError structure.</param>
        public override void Parse(Stream s)
        {
            base.Parse(s);
            this.ErrorHRESULT = new bit32StreamObjectHeaderStart();
            this.ErrorHRESULT.Parse(s);
            ValidationChecker.ValidationCheckHeader(this.ErrorHRESULT, 0x52, "Error HRESULT");
            this.ErrorCode = ReadUint();
        }
    }

    public static class ValidationChecker
    {
        public static bool ValidationCheckHeader(object header, ushort expectedType, string headerName)
        {
            if (header == null)
                return false;

            if (header is bit16StreamObjectHeaderStart)
            {
                bit16StreamObjectHeaderStart bit16Header = new bit16StreamObjectHeaderStart();
                bit16Header = header as bit16StreamObjectHeaderStart;
                if (bit16Header.Type != (StreamObjectTypeHeaderStart)expectedType)
                {
                    FSSHTTPandWOPIInspector.validationErrors += string.Format("MS-FSSHTTPB: The Type field for {0} StreamObjectStart16BitHeader should be {1:x}, but not {2:x}.", headerName, expectedType, bit16Header.Type);
                    return false;
                }
            }

            if (header is bit32StreamObjectHeaderStart)
            {
                bit32StreamObjectHeaderStart bit32Header = new bit32StreamObjectHeaderStart();
                bit32Header = header as bit32StreamObjectHeaderStart;
                if (bit32Header.Type != (StreamObjectTypeHeaderStart)expectedType)
                {
                    FSSHTTPandWOPIInspector.validationErrors += string.Format("MS-FSSHTTPB: The Type field for {0} StreamObjectStart32BitHeader should be {1:x}, but not {2:x}.", headerName, expectedType, bit32Header.Type);
                    return false;
                }
            }

            if (header is bit16StreamObjectHeaderEnd)
            {
                bit16StreamObjectHeaderEnd bit16HeaderEnd = new bit16StreamObjectHeaderEnd();
                bit16HeaderEnd = header as bit16StreamObjectHeaderEnd;
                if (bit16HeaderEnd.Type != (StreamObjectTypeHeaderEnd)expectedType)
                {
                    FSSHTTPandWOPIInspector.validationErrors += string.Format("MS-FSSHTTPB: The Type field for {0} StreamObjectEnd16BitHeader should be {1:x}, but not {2:x}.", headerName, expectedType, bit16HeaderEnd.Type);
                    return false;
                }
            }

            if (header is bit8StreamObjectHeaderEnd)
            {
                bit8StreamObjectHeaderEnd bit8HeaderEnd = new bit8StreamObjectHeaderEnd();
                bit8HeaderEnd = header as bit8StreamObjectHeaderEnd;
                if (bit8HeaderEnd.Type != (StreamObjectTypeHeaderEnd)expectedType)
                {
                    FSSHTTPandWOPIInspector.validationErrors += string.Format("MS-FSSHTTPB: The Type field for {0} StreamObjectEnd16BitHeader should be {1:x}, but not {2:x}.", headerName, expectedType, bit8HeaderEnd.Type);
                    return false;
                }
            }

            return true;
        }
    }
}
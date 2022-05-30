using System.Numerics;

namespace HKX2
{
    [System.Flags]
    public enum InstanceFlags : uint
    {
        UKN_0 = 0b10000000000000000000000000000000,
        UKN_1 = 0b01000000000000000000000000000000,
        UKN_2 = 0b00100000000000000000000000000000,
        UKN_3 = 0b00010000000000000000000000000000,
        UKN_4 = 0b00001000000000000000000000000000,
        UKN_5 = 0b00000100000000000000000000000000,
        UKN_6 = 0b00000010000000000000000000000000,
        UKN_7 = 0b00000001000000000000000000000000,
        UKN_8 = 0b00000000100000000000000000000000,
        UKN_9 = 0b00000000010000000000000000000000,
        UKN_10 = 0b00000000001000000000000000000000,
        UKN_11 = 0b00000000000100000000000000000000,
        UKN_12 = 0b00000000000010000000000000000000,
        UKN_13 = 0b00000000000001000000000000000000,
        UKN_14 = 0b00000000000000100000000000000000,
        UKN_15 = 0b00000000000000010000000000000000,
        UKN_16 = 0b00000000000000001000000000000000,
        UKN_17 = 0b00000000000000000100000000000000,
        UKN_18 = 0b00000000000000000010000000000000,
        UKN_19 = 0b00000000000000000001000000000000,
        UKN_20 = 0b00000000000000000000100000000000,
        UKN_21 = 0b00000000000000000000010000000000,
        UKN_22 = 0b00000000000000000000001000000000,
        UKN_23 = 0b00000000000000000000000100000000,
        UKN_24 = 0b00000000000000000000000010000000,
        UKN_25 = 0b00000000000000000000000001000000,
        UKN_26 = 0b00000000000000000000000000100000,
        UKN_27 = 0b00000000000000000000000000010000,
        UKN_28 = 0b00000000000000000000000000001000,
        SCALED = 0b00000000000000000000000000000100,
        UKN_30 = 0b00000000000000000000000000000010,
        CONVEX_VERTICES_SHAPE = 0b00000000000000000000000000000001,
    }

    public class hkpStaticCompoundShapeInstance : IHavokObject
    {
        public uint m_childFilterInfoMask;
        public uint m_filterInfo;
        public hkpShape m_shape;

        public Vector3 m_position;
        public InstanceFlags m_instanceFlags;
        public Quaternion m_rotation;
        public Vector3 m_scale;
        public float m_ukn;

        public ulong m_userData;
        public virtual uint Signature => 0x0;

        public virtual void Read(PackFileDeserializer des, BinaryReaderEx br)
        {
            m_position = des.ReadVector3(br);
            m_instanceFlags = (InstanceFlags)des.ReadUInt32(br);
            m_rotation = des.ReadQuaternion(br);
            m_scale = des.ReadVector3(br);
            m_ukn = des.ReadSingle(br);

            m_shape = des.ReadClassPointer<hkpShape>(br);
            m_filterInfo = br.ReadUInt32();
            m_childFilterInfoMask = br.ReadUInt32();
            m_userData = br.ReadUSize();

            if (des._header.PointerSize == 8) br.ReadUInt64();
        }

        public virtual void Write(PackFileSerializer s, BinaryWriterEx bw)
        {
            s.WriteVector3(bw, m_position);
            s.WriteUInt32(bw, (uint)m_instanceFlags);
            s.WriteQuaternion(bw, m_rotation);
            s.WriteVector3(bw, m_scale);
            s.WriteSingle(bw, m_ukn);

            s.WriteClassPointer(bw, m_shape);
            bw.WriteUInt32(m_filterInfo);
            bw.WriteUInt32(m_childFilterInfoMask);
            bw.WriteUSize(m_userData);

            if (s._header.PointerSize == 8) bw.WriteUInt64(0);
        }
    }
}
using System;

namespace Wikify.Display
{
    public struct ImageResolution
    {
        public uint x;
        public uint y;

        public ImageResolution(uint x, uint y)
        {
            this.x = x;
            this.y = y;
        }

        public override bool Equals(object obj)
        {
            return obj is ImageResolution other &&
                   x == other.x &&
                   y == other.y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y);
        }

        public void Deconstruct(out uint x, out uint y)
        {
            x = this.x;
            y = this.y;
        }

        public static implicit operator (uint x, uint y)(ImageResolution value)
        {
            return (value.x, value.y);
        }

        public static implicit operator ImageResolution((uint x, uint y) value)
        {
            return new ImageResolution(value.x, value.y);
        }
    }
}

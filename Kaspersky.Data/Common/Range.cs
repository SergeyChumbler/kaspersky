namespace Kaspersky.Data.Common
{
    public class Range
    {
        public int Offset { get; }
        public int Limit { get; }

        public Range() { }

        public Range(int limit) => Limit = limit;

        public Range(int offset, int limit)
        {
            Offset = offset;
            Limit = limit;
        }

        public bool IsEmpty() => Offset <= 0 && Limit <= 0;

        public static bool IsNullOrEmpty(Range range) => range == null || range.IsEmpty();
    }
}

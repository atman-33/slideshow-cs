
namespace Slideshow.Domain.ValueObjects
{
    public sealed class PageId : ValueObject<PageId>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="value"></param>
        public PageId(int value)
        {
            Value = value;
        }

        public int Value { get; }

        protected override bool EqualsCore(PageId other)
        {
            return Value == other.Value;
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}

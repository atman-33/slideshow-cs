
namespace Slideshow.Domain.ValueObjects
{
    public sealed class SlideNo : ValueObject<SlideNo>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="value"></param>
        public SlideNo(int value)
        {
            Value = value;
        }

        public int Value { get; }

        protected override bool EqualsCore(SlideNo other)
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

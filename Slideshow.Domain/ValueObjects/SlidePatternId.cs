
namespace Slideshow.Domain.ValueObjects
{
    public sealed class SlidePatternId : ValueObject<SlidePatternId>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="value"></param>
        public SlidePatternId(int value)
        {
            Value = value;
        }

        public int Value { get; }

        protected override bool EqualsCore(SlidePatternId other)
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

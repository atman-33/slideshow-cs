
namespace Slideshow.Domain.ValueObjects
{
    public sealed class SlidePatternName : ValueObject<SlidePatternName>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="value"></param>
        public SlidePatternName(string value)
        {
            Value = value;
        }

        public string Value { get; }

        protected override bool EqualsCore(SlidePatternName other)
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

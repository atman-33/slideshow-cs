using Slideshow.Domain.ValueObjects;

namespace Slideshow.Domain.ValueObjects
{
    public sealed class SlidePatternExampleId : ValueObject<SlidePatternExampleId>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="value"></param>
        public SlidePatternExampleId(int value)
        {
            Value = value;
        }

        public int Value { get; }

        protected override bool EqualsCore(SlidePatternExampleId other)
        {
            return Value == other.Value;
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            if (Value == null)
            {
                return String.Empty;
            }

            return Value.ToString();
        }
    }
}

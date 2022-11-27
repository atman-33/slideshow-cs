
using Slideshow.Domain.ValueObjects;

namespace Template.Domain.ValueObjects
{
    public sealed class SlideWaitingTime : ValueObject<SlideWaitingTime>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="value"></param>
        public SlideWaitingTime(float value)
        {
            Value = value;
        }

        public float Value { get; }

        protected override bool EqualsCore(SlideWaitingTime other)
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

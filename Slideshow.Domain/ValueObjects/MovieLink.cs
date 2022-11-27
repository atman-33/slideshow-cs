
using Slideshow.Domain.ValueObjects;

namespace Template.Domain.ValueObjects
{
    public sealed class MovieLink : ValueObject<MovieLink>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="value"></param>
        public MovieLink(string? value)
        {
            Value = value;
        }

        public string? Value { get; }

        protected override bool EqualsCore(MovieLink other)
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

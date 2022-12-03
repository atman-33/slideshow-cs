
using Slideshow.Domain.ValueObjects;

namespace Slideshow.Domain.ValueObjects
{
    public sealed class Note : ValueObject<Note>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="value"></param>
        public Note(string? value)
        {
            Value = value;
        }

        public string? Value { get; }

        protected override bool EqualsCore(Note other)
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

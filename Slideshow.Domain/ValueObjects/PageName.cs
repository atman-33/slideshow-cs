
namespace Slideshow.Domain.ValueObjects
{
    public sealed class PageName : ValueObject<PageName>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="value"></param>
        public PageName(string value)
        {
            Value = value;
        }

        public string Value { get; }

        protected override bool EqualsCore(PageName other)
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

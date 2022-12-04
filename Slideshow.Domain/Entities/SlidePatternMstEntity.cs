using Slideshow.Domain.ValueObjects;

namespace Slideshow.Domain.Entities
{
    public sealed class SlidePatternMstEntity
    {

        public SlidePatternMstEntity(
            int slidePatternId,
            string slidePatternName)
        {
            SlidePatternId = new SlidePatternId(slidePatternId);
            SlidePatternName = new SlidePatternName(slidePatternName);
        }

        public SlidePatternId SlidePatternId { get; }
        public SlidePatternName SlidePatternName { get; }

    }
}

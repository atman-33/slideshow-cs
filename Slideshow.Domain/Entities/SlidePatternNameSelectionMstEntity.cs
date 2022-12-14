using Slideshow.Domain.ValueObjects;

namespace Slideshow.Domain.Entities
{
    public sealed class SlidePatternNameSelectionMstEntity
    {

        public SlidePatternNameSelectionMstEntity(
            int slidePatternExampleId,
            string slidePatternName)
        {
            SlidePatternExampleId = new SlidePatternExampleId(slidePatternExampleId);
            SlidePatternNameExample = new SlidePatternName(slidePatternName);
        }

        public SlidePatternExampleId SlidePatternExampleId { get; }
        public SlidePatternName SlidePatternNameExample { get; }

    }
}

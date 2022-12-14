using Slideshow.Domain.ValueObjects;

namespace Slideshow.Domain.Entities
{
    public sealed class SlidePatternPageMstEntity
    {

        public SlidePatternPageMstEntity(
            int slidePatternId,
            int slideNo,
            int pageId)
        {
            SlidePatternId = new SlidePatternId(slidePatternId);
            SlideNo = new SlideNo(slideNo);
            PageId = new PageId(pageId);
        }

        public SlidePatternId SlidePatternId { get; }
        public SlideNo SlideNo { get; }
        public PageId PageId { get; }

    }
}

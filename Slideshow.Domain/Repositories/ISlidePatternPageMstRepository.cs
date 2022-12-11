using Slideshow.Domain.Entities;

namespace Slideshow.Domain.Repositories
{
    public interface ISlidePatternPageMstRepository
    {
        IReadOnlyList<SlidePatternPageMstEntity> GetData();

        void Save(SlidePatternPageMstEntity entity);

        void Delete(SlidePatternPageMstEntity entity);
    }
}

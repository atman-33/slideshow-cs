using Slideshow.Domain.Entities;

namespace Slideshow.Domain.Repositories
{
    public interface ISlidePatternMstRepository
    {
        IReadOnlyList<SlidePatternMstEntity> GetData();

        void Save(SlidePatternMstEntity entity);

        void Delete(SlidePatternMstEntity entity);
    }
}

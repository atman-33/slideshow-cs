using Slideshow.Domain.Entities;
using System.Collections.Generic;

namespace Slideshow.Domain.Repositories
{
    public interface ISlidePatternNameSelectionMstRepository
    {
        IReadOnlyList<SlidePatternNameSelectionMstEntity> GetData();

        void Save(SlidePatternNameSelectionMstEntity entity);

        void Delete(SlidePatternNameSelectionMstEntity entity);
    }
}

using Slideshow.Domain.Entities;

namespace Slideshow.Domain.Repositories
{
    public interface IPageMstRepository
    {
        IReadOnlyList<PageMstEntity> GetData();

        void Save(PageMstEntity enitity);
    }
}

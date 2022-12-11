using Slideshow.Domain;
using Slideshow.Domain.Repositories;
using Slideshow.Infrastructure.Oracle;
using Slideshow.Infrastructure.SQLite;

namespace Slideshow.Infrastructure
{
    /// <summary>
    /// Factories
    /// </summary>
    public static class Factories
    {
        public static IPageMstRepository CreatePageMst()
        {
#if DEBUG
            if (Shared.IsFake)
            {
                return new PageMstSQLite();
            }
#endif

            return new PageMstOracle();
        }
        public static ISlidePatternMstRepository CreateSlidePatternMst()
        {
#if DEBUG
            if (Shared.IsFake)
            {
                return new SlidePatternMstSQLite();
            }
#endif

            return new SlidePatternMstOracle();
        }
        public static ISlidePatternPageMstRepository CreateSlidePatternPageMst()
        {
#if DEBUG
            if (Shared.IsFake)
            {
                return new SlidePatternPageMstSQLite();
            }
#endif

            return new SlidePatternPageMstOracle();
        }
        public static ISlidePatternNameSelectionMstRepository CreateSlidePatternNameSelectionMst()
        {
#if DEBUG
            if (Shared.IsFake)
            {
                return new SlidePatternNameSelectionMstSQLite();
            }
#endif

            return new SlidePatternNameSelectionMstOracle();
        }

    }
}

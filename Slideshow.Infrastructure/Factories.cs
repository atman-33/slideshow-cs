using Slideshow.Domain;
using Slideshow.Domain.Repositories;
using Slideshow.Infrastructure.Oracle;
using Template.Infrastruture.SQLite;

namespace Template.Infrastruture
{
    /// <summary>
    /// Factories
    /// </summary>
    public static class Factories
    {
        public static IPageMstRepository CreatePageMst()
        {
            //// FakeはDEBUGのみ実装されるようリスク回避
#if DEBUG
            if (Shared.IsFake)
            {
                return new PageMstSQLite();
            }
#endif

            return new PageMstOracle();
        }
    }
}

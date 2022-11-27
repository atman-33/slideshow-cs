using Slideshow.Domain.Entities;
using Slideshow.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slideshow.Infrastructure.Oracle
{
    internal class PageMstOracle : IPageMstRepository
    {
        public IReadOnlyList<PageMstEntity> GetData()
        {
            throw new NotImplementedException();
        }

        public void Save(PageMstEntity enitity)
        {
            throw new NotImplementedException();
        }
    }
}

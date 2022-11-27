using Slideshow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slideshow.WPF.ViewModels
{
    public class PageSettingViewModelPageMst
    {
        private PageMstEntity _entity;

        public PageSettingViewModelPageMst(PageMstEntity entity)
        {
            _entity = entity;
        }

        public int PageId => _entity.PageId.Value;
        public string PageName => _entity.PageName.Value;

        public float SlideWaitingTime => _entity.SlideWaitingTime.Value;

        public string Note1 => _entity.Note1.Value;
        public string Note2 => _entity.Note2.Value;
        public string Note3 => _entity.Note3.Value;

        public string ImageLink => _entity.ImageLink.Value;
        public int? ImagePageNo => _entity.ImagePageNo.Value;
    }
}

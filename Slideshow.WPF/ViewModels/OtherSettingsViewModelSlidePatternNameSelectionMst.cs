using Slideshow.Domain.Entities;

namespace Slideshow.WPF.ViewModels
{
    public class OtherSettingsViewModelSlidePatternNameSelectionMst
    {
        private SlidePatternNameSelectionMstEntity _entity;

        public OtherSettingsViewModelSlidePatternNameSelectionMst(SlidePatternNameSelectionMstEntity entity)
        {
            _entity = entity;
        }

        public int SlidePatternExampleId => _entity.SlidePatternExampleId.Value;
        public string SlidePatternNameExample => _entity.SlidePatternNameExample.Value;

        public SlidePatternNameSelectionMstEntity Entity { get { return _entity; } }
    }
}

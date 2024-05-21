using Source.Codebase.Controllers.Presenters;

namespace Source.Codebase.Presentation.Abstract
{
    public interface IViewBase
    {
        void Construct(IPresenter presenter);
    }
}
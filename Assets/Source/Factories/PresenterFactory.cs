using Sourse.Candies;
using Sourse.Presenter;

namespace Sourse.Factories
{
    public class PresenterFactory
    {
        public T Get<T>(Candy candy, CandyView candyView) where T : IPresenter, new()
        {
            return new T();
        }
    }
}

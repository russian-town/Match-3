using Source.Candies;
using Source.Presenter;

namespace Source.Factories
{
    public class PresenterFactory
    {
        public T Get<T>(Candy candy, CandyView candyView) where T : IPresenter, new()
        {
            return new T();
        }
    }
}

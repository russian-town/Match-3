using Sourse.Candies;

namespace Sourse.Presenter
{
    public class CandyPresenter : IPresenter
    {
        private Candy _candy;
        private CandyView _candyView;

        public CandyPresenter(Candy candy, CandyView candyView)
        {
            _candy = candy;
            _candyView = candyView;
        }

        public void Enable()
        {
        }

        public void Disable()
        {
        }
    }
}

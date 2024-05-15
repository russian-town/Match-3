using Sourse.GameboardContent;
using UnityEngine;

namespace Sourse.Factories
{
    public class GameboardViewFactory
    {
        public GameboardView Get(GameboardView gameboardViewTemplate)
        {
            return Object.Instantiate(gameboardViewTemplate);
        }
    }
}

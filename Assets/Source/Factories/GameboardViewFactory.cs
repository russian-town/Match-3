using Source.GameboardContent;
using UnityEngine;

namespace Source.Factories
{
    public class GameboardViewFactory
    {
        public GameboardView Create(GameboardView gameboardViewTemplate)
        {
            return Object.Instantiate(gameboardViewTemplate);
        }
    }
}

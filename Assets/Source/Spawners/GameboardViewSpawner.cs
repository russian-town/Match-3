using Sourse.GameboardContent;
using UnityEngine;

public class GameboardViewSpawner
{
    public GameboardView Get(GameboardView gameboardViewTemplate)
    {
        return Object.Instantiate(gameboardViewTemplate);
    }
}

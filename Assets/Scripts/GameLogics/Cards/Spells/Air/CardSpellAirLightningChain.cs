using System.Collections.Generic;
using UnityEngine;

public class CardSpellAirLightningChain : CardSpell
{
    private const int DamageDealt = 3;
    private const int NR_ATTACKED_DRAGONS = 4;
    public CardSpellAirLightningChain(int owner)
    {
        Name = "Air Lightning Chain";
        Description = $"Deals {DamageDealt} Damage to {NR_ATTACKED_DRAGONS} dragons" +
                      $"The first target is selectable, the next targets are the " +
                      $"nearest ones from the previous one";
        Race = AIR;
        ID = AIR_LIGHTNING_CHAIN;
        Owner = owner;
        ManaCost = 5;
    }

    private int getNearestIndex(OnBoardDragon current, List<OnBoardDragon> allDragons, bool[] visited)
    {
        int i = -1;

        int minDistance = 900;
        int minDistanceIndex = -1;

        foreach (var dragon in allDragons)
        {
            i += 1;
            if (visited[i] == true)
                continue;

            var distance = dragon.DistanceTo(current);
            if (minDistance > distance)
            {
                minDistance = distance;
                minDistanceIndex = i;
            }
            else if(minDistance == distance && Random.Range(0.0f, 1.0f) < 0.5f)
            {
                minDistance = distance;
                minDistanceIndex = i;
            }
        }

        return minDistanceIndex;
    }

    public override void GoPlay(Vector2Int targetPosition)
    {
        int y = targetPosition.y;
        int x = targetPosition.x;

        var allDragons = Board.GetAllDragons();
        bool[] visited = new bool[allDragons.Count];

        int i = 0;
        foreach (var dragon in allDragons)
        {
            if (dragon.BoardX == x && dragon.BoardY == y)
                visited[i] = true;
            i += 1;
        }

        var attackedDragon = (OnBoardDragon)Board.Destructables[y, x];

        for (int j = 0; j < NR_ATTACKED_DRAGONS; j++)
        {
            attackedDragon.ReceiveDamage(DamageDealt);
            int index = getNearestIndex(attackedDragon, allDragons, visited);
            if (index == -1)
                break;

            visited[index] = true;
            attackedDragon = allDragons[index];
        }
    }

    public override List<Vector2Int> GetAvailableTargets()
    {
        List<Vector2Int> result = new List<Vector2Int>();

        var enemyDragons = Board.GetAllDragonsOfOwner(3 - Owner);
        foreach (var dragon in enemyDragons)
        {
            result.Add(new Vector2Int(dragon.BoardX, dragon.BoardY));
        }

        var allyDragons = Board.GetAllDragonsOfOwner(Owner);
        foreach (var dragon in allyDragons)
        {
            result.Add(new Vector2Int(dragon.BoardX, dragon.BoardY));
        }

        return result;
    }
}

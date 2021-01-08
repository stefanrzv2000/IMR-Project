using System.Collections;
using System.Collections.Generic;
using Photon.Pun.Simple.Internal;
using UnityEngine;

public class CardSpellEarthCross : CardSpell
{
    private const int DamageDealt = 5;
    public CardSpellEarthCross(int owner)
    {
        Name = "Earth Stone Cross";
        Description = $"Deals {DamageDealt} Damage to enemies in the selected cross(+) area";
        Race = EARTH;
        ID = EARTH_CROSS;
        Owner = owner;
        ManaCost = 4;
    }

    public override void GoPlay(Vector2Int targetPosition)
    {
        var allDragons = Board.GetAllDragons();
        foreach (var dragon in allDragons)
        {
            if (dragon.DistanceTo(targetPosition) <= 1)
                dragon.ReceiveDamage(DamageDealt);
        }
    }

    public override List<Vector2Int> GetAvailableTargets()
    {
        return Board.GetAllInnerPositions(); ;
    }

    public override List<Vector2Int> GetHoverPositions(Vector2Int position)
    {
        var result = new List<Vector2Int>();
        int[] dx = {-1, 0, 1, 0};
        int[] dy = {0, 1, 0, -1};

        for (int i = 0; i < 4; i++)
        {
            int nx = position.x + dx[i];
            int ny = position.y + dy[i];
            if (nx < 1 || nx == Board.Width-1)
                continue;
            if (ny < 1 || ny == Board.Height - 1)
                continue;
            result.Add(new Vector2Int(nx, ny));
        }
        result.Add(position);

        return result;
    }
}

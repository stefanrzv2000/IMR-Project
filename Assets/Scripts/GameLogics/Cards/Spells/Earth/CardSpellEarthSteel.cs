using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpellEarthSteel : CardSpell
{
    private const int PermanentHealth = 1;
    private const int PermanentAttack = 2;
    private const int CastTimes = 3;

    public CardSpellEarthSteel(int owner)
    {
        Name = "Earth Steel Gear";
        Description = $"Gives <b>+{PermanentHealth} Health</b> and <b>+{PermanentAttack} Attack</b> to an ally dragon." +
                      $"Auto Casts <b>{CastTimes}</b> times";
        Race = EARTH;
        ID = EARTH_STEEL;
        Owner = owner;
        ManaCost = 5;
    }

    public override void GoPlay(Vector2Int targetPosition)
    {
        var allyDragons = Board.GetAllDragonsOfOwner(Owner);
        for (int i = 0; i < CastTimes; i++)
        {
            if (allyDragons.Count > 0)
            {
                int index = Random.Range(0, allyDragons.Count);
                var selectedDragon = allyDragons[index];
                selectedDragon.ReceiveBuff(PermanentHealth, PermanentAttack, 0, 0, star: true, textUpdate: $"+{PermanentAttack}");

                //selectedDragon.Attack += PermanentAttack;
                //selectedDragon.MaxHealth += PermanentHealth;
                //selectedDragon.Health += PermanentHealth;
            }
        }
    }

    public override List<Vector2Int> GetAvailableTargets()
    {
        List<Vector2Int> result = new List<Vector2Int>();

        var allyDragons = Board.GetAllDragonsOfOwner(Owner);
        foreach (var dragon in allyDragons)
        {
            result.Add(new Vector2Int(dragon.BoardX, dragon.BoardY));
        }

        return result;
    }
}

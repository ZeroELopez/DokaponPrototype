using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[System.Serializable]
public class Trap : IItem
{
    public string name { get; private set; }
    public string description { get; private set; }
    public int basePrice { get; private set; }
    public Player player { get; private set; }
    public SpaceScript space { get; private set; }


    public Trap(string newName, string newDescription, int newBasePrice, int newDamage, bool EndTurn)
    {
        name = newName;
        description = newDescription;
        basePrice = newBasePrice;
        damage = newDamage;
        forceEndTurn = EndTurn;
    }

    public int damage = 10;
    public bool forceEndTurn = false;


    public void Pocket(Player newPlayer) =>
        player.inventory.Add(this);


    public void SteppedOn(Player newPlayer)
    {
        player.Damage(damage);
        if (forceEndTurn) player.ForceEndTurn();
    }



    public void Use(Player newPlayer)
    {
        player = newPlayer;
        space = BoardScript.instance.board[player.boardPos];
        space.items.Add(this);
    }
    public void Undo()
    {
        space.items.Remove(this);
    }

    public void Remove()
    {

    }

    public IItem Copy() => new Trap(name, description, basePrice, damage,forceEndTurn);

}


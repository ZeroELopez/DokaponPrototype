using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[System.Serializable]
public class Trap : IItem
{
    public string name { get; private set; }
    public int basePrice { get; private set; }
public PlayerScript player {get;private set;}

    public int damage = 10;
    public bool forceEndTurn = false;

    SpaceScript space;

    public void Pocket(PlayerScript player) =>
        player.inventory.Add(this);


    public void SteppedOn(PlayerScript player)
    {
        player.Damage(damage);
        if (forceEndTurn) player.ForceEndTurn();
    }


    public void Use(PlayerScript newPlayer)
    {
player = newPlayer;
        space = BoardScript.instance.board[player.boardPos];
        space.items.Add(this);
    }
				public void Undo(){
space.items.Remove(this);
}

    public void Remove()
    {
        
    }
}


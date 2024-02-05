using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Food : IItem
{
    public string name { get; private set; }

    public int basePrice { get; private set; }

public PlayerScript player;


    public void Pocket(PlayerScript player) =>
        player.inventory.Add(this);


int health;
int movement;    


    public void Remove() =>
player.inventory.Remove(this);
    

    public void SteppedOn(PlayerScript player)
    {

    }

    public void Use(PlayerScript newPlayer)
    {
player = newPlayer;

								player.Health += health;
								player.Movement += movement;
    }

public void Undo(){
player.Health -= health;
player.Movement -= movement;
}


}
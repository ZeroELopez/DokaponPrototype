using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Arrow : IItem
{
    public string name { get; private set; }

    public int basePrice { get; private set; }

    public void Pocket(PlayerScript player) =>
        player.inventory.Add(this);

    float rotation = 0;
    int pause = 0;
    int damage = 20;
    public void Remove()
    {
    }

    public void SteppedOn(PlayerScript player)
    {
        
    }

    public void Use(PlayerScript player)
    {
        player.SetControllable(false);



        player.SetControllable(true);

        if (StateManager.state == GameStates.Play)
        {

        }
    }


}

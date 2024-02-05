using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Camp : Actions
{
    public Vector2Int loc { get; set; }
    public PlayerScript player { get; set; }

    IItem food;


    public Camp(IItem newItem, PlayerScript newPlayer)
    {
        item = newItem;
        player = newPlayer;
    }

    public void Play()
    {
        if (player.movementPoints <= 0)
            return;

        item.Use(player);
        player.actions.Add(this);
        player.inventory.Remove(item);
        player.movementPoints--;
    }

    public void Reverse()
    {
        item.Remove();
        player.inventory.Add(item);
        player.movementPoints++;
    }

    public void Undo()
    {
        Reverse();
        player.actions.Remove(this);
    }
}
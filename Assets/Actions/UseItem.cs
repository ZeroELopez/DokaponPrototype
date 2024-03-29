﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class UseItem : Actions
{
    public Vector2Int loc { get; set; }
    public Player player { get; set; }

    IItem item;
    public UseItem(IItem newItem, Player newPlayer)
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


﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ShopAtStore : Actions
{
    public Vector2Int loc { get; set; }
    public Player player { get; set; }

    Store shop;

				List<IItem> items = new List<IItem>();

    public ShopAtStore(Player newPlayer)
    {
        player = newPlayer;
    }

    public void Play()
    {
        if (player.movementPoints <= 0)
            return;


        shop.Use(player);
shop.BuyItem += AddItem;
shop.UndoBuy += RemoveItem;

player.movementPoints--;
    }

void AddItem(IItem newItem) => items.Add(newItem);

void RemoveItem(IItem newItem) => items.Remove(newItem);

    public void Reverse()
    {
        item.Remove();
        player.movementPoints++;
    }

    public void Undo()
    {
        Reverse();
        player.actions.Remove(this);
    }
}
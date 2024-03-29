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

    Inventory shop;

    List<IItem> items = new List<IItem>();

    public ShopAtStore(Player newPlayer, Inventory newShop)
    {
        player = newPlayer;
        shop = newShop;
    }

    public void Play()
    {
        if (player.movementPoints <= 0)
            return;

        player.movementPoints--;

        if (StateManager.state == GameStates.Play)
        {
            Confirm();
            return;
        }

        shop.Use(player);
        shop.onBuyItem += AddItem;
        shop.onUndoBuy += RemoveItem;
        shop.onConfirm += Confirm;
    }

    void AddItem(IItem newItem) => items.Add(newItem);

    void RemoveItem() => items.RemoveAt(items.Count - 1);

    void Confirm()
    {
        shop.onBuyItem -= AddItem;
        shop.onUndoBuy -= RemoveItem;
        shop.onConfirm -= Confirm;

        foreach (IItem item in items)
            item.Pocket(player);

        if (StateManager.state == GameStates.Play)
            return;

        if (items.Count > 0)
            player.actions.Add(this);
    }

    public void Reverse()
    {
        foreach (IItem item in items)
            player.inventory.Remove(item);
        player.movementPoints++;
    }

    public void Undo()
    {
        Reverse();
        player.actions.Remove(this);
    }
}
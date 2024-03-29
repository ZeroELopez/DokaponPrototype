﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : IItem
{
    public Sprite icon { get; private set; }
    public string name { get; private set; }
    public string description { get; private set; }
    public int basePrice { get; private set; }
    public Player player { get; private set; }
    public SpaceScript space { get; private set; }

    List<IItem> items = new List<IItem>();
    public Action<IItem> onBuyItem;
    public Action onUndoBuy;
    public Action onConfirm;
    public Action<IItem> onNewSelection;
    int money;
    public float multiply = 1.0f;

    int select = 0;
    string[] allItemNames;

    public Inventory(string newName, string newDescription, int newBasePrice, int newMoney, float newMultiply, string[] itemNames)
    {
        name = newName;
        description = newDescription;
        basePrice = newBasePrice;
        money = newMoney;
        multiply = newMultiply;
        allItemNames = itemNames;


        //foreach (string n in itemNames)
        //    inventory.Add(ItemList.GetItem(n));
    }

    public IItem this[int index] => items[index];

    public void Remove(IItem item) => items.Remove(item);
    public void Add(IItem item) => items.Add(item);

    public void Clear() => items.Clear();
    public int Count => items.Count;
    public List<IItem> getItems() => items;

    public void Pocket(Player player) =>
        player.inventory.items.Add(this);


    public void Remove()
    {
    }

    public void SteppedOn(Player newPlayer)
    {

    }

    public void Use(Player newPlayer)
    {
        items.Clear();

        foreach (string n in allItemNames)
            items.Add(ItemList.GetItem(n));


        player = newPlayer;
        space = BoardScript.instance.board[player.boardPos];
        space.items.Add(this);

        if (StateManager.state == GameStates.Play)
        {

            return;
        }

        player.SetControllable(false);
        SetInputs();
        onNewSelection?.Invoke(items[select]);
    }

    public void Undo()
    {
        space.items.Remove(this);
    }
    float rotationSpeed;

    void SetInputs()
    {
        ShowStore.instance.OpenStore(this);

        onNewSelection += ShowStore.instance.UpdateDetails;
        Inputs.instance.controls.Player.Movement.performed += onDirection;
        Inputs.instance.controls.Player.Confirm.performed += Buy;
        Inputs.instance.controls.Player.UndoAction.performed += UndoBuy;
    }

    void UnsetInputs()
    {
        ShowStore.instance.CloseStore();

        onNewSelection -= ShowStore.instance.UpdateDetails;
        Inputs.instance.controls.Player.Movement.performed -= onDirection;
        Inputs.instance.controls.Player.Confirm.performed -= Buy;
        Inputs.instance.controls.Player.UndoAction.performed -= UndoBuy;
        player.SetControllable(true);
    }

    void onDirection(InputAction.CallbackContext context)
    {
        Vector2 floatDir = context.ReadValue<Vector2>();

        select += -Mathf.RoundToInt(floatDir.y);
        select = Mathf.Clamp(select, 0, items.Count - 1);

        onNewSelection?.Invoke(items[select]);
    }

    void Buy(InputAction.CallbackContext context)
    {
        onBuyItem?.Invoke(items[select]);
    }

    void UndoBuy(InputAction.CallbackContext context)
    {
        onUndoBuy?.Invoke();
    }

    void Exit(InputAction.CallbackContext context)
    {
        onConfirm();
        UnsetInputs();
    }

    public IItem Copy() => new Inventory(name, description, basePrice, money, multiply, allItemNames);


}



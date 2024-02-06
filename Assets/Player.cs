using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.InputSystem;
using System;

[System.Serializable]
public class Player
{
    [SerializeField] int MaxHealth = 100;
    public int Health;

    public List<IItem> inventory = new List<IItem>();
    public int money = 1000;

    public void SetControllable(bool on)
    {
        if (on)
            EnableControls();
        else
            DisableControls();
    }

    public bool TurnDone { get; private set; }
    public int movementPoints = 5;
    public Vector2Int boardPos = Vector2Int.zero;
    public List<Actions> actions = new List<Actions>();


    public Action onTurnDone;


    public Player()
    {
        Health = MaxHealth;
        StateManager.onPlanningStart += TurnStart;
        //StateManager.onPlaySetUp += StartPlay;
        StateManager.onPlayAction += PlayAction;
        StateManager.onReverseAction += ReverseAction;

        ItemList.GetItem("Bear Trap").Pocket(this);
        ItemList.GetItem("Catapult").Pocket(this);
        ItemList.GetItem("Beef").Pocket(this);
        ItemList.GetItem("Orange Gatorade").Pocket(this);
        ItemList.GetItem("Blue Gatorade").Pocket(this);
    }

    void EnableControls()
    {
        Inputs.instance.controls.Player.Movement.performed += onDirection;

        Inputs.instance.controls.Player.Confirm.performed += Confirm;
        Inputs.instance.controls.Player.UseTopItem.performed += UseTopItem;
        Inputs.instance.controls.Player.UndoAction.performed += UndoAction;
    }

    void DisableControls()
    {
        Inputs.instance.controls.Player.Movement.performed -= onDirection;

        Inputs.instance.controls.Player.Confirm.performed -= Confirm;
        Inputs.instance.controls.Player.UseTopItem.performed -= UseTopItem;
        Inputs.instance.controls.Player.UndoAction.performed -= UndoAction;
    }

    public void OnDestroy()
    {
        StateManager.onPlanningStart -= TurnStart;
        //StateManager.onPlaySetUp -= StartPlay;
        StateManager.onPlayAction -= PlayAction;
        StateManager.onReverseAction -= ReverseAction;

        DisableControls();
    }
    //

    void PlayAction(int i)
    {
        if (i >= actions.Count)
            return;

        actions[i].Play();
    }

    void ReverseAction(int i)
    {
        if (i >= actions.Count)
            return;

        actions[i].Reverse();
    }

    public void Damage(int damage)
    {
        Health -= damage;
    }

    public void ForceEndTurn() => actions.Clear();



    void onDirection(InputAction.CallbackContext context)
    {
        Vector2 floatDir = context.ReadValue<Vector2>();
        Vector2Int dir = new Vector2Int(Mathf.CeilToInt(floatDir.x), Mathf.CeilToInt(floatDir.y));

        if (dir == Vector2Int.zero)
            return;

        new Movement(new Vector2Int(dir.x, 0), this).Play();
        new Movement(new Vector2Int(0, dir.y), this).Play();
    }

    void Confirm(InputAction.CallbackContext context)
    {
        if (movementPoints > 0 || !context.started)
            return;

        SetControllable(false);
        TurnDone = true;
        onTurnDone?.Invoke();
    }
    void UseTopItem(InputAction.CallbackContext context) => UseItem(inventory.Count - 1);

    void UndoAction(InputAction.CallbackContext context)
    {
        if (actions[actions.Count - 1].GetType() == typeof(UseItem))
            actions[actions.Count - 1].Undo();
    }
    void UseItem(int index)
    {
        if (index < 0 || index >= inventory.Count)
            return;

        new UseItem(inventory[index], this).Play();
        Debug.Log("Used Item");
    }

    void TurnStart()
    {
        TurnDone = false;
        movementPoints = 5;
        actions.Clear();
    }

}

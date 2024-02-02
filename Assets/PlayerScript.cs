using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections;
using System;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] int MaxHealth = 100;
    [ShowInInspector] int Health;

    public void SetControllable(bool on)
    {
        if (on)
            inputs.Enable();
        else
            inputs.Disable();
    }

    public bool TurnDone { get; private set; }
    public int movementPoints = 5;
    public Vector2Int boardPos = Vector2Int.zero;
     public List<Actions> actions = new List<Actions>();

    public List<IItem> inventory = new List<IItem>();
    public int money = 1000;

    public Action onTurnDone;
    public Controls inputs;

    [SerializeField] float followSpeed = 2;

    private void Start()
    {
        Health = MaxHealth;
        StateManager.onPlanningStart += TurnStart;
        StateManager.onPlaySetUp += StartPlay;
        StateManager.onPlayAction += PlayAction;
        StateManager.onReverseAction += ReverseAction;

        inputs.Player.Movement.performed += onDirection;

        inputs.Player.Confirm.performed += Confirm;
        inputs.Player.UseTopItem.performed += UseTopItem;
        inputs.Player.UndoAction.performed += UndoAction;

        inventory.Add(new Trap());
    }

    private void OnDestroy()
    {
        StateManager.onPlanningStart -= TurnStart;
        StateManager.onPlaySetUp -= StartPlay;
        StateManager.onPlayAction -= PlayAction;
        StateManager.onReverseAction -= ReverseAction;

        inputs.Player.Movement.performed -= onDirection;

        inputs.Player.Confirm.performed -= Confirm;
        inputs.Player.UseTopItem.performed -= UseTopItem;
        inputs.Player.UndoAction.performed -= UndoAction;

    }
    //
    void StartPlay()=>        StartCoroutine("ReverseActions");

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

    private void Update()
    {
        Visuals();
        Debug.Log("Inventory Count: " + inventory.Count);
    }

    void Visuals()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            BoardScript.instance.board[boardPos].transform.position,
            Time.deltaTime * followSpeed
            );
    }

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

        new UseItem(inventory[index],this).Play();
        Debug.Log("Used Item");
    }

    void TurnStart()
    {
        TurnDone = false;
        movementPoints = 5;
        actions.Clear();
    }

}



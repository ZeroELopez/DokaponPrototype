using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class Store : IItem
{
    public string name { get; private set; }
    public string description { get; private set; }
    public int basePrice { get; private set; }
    public Player player { get; private set; }
    public SpaceScript space { get; private set; }

				public List<IItem> inventory = new List<IItem>();
public Action<IItem> BuyItem;
public Action UndoBuy;
public Action Confirm;
int money;
float multiply 1.0f;

int select = 0;


    public Store(string newName, string newDescription, int newBasePrice, int newMoney, float newMultiply, string[] itemNames)
    {
        name = newName;
        description = newDescription;
        basePrice = newBasePrice;
        money = newMoney;
multiply = newMultiply;

foreach (string n in itemNames)
inventory.Add(ItemList.Grap(n));
    }


    public void Pocket(Player player) =>
        player.inventory.Add(this);

   
    public void Remove()
    {
    }

    public void SteppedOn(Player newPlayer)
    {
        
    }

    public void Use(Player newPlayer)
    {
        player = newPlayer;
        space = BoardScript.instance.board[player.boardPos];
        space.items.Add(this);

        if (StateManager.state == GameStates.Play)
        {

            return;
        }

        player.SetControllable(false);
        SetInputs();

    }

    public void Undo()
    {
        space.items.Remove(this);
    }
    float rotationSpeed;

    void SetInputs()
    {
        Inputs.instance.controls.Player.Movement.performed += onDirection;
        Inputs.instance.controls.Player.Confirm.performed += Confirm;
    }

    void UnsetInputs()
    {
        Inputs.instance.controls.Player.Movement.performed -= onDirection;
        Inputs.instance.controls.Player.Confirm.performed -= Confirm;
        player.SetControllable(true);
    }

    void onDirection(InputAction.CallbackContext context)
    {
        Vector2 floatDir = context.ReadValue<Vector2>();

        select += Mathf.ToInt(floatDir.y);
select = Mathf.Clamp(select,0,inventory.Count - 1);
    }

void Buy(InputAction.CallbackContext context){
BuyItem(inventory[select]);
}

void UndoBuy(InputAction.CallbackContext context){
UndoBuy();
}

    void Exit(InputAction.CallbackContext context)
        {
Confirm();
UnsetInputs();
}

    public IItem Copy() => new Store(name, description, basePrice, money, multiply, itemNames);


}



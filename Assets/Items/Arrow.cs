using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class Arrow : IItem
{
    public string name { get; private set; }
    public string description { get; private set; }
    public int basePrice { get; private set; }
    public Player player { get; private set; }

    public Arrow(string newName, string newDescription, int newBasePrice, float newRotation, int newPause, int newDamage)
    {
        name = newName;
        description = newDescription;
        basePrice = newBasePrice;
        rotation = newRotation;
        pause = newPause;
        damage = newDamage;
    }


    public void Pocket(Player player) =>
        player.inventory.Add(this);

    float rotation = 0;
    int pause = 0;
    int damage = 20;
    public void Remove()
    {
    }

    public void SteppedOn(Player newPlayer)
    {
        
    }

    public void Use(Player newPlayer)
    {
        player = newPlayer;

        if (StateManager.state == GameStates.Play)
        {

            return;
        }

        player.SetControllable(false);
        SetInputs();

    }

    public void Undo()
    {

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

        rotation += floatDir.x * rotationSpeed;
    }

    void Confirm(InputAction.CallbackContext context) =>
        UnsetInputs();

    public IItem Copy() => new Arrow(name, description, basePrice, rotation, pause, damage);


}



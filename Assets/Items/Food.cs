using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Food : IItem
{
    public Sprite icon { get; private set; }
    public string name { get; private set; }
    public string description { get; private set; }
    public int basePrice { get; private set; }

    public Player player { get; private set; }
    public SpaceScript space { get; private set; }
    public Food(string newName, string newDescription, int newBasePrice, int newHealth, int newMovement)
    {
        name = newName;
        description = newDescription;
        basePrice = newBasePrice;
        health = newHealth;
        movement = newMovement;
    }

    public void Pocket(Player newPlayer) =>
        player.inventory.Add(this);


    int health;
    int movement;


    public void Remove() =>
player.inventory.Remove(this);


    public void SteppedOn(Player newPlayer)
    {

    }

    public void Use(Player newPlayer)
    {
        player = newPlayer;

        player.Health += health;
        player.movementPoints += movement;
    }

    public void Undo()
    {
        player.Health -= health;
        player.movementPoints -= movement;
    }

    public IItem Copy() => new Food(name, description, basePrice, health, movement);
}
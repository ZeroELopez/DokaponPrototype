using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public interface IItem
{
    Sprite icon { get; }
    string name { get; }
    string description { get; }
    int basePrice { get; }
    Player player { get; }
    SpaceScript space { get; }

    void Use(Player newPlayer);
    void Undo();

    void Remove();
    void Pocket(Player newPlayer);
    void SteppedOn(Player newPlayer);

    IItem Copy();
}


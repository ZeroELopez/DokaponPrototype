using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public static class ItemList
{

    private static List<IItem> prefabItems = new List<IItem>();

    public static IItem GetItem(string name)
    {
        foreach (IItem item in prefabItems)
            if (item.name == name)
                return item.Copy();

        return prefabItems[0].Copy();
    }

    public static void CreateItems()
    {
        prefabItems.AddRange(new IItem[11]
        {
                        new Food(
    newName: "Useless Rock",
    newDescription: "Absolutely Useless. This is an error catch for copying an item but getting the name wrong",
    newBasePrice: 0,
    newHealth: 0,
    newMovement: 1
    ),
            new Arrow(
    newName: "Catapult",
    newDescription: "A high damage long range weapon",
    newBasePrice: 200,
    newRotation: 0,
    newPause: 2,
    newDamage: 20
    ),
            new Arrow(
    newName: "Arrows",
    newDescription: "Fire a volley of arrows at opponents. Not all may hit",
    newBasePrice: 100,
    newRotation: 0,
    newPause: 1,
    newDamage: 1
    ),
            new Food(
    newName: "Beef",
    newDescription: "Regenerate Health",
    newBasePrice: 20,
    newHealth: 50,
    newMovement: 1
    ),
            new Food(
    newName: "Orange Gatorade",
    newDescription: "Gain an extra movementPoint",
    newBasePrice: 50,
    newHealth: 5,
    newMovement: 2
    ),
            new Food(
    newName: "Blue Gatorade",
    newDescription: "Garbage. A Poison that degenerates health. Only use to waste time (losing a movementPoint on purpose)",
    newBasePrice: 40,
    newHealth: -5,
    newMovement: 0
    ),
            new Trap(
    newName: "Bear Trap",
    newDescription: "High damage trap that forces oppenent to end their turn",
    newBasePrice: 200,
    newDamage:25,
    EndTurn: true
    ),
                        new Trap(
    newName: "Rope Trap",
    newDescription: "Low damage trap that forces oppenent to end their turn",
    newBasePrice: 200,
    newDamage:5,
    EndTurn: true
    ),

    new Store(
newName: "Trap Store", 
newDescription: "This is the trap store, careful where you step.", 
newBasePrice: 5000, 
newMoney: 0, 
newMultiply: 1f, 
string[] {"Bear Trap", "Rope Trap"}
),
new Store(
newName: "Food Store", 
newDescription: "I swear to god, if you eat any of my products before paying I'll fucking destroy you", 
newBasePrice: 5000, 
newMoney: 0, 
newMultiply: 1f, 
string[] {"Beef", "Orage Gaterade", "Blue Gaterade"}
),
new Store(
newName: "War Armory", 
newDescription: "Our men are willing to do war crimes...for a price", 
newBasePrice: 5000, 
newMoney: 0, 
newMultiply: 1f, 
string[] {"Catapult", "Volley of Arrows"}
)
        }

        );

    }

}
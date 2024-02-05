using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Arrow : IItem
{
    public string name { get; private set; }

    public int basePrice { get; private set; }

    public void Pocket(PlayerScript player) =>
        player.inventory.Add(this);

    float rotation = 0;
    int pause = 0;
    int damage = 20;
    public void Remove()
    {
    }

    public void SteppedOn(PlayerScript player)
    {
        
    }

    public void Use(PlayerScript player)
    {
        player.SetControllable(false);



        player.SetControllable(true);

        if (StateManager.state == GameStates.Play)
        {

        }
    }


}


public class ArrowScript : Monobehaviour
{
Arrow arrow;
int startHour = 0;
int waitHour = 1; 10 
bool active = true;
Animator thisAnimator;

private void Start(){
startHour = StateManager.hour;
thisAnimator = GetComponent<Animator>();
StateManager.newHour += onHour
}

void onHour{
if (!active)
return;

if (startHour + waitHour > StateManager.hour)
return;

thisAnimator.Play("Fire");
}

public void setActive(bool on){
active = on;

if (on)
startHour = StateManager.hour;
}
	
}

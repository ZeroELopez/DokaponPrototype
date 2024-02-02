using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[System.Serializable]
public class Movement : Actions
{
    public Vector2Int loc { get; set; }
    public PlayerScript player { get; set; }

    public Vector2Int dir;


    public Movement(Vector2Int newDir, PlayerScript newPlayer)
    {
        dir = newDir;
        player = newPlayer;
        loc = player.boardPos;
    }

    public void Play()
    {
        Vector3 newPos;

        if (!BoardScript.instance.CheckSpace(dir + player.boardPos, out newPos))
            return;

        if (player.actions.Count > 0 && player.actions[player.actions.Count - 1].loc == dir + player.boardPos 
            && player.actions[player.actions.Count - 1].GetType() == typeof(Movement))
        {
            player.actions[player.actions.Count - 1].Undo();
            return;
        }

        if (player.movementPoints <= 0 || dir == Vector2Int.zero)
            return;


        player.actions.Add(this);
        player.boardPos += dir;
        player.movementPoints--;

        if (StateManager.state == GameStates.Play)
            BoardScript.instance.board[player.boardPos].SteppedOn(player);
    }

    public void Reverse()
    {
        player.boardPos -= dir;
        player.movementPoints++;
    }

    public void Undo()
    {
        Reverse();
        player.actions.Remove(this);
    }
}

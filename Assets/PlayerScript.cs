using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections;
using System;

public class PlayerScript : MonoBehaviour
{
    public Player player;



    private void OnDestroy()
    {
        player.OnDestroy();
    }

    private void Update()
    {
        Visuals();
        Debug.Log("Inventory Count: " + player.inventory.Count);
    }

    [SerializeField] float followSpeed = 2;

    void Visuals()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            BoardScript.instance.board[player.boardPos].transform.position,
            Time.deltaTime * followSpeed
            );
    }
}



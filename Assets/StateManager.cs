using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

public class StateManager : MonoBehaviour
{
    [ShowInInspector]List<Player> players = new List<Player>();
    [SerializeField] int playerNumber;
    [SerializeField] GameObject playerPrefab;
    public static GameStates state { get; private set; }
    public static int hour;
    public static Action onPlanningStart;
    public static Action onPlaySetUp;
    public static Action<int> onPlayAction;
    public static Action<int> onReverseAction;

    private void Start()
    {
        ItemList.CreateItems();

        for (int i = 0; i < playerNumber;i++)
        {
            players.Add(Instantiate(playerPrefab, transform).GetComponent<PlayerScript>().player = new Player());
            players[i].onTurnDone += TurnSystem;
        }
        Debug.Log("State Manager Awake");
        StartPlanning();
    }

    void TurnSystem()
    {
        if (CheckTurns())
        {
            StartPlay();
            return;
        }

        for (int i = 0; i < players.Count; i++)
            if (!players[i].TurnDone)
                players[i].SetControllable(true);
    }

    bool CheckTurns()
    {
        for (int i = 0; i < players.Count; i++)
            if (!players[i].TurnDone)
                return false;

        return true;
    }

    void StartPlanning()
    {
        state = GameStates.Planning;
        players[0].SetControllable(true);
        onPlanningStart?.Invoke();
    }

    void StartPlay()
    {
        state = GameStates.Play;
        StartCoroutine("Reverse");
    }

    private IEnumerator Reverse()
    {
        int action = 10;

        while (action >= 0)
        {
            onReverseAction(action);
            action--;

            yield return new WaitForSeconds(0.5f);
        }

        StartCoroutine("Play");
    }

    private IEnumerator Play()
    {
        int action = 0;

        while (action < 10)
        {
            hour = action;
            onPlayAction(action);
            action++;

            yield return new WaitForSeconds(0.5f);
        }

        StartPlanning();
    }


}


public enum GameStates
{
    Planning,Play
}
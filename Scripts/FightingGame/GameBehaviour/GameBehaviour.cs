using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using StateMachine;
public class GameBehaviour : StateGameBehaviour
{
    public static GameBehaviour instance;

    [SerializeField] TMP_Text txt;

    void Start()
    {
        instance = this;

        txt.text = "";
    }

    public void EndGame(string text)
    {
        txt.text = text;
    }
}

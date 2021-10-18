using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;

public class PlayerContainer : MonoBehaviour
{
    public static PlayerContainer instance;

    List<GameObject> players;
    [SerializeField] GameObject[] lifeBars = new GameObject[4];

    [SerializeField] List<GameObject> h;

    [SerializeField] int _nPlayers;
    int n = 0;
    public int NPlayers { get { return _nPlayers; } }

    int count;

    PlayerInputManager inputManager;

    private void Start()
    {
        instance = this;

        VerifiyngStarterPlayers();

        _nPlayers = h.Count;

        inputManager = this.GetComponent<PlayerInputManager>();
    }

    void LateUpdate()
    {
        SetLayer();
    }

    public void SubtractPlayer()
    {
        _nPlayers--;

        if (NPlayers == 1)
        {
            GameBehaviour.instance.EndGame("PLAYER " + "FULANO " + "WINS!!");
        }
        else if (NPlayers < 1)
        {
            GameBehaviour.instance.EndGame("DRAW GAME!!");
        }
    }

    public void VerifiyngStarterPlayers()
    {
        players = GameObject.FindGameObjectsWithTag("PLAYER").ToList();

        foreach (var item in players)
        {
            lifeBars[n].GetComponent<LifeBarBehaviour>().CharPortrait.sprite = item.GetComponent<Player>().Hero.CharPortrait;
            item.GetComponent<Player>().lifeBar = lifeBars[n];

            h.Add(item.gameObject);

            n++;
        }

        foreach (var item in lifeBars)
        {
            if (item.GetComponent<LifeBarBehaviour>().CharPortrait.sprite == null) item.GetComponent<LifeBarBehaviour>().SettingLifeBars(false);
            else item.GetComponent<LifeBarBehaviour>().SettingLifeBars(true);
        }

        _nPlayers++;

        if (NPlayers > players.Count)
        {
            _nPlayers = h.Count;
        }

        n = 0;
    }

    public void AddingPlayer()
    {
        players.Add(inputManager.playerPrefab);

        foreach (var item in players)
        {

            lifeBars[n].GetComponent<LifeBarBehaviour>().CharPortrait.sprite = item.GetComponent<Player>().Hero.CharPortrait;
            item.GetComponent<Player>().lifeBar = lifeBars[n];

            h.Add(item.gameObject);

            n++;
        }

        foreach (var item in lifeBars)
        {
            if (item.GetComponent<LifeBarBehaviour>().CharPortrait.sprite == null) item.GetComponent<LifeBarBehaviour>().SettingLifeBars(false);
            else item.GetComponent<LifeBarBehaviour>().SettingLifeBars(true);
        }

        _nPlayers++;

        if (NPlayers > players.Count)
        {
            _nPlayers = h.Count;
        }

        n = 0;
    }
    public void NewPlayer()
    {
        AddingPlayer();
    }

    public void SetLayer()
    {
        h = h.OrderByDescending(h => h.GetComponent<SpritePosition>().OverAllPos()).ToList();

        switch (NPlayers)
        {
            case 4:
                {

                    h.ElementAt(0).GetComponent<SpritePosition>().ChangeLayer("PLAYER1STPLAN");

                    h.ElementAt(1).GetComponent<SpritePosition>().ChangeLayer("PLAYER2NDPLAN");

                    h.ElementAt(2).GetComponent<SpritePosition>().ChangeLayer("PLAYER3RDPLAN");

                    h.ElementAt(3).GetComponent<SpritePosition>().ChangeLayer("PLAYER4THPLAN");

                    break;
                }

            case 3:
                {

                    h.ElementAt(0).GetComponent<SpritePosition>().ChangeLayer("PLAYER1STPLAN");

                    h.ElementAt(1).GetComponent<SpritePosition>().ChangeLayer("PLAYER2NDPLAN");

                    h.ElementAt(2).GetComponent<SpritePosition>().ChangeLayer("PLAYER3RDPLAN");

                    break;
                }
            case 2:
                {

                    h.ElementAt(0).GetComponent<SpritePosition>().ChangeLayer("PLAYER1STPLAN");

                    h.ElementAt(1).GetComponent<SpritePosition>().ChangeLayer("PLAYER2NDPLAN");

                    break;
                }
            case 1:
                {

                    h.ElementAt(0).GetComponent<SpritePosition>().ChangeLayer("PLAYER1STPLAN");

                    break;
                }
        }
    }
}

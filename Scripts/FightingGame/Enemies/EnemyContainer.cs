using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyContainer : MonoBehaviour
{
    public static EnemyContainer instance;

    [SerializeField] List<GameObject> enemies;

    [SerializeField] int _nEnemies;
    public int NEnemies { get { return _nEnemies; } }
    void Start()
    {
        instance = this;

        FindingEnemies();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //SetLayer();
    }

    public void AddingEnemies()
    {
        _nEnemies++;
    }

    public void FindingEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag("ENEMY").ToList();

        _nEnemies = enemies.Count;
    }

    public void SetLayer()
    {
        enemies = enemies.OrderByDescending(enemies => enemies.GetComponent<SpritePosition>().OverAllPos()).ToList();

        enemies.ElementAt(0).GetComponent<SpritePosition>().ChangeLayer("PLAYER1STPLAN");

        enemies.ElementAt(1).GetComponent<SpritePosition>().ChangeLayer("PLAYER2NDPLAN");

        enemies.ElementAt(2).GetComponent<SpritePosition>().ChangeLayer("PLAYER3RDPLAN");

        enemies.ElementAt(3).GetComponent<SpritePosition>().ChangeLayer("PLAYER4THPLAN");
    }
}

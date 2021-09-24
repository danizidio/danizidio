using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] ScriptableCharacters _hero;
    public ScriptableCharacters Hero { get { return _hero; }}

    [SerializeField] float _currentLife;
    [SerializeField] Transform _footDetector;
    public float CurrentLife { get { return _currentLife; } set { _currentLife = value; } }
    public Transform FootDetector { get { return _footDetector; } }

    [SerializeField] GameObject _sprite;
    public GameObject Sprite { get { return _sprite; } set { _sprite = value; } }

    public GameObject lifeBar;

    private void Start()
    {
        CurrentLife = Hero.Life;
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable e = other.GetComponent<IDamageable>();

        if (e != null && other.GetComponent<PlayerStateMachine>().GetCurrentPlayerState() != PlayerStates.DEAD)
        {
            StartCoroutine(e.Combat(Hero.Attack, Hero.CriticalAttack, 1));

            lifeBar.GetComponent<LifeBarBehaviour>().ComboIncrement();
            lifeBar.GetComponent<LifeBarBehaviour>().EnemyBarUpdate(other.GetComponent<Player>().CurrentLife, other.GetComponent<Player>().Hero.Life, other.GetComponent<Player>().Hero.CharPortrait);
        }
    }

    public IEnumerator Combat(int damage, float critical, int manyTimes)
    {
        int maxqntd = manyTimes;

        while (manyTimes > 0)
        {
            if (GetComponent<PlayerStateMachine>().PlrState == PlayerStates.BLOCKING || GetComponent<PlayerStateMachine>().PlrState == PlayerStates.LOWBLOCKING)
            {
                GetComponent<Animator>().SetTrigger("HitBlock");
            }
            else
            {
                if (manyTimes == maxqntd)
                {
                    CurrentLife -= (damage - Hero.Defense);

                    lifeBar.GetComponent<LifeBarBehaviour>().UpdateLifeBar(CurrentLife, Hero.Life);

                    GetComponent<PlayerStateMachine>().AttackerState = GetComponent<PlayerStateMachine>().AtkState;

                    GetComponent<PlayerStateMachine>().NextPlayerState(PlayerStates.ONHIT);
                }

                if (GetComponent<PlayerStateMachine>().PlrState == PlayerStates.AIRBORNE && GetComponent<PlayerStateMachine>().AtkState != AttackStates.LAUNCHER)
                {
                    GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(Hero.PushX, Hero.PushY + 30f), ForceMode2D.Impulse);
                }
            }

            manyTimes--;
            
            if(CurrentLife <= 0)
            {
                GetComponent<PlayerStateMachine>().NextPlayerState(PlayerStates.DYING);
            }

            yield return new WaitForSeconds(.2f);
        }
    }
    
}

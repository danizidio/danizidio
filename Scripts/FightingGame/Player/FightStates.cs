using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace StateMachine
{
    public class FightStates : MonoBehaviour
    {
        StatePlayerBehaviour state;

        private void Start()
        {
            state = GetComponent<StatePlayerBehaviour>();
        }

        public void FightStateMachine(AttackStates value)
        {
            switch (value)
            {
                case AttackStates.NORMAL:
                    {
                        
                        if (e.GetComponent<Moving>().isAttacking)
                        {
                            GetCountered(e);
                        }

                        StartCoroutine(GiveHits(.03f, e, atkHits));

                        break;
                    }
                case AttackStates.OVERHEAD:
                    {
                        
                        if (e.GetComponent<Player>().Moving.isAttacking)
                        {
                            GetCountered(e);
                        }

                        if (e.GetComponent<StateMachines>().PlrState == PlayerStates.BLOCKING)
                        {
                            e.GetComponent<Animator>().SetTrigger("HitBlock");
                        }
                        else
                        {
                            (e as InterfaceEnemyCombat).PlayerHitEnemy(p.Atributes.Attack, e.GetComponent<Player>().Atributes.Defense, p.Atributes.CriticalAttack);

                            ComboCounter.instance.UIOverhead();
                        }
                        
                        break;
                    }
                case AttackStates.LOW:
                    {
                        
                        if (e.GetComponent<StateMachines>().PlrState == PlayerStates.LOWBLOCKING)
                        {
                            print("BLOCK!!");
                        }
                        else
                        {
                            (e as InterfaceEnemyCombat).PlayerHitEnemy(p.Atributes.Attack, e.GetComponent<Player>().Atributes.Defense, p.Atributes.CriticalAttack);

                            ComboCounter.instance.UILowHit();
                        }

                        if (e.GetComponent<Player>().Moving.isAttacking)
                        {
                            GetCountered(e);
                        }
                        
                        break;
                    }
                case AttackStates.SPECIAL:
                    {
                        
                        if (e.GetComponent<Player>().Moving.isAttacking)
                        {
                            GetCountered(e);
                        }

                        StartCoroutine(GiveHits(.03f, e, atkHits));
                        
                        break;
                    }
                case AttackStates.KNOCKUP:
                    {
                        
                        (e as InterfaceEnemyCombat).PlayerHitEnemy(p.Atributes.Attack, e.GetComponent<Player>().Atributes.Defense, p.Atributes.CriticalAttack);
                        e.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(p.Atributes.PushX, p.Atributes.PushY), ForceMode2D.Impulse);

                        e.GetComponent<Animator>().SetTrigger("KnockDown");

                        StartCoroutine(GiveHits(.03f, e, atkHits));
                        
                        break;
                    }
                case AttackStates.KNOCKDOWN:
                    {
                        
                        (e as InterfaceEnemyCombat).PlayerHitEnemy(p.Atributes.Attack, e.GetComponent<Player>().Atributes.Defense, p.Atributes.CriticalAttack);
                        e.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(p.Atributes.PushX, p.Atributes.PushY), ForceMode2D.Impulse);

                        e.GetComponent<Animator>().SetTrigger("KnockDown");

                        StartCoroutine(GiveHits(.03f, e, atkHits));
                        
                        break;
                    }
                case AttackStates.LAUNCHER:
                    {
                        
                        (e as InterfaceEnemyCombat).PlayerHitEnemy(p.Atributes.Attack / 2, e.GetComponent<Player>().Atributes.Defense, p.Atributes.CriticalAttack);
                        e.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(p.Atributes.PushX, p.Atributes.PushY + 120f), ForceMode2D.Impulse);

                        e.GetComponent<Animator>().SetTrigger("AirBorne");

                        e.GetComponent<StateMachines>().ChangePlayerState(PlayerStates.ONHIT);

                        StartCoroutine(GiveHits(.01f, e, atkHits));
                        
                        break;
                    }
            }
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public class MovingStates : MonoBehaviour
    {
        StatePlayerBehaviour state;

        Animator anim;
        Moving m;
        Player p;

        private void Awake()
        {
            state = GetComponent<StatePlayerBehaviour>();

            anim = this.GetComponent<Animator>();
            m = this.GetComponent<Moving>();
            p = this.GetComponent<Player>();

        }
        public void MovingStateMachine(PlayerStates value)
        {
            switch (value)
            {
                case PlayerStates.IDLE:
                    {
                        
                        anim.SetBool("BLOCK", false);
                        anim.SetBool("BLOCKLOW", false);
                        anim.SetBool("MOVE", false);

                        if (m.XAxyz != 0 | m.ZAxyz != 0)
                        {
                            state.NextPlayerState(PlayerStates.MOVING);
                        }

                       anim.SetBool("JUMP", !m.IsOnGround());

                        break;
                    }
                case PlayerStates.MOVING:
                    {
                        if (m.CanAttack)
                        {
                            transform.Translate(new Vector3(m.XAxyz * p.Hero.MoveSpeed, 0, m.ZAxyz * p.Hero.MoveSpeed));
                        }

                        anim.SetBool("MOVE", true);

                        if (m.XAxyz == 0 && m.ZAxyz == 0)
                        {
                            state.NextPlayerState(PlayerStates.IDLE);
                        }

                        anim.SetBool("JUMP", !m.IsOnGround());

                        break;
                    }
                case PlayerStates.ATTACKING:
                    {
                        if (state.GetCurrentAttackState() == AttackStates.OVERHEAD)
                        {
                            anim.SetTrigger("OVERHEAD");
                        }

                        if (state.GetCurrentAttackState() == AttackStates.LOW)
                        {
                            anim.SetTrigger("LOWATTACK");
                        }

                        if (state.GetCurrentAttackState() == AttackStates.NORMAL)
                        {
                            if (!m.attack1 && !m.attack2 && !m.attack3)
                            {
                                anim.SetTrigger("ATTACK1");
                            }

                            if (m.attack1)
                            {
                                anim.SetTrigger("ATTACK2");
                            }

                            if (m.attack2)
                            {
                                anim.SetTrigger("ATTACK3");
                            }
                        }
                        if (state.GetCurrentAttackState() == AttackStates.LAUNCHER)
                        {
                            anim.SetTrigger("LAUNCHER");
                        }

                        if (state.GetCurrentAttackState() == AttackStates.SPECIAL)
                        {
                            anim.SetTrigger("SPECIAL");
                        }

                        state.NextPlayerState(PlayerStates.IDLE);

                        break;
                    }
                case PlayerStates.BLOCKING:
                    {
                        anim.SetBool("BLOCK", true);
                        anim.SetBool("BLOCKLOW", false);

                        break;
                    }
                case PlayerStates.LOWBLOCKING:
                    {
                        anim.SetBool("BLOCKLOW", true);
                        anim.SetBool("BLOCK", false);

                        break;
                    }
                case PlayerStates.ONHIT:
                    {
                        if (!m.takeHit1 && !m.takeHit2)
                        {
                            anim.SetTrigger("HIT1");
                        }
                        if (m.takeHit1)
                        {
                            anim.SetTrigger("HIT2");
                            //GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(Atributes.PushX, Atributes.PushY), ForceMode2D.Impulse);
                        }

                        if (state.GetCurrentAttackState() == AttackStates.LAUNCHER)
                        {
                            anim.SetTrigger("AIRBORNE");
                            state.NextPlayerState(PlayerStates.AIRBORNE);
                        }
                        else if (state.GetCurrentAttackState() != AttackStates.LAUNCHER && state.PlrPreviousState == PlayerStates.AIRBORNE)
                        {
                            //   cd = t;

                            anim.SetTrigger("AIRBORNE");
                            state.NextPlayerState(PlayerStates.AIRBORNE);
                        }
                        else if (state.GetCurrentAttackState() == AttackStates.KNOCKDOWN || state.GetCurrentAttackState() == AttackStates.KNOCKUP)
                        {
                            anim.SetTrigger("KNOCKDOWN");
                            state.NextPlayerState(PlayerStates.KNOCKDOWN);
                        }
                        else
                        {
                            state.NextPlayerState(PlayerStates.IDLE);
                        }

                        break;
                    }
                case PlayerStates.AIRBORNE:
                    {
                        /*
                        cd -= Time.deltaTime;

                        if (cd <= 0)
                        {
                            cd = t;

                            NextPlayerState(PlayerStates.KNOCKDOWN);
                        }
                        */
                        break;
                    }
                case PlayerStates.KNOCKDOWN:
                    {
                        anim.SetTrigger("KNOCKDOWN");

                        state.NextPlayerState(PlayerStates.WAKEUP);

                        break;
                    }
                case PlayerStates.WAKEUP:
                    {

                        state.NextPlayerState(PlayerStates.IDLE);

                        break;
                    }
                case PlayerStates.DYING:
                    {

                        anim.SetTrigger("DEAD");

                        PlayerContainer.instance.SubtractPlayer();

                        state.NextPlayerState(PlayerStates.DEAD);

                        break;
                    }
                case PlayerStates.DEAD:
                    {


                        break;
                    }
            }
        }
    }
}




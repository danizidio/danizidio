 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using StateMachine;
public class CharacterInputs : MonoBehaviour
{
    [SerializeField] PlayerInputActions controls;

    Moving m;
    Player p;
    PlayerStateMachine st;
    
    float f;

    private void Awake()
    {
        controls = new PlayerInputActions();
    }

    private void Start()
    {
        m = this.gameObject.GetComponent<Moving>();
        p = this.gameObject.GetComponent<Player>();
        st = this.gameObject.GetComponent<PlayerStateMachine>();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    public void MovingLeftRight(InputAction.CallbackContext context)
    {
        if (m.CanAttack)
        {
            m.XAxyz = context.ReadValue<float>();

            if (m.XAxyz < 0)
            {
                p.Sprite.transform.localScale = new Vector3(m.XAxyz, transform.localScale.y, transform.localScale.z);
            }
            if (m.XAxyz > 0)
            {
                p.Sprite.transform.localScale = new Vector3(m.XAxyz, transform.localScale.y, transform.localScale.z);
            }

            st.nextState(PlayerStates.MOVING);
        }
    }

    public void MovingUpDown(InputAction.CallbackContext context)
    {
        if (m.CanAttack)
        {
            m.ZAxyz = context.ReadValue<float>();

            st.nextState(PlayerStates.MOVING);
        }
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (m.CanAttack)
        {
            if (context.started)
            {
                f = context.ReadValue<float>();
            }
            if (context.performed && m.CanAttack)
            {
                m.MobilityDash(GetComponent<Player>().Hero.MoveSpeed * f);
            }
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(context.started && m.CanAttack)
        {
            m.CanJump(GetComponent<Player>().Hero.JumpSpeed);

            st.nextState(PlayerStates.MOVING);
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed && m.CanAttack)
        {
            st.NextPlayerState(PlayerStates.ATTACKING);
            st.NextAttackStates(AttackStates.NORMAL);
        }
    }

    public void Special(InputAction.CallbackContext context)
    {
        if (context.performed && m.CanAttack)
        {
            st.NextPlayerState(PlayerStates.ATTACKING);
            st.NextAttackStates(AttackStates.SPECIAL);
        }
    }

    public void Launcher(InputAction.CallbackContext context)
    {
        if (context.performed && m.CanAttack)
        {
            st.NextAttackStates(AttackStates.LAUNCHER);
            st.NextPlayerState(PlayerStates.ATTACKING);
        }
    }

    public void Overhead(InputAction.CallbackContext context)
    {
        if (context.performed && m.CanAttack)
        {
            st.NextAttackStates(AttackStates.OVERHEAD);
            st.NextPlayerState(PlayerStates.ATTACKING);
        }
    }
}

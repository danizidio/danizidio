using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

public class Moving : MonoBehaviour
{
    float _xAxyz, _zAxyz;
    public float XAxyz { get { return _xAxyz; } set { _xAxyz = value; } }
    public float ZAxyz { get { return _zAxyz; } set { _zAxyz = value; } }

    [SerializeField] bool _canAttack;
    [SerializeField] float dashDistance;
    
    Player p;
    PlayerStateMachine st;

    Animator _anim;
    
    private bool _attack1,
                 _attack2,
                 _attack3,
                 _isAttacking,
                 _takeHit1,
                 _takeHit2,
                 _isKnockedDown,
                 _isKnockedDownUp,
                 _isjumping;

    public bool CanAttack { get { return _canAttack; } }
    public bool attack1 { get { return _attack1; } set { _attack1 = value; } }
    public bool attack2 { get { return _attack2; } set { _attack2 = value; } }
    public bool attack3 { get { return _attack3; } set { _attack3 = value; } }
    public bool isAttacking { get { return _isAttacking; } set { _isAttacking = value; } }
    public bool takeHit1 { get { return _takeHit1; } set { _takeHit1 = value; } }
    public bool takeHit2 { get { return _takeHit2; } set { _takeHit2 = value; } }
    public bool IsKnockedDown { get { return _isKnockedDown; } set { _isKnockedDown = value; } }
    public bool IsKnockedDownUp { get { return _isKnockedDownUp; } set { _isKnockedDownUp = value; } }
    public bool IsJumping { get { return _isjumping; } set { _isjumping = value; } }

    private void Start()
    {
        p = this.GetComponent<Player>();
        //st = this.GetComponent<PlayerStateMachine>();

        //st.nextState = st.NextPlayerState;
    }

    public void MobilityDash(float mobility)
    {
        transform.Translate(new Vector3 (mobility * dashDistance, 0f, 0f));
    }

    public void CanJump(float mobility)
    {
       if (IsOnGround() && CanAttack)
         {
             Instantiate(p.Hero.Dust, new Vector3(p.FootDetector.position.x, p.FootDetector.position.y, p.FootDetector.position.z), Quaternion.identity);
             this.GetComponent<Rigidbody>().AddForce(new Vector2(0, mobility));

             st.nextState(PlayerStates.MOVING);
        }
    }

    public bool IsOnGround()
    {
        return (Physics.Linecast(transform.position, p.FootDetector.position, 1 << LayerMask.NameToLayer("GROUND")) |
        (Physics.Linecast(transform.position, p.FootDetector.position, 1 << LayerMask.NameToLayer("FLOATINGPLATFORM")) |
        (Physics.Linecast(transform.position, p.FootDetector.position, 1 << LayerMask.NameToLayer("ACTORS")) |
        (Physics.Linecast(transform.position, p.FootDetector.position, 1 << LayerMask.NameToLayer("PUSHPULL"))))));
    }
}

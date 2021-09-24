using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script for enemy IA
//Based on identifiyng an array of targets and get focus on the closer one
//This enemy will read the Player state on the focus object to know how to react 
//The url below shows a video of the enemy identifying the target and then identifying the action to act properly
//https://onedrive.live.com/?authkey=%21AKLhqwqflagGla8&cid=8EB27566A27E4C95&id=8EB27566A27E4C95%21742274&parId=8EB27566A27E4C95%21742268&o=OneUp

public enum IAStates
{
    PATROLLING,
    ENGAGING,
    CHASING
}
public class EnemyIA : MonoBehaviour
{
    Player e;
    StateMachines st;
    GameObject[] _players;

    [SerializeField]float[] dist;
    [SerializeField] float maxDist, minDist, pivotDist;

    int n = 0;
    float pivotSpeed;

    [SerializeField] GameObject characterToFocus;

    public GameObject[] Players { get { return _players; } set { _players = value; } }

    [SerializeField] IAStates _previousState;
    [SerializeField] IAStates _currentState;
    [SerializeField] IAStates _nextState;
    [SerializeField] PlayerStates _charFocused;

    public IAStates PreviousState { get { return _previousState; } set { _previousState = value; } }
    public IAStates CurrentState { get { return _currentState; } set { _currentState = value; } }
    public IAStates NextState { get { return _nextState; } set { _nextState = value; } }

    void Start()
    {
        e = this.GetComponent<Player>();
        st = this.GetComponent<StateMachines>();

        FindingPlayers();
        pivotSpeed = -e.Atributes.MoveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        CompairDistance();
        //CompairDistance(1, Players[1]);
        IAPrimaryState(CurrentState);

        CurrentState = NextState;
    }

    public void FindingPlayers()
    {
        Players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject character in Players)
        {
            character.GetComponent<StateMachines>();

            Players[n] = character;

            n++;
        }
        dist = new float[Players.Length];

        n = 0;
    }

    public void IAPrimaryState(IAStates value)
    {
        switch (value)
        {
            case IAStates.PATROLLING:
                {
                    Movimento();

                    e.GetComponent<Animator>().SetBool("Engage", false);
                    break;
                }
            case IAStates.ENGAGING:
                {

                    Engaging();

                    break;
                }
            case IAStates.CHASING:
                {
                    e.GetComponent<Animator>().SetBool("Engage", false);

                    break;
                }
        }
    }

    public float CompairDistance()
    {
        foreach (var item in Players)
        {
            float calculate = Vector2.Distance(this.transform.position, item.transform.position);

            if (calculate <= minDist)
            {
                characterToFocus = item;

                ChangeIAState(IAStates.ENGAGING);
            }
            if (characterToFocus == item && (calculate > minDist && calculate < maxDist))
            {
                ChangeIAState(IAStates.CHASING);
            }
            if (calculate >= maxDist && characterToFocus == item)
            {
                characterToFocus = null;

                ChangeIAState(IAStates.PATROLLING);
            }
            
            dist[n] = calculate;

            n++;
            
            if (n >= dist.Length)
            {
                n = 0;
            }
        }

        return pivotDist;
    }

    public void Movimento()
    {
        //animBehaviour.SetBool("walking", true);
        //animBehaviour.SetBool("Idle", false);
        //animBehaviour.SetBool("attacking", false);


        if (!e.IsOnGround)
        {

            //e.gameObject.transform.localScale = new Vector2(e.gameObject.transform.localScale.x, e.gameObject.transform.localScale.y);

            e.GetComponent<MovingBase>().XAxyz = pivotSpeed *= -1;
        }

        if (e.IsOnGround)
        {
            e.GetComponent<MovingBase>().XAxyz = pivotSpeed;
            //e.GetComponent<MovingBase>().DefaultMoving(pivotSpeed);
            //e.GetComponent<Rigidbody2D>().velocity = new Vector2(pivotSpeed, e.GetComponent<Rigidbody2D>().velocity.y);
        }
    }

    public void Engaging()
    {
        e.GetComponent<MovingBase>().XAxyz = 0;

        _charFocused = characterToFocus.GetComponent<StateMachines>().PlrPreviousState;

        if (_charFocused == PlayerStates.ATTACKING)
        {
            st.ChangePlayerState(PlayerStates.BLOCKING);
        }
        else
        {
            e.GetComponent<Animator>().SetBool("Engage", true);
        }
    }
    public IAStates GetCurrentIAState()
    {
        return CurrentState;
    }

    public void ChangeIAState(IAStates newState)
    {
        PreviousState = CurrentState;

        NextState = newState;
    }

}

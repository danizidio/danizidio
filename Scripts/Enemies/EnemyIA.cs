using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script for enemy IA
//Based on identifiyng an array of targets and get focus on the closer one
//This enemy will read the Player state on the focus object to know how to react 
//The url below shows a video of the enemy identifying the target and then identifying the action to act properly
//https://onedrive.live.com/?authkey=%21AKLhqwqflagGla8&cid=8EB27566A27E4C95&id=8EB27566A27E4C95%21742274&parId=8EB27566A27E4C95%21742268&o=OneUp

//ENUM for IA behaviours
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

    void Update()
    {
        CompairDistance();
        
        IAPrimaryState(CurrentState);

        CurrentState = NextState;
    }

//Looking for GameObjects with the specified tag
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

//Compair the distance between the objects stored in the Player Array
//Set the characterToFocus gameobject
//Change the State of this enemy if some player is closer or far enough
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

//Enemy movement
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

//If player is closer enough this enemy will change his state to engaging
//Changing the current animation and reading the focused player attack states 
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
   
//Just to verify the current state in case of need   
    public IAStates GetCurrentIAState()
    {
        return CurrentState;
    }

//Method to change the next state for this enemy
//The next state will become the current state on the next frame of the update
    public void ChangeIAState(IAStates newState)
    {
        PreviousState = CurrentState;

        NextState = newState;
    }

}

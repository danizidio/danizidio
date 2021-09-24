using UnityEngine;

namespace StateMachine
{
    public enum AttackStates
    {
        NULL,
        NORMAL,
        OVERHEAD,
        LOW,
        SPECIAL,
        KNOCKUP,
        KNOCKDOWN,
        LAUNCHER
    }

    public enum PlayerStates
    {
        IDLE,
        MOVING,
        ATTACKING,
        BLOCKING,
        LOWBLOCKING,
        ONHIT,
        AIRBORNE,
        KNOCKDOWN,
        WAKEUP,
        DYING,
        DEAD
    }

    public enum GameStates
    {
        BEGINING,
        START,
        GAMEPLAY,
        FINISHING,
        END
    }

    public class StateGameBehaviour: MonoBehaviour
    {
        [SerializeField] GameStates _gamePreviousState;
        [SerializeField] GameStates _gameState;
        [SerializeField] GameStates _gameNextState;

        public GameStates GamePreviousState
        { get { return _gamePreviousState; } set { _gamePreviousState = value; } }

        public GameStates GameState
        { get { return _gameState; } set { _gameState = value; } }

        public GameStates GameNextState
        { get { return _gameNextState; } set { _gameNextState = value; } }

        public GameStates NextGameStates(GameStates newState)
        {
            GamePreviousState = GameState;
            return GameNextState = newState;
        }
        public GameStates GetCurrentGameState()
        {
            return GameState;
        }

    }

    public class StatePlayerBehaviour: MonoBehaviour
    {
        [SerializeField] AttackStates _atkPreviousState;
        [SerializeField] AttackStates _atkState;
        [SerializeField] AttackStates _atkNextState;
        [SerializeField] AttackStates _attackerState;

        [SerializeField] PlayerStates _plrPreviousState;
        [SerializeField] PlayerStates _plrState;
        [SerializeField] PlayerStates _plrNextState;

       
        public AttackStates AtkPreviousState
        { get { return _atkPreviousState; } set { _atkPreviousState = value; } }

        public AttackStates AtkState
        { get { return _atkState; } set { _atkState = value; } }

        public AttackStates AtkNextState
        { get { return _atkNextState; } set { _atkNextState = value; } }

        public AttackStates AttackerState
        { get { return _attackerState; } set { _attackerState = value; } }

        public PlayerStates PlrPreviousState
        { get { return _plrPreviousState; } set { _plrPreviousState = value; } }

        public PlayerStates PlrState
        { get { return _plrState; } set { _plrState = value; } }

        public PlayerStates PlrNextState
        { get { return _plrNextState; } set { _plrNextState = value; } }

        public PlayerStates NextPlayerState(PlayerStates newState)
        {
            PlrPreviousState = PlrState;
            return PlrNextState = newState;
        }

        public AttackStates NextAttackStates(AttackStates newState)
        {
            AtkPreviousState = AtkState;
            return AtkNextState = newState;
        }

        public AttackStates GetCurrentAttackState()
        {
            return AtkState;
        }

        public PlayerStates GetCurrentPlayerState()
        {
            return PlrState;
        }

    }

}



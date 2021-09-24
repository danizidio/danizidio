using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace StateMachine
{
    public class PlayerStateMachine : StatePlayerBehaviour
    {

        public delegate PlayerStates NextState(PlayerStates p);

        public NextState nextState;

        MovingStates movingStates;
        FightStates fightStates;

        private void Start()
        {
            movingStates = GetComponent<MovingStates>();
            fightStates = GetComponent<FightStates>();
        }

        private void Update()
        {
            movingStates.MovingStateMachine(PlrState);
            fightStates.FightStateMachine(AtkState);

            PlrState = PlrNextState;
            AtkState = AtkNextState;

        }
    }
}
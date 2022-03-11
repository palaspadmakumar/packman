using System.Collections;
using com.hellion.statemachine;
using UnityEngine;

namespace com.hellion.packman
{
    public class StateCharecterDead : IState<Charecter>
    {
        public static StateCharecterDead Instance { get { if (instance == null) instance = new StateCharecterDead(); return instance; } }
        private static StateCharecterDead instance;

        public void Destroy(Charecter StateObject)
        {
        }

        public void Enter(Charecter StateObject, params object[] args)
        {
            StateObject.SetCharecterState(ECharecterState.DEAD);
            StateObject.SetCharecterToDeadState();
            StateObject.StartCoroutine(ResetDeathTimer(StateObject, StateObject.GetDeathTime()));
        }

        IEnumerator ResetDeathTimer(Charecter StateObject, float delay)
        {
            yield return new WaitForSeconds(delay);
            StateObject.stateMachine.ChangeState(StateCharecterBlink.Instance);
        }

        public void Update(Charecter StateObject)
        {
        }
    }
}

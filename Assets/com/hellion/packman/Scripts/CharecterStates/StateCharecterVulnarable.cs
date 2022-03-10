using System.Collections;
using com.hellion.statemachine;
using UnityEngine;

namespace com.hellion.packaman
{
    public class StateCharecterVulnarable : IState<Charecter>
    {
        public static StateCharecterVulnarable Instance { get { if (instance == null) instance = new StateCharecterVulnarable(); return instance; } }
        private static StateCharecterVulnarable instance;
        float _vulanaibilityTime = 0;


        public void Destroy(Charecter StateObject)
        {
        }

        public void Enter(Charecter StateObject, params object[] args)
        {
            _vulanaibilityTime = StateObject.GetVulnTime();
            StateObject.SetCharecterState(Charecter.ECharecterState.VULNARABLE);
            StateObject.SetCharecterToVulnarable();
            StateObject.StartCoroutine(WaitTillVulnabilityTime(StateObject));
        }

        IEnumerator WaitTillVulnabilityTime(Charecter StateObject)
        {
            yield return new WaitForSeconds(_vulanaibilityTime);
            StateObject.stateMachine.ChangeState(StateCharecterBlink.Instance);
        }

        public void Update(Charecter StateObject)
        {
        }
    }
}

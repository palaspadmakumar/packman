using com.hellion.statemachine;
using UnityEngine;

namespace com.hellion.packaman
{
    public class StateCharecterVulnarable : IState<Charecter>
    {
        public static StateCharecterVulnarable Instance { get { if (instance == null) instance = new StateCharecterVulnarable(); return instance; } }
        private static StateCharecterVulnarable instance;
        float _time = 0, _vulanaibilityTime = 0;


        public void Destroy(Charecter StateObject)
        {
        }

        public void Enter(Charecter StateObject, params object[] args)
        {
            _time = 0;
            _vulanaibilityTime = StateObject.GetVulnTime();
            StateObject.SetCharecterState(Charecter.ECharecterState.VULNARABLE);
            StateObject.SetCharecterToVulnarable();
        }

        public void Update(Charecter StateObject)
        {
            _time += Time.deltaTime;
            if (_time >= _vulanaibilityTime)
            {
                StateObject.stateMachine.ChangeState(StateCharecterBlink.Instance);
            }
        }
    }
}

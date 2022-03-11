using System.Collections;
using com.hellion.statemachine;
using UnityEngine;

namespace com.hellion.packman
{
    public class StateCharecterBlink : IState<Charecter>
    {
        public static StateCharecterBlink Instance { get { if (instance == null) instance = new StateCharecterBlink(); return instance; } }
        private static StateCharecterBlink instance;
        float blinkdelay = 0, blinkfreq = 0;


        public void Destroy(Charecter StateObject)
        {
        }

        public void Enter(Charecter StateObject, params object[] args)
        {
            blinkdelay = StateObject.GetBlinkTime();
            blinkfreq = StateObject.GetBlinkFrequece();
            StateObject.SetCharecterToAliveState();
            StateObject.StartCoroutine(Blink(StateObject));
        }

        public void Update(Charecter StateObject)
        {
        }

        IEnumerator Blink(Charecter StateObject)
        {
            float _blinkTime = 0;
            float _time = 0;
            bool isDead = false;
            while (_time < blinkdelay)
            {
                _time += Time.deltaTime;
                _blinkTime += Time.deltaTime;
                if (_blinkTime >= blinkfreq)
                {
                    if (!isDead)
                    {
                        isDead = true;
                        StateObject.SetCharecterToDeadState();
                    }
                    else
                    {
                        isDead = false;
                        StateObject.SetCharecterToAliveState();
                    }
                    _blinkTime = 0;
                }
                yield return new WaitForEndOfFrame();

            }

            StateObject.stateMachine.ChangeState(StateCharecterAlive.Instance);
        }
    }
}

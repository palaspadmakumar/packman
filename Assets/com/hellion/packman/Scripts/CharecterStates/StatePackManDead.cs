using System.Collections;
using com.hellion.statemachine;
using UnityEngine;

namespace com.hellion.packaman
{
    public class StatePackManDead : IState<Charecter>
    {
        public static StatePackManDead Instance { get { if (instance == null) instance = new StatePackManDead(); return instance; } }
        private static StatePackManDead instance;

        public void Destroy(Charecter StateObject)
        {
        }

        public void Enter(Charecter StateObject, params object[] args)
        {
            GameManager.Instance.PauseGame();
            StateObject.GetPackManAnimator().SetTrigger("Dead");
            StateObject.SetCharecterState(Charecter.ECharecterState.DEAD);
            StateObject.StartCoroutine(ResetDeathTimer(StateObject.GetDeathTime()));

        }

        IEnumerator ResetDeathTimer(float delay)
        {
            yield return new WaitForSeconds(1.5f);
            GameManager.Instance.PlayerDead();
        }

        public void Update(Charecter StateObject)
        {
        }
    }
}

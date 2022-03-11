using com.hellion.statemachine;

namespace com.hellion.packman
{
    public class StateCharecterAlive : IState<Charecter>
    {
        public static StateCharecterAlive Instance { get { if (instance == null) instance = new StateCharecterAlive(); return instance; } }
        private static StateCharecterAlive instance;

        public void Destroy(Charecter StateObject)
        {
        }

        public void Enter(Charecter StateObject, params object[] args)
        {
            StateObject.SetCharecterState(ECharecterState.ALIVE);
            StateObject.SetCharecterToAliveState();
        }

        public void Update(Charecter StateObject)
        {
        }
    }
}

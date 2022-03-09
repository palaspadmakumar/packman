namespace com.hellion.statemachine
{
    public class StateMachine<T>
    {
        private T StateObject = default;
        public IState<T> CurrentState = default;

        /// <summary>
        /// Intitalize State
        /// </summary>
        /// <param name="StateObject">Current State Object</param>
        public StateMachine(T StateObject, IState<T> InitialState)
        {
            CurrentState?.Destroy(this.StateObject);
            this.StateObject = StateObject;
            CurrentState = InitialState;
            CurrentState.Enter(StateObject);
        }

        /// <summary>
        /// Intitalize State
        /// </summary>
        /// <param name="StateObject">Current State Object</param>
        public StateMachine(T StateObject)
        {
            this.StateObject = StateObject;
        }

        public void Update(T StateObject)
        {
            CurrentState?.Update(StateObject);
        }

        /// <summary>
        /// Change the current state to another state
        /// </summary>
        /// <param name="NewState">Contains the state that the current state would switch to</param>
        public void ChangeState(IState<T> NewState, params object[] args)
        {
            CurrentState?.Destroy(StateObject);
            CurrentState = NewState;
            CurrentState?.Enter(StateObject, args);
        }
    }
}

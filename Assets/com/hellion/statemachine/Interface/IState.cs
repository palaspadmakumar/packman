namespace com.hellion.statemachine
{
    public interface IState<T>
    {
        /// <summary>
        /// Start actions for current State
        /// </summary>
        /// <param name="StateObject">Current State Object</param>
        void Enter(T StateObject, params object[] args);

        /// <summary>
        /// Updates for the current state or the next state
        /// </summary>
        void Update(T StateObject);

        /// <summary>
        /// Destroys current State
        /// </summary>
        /// <param name="StateObject">Current State Object</param>
        void Destroy(T StateObject);
    }
}

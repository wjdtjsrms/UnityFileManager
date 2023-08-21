namespace JSGCode.Base
{
    using System.Collections.Generic;

    public class BasicStateSubject<T> : IStateSubject<T> where T : System.Enum
    {
        private List<IStateObserver<T>> observers;
        private T currentState = default(T);
        private T prevState = default(T);

        public T PrevState => prevState;

        public T CurrentState
        {
            get => currentState;
            set
            {
                if (currentState.Equals(value))
                    return;

                prevState = currentState;
                currentState = value;
                NotifyChangeState(value);
            }
        }

        public BasicStateSubject()
        {
            observers = new List<IStateObserver<T>>(); ;
        }

        public void AddObserver(IStateObserver<T> observer)
        {
            if (observers.Contains(observer) == false)
                observers.Add(observer);
        }

        public void NotifyChangeState(T state)
        {
            foreach (var controller in observers.ToArray())
                controller.Notify(state);
        }

        public void RemoveObserver(IStateObserver<T> observer)
        {
            if (observers.Contains(observer))
                observers.Remove(observer);
        }

        public void Clear()
        {
            observers.Clear();
        }
    }
}
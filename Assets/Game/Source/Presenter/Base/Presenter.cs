using UnityEngine;

namespace Splatrika.StackClone.Presenter
{
    public abstract class Presenter<T> : MonoBehaviour
    {
        protected T Model { get; private set; }

        protected virtual void OnInit(T model) { }
        protected virtual void OnUpdate(float deltaTime) { }

        public void Init(T model)
        {
            Model = model;
            OnInit(model);
        }


        private void Update()
        {
            if (Model is IUpdatable updatable)
            {
                updatable.Update(Time.deltaTime);
            }
            OnUpdate(Time.deltaTime);
        }
    }
}

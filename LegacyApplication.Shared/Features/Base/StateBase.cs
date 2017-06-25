using LegacyApplication.Shared.Configurations;

namespace LegacyApplication.Shared.Features.Base
{
    public abstract class StateBase
    {
        public delegate void StateCreateHandler(object sender);
        public event StateCreateHandler OnCreatedState;

        protected StateBase(int state = AppSettings.开始节点)
        {
            _stateValue = state;
            OnCreatedState += StateBaseOnCreatedState;
            OnCreatedState?.Invoke(this);
            OnCreatedState -= StateBaseOnCreatedState;
        }

        ~StateBase()
        {
            if (OnCreatedState != null)
            {
                OnCreatedState -= StateBaseOnCreatedState;
            }
        }

        private void StateBaseOnCreatedState(object sender)
        {
            Initialize(_stateValue);
        }

        private readonly int _stateValue;
        protected abstract void Initialize(int state);
        protected abstract void CheckAuthorization();
        
    }
}

using LegacyApplication.Shared.Configurations;
using LegacyApplication.Shared.Exceptions.Stateless;
using LegacyApplication.Shared.Features.Base;
using Stateless;

namespace LegacyApplication.States.HumanResources.NewEmployee
{
    public class NewEmployeeApplication: StateBase
    {
        private enum State
        {
            开始 = AppSettings.开始节点,

            人资审批 = 100,
            总经理审批 = 500,

            通过 = AppSettings.审批通过,
            拒绝 = AppSettings.审批未通过
        }

        private enum Trigger
        {
            提交申请 = AppSettings.提交申请,

            人资审批通过 = 100,
            人资审批拒绝 = -100,

            总经理审批通过 = 500,
            总经理审批拒绝 = -500
        }

        private State _state;
        private StateMachine<State, Trigger> _machine;
        
        protected override void Initialize(int state)
        {
            _state = (State)state;
            CheckAuthorization();

            _machine = new StateMachine<State, Trigger>(() => _state, s => _state = s);

            _machine.Configure(State.开始)
                .PermitIf(Trigger.提交申请, State.人资审批);

            _machine.Configure(State.人资审批)
                .PermitIf(Trigger.人资审批通过, State.总经理审批)
                .PermitIf(Trigger.人资审批拒绝, State.拒绝);

            _machine.Configure(State.总经理审批)
                .PermitIf(Trigger.总经理审批通过, State.通过)
                .PermitIf(Trigger.总经理审批拒绝, State.拒绝);

            _machine.Fire(Trigger.提交申请);
        }

        protected override void CheckAuthorization()
        {
            if (_state == State.拒绝 || _state == State.拒绝)
            {
                return;
            }
            return;
            throw new UnAuthorizedStateException("您没有该节点的审批权限");
        }

        public void 人资审批(bool isOk)
        {
            _machine.Fire(isOk ? Trigger.人资审批通过 : Trigger.人资审批拒绝);
        }

        public void 总经理审批(bool isOk)
        {
            _machine.Fire(isOk ? Trigger.总经理审批通过 : Trigger.总经理审批拒绝);
        }
    }
}

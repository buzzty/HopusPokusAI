namespace AISystem.HopusPocus
{
    public abstract class HPAnimatorDrivenPhase : HPPhaseState
    {
        private bool _isAnimationDone;

        public void AnimationDone()
        {
            _isAnimationDone = true;
        }

        public override void OnStateEnter(HPEnemyPhaseFSM phaseFSM, Enemy enemy)
        {
            base.OnStateEnter(phaseFSM, enemy);

            _isAnimationDone = false;
        }

        public override bool OnStateUpdate(HPEnemyPhaseFSM phaseFSM, Enemy enemy)
        {
            if (_isAnimationDone)
            {
                return true;
            }

            return base.OnStateUpdate(phaseFSM, enemy);
        }
    }
}
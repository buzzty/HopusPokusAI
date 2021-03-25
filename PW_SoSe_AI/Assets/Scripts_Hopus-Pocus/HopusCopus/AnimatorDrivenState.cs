namespace AISystem.HopusPocus
{
    public abstract class AnimatorDrivenPhase : PhaseState
    {
        private bool _isAnimationDone;

        public void AnimationDone()
        {
            _isAnimationDone = true;
        }

        public override void OnStateEnter(EnemyPhaseFSM phaseFSM, Enemy enemy)
        {
            base.OnStateEnter(phaseFSM, enemy);

            _isAnimationDone = false;
        }

        public override bool OnStateUpdate(EnemyPhaseFSM phaseFSM, Enemy enemy)
        {
            if (_isAnimationDone)
            {
                return true;
            }

            return base.OnStateUpdate(phaseFSM, enemy);
        }
    }
}
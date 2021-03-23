namespace AISystem
{
	public interface IAnimatorDriveable
	{
		bool IsAnimationDone { get; }
		void AnimationDone();
	}
}
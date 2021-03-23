using Core.Utility;

namespace Core
{
	public abstract class MonoSingleton<T> : CachedMonoBehaviour where T : MonoSingleton<T>
	{
		public static T Instance => _instance as T;

		private static MonoSingleton<T> _instance;
		
		protected override void Awake()
		{
			base.Awake();

			if ((_instance != null) && (_instance != this))
			{
				Destroy(gameObject);
			}
			else
			{
				_instance = this;
			}
		}
	}
}
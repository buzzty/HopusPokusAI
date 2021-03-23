using System.Collections;
using Core.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

namespace UI
{
	public class PlayerHPUI : CachedMonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _healthAmountText = default;
		[SerializeField] private PlayableDirector _onHitTimeline = default;
		[SerializeField] private Image _inner = default;
		[SerializeField] private float _blinkSpeed = 0.5f;

		public void InitForHP(int hp)
		{
			_healthAmountText.text = hp.ToString();
		}
		
		public void UpdateUI(int hp)
		{
			_onHitTimeline.Play();
			_healthAmountText.text = hp.ToString();

			if (hp == 1)
			{
				StartCoroutine(LowHPColorBlink());
			}
		}

		private IEnumerator LowHPColorBlink()
		{
			float blinkDuration = 0f;
			Color fromColor = _inner.color;
			Color toColor = Color.red;
			while (true)
			{
				_inner.color = Color.Lerp(fromColor, toColor, blinkDuration / _blinkSpeed);
				yield return null;
				blinkDuration += Time.deltaTime;
				
				if (_inner.color.Compare(toColor))
				{
					_inner.color = toColor;
					toColor = toColor.Compare(Color.red) ? Color.white : Color.red;
					fromColor = _inner.color;

					blinkDuration = 0f;
				}
			}
		}
	}
}
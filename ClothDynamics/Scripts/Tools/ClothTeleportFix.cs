using System.Collections;
using UnityEngine;

namespace ClothDynamics
{
	public class ClothTeleportFix : MonoBehaviour
	{
		[SerializeField]
		private GPUClothDynamics[] _cds;
		[SerializeField]
		private float _teleportDuration = 1.0f;
		//private WaitForSeconds _waitForSeconds = new WaitForSeconds(_teleportDuration);

		private void Awake()
		{
			if (_cds == null || _cds.Length < 1) _cds = GetComponentsInChildren<GPUClothDynamics>();
		}

		public void OnTeleportEvent()
		{
			print("OnTeleportEvent() triggered!");
			foreach (var cd in _cds)
			{
				var saveMinBlend = cd._minBlend;
				cd._minBlend = 1;
				StartCoroutine(DelayBlendBack(cd, saveMinBlend));
			}
		}

		private IEnumerator DelayBlendBack(GPUClothDynamics cd, float saveMinBlend)
		{
			//yield return _waitForSeconds;
			float blendTime = 0;
			while (blendTime < _teleportDuration)
			{
				float step = blendTime / _teleportDuration;
				cd._minBlend = Mathf.Lerp(cd._minBlend, saveMinBlend, step);
				blendTime += Time.deltaTime;
				yield return null;
			}
			cd._minBlend = saveMinBlend;
		}
	}
}
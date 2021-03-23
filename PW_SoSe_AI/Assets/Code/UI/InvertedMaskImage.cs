using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace UI
{
	public class InvertedMaskImage : Image 
	{
		private static readonly int StencilComp = Shader.PropertyToID("_StencilComp");

		public override Material materialForRendering
		{
			get
			{
				Material result = new Material(base.materialForRendering);
				result.SetInt(StencilComp, (int)CompareFunction.NotEqual);
				return result;
			}
		}
	}
}
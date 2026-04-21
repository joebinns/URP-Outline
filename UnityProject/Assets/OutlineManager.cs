using UnityEngine;
using UnityEngine.Rendering.Universal;

[ExecuteAlways]
public class OutlineManager : MonoBehaviour
{
	[SerializeField, Min(0f)] private float _minOutlineWidth = 9;
	[SerializeField, Min(0f)] private float _maxOutlineWidth = 11;
	[SerializeField, Min(0f)] private float _pulsePeriod = 1f;

	private float _outlineWidth;
	private float _progress;

	private const string BASE_OUTLINE_WIDTH_PROPERTY = "_Base_Outline_Width";
	private const string OUTLINE_WIDTH_PROPERTY = "_Outline_Width";
	private const int BASE_OUTLINE_WIDTH = 10;

	// UPDATE

	private void Update()
	{
		var baseOutlineWidth = (_minOutlineWidth + _maxOutlineWidth) / 2f;
		var range = _maxOutlineWidth - _minOutlineWidth;
		_outlineWidth = baseOutlineWidth + range * Mathf.Sin(_progress * 2f * Mathf.PI);
		_progress += Time.deltaTime / _pulsePeriod;
		RefreshShaderProperties();
		RFO.Instance?.SetOutlineWidth(_outlineWidth);
	}

	// REFRESH

	private void RefreshShaderProperties()
	{
		Shader.SetGlobalFloat(BASE_OUTLINE_WIDTH_PROPERTY, BASE_OUTLINE_WIDTH);
		Shader.SetGlobalFloat(OUTLINE_WIDTH_PROPERTY, _outlineWidth);
	}
}

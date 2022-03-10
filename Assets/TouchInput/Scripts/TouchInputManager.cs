using System;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public sealed class TouchInputManager : MonoBehaviour, IInputManager
{
	[SerializeField] private RectTransform mKnob;

	private RectTransform mRectTransform;

	private Vector2 mInputVector;
	private float mRadius;
	private Vector3 mScreenSpacePosition;

	public event Action<Vector2> InputUpdated;

	public Vector2 InputVector
	{
		get
		{
			return mInputVector;
		}

		set
		{
			mInputVector = value;
			InputUpdated?.Invoke(mInputVector);
		}
	}

	public void OnBeginDrag()
	{
		InputVector = GetInputVector(Input.mousePosition);
	}

	public void OnDrag()
	{
		InputVector = GetInputVector(Input.mousePosition);
	}

	public void OnEndDrag()
	{
		InputVector = GetInputVector(mScreenSpacePosition);
	}

	private Vector2 GetInputVector(Vector3 inputPosition)
	{
		return Vector2.ClampMagnitude(inputPosition - mScreenSpacePosition, mRadius) / mRadius;
	}

	private Vector2 GetPositionFromInputVector(Vector2 inputVector)
	{
		return (Vector2)mRectTransform.position + InputVector * mRadius;
	}

	private void Start()
	{
		Debug.Log("Start");

		mRectTransform = GetComponent<RectTransform>();
		Vector3[] corners = new Vector3[4];
		mRectTransform.GetWorldCorners(corners);
		float width = corners[3].x - corners[0].x;
		float height = corners[1].y - corners[0].y;
		mRadius = Mathf.Min(width, height) * 0.5f;
		mScreenSpacePosition = mRectTransform.position;
	}

	private void Update()
	{
		mKnob.position = Vector3.Lerp(mKnob.position, GetPositionFromInputVector(mInputVector), 20 * Time.deltaTime);
	}
}

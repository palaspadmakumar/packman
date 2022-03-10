using System;
using UnityEngine;

public sealed class TouchInputManagerProxy : MonoBehaviour, IInputManager
{
	[SerializeField] private TouchInputManager mTouchInputManager;

	private Vector2 mInputVector;

	public event Action<Vector2> InputUpdated;

	private Vector2 InputVector
	{
		get
		{
			return mInputVector;
		}

		set
		{
			if (Vector2.Distance(mInputVector, value) < 0.5f)
			{
				return;
			}

			mInputVector = value;
			InputUpdated?.Invoke(mInputVector);
			Debug.Log(mInputVector);
		}
	}

	private void Start()
	{
		if (mTouchInputManager == null)
		{
			return;
		}

		mTouchInputManager.InputUpdated += OnInputUpdated;
	}

	private void OnDestroy()
	{
		mTouchInputManager.InputUpdated -= OnInputUpdated;
	}

	private void OnInputUpdated(Vector2 inputVector)
	{
		Vector2 vector = inputVector.normalized / Mathf.Sin(45f * Mathf.PI / 180f);
		vector = new Vector2((int)vector.x, (int)vector.y);
		InputVector = vector;
	}
}

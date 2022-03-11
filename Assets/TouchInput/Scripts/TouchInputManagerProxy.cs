using System;
using UnityEngine;

public sealed class TouchInputManagerProxy : MonoBehaviour
{
    [SerializeField] private TouchInputManager mTouchInputManager;

    private Vector2Int mInputVector;

    public static Action<Vector2Int> InputUpdated;

    private Vector2 InputVector
    {
        get
        {
            return mInputVector;
        }

        set
        {
            mInputVector = Vector2Int.RoundToInt(value);
        }
    }

    private void Update()
    {
        InputUpdated?.Invoke(mInputVector);
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

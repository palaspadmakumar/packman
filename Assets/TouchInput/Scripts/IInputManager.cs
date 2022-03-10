using System;
using UnityEngine;

public interface IInputManager
{
	event Action<Vector2> InputUpdated;
}

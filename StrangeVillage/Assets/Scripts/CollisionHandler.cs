using UnityEngine;
using UnityEngine.Events;

public class CollisionHandler : MonoBehaviour
{
	public UnityEvent colliderEntered;

	private void OnTriggerEnter(Collider other)
	{
		if (!other.CompareTag("Player"))
		{
			return;
		}

		colliderEntered.Invoke();
	}
}

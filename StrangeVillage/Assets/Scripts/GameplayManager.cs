using UnityEngine;
using UnityEngine.Events;

public class GameplayManager : MonoBehaviour
{
	[field: SerializeField] public bool IsDaytime { get; set; }
	[SerializeField] private int _daysCount;
	[SerializeField] private GameObject _positiveIncidentChecker;
	[field: SerializeField] public bool IsIncidentHappened { get; set; }
	[field: SerializeField] public bool IsIncidentPositive { get; set; }
	[SerializeField] private UnityEvent _positiveIncidentHappened;
	[SerializeField] private UnityEvent _negativeIncidentHappened;

	public int DaysCounter { get; set; } = 0;

	public void IncrementDay()
	{
		if (IsIncidentHappened)
		{
			return;
		}
		
		DaysCounter++;

		if (DaysCounter >= _daysCount)
		{
			IsIncidentPositive = _positiveIncidentChecker.activeInHierarchy;
			IsIncidentHappened = true;

			if (IsIncidentPositive)
			{
				_positiveIncidentHappened.Invoke();
			}
			else
			{
				_negativeIncidentHappened.Invoke();
			}
		}
	}
}

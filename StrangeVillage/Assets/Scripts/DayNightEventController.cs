using System;
using System.Collections.Generic;
using Pinwheel.Jupiter;
using UnityEngine;
using UnityEngine.Events;

public class DayNightEventController : MonoBehaviour
{
	[SerializeField] private JDayNightCycle _cycle;
	[SerializeField] private List<TimeDayEventData> _timeDayEventDataCollection;

	private bool _isFullCycle;
	private float _previousCachedTime;

	private void Start()
	{
		_previousCachedTime = _cycle.Time;
	}

	private void Update()
	{
		float currentTime = _cycle.Time;

		if (_previousCachedTime > currentTime)
		{
			_isFullCycle = true;
		}

		if (_isFullCycle)
		{
			// Invoke all not already invoked events.
			foreach (TimeDayEventData timeDayEventData in _timeDayEventDataCollection)
			{
				if (timeDayEventData.IsAlreadyInvoked)
				{
					timeDayEventData.IsAlreadyInvoked = false;
					continue;
				}

				timeDayEventData.Activate();
			}

			_isFullCycle = false;
		}

		foreach (TimeDayEventData timeDayEventData in _timeDayEventDataCollection)
		{
			if (timeDayEventData.IsAlreadyInvoked)
			{
				continue;
			}

			if (timeDayEventData.TimeOfDay < currentTime)
			{
				timeDayEventData.Activate();
				timeDayEventData.IsAlreadyInvoked = true;
			}
		}

		_previousCachedTime = currentTime;
	}

	[Serializable]
	private class TimeDayEventData
	{
		[field: SerializeField] public bool IsAlreadyInvoked { get; set; }
		[field: SerializeField, Range(0f, 24f)]
		public float TimeOfDay { get; set; }
		[field: SerializeField] public UnityEvent<float> Activated { get; set; }

		public void Activate()
		{
			Activated.Invoke(TimeOfDay);
		}
	}
}

using Pinwheel.Jupiter;
using TMPro;
using UnityEngine;

public class DayNightCounterController : MonoBehaviour
{
	[SerializeField] private JDayNightCycle _cycle;
	[SerializeField] private GameplayManager _manager;
	[SerializeField] private TMP_Text _textComponent;

	private void Update()
	{
		SetUI(_manager.DaysCounter + 1, _cycle.Time);
	}

	private void SetUI(int daysCount, float time)
	{
		// Calculate hours and minutes from time
		int hours = Mathf.FloorToInt(time);
		int minutes = Mathf.FloorToInt((time - hours) * 60);

		// Format time as HH:MM
		string timeString = $"{hours:D2}:{minutes:D2}";

		// Determine the correct day string (singular or plural)
		string dayString = daysCount > 1 ? "Days" : "Day";

		// Set the text component
		_textComponent.text = $"{daysCount} {dayString} / {timeString}";
	}
}

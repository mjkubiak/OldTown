using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class PuzzleController : MonoBehaviour
{
	public Transform playerVisionTransform;
	public LayerMask layerMask;

	public GameObject leftCorridorVision;
	public GameObject rightCorridorVision;

	public GameObject leftClosedCorridor;
	public GameObject leftOpenedCorridor;
	public GameObject rightClosedCorridor;
	public GameObject rightOpenedCorridor;

	public GameObject leftArrow;
	public GameObject rightArrow;

	public TMP_Text textComponent;
	
	public int puzzleCounter;
	public List<Side> correctDirection;
	public int puzzleLength = 4;

	public bool isAlreadyAnswered;

	public GameObject finalDoorBlocker;

	public UnityEvent puzzleCompleted;

	private void Start()
	{
		GeneratePuzzle();
		ShowNextQuestion();
	}

	private void FixedUpdate()
	{
		bool isHit = Physics.Raycast(playerVisionTransform.position, playerVisionTransform.forward, out RaycastHit hit, 5f, layerMask, QueryTriggerInteraction.Collide);

		if (!isHit) return;
		
		if (hit.collider.gameObject == leftCorridorVision)
		{
			ProcessAnswer(Side.Left);
		}
		else if (hit.collider.gameObject == rightCorridorVision)
		{
			ProcessAnswer(Side.Right);
		}
	}

	private void ProcessAnswer(Side side)
	{
		if (isAlreadyAnswered) return;
		
		isAlreadyAnswered = true;
		
		string sideText = side == Side.Left ? "Left" : "Right";
		Debug.LogWarning($"{sideText} hitted");

		bool isLeft = side == Side.Left;
		
		leftOpenedCorridor.SetActive(!isLeft);
		leftClosedCorridor.SetActive(isLeft);
			
		rightClosedCorridor.SetActive(!isLeft);
		rightOpenedCorridor.SetActive(isLeft);
		
		if (side != correctDirection[puzzleCounter])
		{
			// Correct answer
			puzzleCounter++;
			return;
		}
		
		// Wrong answer
		puzzleCounter = 0;
		GeneratePuzzle();
	}

	public void ShowNextQuestion()
	{
		if (puzzleCounter >= correctDirection.Count)
		{
			Debug.LogWarning("Finished puzzle");
			
			leftOpenedCorridor.SetActive(false);
			leftClosedCorridor.SetActive(false);
			
			rightClosedCorridor.SetActive(false);
			rightOpenedCorridor.SetActive(false);

			finalDoorBlocker.SetActive(false);
			
			puzzleCompleted.Invoke();
			
			return;
		}

		Side currentDirection = correctDirection[puzzleCounter];
		bool isLeft = currentDirection == Side.Left;
		
		leftArrow.SetActive(isLeft);
		rightArrow.SetActive(!isLeft);
		
		textComponent.SetText($"Level {puzzleCounter + 1}");

		isAlreadyAnswered = false;
	}

	private void GeneratePuzzle()
	{
		correctDirection = new();

		for (int i = 0; i < puzzleLength; i++)
		{
			correctDirection.Add(Random.value > 0.5 ? Side.Left : Side.Right);
		}
	}
	
	public enum Side
	{
		Left,
		Right
	}
}

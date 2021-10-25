/*
	Module name BattleDialogueBox
	Module creation date - 04-Sep-2021
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleDialogueBox : MonoBehaviour
{
	// SerializedField var are visible in Unity Editor

	// Text effect variables
	[SerializeField] int letterPerSecond;
	[SerializeField] Color highlightedColor;
	// Referance variables for text
	[SerializeField] Text dialogueText;
	[SerializeField] Text ppText;
	[SerializeField] Text typeText;

	[SerializeField] Text yesText;
	[SerializeField] Text noText;
	// Referance variables for panels
	[SerializeField] GameObject actionSelector;
	[SerializeField] GameObject moveSelector;
	[SerializeField] GameObject moveDetails;
	[SerializeField] GameObject choiceBox;

	// Referance variables for text list of actions and moves
	[SerializeField] List<Text> actionTexts;
	[SerializeField] List<Text> moveTexts;

	// To display dialogueText
	public void SetDialogue(string dialogue)
	{
		dialogueText.text = dialogue;
	}
	// To display dialogueText with animation
	public IEnumerator TypeDialogue(string dialogue)
	{
		dialogueText.text = "";
		foreach (var letter in dialogue.ToCharArray())
		{
			dialogueText.text += letter;
			yield return new WaitForSeconds(1f/letterPerSecond);
		}

		yield return new WaitForSeconds(1f);
	}

	// To enable/disable UI panels using a bool
	public void EnableDialogueText(bool enabled)
	{
		dialogueText.enabled = enabled;
	}
	public void EnableActionSelector(bool enabled)
	{
		actionSelector.SetActive(enabled);
	}
	public void EnableMoveSelector(bool enabled)
	{
		moveSelector.SetActive(enabled);
		moveDetails.SetActive(enabled);
	}

	public void EnableChoiceBox(bool enabled)
	{
		choiceBox.SetActive(enabled);
	}

	// Update action panel texts according to player input
	public void UpdateActionSelection(int selectedAction)
	{
		for (int i = 0; i < actionTexts.Count; i++)
		{
			if (i == selectedAction)
				actionTexts[i].color = highlightedColor;
			else
				actionTexts[i].color = Color.black;
		}
	}
	// Update move panel texts according to player input
	public void UpdateMoveSelection(int selectedMove, Move move)
	{
		for (int i = 0; i < moveTexts.Count; i++)
		{
			if (i == selectedMove)
				moveTexts[i].color = highlightedColor;
			else
				moveTexts[i].color = Color.black;
		}

		ppText.text = $"PP {move.PP}/{move.Base.PP}";
		typeText.text = move.Base.Type.ToString();

		if(move.PP == 0)
		{
			ppText.color = Color.red;
		}
		else 
			ppText.color = Color.black;
	}
	// Set move names available from player unit
	public void SetMoveNames(List<Move> moves)
	{
		for (int i = 0; i < moveTexts.Count; ++i)
		{
			if (i < moves.Count)
				moveTexts[i].text = moves[i].Base.Name;
			else
				moveTexts[i].text = "-";
		}
	}

	public void UpdateChoiceBox(bool yesSelected)
	{
		if (yesSelected)
		{
			yesText.color = highlightedColor;
			noText.color = Color.black;
		}
		else
		{
			yesText.color = Color.black;
			noText.color = highlightedColor;
		}
	}
}

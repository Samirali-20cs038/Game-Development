/*
	Module name GameController
	Module creation date - 20-Sep-2021
	@author: Abhishek Kayasth
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Public Enum for game states
public enum GameState { FreeRoam, Battle }

public class GameController : MonoBehaviour
{
	// SerializedField var (Visible in Unity Editor)
	[SerializeField] PlayerController playerController;
	[SerializeField] BattleSystem battleSystem;
	[SerializeField] Camera worldCamera;
	// Local variables
	GameState state;

	private void Start()
	{
		playerController.OnEncounter += StartBattle;
		battleSystem.OnBattleOver += EndBattle;
	}
	// This method reverts game state back into free roam state
	void EndBattle(bool won)
	{
		state = GameState.FreeRoam;
		battleSystem.gameObject.SetActive(false);
		worldCamera.gameObject.SetActive(true);
	}
	// This method makes game state into battle state
	void StartBattle()
	{
		state = GameState.Battle;
		battleSystem.gameObject.SetActive(true);
		worldCamera.gameObject.SetActive(false);

		var playerParty = playerController.GetComponent<PokemonParty>();
		var wildPokemon = FindObjectOfType<MapArea>().GetComponent<MapArea>().GetRandomWildPokemon();

		battleSystem.StartBattle(playerParty, wildPokemon);
	}
	//
	private void Update()
	{
		// Check current state and handle updates according to it
		if (state == GameState.FreeRoam)
		{
			playerController.HandleUpdate();
		}
		else if (state == GameState.Battle)
		{
			battleSystem.HandleUpdate();
		}
	}
}

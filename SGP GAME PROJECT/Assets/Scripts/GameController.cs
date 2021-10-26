using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Public Enum for game states
public enum GameState { FreeRoam, Battle, Dialogue,Cutscene }

public class GameController : MonoBehaviour
{
	// SerializedField var (Visible in Unity Editor)
	[SerializeField] PlayerController playerController;
	[SerializeField] BattleSystem battleSystem;
	[SerializeField] Camera worldCamera;
	// Local variables
	GameState state;
	public static GameController Instance { get; private set;}

	private void Awake()
	{
		Instance = this;
		ConditionsDB.Init();
	}

	private void Start()
	{
		battleSystem.OnBattleOver += EndBattle;


		DialogueManager.Instance.OnShowDialog += () =>
		{
			state = GameState.Dialogue;
		};

		DialogueManager.Instance.OnCloseDialog += () =>
		{
			if(state == GameState.Dialogue)
				state = GameState.FreeRoam;
		};
	}
	
	// This method makes game state into battle state
	public void StartBattle()
	{
		state = GameState.Battle;
		battleSystem.gameObject.SetActive(true);
		worldCamera.gameObject.SetActive(false);

		var playerParty = playerController.GetComponent<PokemonParty>();
		var wildPokemon = FindObjectOfType<MapArea>().GetComponent<MapArea>().GetRandomWildPokemon();

		var wildPokemonCopy = new Pokemon (wildPokemon.Base, wildPokemon.Level);

		battleSystem.StartBattle(playerParty, wildPokemonCopy);
	}

	TrainerController trainer;
	
	public void StartTrainerBattle(TrainerController trainer)
	{
		state = GameState.Battle;
		battleSystem.gameObject.SetActive(true);
		worldCamera.gameObject.SetActive(false);

		this.trainer = trainer;
		var playerParty = playerController.GetComponent<PokemonParty>();
		var trainerParty = trainer.GetComponent<PokemonParty>();

		battleSystem.StartTrainerBattle(playerParty, trainerParty);
	}

	public void OnEnterTrainersView(TrainerController trainer)
	{
		state = GameState.Cutscene;
		StartCoroutine(trainer.TriggerTrainerBattle(playerController));
	}

	// This method reverts game state back into free roam state
	void EndBattle(bool won)
	{
		if (trainer != null && won == true)
		{
			trainer.BattleLost();
			trainer=null;
		}

		state = GameState.FreeRoam;
		battleSystem.gameObject.SetActive(false);
		worldCamera.gameObject.SetActive(true);
	}
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
		else if (state == GameState.Dialogue)
		{
			DialogueManager.Instance.HandleUpdate();
		}
	}
}

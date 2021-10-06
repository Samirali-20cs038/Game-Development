/*
	Module name BattleDialogueBox
	Module creation date - 03-Sep-2021
	@author: Abhishek Kayasth
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
	// referance variables
	[SerializeField] Text nameText;
	[SerializeField] Text levelText;
	[SerializeField] HPBar hpBar;
	// cache variable
	Pokemon _pokemon;

	// Set data of given Pokemon on HUD UI elements
	public void SetData(Pokemon pokemon)
	{
		_pokemon = pokemon; // caching into local variable
		nameText.text = pokemon.Base.Name;
		levelText.text = "Lvl " + pokemon.Level;
		hpBar.SetHP((float)pokemon.HP / pokemon.MaxHp);
	}
	// Updates HP bar with animation 
	public IEnumerator UpdateHP()
	{
		yield return hpBar.SetHPSmooth((float)_pokemon.HP / _pokemon.MaxHp);
	}
}

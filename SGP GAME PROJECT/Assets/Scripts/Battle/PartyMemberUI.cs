using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyMemberUI : MonoBehaviour
{
    // referance variables
	[SerializeField] Text nameText;
	[SerializeField] Text levelText;
	[SerializeField] HPBar hpBar;

	[SerializeField] Color highlightedColor;
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

	public void SetSelected(bool selected)
	{
		if(selected)
			nameText.color = highlightedColor;
		else
			nameText.color = Color.black;
	}
}

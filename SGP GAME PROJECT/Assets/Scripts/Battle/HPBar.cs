using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
	[SerializeField] GameObject health;

	public void SetHP(float hpNormalized)
	{
		health.transform.localScale = new Vector3(hpNormalized, 1f);
	}

	public IEnumerator SetHPSmooth(float newHP)
	{
		float currHP = health.transform.localScale.x;
		float changeAmt = currHP - newHP;

		while (currHP - newHP > Mathf.Epsilon)
		{
			currHP -= changeAmt * Time.deltaTime;
			health.transform.localScale = new Vector3(currHP, 1f);
			yield return null;
		}
		health.transform.localScale = new Vector3(newHP, 1f);
	}
}

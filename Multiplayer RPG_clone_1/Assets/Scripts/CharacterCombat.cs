using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStatsNetwork))]
public class CharacterCombat : MonoBehaviour
{
    public float attackRate = 1f;
	private float attackCountdown = 0f;

	public event System.Action OnAttack;

	public Transform healthBarPos;

	CharacterStatsNetwork myStats;
	CharacterStatsNetwork enemyStats;

    private void Start ()
	{
		myStats = GetComponent<CharacterStatsNetwork>();
		//HealthUIManager.instance.Create (healthBarPos, myStats);
	}

    void Update ()
	{
		attackCountdown -= Time.deltaTime;
	}

    public void Attack (CharacterStatsNetwork enemyStats)
	{
		if (attackCountdown <= 0f)
		{
			this.enemyStats = enemyStats;
			attackCountdown = 1f / attackRate;

			StartCoroutine(DoDamage(enemyStats,.6f));

			if (OnAttack != null) {
				OnAttack();
			}
		}
	}

    IEnumerator DoDamage(CharacterStatsNetwork stats, float delay)
    {
		yield return new WaitForSeconds (delay);

		Debug.Log (transform.name + " swings for " + myStats.damage.GetValue () + " damage");
		enemyStats.TakeDamage(myStats.damage.GetValue());
	}
}

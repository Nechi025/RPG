using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Fighter
{
    private void Awake()
    {
        this.stats = new Stats(19, 50, 40, 35, 10);
    }

    public override void InitTurn()
    {
        StartCoroutine(IA());
    }

    IEnumerator IA()
    {
        yield return new WaitForSeconds(1f);

        Skill skill = skills[Random.Range(0, skills.Length)];

        skill.SerEmitterAndReciever(
            this, combatManager.GetOpposingFigther()
            );

        combatManager.OnFigtherSkill(skill);
    }
}

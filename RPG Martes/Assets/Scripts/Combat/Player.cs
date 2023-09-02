using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Fighter
{
    [Header("UI")]
    public PlayerSkillPanel skillPanel;

    private void Awake()
    {
        this.stats = new Stats(21, 60, 50, 45, 20);
    }

    public override void InitTurn()
    {
        skillPanel.Show();

        for (int i = 0; i < skills.Length; i++)
        {
            skillPanel.ConfigureButtons(i, skills[i].skillName);
        }
    }

    public void ExcecuteSkill(int index)
    {
        skillPanel.Hide();

        Skill skill = skills[index];

        skill.SerEmitterAndReciever(
            this, combatManager.GetOpposingFigther()
            );

        combatManager.OnFigtherSkill(skill);
    }
}

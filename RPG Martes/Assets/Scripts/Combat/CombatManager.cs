using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CombatStatus
{
    WAITING_FOR_FIGHTER,
    FIGTHER_ACTION,
    CHECK_FOR_VICTORY,
    NEXT_TURN,
}

public class CombatManager : MonoBehaviour
{
    public Fighter[] fighters;
    private int fightersIndex;

    private bool isCombatActive;

    private CombatStatus combatStatus;

    private Skill currentFighterSkill;

    private void Start()
    {
        LogPanel.Write("Battle initiated");

        foreach (var fgtr in fighters)
        {
            fgtr.combatManager = this;
        }

        combatStatus = CombatStatus.NEXT_TURN;

        this.fightersIndex = -1;
        this.isCombatActive = true;
        StartCoroutine(this.CombatLoop());
    }

    IEnumerator CombatLoop()
    {
        while (this.isCombatActive)
        {
            switch (this.combatStatus)
            {
                case CombatStatus.WAITING_FOR_FIGHTER:
                    yield return null;
                    break; 

                case CombatStatus.FIGTHER_ACTION:
                    LogPanel.Write($"{fighters[fightersIndex].idName} uses {currentFighterSkill.skillName}.");

                    yield return null;

                    currentFighterSkill.Run();

                    yield return new WaitForSeconds(currentFighterSkill.animationDuration);
                    combatStatus = CombatStatus.CHECK_FOR_VICTORY;

                    currentFighterSkill = null;
                    break;

                case CombatStatus.CHECK_FOR_VICTORY:
                    foreach (var fgtr in fighters)
                    {
                        if (fgtr.isAlive == false)
                        {
                            isCombatActive = false;

                            LogPanel.Write("Victory!");
                        }
                        else
                        {
                            combatStatus = CombatStatus.NEXT_TURN;
                        }
                    }
                    yield return null;
                    break;

                case CombatStatus.NEXT_TURN:
                    yield return new WaitForSeconds(1f);

                    this.fightersIndex = (this.fightersIndex + 1) % this.fighters.Length;

                    var currentTurn = this.fighters[this.fightersIndex];

                    LogPanel.Write($"{currentTurn.idName} has the turn.");
                    currentTurn.InitTurn();

                    combatStatus = CombatStatus.WAITING_FOR_FIGHTER;

                    break;
            }
        }
    }

    public Fighter GetOpposingFigther()
    {
        if (fightersIndex == 0)
        {
            return fighters[1];
        }
        else
        {
            return fighters[0];
        }
    }

    public void OnFigtherSkill(Skill skill)
    {
        currentFighterSkill = skill;
        combatStatus = CombatStatus.FIGTHER_ACTION;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HealthModType
{
    STAT_BASED, FIXED, PERCENTAGE
}

public class HealthModSkill : Skill
{
    [Header("Health Mod")]
    public float amount;

    public HealthModType modType;

    protected override void OnRun()
    {
        float amount = GetModification();

        reciever.ModifyHealth(amount);
    }

    public float GetModification()
    {
        switch (modType)
        {
            case HealthModType.STAT_BASED:

                Stats emitterStats = emitter.GetCurrentStats();
                Stats recieverStats = reciever.GetCurrentStats();

                float rawDamage = (((2 * emitterStats.level) / 5) + 2) * amount * (emitterStats.attack / recieverStats.defense);

                return (rawDamage / 50) + 2;

            case HealthModType.FIXED:
                return amount;

            case HealthModType.PERCENTAGE:
                Stats rStats = reciever.GetCurrentStats();

                return rStats.maxHealth * this.amount;
        }

        throw new System.InvalidOperationException("HealtModSkill::GetDamage. Unreacgable!");
    }
}

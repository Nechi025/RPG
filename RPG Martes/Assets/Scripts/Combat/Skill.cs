using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [Header("Base Skill")]
    public string skillName;
    public float animationDuration;

    public bool selfInflicted;

    //public GameObject effectPrfb;

    protected Fighter emitter;
    protected Fighter reciever;

    /*private void Animate()
    {
        var go = Instantiate(this.effectPrfb, this.reciever.transform.position, Quaternion.identity);
        Destroy(go, this.animationDuration);
    }*/

    public void Run()
    {
        if (selfInflicted)
        {
            reciever = emitter;
        }

        //Animate();

        OnRun();
    }

    public void SerEmitterAndReciever(Fighter _emitter, Fighter _reciever)
    {
        emitter = _emitter;
        reciever = _reciever;
    }

    protected abstract void OnRun();
}

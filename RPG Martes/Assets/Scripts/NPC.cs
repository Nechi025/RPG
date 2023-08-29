using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, Interactable
{
    [SerializeField] private Texts texts;
    [SerializeField] private GameObject activeDesactiveObject;

    public void Interact()
    {
        Debug.Log("Hay un NPC aca");
        activeDesactiveObject.SetActive(true);
        FindObjectOfType<DialogueController>()?.ActiveSign(texts); 
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour
{
    public Queue <string> dialogueQueue = new Queue<string>();
    Texts texts;
    [SerializeField] TextMeshProUGUI textScreen;
    [SerializeField] PlayerController player;

    public void ActiveSign(Texts textObject)
    {
        texts = textObject;
        ActiveText();
    }

    public void ActiveText()
    {

        dialogueQueue.Clear();
        foreach (string textSave in texts.arraytexts)
        {
            dialogueQueue.Enqueue(textSave);
        }
        NextPhrase();
    }

    public void NextPhrase()
    {
        if (dialogueQueue.Count == 0)
        {
            CloseSing();
            return;
        }

        string actualPhrase = dialogueQueue.Dequeue();
        textScreen.text = actualPhrase;
        StartCoroutine(ShowCharacters(actualPhrase));
    }

    IEnumerator ShowCharacters (string texttoShow)
    {
        textScreen.text = "";
        foreach (char character in texttoShow.ToCharArray())
        {
            textScreen.text += character;
            yield return new WaitForSeconds(0.02f);
        }
    }

    void CloseSing()
    {
        this.gameObject.SetActive(false);
        player.isInteracting = false;
    }
}



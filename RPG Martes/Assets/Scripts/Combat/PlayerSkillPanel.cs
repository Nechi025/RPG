using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerSkillPanel : MonoBehaviour
{
    public GameObject[] skillButtons;
    public TextMeshProUGUI[] skillButtonLabels;

    private void Awake()
    {
        Hide();

        foreach(var btn in skillButtons)
        {
            btn.SetActive(false);
        }
    }

    public void ConfigureButtons(int index, string skillName)
    {
        skillButtons[index].SetActive(true);
        skillButtonLabels[index].text = skillName;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}

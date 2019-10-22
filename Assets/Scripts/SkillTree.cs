using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Дерево прокачки содержит открываемые скилы
// Новые навоки открываются за счет балов магии
public class SkillTree : MonoBehaviour
{
    [SerializeField]
    Image[] skillsImage;

    [SerializeField]
    Image[] treeElements;

    [SerializeField]
    Button[] litheButtons;
    bool[] litheButtonActive;

    [SerializeField]
    Button[] darckButtons;
    bool[] darckButtonActive;

    private void Awake()
    {
        litheButtonActive = new bool[5] { true, true, true, true, true };
        darckButtonActive = new bool[5] { true, true, true, true, true };
    }

    public void LiteButton1Klick()
    {
        if (litheButtonActive[0])
        {
            skillsImage[3].color = Color.white;
            skillsImage[4].color = Color.white;
            treeElements[0].color = Color.white;
            treeElements[1].color = Color.white;
            litheButtons[2].interactable = true;
            litheButtons[3].interactable = true;
            SkillProgress.instance.generalIndicator++;
            SkillProgress.instance.lightPoint--;
            SkillProgress.instance.UpdateUI();
            litheButtonActive[0] = false;
            litheButtons[0].interactable = false;
        }
    }

    public void LiteButton2Klick()
    {
        if (litheButtonActive[1])
        {
            skillsImage[4].color = Color.white;
            skillsImage[5].color = Color.white;
            treeElements[2].color = Color.white;
            treeElements[3].color = Color.white;
            litheButtons[3].interactable = true;
            litheButtons[4].interactable = true;
            SkillProgress.instance.generalIndicator++;
            SkillProgress.instance.lightPoint--;
            SkillProgress.instance.UpdateUI();
            litheButtonActive[1] = false;
            litheButtons[1].interactable = false;
        }
    }

    public void LiteButton3Klick()
    {
        if (litheButtonActive[2])
        {
            SkillProgress.instance.generalIndicator++;
            SkillProgress.instance.lightPoint--;
            SkillProgress.instance.UpdateUI();
            litheButtonActive[2] = false;
            litheButtons[2].interactable = false;
        }
    }

    public void LiteButton4Klick()
    {
        if (litheButtonActive[3])
        {
            SkillProgress.instance.generalIndicator++;
            SkillProgress.instance.lightPoint--;
            SkillProgress.instance.UpdateUI();
            litheButtonActive[3] = false;
            litheButtons[3].interactable = false;
        }
    }

    public void LiteButton5Klick()
    {
        if (litheButtonActive[4])
        {
            SkillProgress.instance.generalIndicator++;
            SkillProgress.instance.lightPoint--;
            SkillProgress.instance.UpdateUI();
            litheButtonActive[4] = false;
            litheButtons[4].interactable = false;
        }
    }

    public void DarckButton1Klick()
    {
        if (darckButtonActive[0])
        {
            skillsImage[4].color = Color.white;
            skillsImage[5].color = Color.white;
            treeElements[2].color = Color.white;
            treeElements[3].color = Color.white;
            darckButtons[2].interactable = true;
            darckButtons[3].interactable = true;
            SkillProgress.instance.generalIndicator--;
            SkillProgress.instance.darkPoint--;
            SkillProgress.instance.UpdateUI();
            darckButtonActive[0] = false;
            darckButtons[0].interactable = false;
        }
    }

    public void DarckButton2Klick()
    {
        if (darckButtonActive[1])
        {
            skillsImage[5].color = Color.white;
            skillsImage[6].color = Color.white;
            treeElements[4].color = Color.white;
            treeElements[5].color = Color.white;
            darckButtons[3].interactable = true;
            darckButtons[4].interactable = true;
            SkillProgress.instance.generalIndicator--;
            SkillProgress.instance.darkPoint--;
            SkillProgress.instance.UpdateUI();
            darckButtonActive[1] = false;
            darckButtons[1].interactable = false;
        }
    }

    public void DarckButton3Klick()
    {
        if (darckButtonActive[2])
        {
            SkillProgress.instance.generalIndicator--;
            SkillProgress.instance.darkPoint--;
            SkillProgress.instance.UpdateUI();
            darckButtonActive[2] = false;
            darckButtons[2].interactable = false;
        }
    }

    public void DarckButton4Klick()
    {
        if (darckButtonActive[3])
        {
            SkillProgress.instance.generalIndicator--;
            SkillProgress.instance.darkPoint--;
            SkillProgress.instance.UpdateUI();
            darckButtonActive[3] = false;
            darckButtons[3].interactable = false;
        }
    }

    public void DarckButton5Klick()
    {
        if (darckButtonActive[4])
        {
            SkillProgress.instance.generalIndicator--;
            SkillProgress.instance.darkPoint--;
            SkillProgress.instance.UpdateUI();
            darckButtonActive[4] = false;
            darckButtons[4].interactable = false;
        }
    }
}

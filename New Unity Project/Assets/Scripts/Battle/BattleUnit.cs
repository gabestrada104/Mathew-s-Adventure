using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUnit : MonoBehaviour
{
   
    [SerializeField] bool isPlayerUnit;

    public Characters Characters { get; set; }
    public void Setup(Characters characters)
    {
        Characters = characters;
        if (isPlayerUnit)
            GetComponent<Image>().sprite = Characters.Base.BackSprite;
        else
            GetComponent<Image>().sprite = Characters.Base.FrontSprite;
    }
}

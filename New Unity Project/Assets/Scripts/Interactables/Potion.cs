using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour, Interactable
{
    
    [SerializeField] Dialog dialog;
    [SerializeField] BattleUnit playerUnit;

    public void Interact()
    {
       



        StartCoroutine(HideShow());


        playerUnit.Characters.RestoreHp();



    }

    public IEnumerator HideShow()
    {
        StartCoroutine(DialogManager.Instance.ShowDialog(dialog));

        yield return new WaitForEndOfFrame();


        gameObject.SetActive(false);


    }



}

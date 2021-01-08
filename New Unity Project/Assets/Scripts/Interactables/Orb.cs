using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour, Interactable
{
    public static int orb1;
    [SerializeField] Dialog dialog;

    public void Interact()
    {
        orb1 = 1;



        StartCoroutine(HideShow());






    }

    public IEnumerator HideShow()
    {
        StartCoroutine(DialogManager.Instance.ShowDialog(dialog));

        yield return new WaitForEndOfFrame();


        gameObject.SetActive(false);


    }



}


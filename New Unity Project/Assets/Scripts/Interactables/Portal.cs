using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour, Interactable
{
    public int keyhold = Orb.orb1;
    [SerializeField] Dialog dialog;
    [SerializeField] Dialog dialog2;
    public void Interact()
    {
        if (Orb.orb1 == 1)
        {
            StartCoroutine(HideShow());


            Orb.orb1 = 0;

        }
        else
        {
            StartCoroutine(DialogManager.Instance.ShowDialog(dialog2));
        }
    }

    public IEnumerator HideShow()
    {
        StartCoroutine(DialogManager.Instance.ShowDialog(dialog));

        yield return new WaitForEndOfFrame();


        gameObject.SetActive(false);


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour,Interactable
{
    public int keyhold = Key.key1;
    [SerializeField] Dialog dialog;
    [SerializeField] Dialog dialog2;
    public void Interact()
    {
        if(Key.key1 == 1)
        {
            StartCoroutine(HideShow());
            

            Key.key1 = 0;
                
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

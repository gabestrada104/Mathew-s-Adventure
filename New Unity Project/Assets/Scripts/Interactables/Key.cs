using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour,Interactable
{
    public static int key1 ;
    [SerializeField] Dialog dialog;
    
    public void Interact()
    {
        key1 = 1;



        StartCoroutine(HideShow());
           

        

    }

   public IEnumerator HideShow()
    {
        StartCoroutine(DialogManager.Instance.ShowDialog(dialog));

        yield return new WaitForEndOfFrame();


        gameObject.SetActive(false);


    }
    


}

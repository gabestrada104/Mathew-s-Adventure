using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chest : MonoBehaviour, Interactable
{
    
    [SerializeField] Dialog dialog;
    [SerializeField] GameObject endGame;
    public float restartDelay;
    private int startCD = 5;
    private int start1 = 5;
    public void Interact()
    {
      

        StartCoroutine(HideShow());
       

    }

    public IEnumerator HideShow()
    {
        StartCoroutine(DialogManager.Instance.ShowDialog(dialog));
        yield return new WaitForSeconds(2f);

       
        StartCoroutine(StartTimer1());
       

    }

    IEnumerator StartTimerCD()
    {

        
        while (startCD > 0)
        {

           

            yield return new WaitForSeconds(1f);

            startCD--;

            if (startCD == 0)
            {

                SceneManager.LoadScene(0);


            }
        }
        

    }

    IEnumerator StartTimer1()
    {


        while (start1 > 0)
        {



            yield return new WaitForSeconds(1f);

            start1--;

            if (start1 == 0)
            {

                endGame.SetActive(true);
                StartCoroutine(StartTimerCD());

            }
        }


    }



}

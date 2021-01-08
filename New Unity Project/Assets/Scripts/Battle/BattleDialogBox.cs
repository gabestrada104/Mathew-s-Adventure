using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleDialogBox : MonoBehaviour
{
    [SerializeField] Text dialogText;
    [SerializeField] int letterPerSecond;
    [SerializeField] Color highlightedColor;
    [SerializeField] Font size;
    [SerializeField] GameObject answerSelector;
    [SerializeField] GameObject questionSelector;
    [SerializeField] GameObject battleTimer;
    [SerializeField] GameObject cdTimer;
    [SerializeField] List<Text> answerText; 
    [SerializeField] Text question; 

    
    public void SetDialog(string dialog)
    {
        dialogText.text = dialog;
    }

    public IEnumerator TypeDialog(string dialog)
    {
        dialogText.text = "";
        foreach(var letter in dialog.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / letterPerSecond);
        }
            
    }

    public void EnableTextDialog(bool enabled)
    {
        dialogText.enabled = enabled;
    }
    

    public void EnableBattletimer(bool enabled)
    {
        battleTimer.SetActive(enabled);
    }
    public void EnableCDtimer(bool enabled)
    {
        cdTimer.SetActive(enabled);
    }

    public void EnableAnswerSelector(bool enabled)
    {
        answerSelector.SetActive(enabled);
        questionSelector.SetActive(enabled);
    }

    

    public void UpdateAnswerSelection(int selectedAnswer)
    {
        for (int i=0; i<answerText.Count; ++i)
        {
             if(i == selectedAnswer)
            {

                answerText[i].fontSize = 40;
               
            }
            else
            {
                answerText[i].color = Color.black;
                answerText[i].fontSize = 25;
            }
        }
    }
   


}

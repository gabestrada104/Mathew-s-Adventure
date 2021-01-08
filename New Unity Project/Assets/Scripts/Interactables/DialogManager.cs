using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField] GameObject dialogbox;
    [SerializeField] Text dialogText;
    [SerializeField] int letterPerSecond;

    public event Action OnShowDialog;
    public event Action OnCloseDialog;
    public static DialogManager Instance { get;  private set; }

    int currentline = 0;
    Dialog dialog;
    bool isTyping;
    Action OnDialogFinished;

    public bool IsShowing { get; private set; }

    private void Awake()
    {
        Instance = this;

    }


    

    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !isTyping)
        {
            ++currentline; 
            if(currentline < dialog.Lines.Count)
            {
                StartCoroutine(TypeDialog(dialog.Lines[currentline]));
            }
            else
            {

                currentline = 0;
                IsShowing = false;
                dialogbox.SetActive(false);
                OnDialogFinished?.Invoke();
                OnCloseDialog?.Invoke();
            }
        }
    }

    public IEnumerator ShowDialog(Dialog dialog, Action OnFinished=null)
    {
        yield return new WaitForEndOfFrame();
        OnShowDialog?.Invoke();
        IsShowing = true;
        this.dialog = dialog;
        OnDialogFinished = OnFinished;
        dialogbox.SetActive(true);
        StartCoroutine(TypeDialog(dialog.Lines[0]));

        
    }

    public IEnumerator TypeDialog(string line)
    {
        isTyping = true;
        dialogText.text = "";
        foreach (var letter in line.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / letterPerSecond);
        }
        isTyping = false;

    }
}

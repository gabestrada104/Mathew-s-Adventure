using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] Dialog dialog;
    Player playerM;

    private void Awake()
    {
        playerM = GetComponent<Player>();
    }

    public IEnumerator TriggerBossBattle(PlayerMovement player)
    {
      
        var diff = player.transform.position - transform.position;
        var movec = diff - diff.normalized;
        movec = new Vector2(Mathf.Round(movec.x), Mathf.Round(movec.y));

       yield return playerM.Move(movec);

        //dialog

        StartCoroutine(DialogManager.Instance.ShowDialog(dialog, () =>
        {
            GameController.Instance.StartNPCBattle(this);

            gameObject.SetActive(false);
        }));

        
    }

    public IEnumerator HideShow()
    {
        StartCoroutine(DialogManager.Instance.ShowDialog(dialog));

        yield return new WaitForEndOfFrame();


        gameObject.SetActive(false);


    }
}

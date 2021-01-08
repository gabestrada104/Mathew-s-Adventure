using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour, Interactable
{
    [SerializeField] Dialog dialog;
    [SerializeField] List<Vector2> movementPattern;
    [SerializeField] float timeBetweenPattern;
    MonsterState state;
    Player player;
    float idleTimer;
    int currentPattern = 0;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    public void Interact()
    {
        if(state == MonsterState.idle)
        {
            StartCoroutine(DialogManager.Instance.ShowDialog(dialog));
        }
        
    }

    private void Update()
    {
        if (DialogManager.Instance.IsShowing) return;

        if(state == MonsterState.idle)
        {
            idleTimer += Time.deltaTime;
            if(idleTimer > timeBetweenPattern)
            {
                idleTimer = 0f;
                if (movementPattern.Count > 0)
                {
                    StartCoroutine(Walk());
                }
            }
        }
        player.HandleUpdate();
    }

    IEnumerator Walk()
    {
        state = MonsterState.Walking;

      yield return  player.Move(movementPattern[currentPattern]);
        currentPattern = (currentPattern + 1) % movementPattern.Count;    
        state = MonsterState.idle;
    }


}

public enum MonsterState { idle, Walking}

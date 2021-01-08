using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState { FreeRoam,Battle,Dialog,Cutscene}
public class GameController : MonoBehaviour
{

    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] BattleSystem battleSystem;
    [SerializeField] Camera worldCamera;

    GameState state;

    public static GameController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        playerMovement.OnEncountered += StartBattle;
        battleSystem.OnBattleOver += EndBattle;
        playerMovement.OnEnterBossView += (Collider2D bossCollider) =>
        {
           var boss = bossCollider.GetComponentInParent<BossController>();

            if (boss!=null)
            {
                state = GameState.Cutscene;
               StartCoroutine(boss.TriggerBossBattle(playerMovement));
            }
        };

        DialogManager.Instance.OnShowDialog += () =>
        {
            state = GameState.Dialog;
        };

        DialogManager.Instance.OnCloseDialog += () =>
        {
            if(state == GameState.Dialog)
            {
                state = GameState.FreeRoam;
            }
            
        };
    }

    void StartBattle()
    {
        state = GameState.Battle;
        battleSystem.gameObject.SetActive(true);
        worldCamera.gameObject.SetActive(false);

        var playerParty = playerMovement.GetComponent<Character2>();
        var wildEnemy = FindObjectOfType<MapArea>().GetComponent<MapArea>().GetRandomWildEnemy();

        battleSystem.StartBattle(playerParty,wildEnemy);
      
    }

    public void StartNPCBattle(BossController boss)
    {
        state = GameState.Battle;
        battleSystem.gameObject.SetActive(true);
        worldCamera.gameObject.SetActive(false);

        var playerParty = playerMovement.GetComponent<Character2>();
        var bossenemy = boss.GetComponent<Character2>();
        

        battleSystem.StartNPCBattle(playerParty, bossenemy);

    }

    void EndBattle(bool won)
    {
        state = GameState.FreeRoam;
        battleSystem.gameObject.SetActive(false);
        worldCamera.gameObject.SetActive(true);
    }
    private void Update()
    {
        if(state == GameState.FreeRoam)
        {
            playerMovement.HandleUpdate();
        }
        else if (state== GameState.Battle)
        {
            battleSystem.HandleUpdate();
        }
        else if (state == GameState.Dialog)
        {
            DialogManager.Instance.HandleUpdate();
        }
    }
}

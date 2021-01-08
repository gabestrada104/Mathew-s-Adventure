using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public enum BattleState {Start, PlayerMove, EnemyMove, Busy }
public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHUD playerHud;
    [SerializeField] BattleHUD enemyHud;
    [SerializeField] BattleDialogBox dialogBox;
    [SerializeField] Text battleTimerCD;
    [SerializeField] Text CD;
    [SerializeField] Text questions;
    [SerializeField] Text ans1;
    [SerializeField] Text ans2;
    [SerializeField] Text ans3;
    

    private int battleTimer = 60;
    private int startCD = 3;

    

    public event Action<bool> OnBattleOver;


    

    int answer_labas;
    int choice1_Labas;
    int choice2_Labas;
    int position_Selector;
  
    BattleState state;
    int currentAction;
    Character2 character2;
    Character2 npc;
    Characters wildEnemy;
    bool isNPCbattle = false;
    PlayerMovement player;
    BossController boss;

   
    public void StartBattle(Character2 character2, Characters wildEnemy)
    {

        this.character2 = character2;
        this.wildEnemy = wildEnemy;

       StartCoroutine(SetUpBattle());

    }

    public void StartNPCBattle(Character2 character2, Character2 npc)
    {

        this.character2 = character2;
        this.npc = npc;

        isNPCbattle = true;

        player = character2.GetComponent<PlayerMovement>();
        boss = npc.GetComponent<BossController>();
        StartCoroutine(SetUpBattle());

    }

    public IEnumerator SetUpBattle()
    {

        battleTimer = 70;
        startCD = 3;

        if (!isNPCbattle)
        { //random enemy
            playerUnit.Setup(character2.GetHealthyChar());
            enemyUnit.Setup(wildEnemy);
            playerHud.SetData(playerUnit.Characters);
            enemyHud.SetData(enemyUnit.Characters);




            yield return dialogBox.TypeDialog($"An enemy {enemyUnit.Characters.Base.Name} appeared Battle Starts In:");


            yield return new WaitForSeconds(1.8f);

            dialogBox.EnableTextDialog(false);

            StartCoroutine(StartTimerCD());
        }
        else
        {//npc enemy
            playerUnit.Setup(character2.GetHealthyChar());
            enemyUnit.Setup(npc.GetHealthyChar());
            playerHud.SetData(playerUnit.Characters);
            enemyHud.SetData(enemyUnit.Characters);



            yield return dialogBox.TypeDialog($"Battle Starts In:");

           

            yield return new WaitForSeconds(1.8f);

            dialogBox.EnableTextDialog(false);

            StartCoroutine(StartTimerCD());

        }

       



    }

    

    void PlayerMove()
    {
    
        int una = UnityEngine.Random.Range(1, 50);
        int dalawa = UnityEngine.Random.Range(1, 50);
        int answer = una + dalawa;

        //choices
        int choice1 = UnityEngine.Random.Range(1, 100);
        int choice2 = UnityEngine.Random.Range(1, 100);
        questions.text =($"{una}+{dalawa}=?");
       

        if (choice1 == answer)
        {
           choice1 = choice1 + UnityEngine.Random.Range(1, 6);
        }
        if(choice2 == answer)
        {
            choice2 = choice2 - UnityEngine.Random.Range(1, 6);
        }
        if(choice1 == choice2)
        {
            choice1 = choice1 + UnityEngine.Random.Range(1, 6);
        }
        //para sa UnityEngine.Random position-X (choices)
    
        int rand_choices;
        rand_choices = UnityEngine.Random.Range(1, 4);

        position_Selector = rand_choices;
        if (rand_choices == 1)
        {
            ans1.text =  answer.ToString();
            ans2.text = choice1.ToString();
            ans3.text = choice2.ToString();
            answer_labas = answer;
            choice1_Labas = choice1;
            choice2_Labas = choice2;

            
        }
        if (rand_choices == 2)
        {
            ans1.text =  choice2.ToString();
            ans2.text =  answer.ToString();
            ans3.text = choice1.ToString();
            answer_labas = answer;
            choice1_Labas = choice1;
            choice2_Labas = choice2;

        }
        if (rand_choices == 3)
        {
            ans1.text = choice2.ToString();
            ans2.text = choice1.ToString();
            ans3.text = answer.ToString();
            answer_labas = answer;
            choice1_Labas = choice1;
            choice2_Labas = choice2;
        }

        //Enables

        state = BattleState.PlayerMove;
        dialogBox.EnableCDtimer(false);
        dialogBox.EnableAnswerSelector(true);
        dialogBox.EnableBattletimer(enabled);

       
      
    }





    ////70CD
    IEnumerator BattleTimerCD()
    {
         
        while (battleTimer > 0)
        {
            
            if (battleTimer == 50 || battleTimer ==30 || battleTimer == 10) //saka lang aatke kalaban
        {

                StartCoroutine(PerformEnemyMoveTimeUP());
                battleTimerCD.text = battleTimer.ToString();
               

            }
           
            else
            {
                battleTimerCD.text = battleTimer.ToString();
            }


            yield return new WaitForSeconds(1f);

            battleTimer--;

        }


        battleTimerCD.text = "Times UP";

        StartCoroutine(TimeOver());
 

    }






    ////3CD
    IEnumerator StartTimerCD()
    {

        dialogBox.EnableCDtimer(true);
        while (startCD > 0)
        {
            
            CD.text = startCD.ToString();

            yield return new WaitForSeconds(1f);

            startCD--;

            if(startCD == 0)
            {
                StartCoroutine(BattleTimerCD());

             
             
            }
        }
        dialogBox.EnableCDtimer(false);
        

        PlayerMove();

    }

    //
    //Player Attack
    //
    IEnumerator PerformPlayerMove()
    {
        state = BattleState.Busy; //set ka ng value ni state
        int rand_number_compliment = UnityEngine.Random.Range(1,4);
        if(rand_number_compliment == 1)
        {
            yield return dialogBox.TypeDialog("Correct!!"); //show dialog box
        }
        if(rand_number_compliment == 2)
        {
            yield return dialogBox.TypeDialog("Awesome!!"); //show dialog box
        }
        if(rand_number_compliment == 3)
        {
             yield return dialogBox.TypeDialog("Nice"); //show dialog box
        }
       // yield return new WaitForSeconds(1f); // wait 1 sec

       bool isFainted = enemyUnit.Characters.TakeDamage(playerUnit.Characters); //babawasan nia ung HP
        yield return enemyHud.UpdateHP(); //animation ng hp
     if (isFainted)
        {
            yield return dialogBox.TypeDialog($"enemy {enemyUnit.Characters.Base.Name} defeated");
            yield return new WaitForSeconds(1f);
            OnBattleOver(true);
        }
        else
        {
            dialogBox.EnableTextDialog(false);
            PlayerMove();
        }
    
    }
    //Enemy Attack Manual
    IEnumerator PerformEnemyMove()
    {
        state = BattleState.Busy;


        yield return dialogBox.TypeDialog("Incorrect!!");


        bool isFainted = playerUnit.Characters.TakeDamage(enemyUnit.Characters);
        yield return playerHud.UpdateHP();
        if (isFainted)
        {
            FindObjectOfType<GameManager>().EndGame();
            yield return dialogBox.TypeDialog($"Game Over");
            yield return new WaitForSeconds(2f);
            

            OnBattleOver(false);


        }
        else
        {
            dialogBox.EnableTextDialog(false);
            PlayerMove();
        }



    }

    //
    //Enemy Attack Timer
    //
    IEnumerator PerformEnemyMoveTimeUP()
    {
      


        bool isFainted = playerUnit.Characters.TakeDamage(enemyUnit.Characters);
        yield return playerHud.UpdateHP();
        if (isFainted)
        {
            FindObjectOfType<GameManager>().EndGame();
            yield return dialogBox.TypeDialog($"Game Over");
            yield return new WaitForSeconds(2f);
            OnBattleOver(false);

        }



    }

    //
    //timeup
    //
    IEnumerator TimeOver()
    {
        state = BattleState.Busy;


        yield return dialogBox.TypeDialog("Incorrect!!");


        bool isFainted = playerUnit.Characters.TimeTakeDamage(enemyUnit.Characters);
        yield return playerHud.UpdateHP();
        if (isFainted)
        {
            FindObjectOfType<GameManager>().EndGame();
            yield return dialogBox.TypeDialog($"Game Over");
            yield return new WaitForSeconds(2f);


            OnBattleOver(false);


        }
        else
        {
            yield return dialogBox.TypeDialog($"TimeUp");
            yield return new WaitForSeconds(1f);
            dialogBox.EnableAnswerSelector(false);
            dialogBox.EnableBattletimer(false);
            OnBattleOver(true);
        }





    }

   

   


    public void HandleUpdate()
    {
        if (state == BattleState.PlayerMove)
        {

            HandleAnswerSelection();
        }


       



    }

    void HandleAnswerSelection()
    {
        if ((Input.GetKeyDown(KeyCode.D))|| (Input.GetKeyDown(KeyCode.RightArrow)))
        {
            if(currentAction < 2)
            {
                ++currentAction;
            }
        }
        else if ((Input.GetKeyDown(KeyCode.A)) || (Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            if(currentAction > 0)
            {
                --currentAction;
            }
        }
        dialogBox.UpdateAnswerSelection(currentAction);






        if (Input.GetKeyDown(KeyCode.Z))
        {
            // CORRECT ANSWER
            if ((currentAction == 0 && position_Selector == 1) ||(currentAction == 1 && position_Selector == 2) ||(currentAction == 2 && position_Selector == 3))
            {
                //answer correct

                Debug.Log("CCCCCCCCCCCCCCC");

                dialogBox.EnableAnswerSelector(false);
                dialogBox.EnableBattletimer(false);
                dialogBox.EnableTextDialog(true);
                StartCoroutine(PerformPlayerMove());
            }

            // WRONG ANSWER
            else
            {
                
                Debug.Log("CCCCCCCCCCCCCCC");
                dialogBox.EnableAnswerSelector(false);
                dialogBox.EnableBattletimer(false);
                dialogBox.EnableTextDialog(true);
                StartCoroutine(PerformEnemyMove());
            }
           
            
            
            

        }
       

    }
}

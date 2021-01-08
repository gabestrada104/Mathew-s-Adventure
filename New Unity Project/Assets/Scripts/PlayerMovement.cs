using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update



    public event Action<Collider2D> OnEnterBossView;
    public event Action OnEncountered;

    private Vector2 input;
   

    private Player player;

    void Awake()
    {

       
        player = GetComponent<Player>();
    }


    
  
    // Update is called once per frame
    public void HandleUpdate()
    {

        if (!player.IsMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input.x != 0) input.y = 0;
            if(input != Vector2.zero)
            {
              StartCoroutine(player.Move(input, OnMoveOver));
            }
        }
        player.HandleUpdate();
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Interact();
        }
    }
    

    void Interact()
    {
        var facingDirection = new Vector3(player.Animator.MoveX, player.Animator.MoveY);

        var interactPosition = transform.position + facingDirection;

        var collider = Physics2D.OverlapCircle(interactPosition, 0.3f, GameLayer.i.InteractableLayer);
        if(collider != null)
        {
            collider.GetComponent<Interactable>()?.Interact();
        }
    }
 

    

    private void CheckForEncounters()
    {
        if (Physics2D.OverlapCircle(transform.position, .2f, GameLayer.i.GrassLayer) !=null)
        {

            if (UnityEngine.Random.Range(1, 101) <= 6)
            {
               player.Animator.IsMoving = false;
                OnEncountered();
            }
        }
    }

    private void CheckIfFOV()
    {
        var collider = Physics2D.OverlapCircle(transform.position, .2f, GameLayer.i.FoVLayer);
        if ( collider!= null)
        {
            player.Animator.IsMoving = false;
            OnEnterBossView?.Invoke(collider);
        }

    }
    private void OnMoveOver()
    {
        CheckForEncounters();
        CheckIfFOV();
    }


}

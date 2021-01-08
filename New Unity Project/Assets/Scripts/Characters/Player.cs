using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    CharacterAnimator animator;
    public float moveSpeed;

    public bool IsMoving { get; private set; }
    private void Awake()
    {
        animator = GetComponent<CharacterAnimator>();
    }

   public IEnumerator Move(Vector2 moveVector, Action OnMoveOver=null)
    {
        animator.MoveX = Mathf.Clamp(moveVector.x, -1f,1f);
        animator.MoveY = Mathf.Clamp(moveVector.y, -1f, 1f);

        var targetposition = transform.position;
        targetposition.x += moveVector.x;
        targetposition.y += moveVector.y;

        if (!isPathclear(targetposition))
            yield break;


        IsMoving = true;
        while ((targetposition - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetposition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetposition;
        IsMoving = false;
        OnMoveOver?.Invoke();

    }

    public void HandleUpdate()
    {
        animator.IsMoving = IsMoving;
    }

    private bool isPathclear(Vector3 targetposition)
    {
        var diff = targetposition - transform.position;
        var dir = diff.normalized;
        if (Physics2D.BoxCast(transform.position + dir, new Vector2(0.2f, 0.2f), 0f, dir, diff.magnitude - 1, GameLayer.i.SolidLayer | GameLayer.i.InteractableLayer | GameLayer.i.PlayerLayer) == true)
            return false;
        return true;
    }

    private bool IsWalkable(Vector3 targetposition)
    {
        //impassable terrain
        if (Physics2D.OverlapCircle(targetposition, .2f, GameLayer.i.SolidLayer | GameLayer.i.InteractableLayer | GameLayer.i.PlayerLayer) != null)
        {
            return false;
        }
        return true;
    }

    public CharacterAnimator Animator
    {
        get => animator;
    }
}

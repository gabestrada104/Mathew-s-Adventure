using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLayer : MonoBehaviour
{
    [SerializeField] LayerMask solidObjectLayer;
    [SerializeField] LayerMask grassLayer;
    [SerializeField] LayerMask interactableLayer;
    [SerializeField] LayerMask fovLayer;
    [SerializeField] LayerMask playerLayer;

    public static GameLayer i { get; set; }

    public void Awake()
    {
        i = this;

    }
    public LayerMask SolidLayer
    {
        get => solidObjectLayer;
    }

    public LayerMask GrassLayer
    {
        get => grassLayer;
    }
    public LayerMask InteractableLayer
    {
        get => interactableLayer;
    } 
    
    public LayerMask FoVLayer
    {
        get => fovLayer;
    }

    public LayerMask PlayerLayer
    {
        get => playerLayer;
    }
}

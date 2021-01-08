using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapArea : MonoBehaviour
{
    [SerializeField] List<Characters> wildEnemy;

    public Characters GetRandomWildEnemy()
    {
        var wD = wildEnemy[Random.Range(0, wildEnemy.Count)];

        wD.Init();
        return wD;
    }
}

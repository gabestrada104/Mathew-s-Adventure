using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Character2 : MonoBehaviour
{
  [SerializeField]  List<Characters> characters;

    
    private void Start()
    {
      foreach (var character in characters)
        {
            character.Init();
        }
    }

    public Characters GetHealthyChar()
    {
       return characters.Where(x => x.HP > 0).FirstOrDefault();
    }
}

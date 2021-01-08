using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] 
public class Characters
{
    [SerializeField] CharacterBase _base;
   public CharacterBase Base {
        get { return _base; }
   }

    
  
    public int HP { get; set; }

   

    public void Init()
    {
    
       
        HP = MaxHP;


    }
    public int Attack
    {
        get { return Base.Attack; }
    }
    public int TimeAttack
    {
        get { return Base.Attack*2; }
    }

    public int MaxHP
    {
        get { return Base.MaxHP; }
    }

    public bool TakeDamage(Characters attacker)
    {
        int damage = attacker.Attack  ;
     
        
        HP -= damage;

        if(HP <= 0)
        {
            HP = 0;
            return true;
        }
        return false;
    }

    public bool TimeTakeDamage(Characters attacker)
    {
        int damage = attacker.TimeAttack;


        HP -= damage;

        if (HP <= 0)
        {
            HP = 0;
            return true;
        }
        return false;
    }

    public void RestoreHp()
    {
    

       

        if (HP < MaxHP)
        {
            int restore = HP + 20;

        }
    }
}

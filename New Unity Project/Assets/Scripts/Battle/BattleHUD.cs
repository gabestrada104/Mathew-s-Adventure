using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] Text hptext;
    [SerializeField] Text attacttext;
    [SerializeField] HealthBar hpbar;
  

    Characters _characters;
    public void SetData(Characters characters)
    {
        _characters = characters;

        nameText.text = characters.Base.Name;
        hpbar.SetHp((float)characters.HP / characters.MaxHP);

       

       
    }

    public IEnumerator UpdateHP()
    {
      yield return  hpbar.SetSmooth((float)_characters.HP / _characters.MaxHP);
    }
    
}

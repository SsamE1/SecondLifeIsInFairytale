using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Sobi
{  

    void Start()
    {   
        switch(this.gameObject.name)
        {
            case "사과(Clone)":
                this.gameObject.name="사과";

                break;
            case "떡(Clone)":
                this.gameObject.name="떡";
                break;
            case "약과(Clone)":
                this.gameObject.name="약과";
                break;   
        }

        //sobiData.sobitpye=SobiData.SobiType.Potion;
    }
    public void UsePotion()
    {
        if(sobiData.maxHp!=null)player.MAXHP+=sobiData.maxHp;
        player.HP+=sobiData.value;
        if(player.HP>player.MAXHP)player.HP=player.MAXHP;
        Destroy(this.gameObject);
        
    }
    
}

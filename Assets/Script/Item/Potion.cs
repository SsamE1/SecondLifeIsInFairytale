using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Sobi
{  

    void Start()
    {   
        switch(this.gameObject.name)
        {
            case "���(Clone)":
                this.gameObject.name="���";

                break;
            case "��(Clone)":
                this.gameObject.name="��";
                break;
            case "���(Clone)":
                this.gameObject.name="���";
                break;   
        }

        //sobiData.sobitpye=SobiData.SobiType.Potion;
    }
    public void UsePotion()
    {
        Debug.Log(player);
        if(sobiData.maxHp!=null)player.MAXHP+=sobiData.maxHp;
        player.HP+=sobiData.value;
        if(player.HP>player.MAXHP)player.HP=player.MAXHP;
        Destroy(this.gameObject);
        
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accessory : Item
{
   public AccessoryData accessoryData;
   
   public void Start()
   {
        if(this.gameObject.name== "¤��(Clone)") this.gameObject.name="¤��";
        else if(this.gameObject.name== "�з���(Clone)") this.gameObject.name="�з���";
        else if(this.gameObject.name== "��ȭ(Clone)") this.gameObject.name="��ȭ";
        else if(this.gameObject.name== "��ݰ�(Clone)") this.gameObject.name="��ݰ�";
   }
    public void EqipAcc(GameObject Accessory)
    {
        Accessory.transform.SetParent(inventoryManager.transform);
        player.MAXHP+=accessoryData.maxHp;
        player.moveSpeed+=accessoryData.moveSpeed;
        player.damage+=accessoryData.damage;
        player.attackSpeed+=accessoryData.attackSpeed;
        

    }
    public void RemoveAcc(GameObject Accessory)
    {
        player.MAXHP-=accessoryData.maxHp;
        //수정1. 악세변경 때 자꾸 풀피댐
        if(player.HP>player.MAXHP)player.HP=player.MAXHP;
        player.moveSpeed-=accessoryData.moveSpeed;
        player.damage-=accessoryData.damage;
        player.attackSpeed-=accessoryData.attackSpeed;
    }
}

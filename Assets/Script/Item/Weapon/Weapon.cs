using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{   
    public WeaponData weaponData;
    public bool isEquiped = false;
    protected void Start()
    {
        if(this.gameObject.name== "���Ѱ�(Clone)") this.gameObject.name="���Ѱ�";
        else if(this.gameObject.name== "���(Clone)") this.gameObject.name="���";
        else if(this.gameObject.name== "���ǰ�(Clone)") this.gameObject.name="���ǰ�";
        else if(this.gameObject.name== "�����ǰ�(Clone)") this.gameObject.name="�����ǰ�";
        else if(this.gameObject.name== "���ǰ�(Clone)") this.gameObject.name="���ǰ�";

        player = Player.instance;
    }
    protected override void Update()
    {
        if(!isEquiped)
        {
            base.Update();
        }
    }
    public void EqipWeapon(GameObject Weapon)
    {
        Weapon.transform.SetParent(player.transform);
        player.MAXHP+=weaponData.maxHp;
        player.moveSpeed+=weaponData.moveSpeed;
        player.damage+=weaponData.damage;
        player.attackSpeed+=weaponData.attackSpeed;
        isEquiped=true;
        player.weaponType = weaponData.weaponType;
        
    }
    public void RemoveWeapon(GameObject Weapon)
    {
        player.MAXHP-=weaponData.maxHp;
        player.HP=player.MAXHP;
        player.moveSpeed-=weaponData.moveSpeed;
        player.damage-=weaponData.damage;
        player.attackSpeed-=weaponData.attackSpeed;
        isEquiped=false;
    }
}

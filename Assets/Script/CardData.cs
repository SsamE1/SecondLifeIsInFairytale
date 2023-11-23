using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardData : MonoBehaviour
{
    public Sprite icon;
    public string title;
    public string text;
    public UseType useType;

    public void UseItem(UseType useType, string title, int amount)
    {
        //�÷��̾� ���ȿ� ������ �ַ��� �÷��̾� ������ �����ϴ� ������Ʈ �ʿ�.
        switch (useType)
        {
            case UseType.maxhp:
                Debug.Log(useType + " " + amount);
                break;
            case UseType.heal:
                Debug.Log(useType + " " + amount);
                break;
            case UseType.damage:
                Debug.Log(useType + " " + amount);
                break;
            case UseType.jump:
                Debug.Log(useType + " " + amount);
                break;
            case UseType.speed:
                Debug.Log(useType + " " + amount);
                break;
            case UseType.Sobi:
                Debug.Log(title);
                GameObject tmp1 = ItemManager.Instance.InstantiateItem("sobi", title);
                Player.instance.inventoryManager.AddReward(tmp1);
                break;
            case UseType.acc:
                Debug.Log(useType + " ");
                GameObject tmp = ItemManager.Instance.InstantiateItem("acc", title);
                Player.instance.inventoryManager.AddReward(tmp);
                break;
            case UseType.sword:
                Debug.Log(useType + " ");
                GameObject tmp2 = ItemManager.Instance.InstantiateItem("sword", title);
                Player.instance.inventoryManager.AddReward(tmp2);
                break;
        }
    }
}
public enum UseType
{
    maxhp,
    heal,
    damage,
    jump,
    speed,
    Sobi,
    apple,
    sword,
    acc,

}
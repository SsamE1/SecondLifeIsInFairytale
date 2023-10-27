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

    public void UseItem(UseType useType, int amount)
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
        }
    }
}
public enum UseType
{
    maxhp,
    heal,
    damage,
    jump,
    speed
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    //�浹���̾�� �÷��̾��� �Ⱥε������ؾߴ�
    //�÷��̾� �뽬�Ҷ� ������?
    // scriptable object�� ������ ������ ����?
    Rigidbody2D rigid;
    public int emptySlot;
    public GameObject ItemStatus;
    public bool isWatched;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        isWatched = false;
    }

    private void Update()
    {
        if (isWatched) //�÷��̾ �Ĵٺ��� status â on
        {
            ShowStatus();
            isWatched = false;
        }
        else
            CloseStatus();
    }

    public void ShowStatus()
    {
        Debug.Log(this.gameObject.name + "����â ");
        ItemStatus.SetActive(true);
    }
    public void CloseStatus()
    {
        ItemStatus.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            for (emptySlot = 0; emptySlot < GameManager.gameManager.player.inventory.Length; emptySlot++)
            {

                if (GameManager.gameManager.player.inventory[emptySlot] == null)  
                break;
            
            }

            switch (this.gameObject.name)
            {

                case "Apple":
                    GameManager.gameManager.player.inventory[emptySlot] = this.gameObject;
                    gameObject.SetActive(false);
                    break;            
                case "RiceCake":
                    GameManager.gameManager.player.inventory[emptySlot] = this.gameObject;
                    gameObject.SetActive(false);
                    break;
                case "Yakgwa":
                    GameManager.gameManager.player.inventory[emptySlot] = this.gameObject;
                    gameObject.SetActive(false);
                    break;
                case "Weapon":
                    GameManager.gameManager.player.inventory[emptySlot] = this.gameObject;
                    gameObject.SetActive(false);
                    break;
                case "StrawShoes":
                    GameManager.gameManager.player.inventory[emptySlot] = this.gameObject;
                    gameObject.SetActive(false);
                    break;


            }
        }

    }
}



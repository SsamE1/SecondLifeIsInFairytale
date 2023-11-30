using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Card : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler, IPointerClickHandler
{
    CardData[] carddata;
    public GameObject Icon;
    public GameObject title;
    public GameObject text;

    int randomIndex;
    int cost;

    public Transform cardScale;

    Vector3 defaultScale;

    public Animator animator;

    private void Start()
    {
        //���� 1~10 �� ����
        cost = Random.Range(1, 4);
        carddata = CardManager.instance.cardData;
        defaultScale = cardScale.localScale;
        this.SetIcon();
        Debug.Log(cost);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetBool("OnCursor", true);
        Debug.Log("Ŀ�� �����ٴ�");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetBool("OnCursor", false);
        Debug.Log("Ŀ�� ��");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Ŭ����");
        if (PlayerPrefs.GetInt("RollPaper") >= cost)
        {
            bool invenfull = true;
            for(int i = 0; i < 3; i++)
            {
                if (Player.instance.inventoryManager.inventory[i] == null)
                {
                    invenfull = false;
                }
            }
            if(invenfull) 
            {
                Debug.Log("�κ��丮�� ��á���ϴ�.");
            }
            else
            {
                carddata[0].UseItem(carddata[randomIndex].useType, title.GetComponent<Text>().text, cost);
                PlayerPrefs.SetInt("RollPaper", PlayerPrefs.GetInt("RollPaper") - cost);
                GameObject parentObject = this.gameObject.transform.parent.gameObject;
                parentObject.SetActive(false);
                Debug.Log(SceneManager.GetActiveScene().name);
                switch (SceneManager.GetActiveScene().name)
                {
                    case "1��":
                        SceneManager.LoadScene("1�忣��");
                        break;
                    case "2��":
                        SceneManager.LoadScene("2�忣��");
                        break;
                    case "3��":
                        SceneManager.LoadScene("3�忣��");
                        break;
                }
                
            }
        }
        else
        {
            Debug.Log("�η縶�Ⱑ �����մϴ�.");
        }
    }

    private void SetIcon()
    {
        if (carddata.Length > 0)
        {
            randomIndex = Random.Range(0, carddata.Length);
            Image imageComponent = Icon.GetComponent<Image>();
            imageComponent.sprite = carddata[randomIndex].icon;
            Text titleComponent = title.GetComponent<Text>();
            titleComponent.text = carddata[randomIndex].title;
            Text textComponent = text.GetComponent<Text>();
            textComponent.text = carddata[randomIndex].text;
            textComponent.text = textComponent.text + " " + cost;
        }
        else
        {
            Debug.Log("����� �̹����� �����ϴ�.");
        }
    }
}

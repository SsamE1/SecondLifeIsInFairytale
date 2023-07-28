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
    int amount;

    public Transform cardScale;

    Vector3 defaultScale;

    private void Start()
    {
        //���� 1~10 �� ����
        amount = Random.Range(1, 10);
        carddata = CardManager.instance.cardData;
        defaultScale = cardScale.localScale;
        this.SetIcon();
        Debug.Log(amount);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        cardScale.localScale = defaultScale * 1.1f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        cardScale.localScale = defaultScale * 1.0f;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        carddata[0].UseItem(carddata[randomIndex].useType, amount);
        GameObject parentObject = this.gameObject.transform.parent.gameObject;
        parentObject.SetActive(false);

        // ������ ���� �� �� �̵�
        SceneManager.LoadScene("MainGame");
        
    }

    private void SetIcon()
    {
        if (carddata.Length > 0)
        {
            randomIndex  = Random.Range(0, carddata.Length);
            Image imageComponent = Icon.GetComponent<Image>();
            imageComponent.sprite = carddata[randomIndex].icon;
            TMP_Text titleComponent = title.GetComponent<TMP_Text>();
            titleComponent.text = carddata[randomIndex].title;
            TMP_Text textComponent = text.GetComponent<TMP_Text>();
            textComponent.text = carddata[randomIndex].text;
            textComponent.text = textComponent.text + " " + amount;
}
        else
        {
            Debug.Log("����� �̹����� �����ϴ�.");
        }
    }
}

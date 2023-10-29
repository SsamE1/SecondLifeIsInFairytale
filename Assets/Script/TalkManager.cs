using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TalkManager : MonoBehaviour
{
    public TMP_Text talkText;
    public GameObject textBox;
    public bool moveAble;
    public Player player;
    public Image fade;
    public Text chapterText;

    private void Awake()
    {
    }
    private void Start()
    {
        // �� ���Խ� �ߴ� UI ����
        chapterText.text = SceneManager.GetActiveScene().name;
        fade.gameObject.SetActive(true);
        chapterText.gameObject.SetActive(true);

        StartCoroutine(FadeIn());                     //�ڷ�ƾ    //�ǳ� ���� ����
        talkText = textBox.GetComponentInChildren<TMP_Text>();
        textBox.gameObject.SetActive(false);
        moveAble = false;
        SceneManager.sceneLoaded += OnSceneLoaded;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) /*&& moveAble == true*/)
        {
            if (player.ScanObj().gameObject.CompareTag("1Page"))
            {
                Move1Page();
            }
            else if (player.ScanObj().gameObject.CompareTag("2Page"))
            {
                Move2Page();
            }
            else if (player.ScanObj().gameObject.CompareTag("Portal"))
            {
                MovePortal();
            }
        }

        /*if (player.ScanObj() != null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (player.ScanObj().gameObject.CompareTag("1Page"))
                {
                    Debug.Log("1Page ����");
                    GameManager.gameManager.Chapter1Setting();
                    Page1Action();
                }
                else if (player.ScanObj().gameObject.CompareTag("2Page"))
                {
                    Debug.Log("2Page ����");
                    GameManager.gameManager.Chapter2Setting();
                    Debug.Log("���ÿ��ä�");
                    Page2Action();
                }
                else if (player.ScanObj().gameObject.CompareTag("Portal"))
                {
                    Debug.Log("Portal ����");
                    PortalAction();
                }
                else
                {
                    Debug.Log("Object ����");
                }
            }
        }*/
    }
    /*public void Page1Action()
    {
        textBox.gameObject.SetActive(true);
        talkText.text = "1 Page portal.. " +
            "press F";
        moveAble = true;
    }
    public void Page2Action()
    {
        textBox.gameObject.SetActive(true);
        talkText.text = "2 Page portal.. " +
            "press F";
        moveAble = true;
    }
    public void PortalAction()
    {
        textBox.gameObject.SetActive(true);
        talkText.text = "This is Portal.. " +
            "press F";
        moveAble = true;
    }*/

    public void Move1Page()
    {
        moveAble = false;
        SceneManager.LoadScene("1Page");
    }
    public void Move2Page()
    {
        moveAble = false;
        SceneManager.LoadScene("2Page");
    }
    public void MovePortal()
    {
        int num = GameManager.gameManager.GetVerse();

        if (num == GameManager.maxVerse) //����
        {
            moveAble = false;
            string scenes = "2page-boss";
            Debug.Log(scenes);
            SceneManager.LoadScene(scenes);
        }
        else // �Ϲ�
        {
            moveAble = false;
            int randomNumber = Random.Range(1, 6);
            string scenes = "2page-" + randomNumber;
            Debug.Log(scenes);
            SceneManager.LoadScene(scenes);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("���ο� ���� �ε�Ǿ����ϴ�: " + scene.name);
    }
    IEnumerator FadeIn()
    {
        float fadeCount = 1;
        while (fadeCount > 0.0f)
        {
            fadeCount -= 0.01f;
            fade.color = new Color(0, 0, 0, fadeCount);
            chapterText.color = new Color(1, 1, 1, fadeCount);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
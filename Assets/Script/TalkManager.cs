using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TalkManager : MonoBehaviour
{
    public static TalkManager instance;
    public TMP_Text talkText;
    public GameObject textBox;
    public bool moveAble;
    public Player player;

    void Awake()
    { instance = this; }

    private void Start()
    {
        talkText = textBox.GetComponentInChildren<TMP_Text>();
        textBox.gameObject.SetActive(false);
        moveAble = false;
        SceneManager.sceneLoaded += OnSceneLoaded;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && moveAble == true)
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

        if (player.ScanObj() != null)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                if (player.ScanObj().gameObject.CompareTag("1Page")){
                    Debug.Log("1Page ����");
                    Page1Action();
                }
                else if (player.ScanObj().gameObject.CompareTag("2Page"))
                {
                    Debug.Log("2Page ����");
                    Page2Action();
                }
                else if (player.ScanObj().gameObject.CompareTag("Portal"))
                {
                    Debug.Log("Portal ����");
                    PortalAction();
                }
            }
        }
    }
    public void Page1Action()
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
    }

    public void Move1Page()
    {
        SceneManager.LoadScene("1Page");
        moveAble = false;
    }
    public void Move2Page()
    {
        SceneManager.LoadScene("2Page");
        moveAble = false;
    }
    public void MovePortal()
    {
        int num = GameManager.gameManager.GetChapter();
        if(num > GameManager.gameManager.maxChapter)
        {
            string chapter = "2page-boss";
            Debug.Log(chapter);
            SceneManager.LoadScene(chapter);
            GameManager.gameManager.currentChapter = 1;
            moveAble = false;
        }
        else
        {
            int randomNumber = Random.Range(1, 6);
            GameManager.gameManager.ChapterPlus();
            string chapter = "2page-" + randomNumber;
            Debug.Log(chapter);
            SceneManager.LoadScene(chapter);
            moveAble = false;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {    
        // �� �ε� �Ŀ� ȣ��� �Լ��� ���⿡ �ۼ�
        Debug.Log("���ο� ���� �ε�Ǿ����ϴ�: " + scene.name);
        //GameManager.gamemanager.Pozol_Spawn();
        //GameManager.gamemanager.ArrowPozol_Spawn();
    }
}
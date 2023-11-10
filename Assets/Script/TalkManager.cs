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
    public MapController mapController;

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
        mapController = GameObject.FindGameObjectWithTag("MapController").GetComponent<MapController>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) /*&& moveAble == true*/)
        {
            if (player.ScanObj().gameObject.CompareTag("Npc"))
            {
                Debug.Log("NPC�� ��ȭ");
                TalkWithNPC();
            }
            else if (player.ScanObj().gameObject.CompareTag("1Page"))
            {
                //1��Ż�϶�
                Move1Page();
            }
            else if (player.ScanObj().gameObject.CompareTag("2Page"))
            {
                //2��Ż�϶�
                Move2Page();
            }
            else if (player.ScanObj().gameObject.CompareTag("Portal"))
            {
                //�׳� ��Ż�϶�
                mapController.MapChange();
                StartCoroutine(FadeIn());
                //MovePortal();
            }
        }
    }

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

    public void TalkWithNPC()
    {
        Npc npc = player.ScanObj().gameObject.GetComponent<Npc>();

        if (npc.talk_count == npc.words.Count)
        {
            textBox.SetActive(false);
            npc.talk_count = 0;
        }
        else
        {
            textBox.SetActive(true);
            talkText.text = npc.words[npc.talk_count++];
        }
    }
}
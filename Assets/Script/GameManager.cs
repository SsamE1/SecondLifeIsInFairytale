using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public static TalkManager talkManager;
    //public static MonsterSpawner monsterSpawner;

    public Player player;
    public Enemy enemy;

    public int level;
    public int maxhp;
    public int hp;
    public int attack;

    public int maxChapter;
    public int currentChapter;

    private void Awake()
    {
        gameManager = this;
        maxChapter = 2;
        currentChapter = 1;
        talkManager = TalkManager.instance;
        //monsterSpawner = MonsterSpawner.instance;
    }

    public static GameManager gamemanager
    {
        get
        {
            if (gameManager == null)
            {
                gameManager = FindObjectOfType<GameManager>();

                // �� ��ȯ �ÿ��� �����ǵ��� �ϱ� ���� �ν��Ͻ��� �������� ������ ����
                if (gameManager == null)
                {
                    GameObject obj = new GameObject("GameManager");
                    gameManager = obj.AddComponent<GameManager>();
                }

                DontDestroyOnLoad(gameManager.gameObject);
            }

            return gameManager;
        }
    }

    public void ChapterPlus()
    {
        currentChapter++;
    }
    public int GetChapter()
    {
        return currentChapter;
    }

    /*public void Pozol_Spawn()
    {
        monsterSpawner.PozolSpawn();
    }
    public void ArrowPozol_Spawn()
    {
        monsterSpawner.Arrow_PozolSpawn();
    }*/
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    public bool isDie;
    GameObject rewardManager;

    private void Start()
    {
        isDie = false;

        GameObject canvas = GameObject.Find("Canvas");
        rewardManager = canvas.transform.Find("RewardManager").gameObject;


        if (rewardManager != null)
        {
            // Ȱ�� ���¸� �����մϴ�.
            rewardManager.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Reward Manager�� ã�� �� �����ϴ�.");
        }
    }

    private void Update()
    {
        if (isDie)
        {
            StartCoroutine(this.Die());
        }
    }
    IEnumerator Die()
    {
        yield return new WaitForSeconds(1f);

        //�״� �ִϸ��̼� ����

        rewardManager.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }

}

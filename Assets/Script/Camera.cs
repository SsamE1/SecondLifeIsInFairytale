using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Camera : MonoBehaviour
{
    public Transform playerTransform; // ī�޶� ����ٴ� �÷��̾��� Transform ������Ʈ
    Vector3 initialPos;
    public float shakeTime, shakeAmount;
    private void Start()
    {
        initialPos = transform.position;
        SceneManager.sceneLoaded += OnSceneLoaded; // sceneLoaded �̺�Ʈ�� OnSceneLoaded �޼ҵ带 ����
        FindPlayerObject(); // ���� �ε�� ������ Player ������Ʈ�� ã�Ƽ� ����
    }

    void Update()
    {
        if (playerTransform != null)
        {
            Vector3 playerVector = playerTransform.position;
            playerVector.z = transform.position.z; // ī�޶��� Z�� ��ġ�� �����Ͽ� 2D ȭ���� ����

            transform.position = playerVector;
        }

        if (GameManager.gameManager.player.ani.GetBool("Hit"))
            StartCoroutine("ShakeCamera");
    }

    IEnumerator ShakeCamera()
    {

        transform.position = initialPos + Random.insideUnitSphere * shakeAmount;
        yield return null;

        if (!GameManager.gameManager.player.ani.GetBool("Hit"))
            transform.position = initialPos;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindPlayerObject(); // ���� �ε�� ������ Player ������Ʈ�� ã�Ƽ� ����
    }

    private void FindPlayerObject()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // �±׸� �̿��� Player ������Ʈ�� ã��

        if (player != null)
        {
            playerTransform = player.transform; // Player ������Ʈ�� Transform�� ������ ����
        }
        else
        {
            Debug.LogWarning("Player ������Ʈ�� ã�� �� �����ϴ�. Player ������Ʈ�� 'Player' �±׸� �߰��ϼ���.");
        }
    }
}

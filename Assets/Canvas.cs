using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas : MonoBehaviour
{
    private static Canvas instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // ������Ʈ�� �� ��ȯ �ÿ��� �ı����� �ʵ��� ����
        }
        else
        {
            // �̹� �ν��Ͻ��� �����ϸ� �ߺ� ������ ���̹Ƿ� �� ������Ʈ�� �ı�
            Destroy(gameObject);
        }
    }
}

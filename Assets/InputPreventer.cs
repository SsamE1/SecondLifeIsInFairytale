using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPreventer : MonoBehaviour
{
    void Update()
    {
        // ��� �Է��� �����Ϸ��� �Ʒ��� ���� ����մϴ�.
        Input.ResetInputAxes();
    }
}

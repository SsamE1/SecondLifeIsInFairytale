using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    Animator ani;
    //�÷��̾����ϸ� �÷��̾�ִ°� �ٽ�ߵ�..?
    private void Awake()
    {
        ani = GetComponent<Animator>(); 
    }
    void SetAttackF()
    {
      ani.SetBool("Attack", false);
    }

 /*   ������ ���̵� ���� ������,
       �ϸ�ȵɶ� setbool�� attack���ϰ��ϸ� ����������? -> �ִϸ��̼� ����ʾȵ�
        canAttack�� �־�?
    �ƴϱ׳� �������Ҷ� setActive���ϸ� �ɰŰ����� ������ ��������� �����ص� ����������?*/
}

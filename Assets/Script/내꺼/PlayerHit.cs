using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    public Player player;
    private void Update()
    {
        TestDead();
        Dead();
     
    }

    void Dead()
    {
        if (player.canDead && player.HP <= 0)
        {
            if (player.haveAmulet == 0)
            {
                Debug.Log("�׾�");
                player.canDead = false;
                StartCoroutine(testDead());
                player.ani.SetTrigger("Dead");
            }
            else
            {
                Debug.Log("���̽�����");
                player.canDead = false;
                StartCoroutine(testDead());
                player.inventoryManager.UseAmulet(player.haveAmulet - 1);
            }
        }

    }

    public IEnumerator testDead()
    {
        yield return new WaitForSeconds(1.0f);
        player.canDead = true;
    }
    public void TestDead()
    {
        if (player.canDead && Input.GetKey(KeyCode.L))
        {
            player.HP = 0;
        }
    }




    //

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy") && player.canTakeDamage)      
            Hit(collision);
    }

    void Hit(Collision2D collision)
    {

        player.HP -= collision.gameObject.GetComponent<Enemy>().Attack;

        Debug.Log(" Player HP :" + player.HP);
        player.ani.SetBool("Hit", true);
        StartCoroutine(KnockBack(collision.gameObject));
    }


    IEnumerator KnockBack(GameObject enemy)
    {
        yield return null;
        player.sr.material.color = new Color(230 / 255f, 110 / 255f, 110 / 255f, 150 / 255f);

        //Vector3 enemyPos = GameManager.gameManager.enemy.transform.position; �̷����ϴϱ� enemy�ϳ��� �˹�ż� �ȵ� �׳� CollsiionEnter���� position�������� �Ǵ°ſ��µ�..
        Vector3 enemyPos = enemy.transform.position;
        Vector3 Vec = transform.position - enemyPos;

        player.rigid.AddForce(Vec.normalized * 10, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f); // �ǰ� 0.5��

        player.sr.material.color = Color.white;
        player.ani.SetBool("Hit", false);

    }
}

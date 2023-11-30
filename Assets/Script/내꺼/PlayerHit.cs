using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerHit : MonoBehaviour
{
    public Player player;
    public bool isDead = false;
    public Image fade;
    public Text text;

    public GameObject btn;
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
                player.ani.SetBool("Dead", true);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if (collision.gameObject.tag.Equals("Enemy") && player.canTakeDamage)
        // Hit(collision.gameObject.GetComponent<Enemy>);

        if (isDead && collision.gameObject.tag.Equals("Ground"))
            player.ani.SetBool("Dead", true);
    }

    public void Hit(int damage, GameObject Enemy)
    {
        if (player.canTakeDamage)
        {
            player.ani.SetBool("Hit", true);
            player.HP -= damage;
            StartCoroutine(KnockBack(Enemy));
        }
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

    public void Gameover()
    {
        player.ani.speed = 0;
        StartCoroutine(FadeIn());
    }
    IEnumerator FadeIn()
    {
        fade.gameObject.SetActive(true);
        //chapterText.gameObject.SetActive(true);
        float fadeCount = 0.0f;
        while (fadeCount < 1.0f)
        {
            fadeCount += 0.01f;
            fade.color = new Color(0, 0, 0, fadeCount);
            text.color = new Color(0.8396226f, 0.02082168f, 0f, fadeCount);
            yield return new WaitForSeconds(0.01f);
        }
        btn.SetActive(true);
    }

    public void MenuBtn()
    {
        player.HP = player.MAXHP;
        player.ani.speed = 1;
        player.ani.SetBool("Dead", false);
        player.ani.Play("Idle");
        btn.SetActive(false);
        fade.gameObject.SetActive(false);
        switch (PlayerPrefs.GetInt("CurrentChapter"))
        {
            case 1:
                SceneManager.LoadScene("1�����");
                break;
            case 2:
                SceneManager.LoadScene("2�����");
                break;
            case 3:
                SceneManager.LoadScene("3�����");
                break;
        }
    }
}

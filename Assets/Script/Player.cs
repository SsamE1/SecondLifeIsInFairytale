using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

using static UnityEngine.ParticleSystem;

public class Player : MonoBehaviour
{
    private static Player instance;
    Vector2 input;
    public int HP = 100;
    public int MAXHP = 100;
    public GameObject[] inventory = new GameObject[3]; // �̻��� �� �ǹ����� ������ �ȵ�????????????????????���ذ��ȵǳ���¥.
    public GameObject[] accessory = new GameObject[2];

    public int moveSpeed = 5;
    public float jumpPower = 5;
    public float rollSpeed = 1.5f;

    bool canDoubleJump = false;

    // GameObject hand;

    public int combo = 1;
    public bool isAttack = false;
    float comboTimer;
    SpriteRenderer sr;

    AudioSource attackAudio;
    public Animator ani;

    Rigidbody2D rigid;
    CapsuleCollider2D playerCollider;


    public Vector2 boxSize;
    public Transform pos;

    public Particle Particle;

    public float potionCoolTime = 2f;
    public bool canHpPotionDrink = true;

    public GameObject weapon;

    public Vector3 dirvec;
    public GameObject scanObj;
    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // sceneLoaded �̺�Ʈ�� OnSceneLoaded �޼ҵ带 ����
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        gameObject.transform.position = Vector3.zero; // ���� �ε�� ������ Player ��ġ (0,0,0)���� �ʱ�ȭ.
    }

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        sr = GetComponent<SpriteRenderer>();

        attackAudio = GetComponent<AudioSource>();

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
    void Update()
    {
        RaycastHit2D rayhit;

        if (transform.localScale.x > 0)
        {
            Debug.DrawRay(transform.position + new Vector3(1, 0, 0), Vector3.forward*20f, Color.green);
            rayhit = Physics2D.Raycast(transform.position + new Vector3(1, 0, 0), Vector3.forward*20f, LayerMask.GetMask("Object"));
        }
        else
        {
            Debug.DrawRay(transform.position + new Vector3(-1, 0, 0), Vector3.back*20f, Color.green);
            rayhit = Physics2D.Raycast(transform.position + new Vector3(-1, 0, 0), Vector3.back*20f, LayerMask.GetMask("Object"));
        }
      
        if (rayhit.collider != null)
        {
            scanObj = rayhit.collider.gameObject;
            if (scanObj.layer == LayerMask.NameToLayer("Item")){
                Item item = scanObj.GetComponent<Item>();
                item.isWatched = true; //������ �Ĵٺ�����
            }
        }
        else
            scanObj = null;

        Jump();
        ComboAttack();
        Roll();
        Dead();
        UseItem();
    }
    void FixedUpdate()
    {
        Move();
    }

    void LateUpdate()
    {
        ani.SetFloat("speed", input.magnitude);
    }

    void UseItem()
    {
        UsePotion(KeyCode.Alpha1, 0);
        UsePotion(KeyCode.Alpha2, 1);
        UsePotion(KeyCode.Alpha3, 2);
    }

    void UsePotion(KeyCode key, int slotNumber) // ���� ref����� callbyreference?
    {
        if (!Input.GetKey(KeyCode.F) && Input.GetKeyDown(key))
        {
            if (canHpPotionDrink)
            {

                if (inventory[slotNumber] == null)
                    return;


                GameObject what = inventory[slotNumber].gameObject;

                if (HP == MAXHP && !what.name.Equals("Yakgwa"))
                {
                    Debug.Log("Ǯ�ǿ���");
                    return;
                }

                switch (what.name)
                {

                    case "Apple":
                        HP += 5;
                        break;
                    case "RiceCake":
                        HP += 15;
                        break;
                    case "Yakgwa":
                        {
                            MAXHP += 20;
                            HP += 10;
                            moveSpeed += 10;
                        }
                        break;
                }


                if (HP > MAXHP)
                    HP = MAXHP;

                Debug.Log("Player HP : " + HP);
                canHpPotionDrink = false;

                inventory[slotNumber] = null;
                Destroy(what);
                StartCoroutine(PotionDelay(value => canHpPotionDrink = value));

            }
            else
                Debug.Log("��Ÿ���̿��뤻");
        }
    }

    IEnumerator PotionDelay(Action<bool> setBool) // �Ű������� �̷��� �� ������ , ���� ���� ������ ������ �ΰ����µ� ��� ���������� �Ű������� �޾Ƽ� ��������������, �ٵ� ���ǵ����Ǿ��ּ� ������������ �ƹ�ư
    {
        yield return new WaitForSeconds(potionCoolTime);
        setBool(true);
    }

    void changeWeapon(int slotNumber)
    {

        GameObject what = inventory[slotNumber].gameObject;
        inventory[slotNumber] = null;


        what.transform.position = weapon.transform.position;
        what.transform.rotation = weapon.transform.rotation;
        what.transform.localScale = weapon.transform.localScale;

        weapon.transform.SetParent(null);
        //  what.transform.SetParent(hand.transform);

        what.gameObject.SetActive(true);
        Destroy(weapon);
    }


    void EquipAccessory(int slotNumber)
    {

        GameObject acc = inventory[slotNumber].gameObject;
        inventory[slotNumber] = null;

        int accSlotNum = 0;

        for (int i = 0; i < accessory.Length; i++)
        {
            if (accessory[i] == null) // �� ���� ã��
            {
                accSlotNum = i;

                if (acc.name.Equals("StrawShoes"))
                {
                    moveSpeed += 10;
                }
                else if (acc.name.Equals("Yeomju"))
                {
                    MAXHP += 10;
                    //��Ʈ����,����
                }
                break;
            }
            else // ��������� ù��°ĭ ����
            {
                accSlotNum = 0;

                if (accessory[accSlotNum].name.Equals("StrawShoes")) // ù ĭ �Ǽ� �ɷ�ġ����, �����Ұ� �����ֱ�
                {
                    moveSpeed -= 10;
                }
                else if (accessory[accSlotNum].name.Equals("Yeomju"))
                {
                    MAXHP -= 10;
                }


                if (acc.name.Equals("StrawShoes"))
                {
                    moveSpeed += 10;
                }
                else if (acc.name.Equals("Yeomju"))
                {
                    MAXHP += 10;
                    //��Ʈ����,����
                }


            }
        }

        accessory[accSlotNum] = acc; // ������ �ߴµ� . .. �ɷ�ġ�� ��� ������ // �����Ӵ����� accessory�迭 Ȯ���ϴ°� �

    }

    //������Ʈ ��ĵ
    public GameObject ScanObj()
    {
        return scanObj;
    }

    void Move()
    {


        if (!ani.GetBool("Hit"))
        {
            float x = Input.GetAxis("Horizontal");
            input = new Vector2(x, 0) * moveSpeed * Time.deltaTime;
            //   rigid.MovePosition(rigid.position + input); // move Vs moveposition 
            //transform.position = transform.position + new Vector3(x,0) * speed * Time.deltaTime;  ���� ���۰����̴¾��µ� rigid>>>>>transform

            rigid.position += input;

            //if(!isAttack)
            if (x < 0)
                transform.localScale = new Vector3(-2.5f, 2.5f, 0);
            else if (x > 0)
                transform.localScale = new Vector3(2.5f, 2.5f, 0);

        }
    }
    void Jump()
    {

        if (Input.GetButtonDown("Jump") && !ani.GetBool("isJump"))
        {
            ani.SetBool("isJump", true);
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

            canDoubleJump = true;

        }
        else if (Input.GetButtonDown("Jump") && canDoubleJump == true)
        {
            ani.SetTrigger("DoubleJump");
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            canDoubleJump = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {



        if (collision.gameObject.tag.Equals("ground"))
            ani.SetBool("isJump", false);

        //�̰� foot�� �ݶ��̴������� �÷��̾��ݶ��̴��� ������ �ǰ� ����
        if (collision.gameObject.tag.Equals("Enemy"))
        {

            HP -= collision.gameObject.GetComponent<Enemy>().Attack;

            Debug.Log(" Player HP :" + HP);
            ani.SetBool("Hit", true);
            StartCoroutine(KnockBack(collision.gameObject));
        }


    }

    void Attack() // ���ݰ��� �޼ҵ常 ���� 4���� �������ʿ䰡 �����Ű�����...
    {
        attackAudio.Play();
        Collider2D[] enemy = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
        foreach (Collider2D collider in enemy)
        {
            if (collider.tag == "Enemy")
                collider.GetComponent<Enemy>().TakeDamage(10);//������ ������                     
        }
    }

    void ComboAttack()
    {
        IsAttack();

        comboTimer += Time.deltaTime;
        if (comboTimer > 1f)
        {
            combo = 1;
            comboTimer = 0;
        }

        if (Input.GetMouseButtonDown(0))
        {


            if (combo == 1)
            {
                ani.SetTrigger("Attack1"); Attack();
                combo++;
            }
            else if (combo == 2 || (ani.GetCurrentAnimatorStateInfo(0).IsName("Attack1") && ani.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f))
            {

                ani.SetTrigger("Attack2"); Attack();
                combo = 1;

                comboTimer = 0;
                StartCoroutine("AttackDelay");
            }
        }
    }

    void IsAttack()
    {
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("Attack1") || ani.GetNextAnimatorStateInfo(0).IsName("Attack1") ||
            ani.GetCurrentAnimatorStateInfo(0).IsName("Attack2") || ani.GetNextAnimatorStateInfo(0).IsName("Attack2") ||
            ani.GetCurrentAnimatorStateInfo(0).IsName("Attack3") || ani.GetNextAnimatorStateInfo(0).IsName("Attack3"))
            isAttack = true;
        else
            isAttack = false;
    }
    IEnumerator AttackDelay() // Attack�� ���ۿ� �ڷ�ƾ���� ������ٸ�...
    {
        yield return new WaitForSeconds(0.5f);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }
    void Roll()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && (!isAttack && !ani.GetBool("Hit") && !ani.GetBool("isJump")))
            StartCoroutine(roll());

    }


    IEnumerator roll() // �ڷ�ƾ���� ���� , �ִϸ��̼��̺�Ʈ�ִ°� ������ , �ڷ�ƾvs�ִϸ��̼��̺�Ʈ?
    {

        float lookingDir = transform.localScale.x;

        UnableCollider();

        ani.SetTrigger("Roll");
        rigid.velocity = new Vector2(lookingDir, 0) * rollSpeed;

        yield return new WaitForSeconds(0.5f);

        EnableCollider();

        rigid.velocity = Vector2.zero;
        rigid.gravityScale = 1.25f;


    }
    IEnumerator KnockBack(GameObject enemy)
    {
        yield return null;
        sr.material.color = new Color(230 / 255f, 110 / 255f, 110 / 255f, 150 / 255f);

        //Vector3 enemyPos = GameManager.gameManager.enemy.transform.position; �̷����ϴϱ� enemy�ϳ��� �˹�ż� �ȵ� �׳� CollsiionEnter���� position�������� �Ǵ°ſ��µ�..
        Vector3 enemyPos = enemy.transform.position;
        Vector3 Vec = transform.position - enemyPos;

        rigid.AddForce(Vec.normalized * 10, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f); // �ǰ� 0.5��

        sr.material.color = Color.white;
        ani.SetBool("Hit", false);

    }

    void Dead()
    {
        if (HP <= 0)
        {
            ani.SetTrigger("Dead");
        }
    }

    void EnableCollider()
    {
        playerCollider.enabled = true;
    }

    void UnableCollider()
    {
        playerCollider.enabled = false;
    }


    void SetFalseisAttack()
    {
        isAttack = false;
    }

    void ParticlePlay()
    {
        Particle.ParticlePlay();
    }

    void ParticleStop()
    {
        Particle.ParticleStop();

    }
}




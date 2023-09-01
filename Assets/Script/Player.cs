using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    private static Player instance;
    Vector2 input;
    int HP = 100;
    public int speed=5;
    public float jumpPower=5;
    public float rollSpeed=1.5f;

    bool canDoubleJump = false;
    
    float curTIme;
    float coolTime = 0.5f;
    GameObject weapon;
   

    AudioSource weaponAudio;
    Animator ani,weaponAni;

    Rigidbody2D rigid;
    CapsuleCollider2D playerCollider;
  

    public Vector2 boxSize;
    public Transform pos;

    public Vector3 dirvec;
    public GameObject scanObj;

    public GameObject[] inventory = new GameObject[5];
    public int MAXHP = 100; public float potionCoolTime = 2f;
    public bool canHpPotionDrink = true;
    public GameObject[] accessory = new GameObject[2];
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
        playerCollider=GetComponent<CapsuleCollider2D>();  
       
        weapon = GameObject.Find("Hand(Weapon)");
        weaponAni = weapon.GetComponent<Animator>();
        weaponAudio = weapon.GetComponent<AudioSource>();

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
        Attack();
        Roll();
        Dead();
    }
    void FixedUpdate()
    {
        Move();
    }

    void UseItem()
    {
        CheckItem(KeyCode.Alpha1, 0);
        CheckItem(KeyCode.Alpha2, 1);
        CheckItem(KeyCode.Alpha3, 2);
        CheckItem(KeyCode.Alpha4, 3);
        CheckItem(KeyCode.Alpha5, 4);
    }

    void CheckItem(KeyCode key, int slotNumber)
    {
        if (Input.GetKeyDown(key))
        {
            if (inventory[slotNumber] == null)
                return;

            if (inventory[slotNumber].gameObject.CompareTag("Potion"))
                UsePotion(slotNumber);
            else if (inventory[slotNumber].gameObject.CompareTag("Weapon"))
                changeWeapon(slotNumber);
            else if (inventory[slotNumber].gameObject.CompareTag("Accessory"))
                EquipAccessory(slotNumber);

        }
    }

    void changeWeapon(int slotNumber)
    {

    }

    void UsePotion(int slotNumber) // ���� ref����� callbyreference?
    {
        if (canHpPotionDrink)
        {
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
                        speed += 10;
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

    void EquipAccessory(int slotNumber) // �ڵ�������
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
                    speed += 10;
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
                    speed -= 10;
                }
                else if (accessory[accSlotNum].name.Equals("Yeomju"))
                {
                    MAXHP -= 10;
                }


                if (acc.name.Equals("StrawShoes"))
                {
                    speed += 10;
                }
                else if (acc.name.Equals("Yeomju"))
                {
                    MAXHP += 10;
                    //��Ʈ����,����
                }


            }
        }

        accessory[accSlotNum] = acc;

    }
    IEnumerator PotionDelay(Action<bool> setBool)
    {
        yield return new WaitForSeconds(potionCoolTime);
        setBool(true);
    }

    //������Ʈ ��ĵ
    public GameObject ScanObj()
    {
        return scanObj;
    }

    void LateUpdate()
    {
        ani.SetFloat("speed", input.magnitude);
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        input = new Vector2(x, 0) * speed * Time.deltaTime;
        //   rigid.MovePosition(rigid.position + input); // move Vs moveposition 
        //transform.position = transform.position + new Vector3(x,0) * speed * Time.deltaTime;  ���� ���۰����̴¾��µ� rigid>>>>>transform


        rigid.position = rigid.position + input;

       if (!weaponAni.GetBool("Attack"))
        {
            if (x < 0)
                transform.localScale = new Vector3(-3.5f, 3.5f, 0);
            else if (x > 0)
                transform.localScale = new Vector3(3.5f, 3.5f, 0);
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
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            canDoubleJump = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "ground")
            ani.SetBool("isJump", false);

        //�̰� foot�� �ݶ��̴������� �÷��̾��ݶ��̴��� ������ �ǰ� ����
        if (collision.gameObject.tag == "Enemy")
        {

            HP -= collision.gameObject.GetComponent<Enemy>().Attack;

            Debug.Log(" Player HP :" + HP);
            ani.SetTrigger("Hit");
            StartCoroutine(KnockBack());
        }
    }

    void Attack()
    {
        if (curTIme <= 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl) && weapon.activeSelf)
            {
               
                weaponAni.SetBool("Attack",true);
                weaponAudio.Play();
                curTIme = coolTime;

                Collider2D[] enemy = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);

                foreach (Collider2D collider in enemy)
                {
                    if (collider.tag == "Monster")
                    {
                        //���� ������ �ִ� �ڵ� �ʿ�
                        Destroy(collider.gameObject); //�ӽ÷� ����
                    }
                    else if (collider.tag == "Boss") //���� ������
                    {
                        Boss boss = collider.GetComponent<Boss>();
                        boss.isDie = true;
                        //Destroy(collider.gameObject); //�ӽ÷� ����
                    }
                }
              
            }
        }
        else 
            curTIme -= Time.deltaTime;

       // �������ִ� �Լ��� ���θ���� �ִϸ��̼� ������ �̺�Ʈ�� ������°���
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }


    void Roll()
    {
        if(Input.GetKeyDown(KeyCode.Q) && !weaponAni.GetBool("Attack")) 
        {
            float lookingDir = transform.localScale.x;
            ani.SetTrigger("Roll");

            rigid.velocity = Vector2.zero;
            rigid.velocity = new Vector2(lookingDir, 0) * rollSpeed;
        }
    }




   IEnumerator KnockBack()
    {
        yield return null;
        Vector3 enemyPos = GameManager.gameManager.enemy.transform.position;
        Vector3 Vec =transform.position - enemyPos;
        rigid.AddForce(Vec.normalized * 6, ForceMode2D.Impulse);

    }


    void Dead()
    {
        if(HP<=0)
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

    void SetActiveF()
    {
        gameObject.SetActive(false);
    }

    void SetWActiveF()
    {
        weapon.SetActive(false);
    }

    void SetWActiveT()
    {
        weapon.SetActive(true);
    }

}




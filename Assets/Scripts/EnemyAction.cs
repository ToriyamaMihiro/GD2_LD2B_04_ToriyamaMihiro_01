using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyAction : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerController inputAcution;

    public GameObject SporePrefab;


    Vector2 inputDirection;

    public int enemyCount = 0;//洗脳した敵の数を数える

    public float hp = 3f;
    float moveSpeed = 1f;
    bool brainWash = false;//洗脳しているか
    bool increseGage = false;
    bool death = false;//hpが0以下になった瞬間か


    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        //コントローラーを使うためのもの
        inputAcution = new PlayerController();
        inputAcution.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        BrainWash();

    }

    void Move()
    {
        if (brainWash)
        {
            PlayerAction playerAction;
            GameObject obj = GameObject.Find("Player");
            playerAction = obj.GetComponent<PlayerAction>();
            //洗脳時プレイヤーと同じ動きをする
            inputDirection = playerAction.inputDirection;
            moveSpeed = playerAction.moveSpeed;
            rb.velocity = new Vector2(inputDirection.x * moveSpeed, inputDirection.y * moveSpeed);
        }
        else
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        //移動方向の入力情報がInputdirectionの中に入るようにする
        inputDirection = context.ReadValue<Vector2>();


        if (inputDirection.x < 0)
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        if (inputDirection.x > 0)
        {
            this.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    void BrainWash()
    {
        BossAction bossAction;
        GameObject obj = GameObject.Find("Boss");
        bossAction = obj.GetComponent<BossAction>();

        PlayerAction playerAction;
        GameObject player = GameObject.Find("Player");
        playerAction = player.GetComponent<PlayerAction>();

        if (hp <= 0 && !brainWash)
        {
            death = true;
        }
        if (death)
        {
            GameObject Spore = Instantiate(SporePrefab, transform.position, Quaternion.identity);//洗脳範囲を持ってくる
            Spore.transform.SetParent(transform);

            brainWash = true;
            increseGage = true;
            death = false;
        }
        if (increseGage)//洗脳されたらボスのじめじめゲージ増加
        {
            bossAction.dampGage += 0.5f;
            increseGage = false;
            playerAction.enemyCount += 1;

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            hp -= 1;
            if (!brainWash)
            {
                Destroy(collision.gameObject);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemySpore")
        {
            if (!brainWash)
            {

                hp -= 0.01f;

            }
        }
    }
}

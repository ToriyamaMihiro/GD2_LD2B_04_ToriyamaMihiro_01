using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerAction : MonoBehaviour
{
    private PlayerController inputAcution;
    public SpriteRenderer playerRenderer;
    private Rigidbody2D rb;
    public GameObject bulletPrefab;

    public Vector2 inputDirection;
    public float moveSpeed = 6f;
    public float damege = 1;
    public int enemyCount = 0;//洗脳した敵の数を数える
    int hp = 10;

    bool isHit = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRenderer = gameObject.GetComponent<SpriteRenderer>();
        Application.targetFrameRate = 60;//frameレートの固定
        rb = this.GetComponent<Rigidbody2D>();
        //コントローラーを使うためのもの
        inputAcution = new PlayerController();
        inputAcution.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        Move();//移動
        Shot();//打つ
        Damege();//ダメージ受けたら無敵時間
        ShotPower();
        Debug.Log(hp);

    }
    void Move()
    {
        rb.velocity = new Vector2(inputDirection.x * moveSpeed, inputDirection.y * moveSpeed);
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

    void Shot()
    {
        if (inputAcution.Player.Shot.triggered)
        {
            Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        }
    }

    void ShotPower()//敵の数によって球の攻撃力を変える
    {
        damege = 1 + (enemyCount * 0.2f);
    }

    void Damege()
    {
        if (isHit)
        {
            float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, level);
        }

    }

    IEnumerator WaitForIt()
    {
        // 3秒間処理を止める
        yield return new WaitForSeconds(3.0f);

        // １秒後ダメージフラグをfalseにして点滅を戻す
        isHit = false;
        playerRenderer.color = new Color(1f, 1f, 1f, 1f);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Boss" || collision.gameObject.tag == "Enemy")
        {
            if (!isHit)
            {
                hp -= 1;
                isHit = true;
                StartCoroutine("WaitForIt");
            }
        }
    }

}
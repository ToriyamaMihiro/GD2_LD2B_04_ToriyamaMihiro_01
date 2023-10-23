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
    public int enemyCount = 0;//���]�����G�̐��𐔂���
    int hp = 10;

    bool isHit = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRenderer = gameObject.GetComponent<SpriteRenderer>();
        Application.targetFrameRate = 60;//frame���[�g�̌Œ�
        rb = this.GetComponent<Rigidbody2D>();
        //�R���g���[���[���g�����߂̂���
        inputAcution = new PlayerController();
        inputAcution.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        Move();//�ړ�
        Shot();//�ł�
        Damege();//�_���[�W�󂯂��疳�G����
        ShotPower();
        Debug.Log(hp);

    }
    void Move()
    {
        rb.velocity = new Vector2(inputDirection.x * moveSpeed, inputDirection.y * moveSpeed);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        //�ړ������̓��͏��Inputdirection�̒��ɓ���悤�ɂ���
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

    void ShotPower()//�G�̐��ɂ���ċ��̍U���͂�ς���
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
        // 3�b�ԏ������~�߂�
        yield return new WaitForSeconds(3.0f);

        // �P�b��_���[�W�t���O��false�ɂ��ē_�ł�߂�
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
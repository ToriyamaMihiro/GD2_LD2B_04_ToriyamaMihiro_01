using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAction : MonoBehaviour
{

    public float dampGage = 0.0f;//���߂��߃Q�[�W
    float maxDampGage = 10f;
    float hp = 20;
    float strongTime = 200;
    float maxStrongTime = 200;

    float stanTimer = 500f;
    float maxStanTimer = 500;

    bool strong = false;
    bool strongFinish = true;
    bool stan = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Strong();
        Weak();

    }

    void Strong()
    {
        //���߂��߃Q�[�W���}�b�N�X�ɂȂ����狭��
        if (maxDampGage <= dampGage)
        {
            strong = true;
            strongFinish = false;
        }
        //�������Ԃ��I������狭�����ԏI��������Ƃ�
        if (--strongTime <= 0)
        {
            strongFinish = true;
        }
        if (!strong)
        {
            strongTime = maxStrongTime;
        }
    }

    void Weak()
    {
        //�������Ԃ��I������琔�t���[����̉�����
        if (strongFinish)
        {
            stanTimer--;
            if (stanTimer <= 0)
            {
                strongFinish = false;
                //if(!strongFinsh);
                //damege*=2;
            }
        }
        else
        {
            stanTimer = maxStanTimer;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerAction playerAction;
        GameObject obj = GameObject.Find("Player");
        playerAction = obj.GetComponent<PlayerAction>();
        if (collision.gameObject.tag == "PlayerBullet")
        {
            dampGage += 0.1f;

            hp -= playerAction.damege;

            Destroy(collision.gameObject);
            Debug.Log(dampGage);
        }
        if (collision.gameObject.tag == "Enemy")
        {
        
            Destroy(collision.gameObject);
            dampGage -= 0.2f;

            playerAction.enemyCount -= 1;

        }
    }
}

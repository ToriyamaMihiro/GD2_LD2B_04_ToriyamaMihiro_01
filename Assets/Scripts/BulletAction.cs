using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAction : MonoBehaviour
{
    public float bullet_speed = 20.0f;  // �ړ��l
    int frame_count = 0;
    const int delete_frame = 780;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // �ʒu�̍X�V
        transform.Translate( bullet_speed * Time.deltaTime,0, 0);

        //���ԂɂȂ�����e������
        if (++frame_count > delete_frame)
        {
            Destroy(gameObject);
        }
    }
}

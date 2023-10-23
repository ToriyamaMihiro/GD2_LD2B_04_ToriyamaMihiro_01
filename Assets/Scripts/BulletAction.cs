using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAction : MonoBehaviour
{
    public float bullet_speed = 20.0f;  // 移動値
    int frame_count = 0;
    const int delete_frame = 780;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 位置の更新
        transform.Translate( bullet_speed * Time.deltaTime,0, 0);

        //時間になったら弾を消す
        if (++frame_count > delete_frame)
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーとカメラの位置を一定に保つ
public class CameraFollow : MonoBehaviour
{
    private Transform player;

    //カメラとプレイヤーの設定距離
    [SerializeField]
    private float boundX = 0.3f, boundY = 0.15f;

    //移動量
    private Vector3 deltaPos;

    //実際のカメラとプレイヤーの距離
    private float deltaX, deltaY;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    //Updateの後に呼ばれる。プレイヤーが移動した後に呼ばれる
    private void LateUpdate()
    {
        //格納されていない場合
        if (!player)
        {
            return;
        }

        deltaPos = Vector3.zero;

        deltaX = player.position.x - transform.position.x;
        deltaY = player.position.y - transform.position.y;

        //X軸：カメラとプレーヤーの距離が設定した値より離れたら
        if(deltaX > boundX || deltaX < -boundX)
        {
            if (transform.position.x < player.position.x)
            {
                deltaPos.x = deltaX - boundX;
            }
            else
            {
                deltaPos.x = deltaX + boundX;
            }
        }

        //Y軸：カメラとプレーヤーの距離が設定した値より離れたら
        if (deltaY > boundY || deltaY < -boundY)
        {
            if (transform.position.y < player.position.y)
            {
                deltaPos.y = deltaY - boundY;
            }
            else
            {
                deltaPos.y = deltaY + boundY;
            }
        }

        //Z軸は固定
        deltaPos.z = 0;

        transform.position += deltaPos;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�v���C���[�ƃJ�����̈ʒu�����ɕۂ�
public class CameraFollow : MonoBehaviour
{
    private Transform player;

    //�J�����ƃv���C���[�̐ݒ苗��
    [SerializeField]
    private float boundX = 0.3f, boundY = 0.15f;

    //�ړ���
    private Vector3 deltaPos;

    //���ۂ̃J�����ƃv���C���[�̋���
    private float deltaX, deltaY;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    //Update�̌�ɌĂ΂��B�v���C���[���ړ�������ɌĂ΂��
    private void LateUpdate()
    {
        //�i�[����Ă��Ȃ��ꍇ
        if (!player)
        {
            return;
        }

        deltaPos = Vector3.zero;

        deltaX = player.position.x - transform.position.x;
        deltaY = player.position.y - transform.position.y;

        //X���F�J�����ƃv���[���[�̋������ݒ肵���l��藣�ꂽ��
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

        //Y���F�J�����ƃv���[���[�̋������ݒ肵���l��藣�ꂽ��
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

        //Z���͌Œ�
        deltaPos.z = 0;

        transform.position += deltaPos;
    }
}

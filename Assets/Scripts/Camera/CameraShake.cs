using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �J�����������_���ɗh�炷
/// </summary>
public class CameraShake : MonoBehaviour
{
    //�h��鋭��
    [SerializeField]
    private float shakeAmount = 0.007f;

    //�J�����|�W
    private Vector3 camPos;

    private int test = 1;

    //�i�[�F���ۂɗh�炷���l
    private float cameraShakingOffset_X, cameraShakingOffset_Y;

    /// <summary>
    /// �h�炷�����_���Ȑ��l�ݒ�
    /// </summary>
    private void StartCameraShaking()
    {
        //�h��鐔�l���擾
        if (shakeAmount > 0)
        {
            camPos = transform.position;
            cameraShakingOffset_X = (Random.value * shakeAmount * 2) - shakeAmount;
            cameraShakingOffset_Y = (Random.value * shakeAmount * 2) - shakeAmount;

            camPos.x += cameraShakingOffset_X;
            camPos.y += cameraShakingOffset_Y;

            transform.position = camPos;
        }
    }

    /// <summary>
    /// �w�莞�ԃJ������h�炷
    /// </summary>
    /// <param name="shakeTime"></param>
    public void ShakeCamera(float shakeTime)
    {
        //�w��̊֐���A���ŌĂԁF�ŏ��̌Ăяo������ / �Ăяo���Ԋu
        InvokeRepeating("StartCameraShaking", 0f, 0.01f);

        Invoke("StopCameraShaking", shakeTime);
    }

    /// <summary>
    /// �J�������~�߂�
    /// </summary>
    private void StopCameraShaking()
    {
        CancelInvoke("StartCameraShaking");

        //�J�����ʒu�̏�����
        transform.localPosition = Vector3.zero;
    }

}

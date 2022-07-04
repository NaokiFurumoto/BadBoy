using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �{�X�ȊO�̂��ׂẴL�����̈ړ����s��
/// </summary>
public class Movement : MonoBehaviour
{
    //protected�͌p���悩�爵�����Ƃ��ł���Aprivate���Ǝ��g�̂�
    [SerializeField]
    protected float xSpeed = 1.5f, ySpeed = 1.5f;

    //�ړ���
    private Vector2 moveDelta;

    /// <summary>
    /// �L�����N�^�[���ړ�������
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    protected void CharacterMovement(float x, float y)
    {
        moveDelta = new Vector2(x * xSpeed, y * ySpeed);

        //Time.deltaTime:1�t���[���̍X�V����0.02�Ƃ�
        transform.Translate(moveDelta.x * Time.deltaTime, moveDelta.y * Time.deltaTime, 0);
    }

    //�ړ��ʂ�Ԃ�
    public Vector2 GetMoveDelta()
    {
        return moveDelta;
    }

}

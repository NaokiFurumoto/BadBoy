using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[�̎ˌ��@�L�[���͌��m�A�ˌ����o�ݒ�
/// </summary>
public class PlayerShot : MonoBehaviour
{
    //�ϐ��F�ˌ��^�C�}�[�@���̎ˌ��܂ł̎���
    //���˃|�W�A���@�w�Ǘ��X�N���v�g
    [SerializeField]
    private float shootTimer, shootTimerDelay = 0.2f;

    [SerializeField]
    private Transform magicSpawnPos;

    private int dell = 1;

    private PlayerMagicSquareManager playerMagicSquareManager;

    /// <summary>
    /// ������
    /// </summary>
    private void Awake()
    {
        playerMagicSquareManager = GetComponent<PlayerMagicSquareManager>();
    }

    private void Update()
    {
        Shooting();
    }

    //�֐��F����
    private void Shooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //�v���C���Ԓ�������łĂ�悤��
            if(Time.time > shootTimer)
            {
                //�����Ԃ̍X�V
                shootTimer = Time.time + shootTimerDelay;

                playerMagicSquareManager.Shoot(magicSpawnPos.position);
            }

        }
    }
}

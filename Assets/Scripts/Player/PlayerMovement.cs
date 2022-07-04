using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[�̈ړ���A�j���[�V�����A�L�����̌����ȂǊǗ�
/// </summary>
public class PlayerMovement : Movement
{
    private float moveX, moveY;

    private Camera mainCam;

    //�}�E�X�̈ʒu�ƕ���
    private Vector2 mousePos, direction;

    //�v���[���[�̌�����ς��邽�߂̈ꎞ�I�Ȋi�[�ϐ�
    private Vector3 tempScale;

    private Animator animator;

    private PlayerMagicSquareManager playerMagicSquareManager;

    private void Awake()
    {
        //�J�������Ŏ擾
        mainCam = Camera.main;
        animator = GetComponent<Animator>();
        playerMagicSquareManager = GetComponent<PlayerMagicSquareManager>();
    }

    /// <summary>
    /// �L�[���͂̎󂯎��
    /// �ǂ̃}�V���ł��ꗥ�ŌĂ΂��Update�I�I
    /// </summary>
    private void FixedUpdate()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        PlayerTurning();
        CharacterMovement(moveX, moveY);
    }

    /// <summary>
    /// �L�����������ׂ������̊֐�
    /// </summary>
    private void PlayerTurning()
    {
        //�X�N���[�����W���Q�[�����W�ɂ��Ď擾
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        //���K��������B(1�A1)�@(-1�C1)�Ƃ��ɕϊ�
        direction = new Vector2(mousePos.x - transform.position.x,
                                mousePos.y - transform.position.y).normalized;

        PlayerAnimation(direction.x, direction.y);
    }

    /// <summary>
    /// �A�j���[�V�����̕ύX
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    private void PlayerAnimation(float x, float y)
    {
        //.5�̐��������ɍ��킹��@10.5�Ȃ�10�Ɂ@11.5�Ȃ�12��
        x = Mathf.RoundToInt(x);
        y = Mathf.RoundToInt(y);

        //���g�̌����̑ޔ�
        tempScale = transform.localScale;

        if (x > 0)
        {
            //��Βl
            tempScale.x = Mathf.Abs(tempScale.x);
        }
        else if (x < 0)
        {
            //��Βl��-�ɂ���
            tempScale.x = -Mathf.Abs(tempScale.x);
        }

        //�����̕ύX
        transform.localScale = tempScale;

        //�A�j���[�V�����̐ݒ�㏉����������
        x = Mathf.Abs(x);

        animator.SetFloat("FaceX", x);
        animator.SetFloat("FaceY", y);

        DirectionMagicSquare(x, y);
    }

    //�v���C���[�̌����Ă�����ɍ��킹�ēn����������ς��A���@�w�̐؂�ւ��֐����ĂԊ֐�
    private void DirectionMagicSquare(float x, float y)
    {
        //Animator����������m�F
        //side
        if (x == 1f && y == 0f)
        {
            playerMagicSquareManager.Activate(0);
        }

        //up
        if (x == 0f && y == 1f)
        {
            playerMagicSquareManager.Activate(1);
        }

        //front
        if (x == 0f && y == -1f)
        {
            playerMagicSquareManager.Activate(2);
        }

        //side_up
        if (x == 1f && y == 1f)
        {
            playerMagicSquareManager.Activate(3);
        }

        //side_down
        if (x == 1f && y == -1f)
        {
            playerMagicSquareManager.Activate(4);
        }
    }
}

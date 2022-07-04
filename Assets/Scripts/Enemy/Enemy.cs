using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�ǐՂƍU��
public class Enemy : Movement
{
    //Todo:�e�X�g�ϐ�
    public bool Test;//�v���[���[�𔭌��������ǂ���

    //�v���[���[�֘A�̏��
    private Transform player;
    private Vector3 playerLastPos, startPos, movementPos;

    //�ǂ�������X�s�[�h/��]�̒x��
    [SerializeField]
    private float chaseSpeed = 0.8f, turningDelay = 1f;

    //�v���C���[�̈ʒu���Ō�ɔc���������ԂƎ��ɕ����]���\�Ȏ���
    private float lastFollowTime, turningTimeDelay = 1f;

    //�ꎞ�I�Ȓl�̕ۑ��F�����ύX�p
    private Vector3 tempScale;

    //�U������
    private bool attacked;

    //�N�[���_�E������
    [SerializeField]
    private float damageCooldown = 1f;
    private float damageCooldownTimer;

    //�U����
    [SerializeField]
    private int damageAmount = 1;

    private Health enemyHealth;

    private Animator animator;

    private int Dell = 1;

    /// <summary>
    /// �������֘A
    /// </summary>
    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        playerLastPos = player.position;
        startPos = transform.position;
        lastFollowTime = Time.time;
        turningTimeDelay *= turningDelay;
        enemyHealth = GetComponent<Health>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        //���g�����S��Ԃ��A�v���C���[�̃R���|�[�l���g��������Ȃ��ꍇ
        if (!enemyHealth.IsAlive() || !player )
        {
            return;
        }

        MoveAnimation();//Update�Ȃ�H�H
        TurnAround();
        ChaseingPlayer();
    }

    /// <summary>
    /// �v���[���[��ǂ�������
    /// ��莞�Ԍo�߂���Ɣ������Ă܂������i��
    /// </summary>
    private void ChaseingPlayer()
    {
        //�v���C��|�𔭌����Ă鎞
        if (Test)
        {
            //�U�����Ă��Ȃ�
            if (!attacked)
            {
                //�U��
                Chase();
            }
            else
            {
                //�N�[���_�E���o�߂��Ă��Ȃ�
                if(Time.time < damageCooldownTimer)
                {
                    //�����ʒu�ɖ߂��Ă���
                    movementPos = startPos - transform.position;
                }
                else//�N�[���_�E���o��
                {
                    attacked = false;
                }
            }
           
        }
        else
        {
            //�������ĂȂ����ꍇ�͏����ʒu�Ɉړ�
            movementPos = startPos - transform.position;
            if(Vector3.Distance(transform.position, startPos) < 0.1f)
            {
                //�����Ȃ�
                movementPos = Vector3.zero;
            }
        }

        //�ړ�������
        CharacterMovement(movementPos.x, movementPos.y);
    }

    /// <summary>
    /// �������ǂ�������
    /// </summary>
    private void Chase()
    {
        //�܂������i�񂾔���
        if (Time.time- lastFollowTime > turningTimeDelay)
        {
            playerLastPos = player.transform.position;
            lastFollowTime = Time.time;
        }

        //�\������Ă�
        if (Vector3.Distance(transform.position, playerLastPos) > 0.15f) 
        {
            movementPos = (playerLastPos - transform.position).normalized * chaseSpeed;
        }
        else//�\���߂Â��Ă���
        {
            movementPos = Vector3.zero;
        }
    }

    /// <summary>
    /// �����ύX
    /// </summary>
    private void TurnAround()
    {
        tempScale = transform.localScale;
        //�v���C���[�̒T�m
        if (Test)
        {
            if (player.position.x > transform.position.x)
            {
                tempScale.x = Mathf.Abs(tempScale.x);
            }

            if (player.position.x < transform.position.x)
            {
                tempScale.x = -Mathf.Abs(tempScale.x);
            }
        }
        else//�T�m�ł��Ă��Ȃ��ꍇ�͍ŏ��̈ʒu�ɖ߂�
        {
            if (startPos.x > transform.position.x)
            {
                tempScale.x = Mathf.Abs(tempScale.x);
            }

            if (startPos.x < transform.position.x)
            {
                tempScale.x = -Mathf.Abs(tempScale.x);
            }
        }

        //��L�ݒ��ɔ��f
        transform.localScale = tempScale;
    }

    /// <summary>
    /// �U������
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            damageCooldownTimer = Time.time + damageCooldown;

            attacked = true;

            collision.GetComponent<Health>().TakeDamage(damageAmount);
               
        }
    }

    /// <summary>
    /// �ړ��A�j���[�V������؂�ւ���
    /// </summary>
    private void MoveAnimation()
    {
        //���̃L�����������Ă邩�ǂ����̔���:Vector2����x�N�g���̑傫�����擾
        if (GetMoveDelta().sqrMagnitude > 0)
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }

        //����ł�����
        //animator.SetBool("Walk", GetMoveDelta().sqrMagnitude > 0);
    }

}

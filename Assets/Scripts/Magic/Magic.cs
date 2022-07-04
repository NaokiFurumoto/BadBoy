using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//���
//�e�̑��x��U���͂̊Ǘ��A���������̐ݒ��A�e�̔�\���Ȃǂ��s��
public class Magic : MonoBehaviour
{

    //��邱��
    //�ϐ��쐬�iRigidBody2D�̊i�[�A�ړ��X�s�[�h�A������܂ł̎���
    //�U���́A�U������A�폜����A�O�Ղ̗L���A�O��
    private Rigidbody2D rb;

    [SerializeField]
    private float moveSpeed = 2.5f, deactiveTimer = 3f;

    [SerializeField]
    private int damageAmount = 25;

    private bool damage;

    [SerializeField]//�폜�Ώۂ�Obj���ǂ����FPool�p
    private bool destroyObj;

    [SerializeField]//�O�Ղ�����̂��̔���
    private bool getTrailRenderer;

    private TrailRenderer trail;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (getTrailRenderer)
        {
            trail = GetComponent<TrailRenderer>();
        }
    }

    /// <summary>
    /// �q�G�����L�[����L���ɂȂ����ۂɌĂ΂��
    /// </summary>
    private void OnEnable()
    {
        //�_���[�W����𖳂���
        damage = false;

        //�������ĕb��ɏ���
        Invoke("DeactiveMagic", deactiveTimer);
    }

    /// <summary>
    /// �����ɂȂ����ۂɌĂ΂��
    /// </summary>
    private void OnDisable()
    {
        //pool�p�Ɉړ����x��0�ɂ���Bpool�͔�\���ɂ��Ďg���܂킷
        rb.velocity = Vector2.zero;
        if (getTrailRenderer)
        {
            trail.Clear();
        }
    }

  
    /// <summary>
    /// ��������Ɉړ������� ���x�x�N�g��
    /// velocity��1�̏ꍇ�A1�b�Ԃ�1unit�̑���
    /// �񃊃A�������B���A��������AddForce
    /// </summary>
    /// <param name="direction"></param>
    public void MoveDirection(Vector3 direction)
    {
        rb.velocity = direction * moveSpeed;
    }

    /// <summary>
    /// �I�u�W�F�N�g���\���ɂ���
    /// </summary>
    void DeactiveMagic()
    {
        //�`�F�b�N����Ă�ƍ폜������
        if (destroyObj)
        {
            Destroy(gameObject);
        }
        //pool������̂Ŕ�\��
        else
        {
            gameObject.SetActive(false);
        }
    }

    
    /// <summary>
    /// ���������u�ԂɌĂ΂��
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || 
            collision.CompareTag("Boss"))
        {
            rb.velocity = Vector2.zero;
            //���Ԋ֐��̃L�����Z��
            CancelInvoke("DeactiveMagic");

            if (!damage)
            {
                damage = true;
                collision.GetComponent<Health>().TakeDamage(damageAmount);
            }

            DeactiveMagic();
        }

        //��Q���ɏՓ�
        if (collision.CompareTag("Blocking"))
        {
            rb.velocity = Vector2.zero;
            //���Ԋ֐��̃L�����Z��
            CancelInvoke("DeactiveMagic");
            DeactiveMagic();
        }
    }

}

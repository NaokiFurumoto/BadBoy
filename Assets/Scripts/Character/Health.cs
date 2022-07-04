using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �S�L�����̗̑͊Ǘ��⎀�S�A�j���[�V����
/// </summary>
public class Health : MonoBehaviour
{
    //�ϐ��F�ő�̗́A���݂̗̑́A�A�j���[�V�����i�[
    [SerializeField]
    private int maxHealth = 5;

    private int health;

    private Animator animator;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        health = maxHealth;
    }

    /// <summary>
    /// ��������
    /// </summary>
    /// <returns></returns>
    public bool IsAlive()
    {
        return health > 0;
    }

    public void OnDestroy()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            animator.SetTrigger("Death");
        }
    }
}

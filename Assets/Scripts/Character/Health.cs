using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 全キャラの体力管理や死亡アニメーション
/// </summary>
public class Health : MonoBehaviour
{
    //変数：最大体力、現在の体力、アニメーション格納
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
    /// 生存判定
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

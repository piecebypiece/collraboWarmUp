using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 작성일자 : 2019-12-16-PM-6-22
// 작성자   : 배형영
// 간단설명 :

public class Goomba : Enemy
{
    // Variable
    #region Variable
    [SerializeField]
    private Rigidbody2D goombaRigidbody = null;
    [SerializeField]
    private Sprite DeathSprite = null;
    private float deathForce = 100f;
    #endregion

    // Property
    #region Property

    #endregion

    // MonoBehaviour
    #region MonoBehaviour


    protected override void Update()
    {
        Move();

    }
    #endregion

    // Private Method
    #region Private Method

    private void Move()
    {
        goombaRigidbody.position += (moveDirections[(int)nowDir] * moveSpeed * Time.deltaTime);
    }
    #endregion

    // Public Method
    #region Public Method
    public override void Init(Direction direction)
    {
        nowDir = direction;
        colliderEnemy.SetEnable(true);
        animator.SetEnable(true);
    }
    /// <summary>
    /// 밟혀 죽었을때
    /// </summary>
    public override void Death()
    {
        colliderEnemy.SetEnable(false);
        animator.SetEnable(false);

        spriteRenderer.SetSprite(DeathSprite);
        goombaRigidbody.AddForce(Vector2.up * deathForce, ForceMode2D.Force);
    }

    /// <summary>
    /// 타일에 맞아서 죽었을때
    /// </summary>
    public override void Hit()
    {
        //Death();
        colliderEnemy.SetEnable(false);
        animator.SetEnable(false);

        spriteRenderer.SetFlipY(true);
        goombaRigidbody.AddForce(Vector2.up * deathForce, ForceMode2D.Force);
    }

    public override void Stop()
    {
        throw new System.NotImplementedException();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == Common.tagEnvirments)
        {
            if (nowDir == Direction.Left)
                nowDir = Direction.Right;
            else
                nowDir = Direction.Left;
        }
    }
    #endregion

}

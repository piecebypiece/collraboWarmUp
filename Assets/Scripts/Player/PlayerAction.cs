using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 작성일자 : 2019-12-11-PM-4-26
// 작성자   : 배형영
// 간단설명 : 조작에 따른 액션 처리 

public class PlayerAction : MonoBehaviour
{
    private enum MarioSize
    {
        Child = 0,
        Adult
    }

    // Variable
    #region Variable
    [SerializeField]
    private PlayerInput playerInput = null;
    [SerializeField]
    private PlayerAnimCtrl playerAnimCtrl = null;
    private Rigidbody2D playerRigidbody = null;

    [SerializeField]
    private float jumpForce = 200f; // 점프 힘
    [SerializeField]
    private float runSpeed = 1.5f;  // 달리는 속도

    [SerializeField]
    private float counterForce = 50f;   // 역중력 힘
    private Vector2 counterJumpForce;   // 역중력 방향

    private bool action = true;
    private bool isGrounded = false;
    private bool isJumping = false;
    
    #endregion

    // Property
    #region Property

    #endregion

    // MonoBehaviour
    #region MonoBehaviour
    private void Awake()
    {
        playerRigidbody = GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;

        if (playerAnimCtrl != null)
            playerAnimCtrl.SetState(false);

        counterJumpForce = Vector2.down * counterForce;
        playerAnimCtrl.AnimEndEvent += AnimEndCall;
        playerAnimCtrl.AdultToChildEvent += AdultToChildCall;
    }
    private void FixedUpdate()
    {
        if (!action)
            return;
        Move();
        CheckGround();


        // 떨어질때 점프불가
        //if (playerRigidbody.velocity.y < 0)
        //{
        //    if(!isJumping)
        //        Jump(false);
        //}
        //else
        //{
        if (playerInput.jumpBtnDown)
        {
            if (isGrounded)
            {
                Jump(true);
            }
        }
        //}
        if (isJumping)
        {
            if (!playerInput.jumpBtnDown && Vector2.Dot(playerRigidbody.velocity, Vector2.up) > 0/*playerRigidbody.velocity.y >= 0f*/)

                playerRigidbody.AddForce(counterJumpForce * playerRigidbody.mass);
        }
    }
    private void OnDisable()
    {
        playerAnimCtrl.AnimEndEvent -= AnimEndCall;
        playerAnimCtrl.AdultToChildEvent -= AdultToChildCall;
    }
    #endregion

    // Private Method
    #region Private Method
    /// <summary>
    /// 움직이기
    /// </summary>
    private void Move()
    {

        if (playerInput.move > 0)
        {
            playerAnimCtrl.SetFlipX(false);
        }
        else if (playerInput.move < 0)
        {
            playerAnimCtrl.SetFlipX(true);
        }
        else
        {
            playerAnimCtrl.PlayRun(false, 1f);
            return;
        }
        Vector2 movePos = playerInput.move * Vector2.right * runSpeed * Time.fixedDeltaTime;
            playerRigidbody.position = playerRigidbody.position + movePos;
        playerAnimCtrl.PlayRun(true, playerInput.move * 5f);
    }

    /// <summary>
    /// 점프 
    /// </summary>
    /// <param name="doForce">AddForce 하는지?</param>
    private void Jump(bool doForce)
    {
        isGrounded = false;
        isJumping = true;
        playerAnimCtrl.PlayJump(true);

        if (doForce)
        {
            playerRigidbody.velocity = Vector2.zero;
            playerRigidbody.AddForce(new Vector2(0, jumpForce) * playerRigidbody.mass);
        }
    }

    private void CheckGround()
    {
        //Vector2 vector2 = new Vector2(transform.position.x, transform.position.y + 0.015f);
        //RaycastHit2D hit = Physics2D.Raycast(vector2, transform.TransformDirection(Vector2.down), 0.03f/*, LayerMask.GetMask(Common.layerEnvirments)*/);
        Collider2D hit = Physics2D.OverlapBox(transform.position, new Vector2(0.12f, 0.03f), 0, LayerMask.GetMask(Common.layerEnvirments));

        //Debug.DrawRay(vector2, transform.TransformDirection(Vector2.down) * 0.03f, Color.red);

        if (hit != null)
        {
        Debug.Log(hit.gameObject.GetInstanceID());
            if(!isGrounded)
            {
                InitJump();
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector2(0.12f, 0.03f));
    }
    /// <summary>
    /// 애니메이션 끝났을때 호출
    /// </summary>
    /// <param name="eventAnim"></param>
    private void AnimEndCall(PlayerAnimCtrl.EventAnim eventAnim)
    {
        switch (eventAnim)
        {
            case PlayerAnimCtrl.EventAnim.Growth:
                action = true;
                SetIgnoreEnemy(false);
                break;
            case PlayerAnimCtrl.EventAnim.Flag:
                break;
            case PlayerAnimCtrl.EventAnim.Hit:
                break;
            case PlayerAnimCtrl.EventAnim.Death:
                break;
        }
    }
    /// <summary>
    /// 적한테 맞았을때
    /// </summary>
    private void Hit()
    {

    }

    /// <summary>
    /// 점프가 가능하도록 초기화
    /// </summary>
    private void InitJump()
    {
        playerAnimCtrl.PlayJump(false);
        isGrounded = true;
        isJumping = false;
    }

    /// <summary>
    /// 적의 충돌을 무시하는지
    /// </summary>
    /// <param name="val"></param>
    private void SetIgnoreEnemy(bool val)
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer(Common.layerPlayer), LayerMask.NameToLayer(Common.layerEnemy), val);
    }
    /// <summary>
    /// 
    /// </summary>
    private void AdultToChildCall()
    {
        StartCoroutine(Invincibility());
    }
    private IEnumerator Invincibility()
    {
        // 무적 시작
        yield return new WaitForSeconds(2f);
    }
    #endregion

    // Public Method
    #region Public Method
    
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D contactPoint = collision.contacts[0];
        Vector2 normal = contactPoint.normal;

        switch (contactPoint.collider.tag)
        {
            // 벽돌
            case Common.tagEnvirments:
                if (normal.y < 0)
                {
                    TileObject tileObject = contactPoint.collider.GetComponent(typeof(TileObject)) as TileObject;
                    tileObject?.ActionCall();
                }
                //else if (normal.y > 0)
                //{
                //    InitJump();
                //}
                break;
            case Common.tagEnemy:
                {
                    if(normal.y > 0)
                    {
                        Enemy enemy = contactPoint.collider.GetComponent(typeof(Enemy)) as Enemy;
                        enemy?.Hit(true, transform.position - contactPoint.collider.transform.position);
                        Jump(true);
                    }
                    else
                    {
                        // 마리오 죽음
                        Hit();
                    }
                }
                break;
            default:
                //if(normal.y > 0)
                //{
                //    InitJump();
                //}
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.tag)
        {
            case Common.tagItem:
                action = false;
                SetIgnoreEnemy(true);
                playerAnimCtrl.PlayGrowth();

                break;
        }
    }
    #endregion
}

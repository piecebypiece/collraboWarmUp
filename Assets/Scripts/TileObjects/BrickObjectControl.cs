using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 작성일자 : 2019-12-22-PM-7-58
// 작성자   : 김세중
// 간단설명 :
public class BrickObjectControl : TileObject
{
    // Variable
    #region Variable
    [SerializeField]
    Animator m_RenderAnimator;
    public struct AnimID
    {
        public int
            Hit;
    }
    public AnimID m_AnimID;
    #endregion

    // Property
    #region Property
    public Animator RenderAnimator
    {
        get => m_RenderAnimator;
        set => m_RenderAnimator = value;
    }

    #endregion

    // MonoBehaviour
    #region MonoBehaviour
    public override void Awake()
    {
        base.Awake();
        m_PoketQueue = new Queue<SpawnerType.ItemType>();
        AnimIDInit();
    }
    #endregion

    // Private Method
    #region Private Method
    void AnimIDInit()
    {
        m_AnimID.Hit = Animator.StringToHash("Hit");
    }
    #endregion

    // Public Method
    #region Public Method

    public override void ActionCall()
    {
        base.ActionCall();
        m_RenderAnimator.SetTrigger(m_AnimID.Hit);
    }
    public void ReSetTriggerHit()
    {
        RenderAnimator.ResetTrigger(m_AnimID.Hit);
    }
    #endregion


}

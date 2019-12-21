using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 작성일자 : 2019-12-19-PM-3-57
// 작성자   : 배형영
// 간단설명 : 공용 클래스나, 메소드, enum등을 선원해놓는곳 

public static class Common 
{
    // Variable
    #region Variable
    
    #endregion
    
    // Property
    #region Property
    
    #endregion

    // MonoBehaviour
    #region MonoBehaviour
    
    #endregion

    // Private Method
    #region Private Method
    
    #endregion

    // Public Method
    #region Public Method
    
    // 확장 메소드
    public static void SetEnable(this Behaviour behaviour, bool val)
    {
        if (behaviour != null)
            behaviour.enabled = val;
    }
    public static void SetSprite(this SpriteRenderer spriteRenderer, Sprite sprite)
    {
        if (spriteRenderer != null)
            spriteRenderer.sprite = sprite;
    }
    public static void SetFlipX(this SpriteRenderer spriteRenderer, bool val)
    {
        if (spriteRenderer != null)
            spriteRenderer.flipX = val;
    }
    public static void SetFlipY(this SpriteRenderer spriteRenderer, bool val)
    {
        if (spriteRenderer != null)
            spriteRenderer.flipY = val;
    }
    #endregion
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 작성일자 : 2019-12-27-PM-1-06
// 작성자   : 김세중
// 간단설명 :


public class PlayerSpawner : Spawner<SpawnerType.PlayerType, GameObject, Transform>,IRegist_Dictionary
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

    public override void Instantiate(GameObject _GameObject, Vector3 _StandardPos, int _row, int _Cloum, Transform _ParentTransform)
    {
        var player = Instantiate<GameObject>(_GameObject, new Vector3(_StandardPos.x + (0.16f * _row), _StandardPos.y + (0.16f * _Cloum), 0), Quaternion.identity, _ParentTransform);
        GameCamera.Instance.SetActiveCamera(GameCamera.CameraKind.PlayerCamera);
        GameCamera.Instance.GetCameraObjectComponent<PlayerCamera>(GameCamera.CameraKind.PlayerCamera).SetPlayer(player.transform);
        player.name = Common.PlayerName;
        SpawnObjects.Add(player);
    }

    public override void Add_Dictionary()
    {
        int playerIndex = 0;
        SpawnerType.PlayerType f_PlayerType = SpawnerType.PlayerType.Player;
        for (int i_Type = 0; i_Type < SpawnObjectList.Count; i_Type++)
        {
            CompareEnumTypeDictionary.Add(f_PlayerType++, SpawnObjectList[playerIndex++]);
        }
    }

    public void Contain_Dictionary()
    {
        Add_Dictionary();
    }

    public void Dictionary_Init()
    {
        CompareEnumTypeDictionary = new EnumDictionary<SpawnerType.PlayerType, GameObject>();
    }
    #endregion

}

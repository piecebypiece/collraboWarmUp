using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 작성일자 : 2019-12-27-PM-12-57
// 작성자   : 김세중
// 간단설명 : GameObject형 Tile을 Spawn해줌


public class ObjectTileSpawner : Spawner<SpawnerType.ObjectTileType, GameObject>,IRegist_Dictionary
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

    #endregion
    public override void Add_Dictionary()
    {
        int ObjectTileindex = 0;
        CompareEnumTypeDictionary.Add(SpawnerType.ObjectTileType.Flag, SpawnObjectList[ObjectTileindex++]);
        CompareEnumTypeDictionary.Add(SpawnerType.ObjectTileType.Brick, SpawnObjectList[ObjectTileindex++]);
        CompareEnumTypeDictionary.Add(SpawnerType.ObjectTileType.RiddleBox, SpawnObjectList[ObjectTileindex++]);
        CompareEnumTypeDictionary.Add(SpawnerType.ObjectTileType.PipeBodyLeft, SpawnObjectList[ObjectTileindex++]);
        CompareEnumTypeDictionary.Add(SpawnerType.ObjectTileType.PipeBodyRight, SpawnObjectList[ObjectTileindex++]);
        CompareEnumTypeDictionary.Add(SpawnerType.ObjectTileType.PipeDoorLeft, SpawnObjectList[ObjectTileindex++]);
        CompareEnumTypeDictionary.Add(SpawnerType.ObjectTileType.PipeDoorRight, SpawnObjectList[ObjectTileindex++]);
    }
        
    public void Contain_Dictionary()
    {
        Add_Dictionary();
    }

    public void Dictionary_Init()
    {
        CompareEnumTypeDictionary = new EnumDictionary<SpawnerType.ObjectTileType, GameObject>();
    }
}

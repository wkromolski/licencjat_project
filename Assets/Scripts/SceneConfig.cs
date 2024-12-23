using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewSceneConfigs", menuName = "Scene Configs")]
public class SceneConfigsSO : ScriptableObject

{
    public string sceneName;
    public bool hasAnomaly;
    public bool requiresSecondDoor;
}

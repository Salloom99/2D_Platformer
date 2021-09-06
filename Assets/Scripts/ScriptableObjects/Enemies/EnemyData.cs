using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyData", menuName = "Data/Enemey Data/Base Data")]
public class EnemyData : ScriptableObject {

    [Header("Idle State")]
    public float minIdleTime;
    public float maxIdleTime;

    [Header("Move State")]
    public float movementSpeed = 7f;

    
}
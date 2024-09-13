using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
internal sealed class PlayerRef : ScriptableObject
{
    [UnityEngine.SerializeField] internal UnityEngine.Transform tf;
    [UnityEngine.SerializeField] internal UnityEngine.Rigidbody rb;
    [UnityEngine.SerializeField] internal UnityEngine.Collider cl;
}
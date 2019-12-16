using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] float speed = 1;

    [Header("Jump Settings")]
    [SerializeField] AnimationCurve Curve;
    [SerializeField] float height = 1;
    [SerializeField] float duration = 1;

    
}

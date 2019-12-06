using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    private GameObject raycastObj;

    [Header("Raycast Settings")]
    [SerializeField] private float rayLength = 10;
    [SerializeField] private LayerMask newLayerMask;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

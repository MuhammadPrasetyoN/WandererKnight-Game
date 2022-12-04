using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUIHealth : MonoBehaviour
{
    private Camera _mainCam;
    [SerializeField] private GameObject healthUI;

    private void Start()
    {
        _mainCam = Camera.main;
    }

    private void LateUpdate()
    {
        var rotation = _mainCam.transform.rotation;

        transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
    }

    public bool IsDisplayed = false;

    public void SetUp()
    {
        healthUI.SetActive(true);
        IsDisplayed = true;
    }

    public void Close()
    {
        healthUI.SetActive(false);
        IsDisplayed = false;
    }
}

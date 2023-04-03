using System;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private string weaponID;
    [SerializeField] private float timeBetweenShots;
    [SerializeField] private Material idleMaterial, highlightMaterial, disabledMaterial;
    [SerializeField] private Transform projectileSpawnPoint;

    private bool canShot;
    private MeshRenderer[] renderers;

    public event Action<string, Transform> OnShot;

    private void Awake()
    {
        canShot = true;

        renderers = GetComponentsInChildren<MeshRenderer>();
    }

    private void OnMouseOver()
    {
        if (canShot == false) return;

        ChangeMaterial(highlightMaterial);

        if (Input.GetMouseButtonDown(0)) 
        {
            Shot();
        }
    }

    private void OnDisable()
    {
        OnShot = null;
    }

    private void OnMouseExit()
    {
        if (canShot == false) return;

        ChangeMaterial(idleMaterial);
    }

    private void ActivateShot() 
    {
        canShot = true;

        ChangeMaterial(idleMaterial);
    }

    private void Shot() 
    {
        canShot = false;

        OnShot?.Invoke(weaponID, projectileSpawnPoint);

        ChangeMaterial(disabledMaterial);

        Invoke(nameof(ActivateShot), timeBetweenShots);
    }

    private void ChangeMaterial(Material newMaterial) 
    {
        foreach (var r in renderers)
        {
            r.material = newMaterial;
        }
    }
}

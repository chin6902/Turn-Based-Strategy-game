using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractSphere : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform barrelVFX;

    private GridPosition gridPosition;
    private Action onInteractComplete;
    private float timer;
    private bool isActive;

    private void Start()
    {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.SetInteractableAtGridPosition(gridPosition, this);

        Pathfinding.Instance.SetIsWalkableGridPosition(gridPosition, false);
    }

    private void Update()
    {
        if (!isActive)
        {
            return;
        }

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            isActive = false;
            onInteractComplete();

            LevelGrid.Instance.ClearInteractableAtGridPosition(gridPosition);
            Instantiate(barrelVFX, transform.position, Quaternion.identity);
            Pathfinding.Instance.SetIsWalkableGridPosition(gridPosition, true);
            Destroy(gameObject);
        }
    }

    public void Interact(Unit unit, Action onInteractComplete)
    {
        this.onInteractComplete = onInteractComplete;
        isActive = true;
        timer = 0.5f;

        unit.RecoverActionPoints();
    }
}

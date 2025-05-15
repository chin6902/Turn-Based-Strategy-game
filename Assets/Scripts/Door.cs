using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public static event EventHandler OnAnyDoorOpened;
    public event EventHandler OnDoorOpened;

    private GridPosition gridPosition;
    private Animator animator;
    private Action onInteractComplete;
    private float timer;
    private bool isActive;

    [SerializeField] private bool isOpen;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.SetInteractableAtGridPosition(gridPosition, this);

        if(isOpen)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
    }

    private void Update()
    {
        if (!isActive)
        {
            return;
        }

        timer -= Time.deltaTime;

        if(timer <= 0 )
        {
            isActive = false;
            onInteractComplete();
        }
    }

    public void Interact(Unit unit, Action onInteractComplete)
    {
        this.onInteractComplete = onInteractComplete;
        isActive = true;
        timer = 0.5f;

        if (isOpen)
        {
            CloseDoor();
        }
        else
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        isOpen = true;
        animator.SetBool("IsOpen", true);
        Pathfinding.Instance.SetIsWalkableGridPosition(gridPosition, true);

        OnDoorOpened?.Invoke(this, EventArgs.Empty);
        OnAnyDoorOpened?.Invoke(this, EventArgs.Empty);
    }

    private void CloseDoor()
    {
        isOpen = false;
        animator.SetBool("IsOpen", false);
        Pathfinding.Instance.SetIsWalkableGridPosition(gridPosition, false);
    }
}

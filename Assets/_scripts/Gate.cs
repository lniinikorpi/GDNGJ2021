using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public GameObject closedGate;
    public GameObject openedGate;
    bool _isOpen = false;
    public BoxCollider2D _boxCollider2D;

    public void OpenAgain()
    {
        SetObjects();
    }

    private void Open()
    {
        if (!_isOpen)
        {
            EnemySpawner.instance.openGates.Add(gameObject.name);
            SetObjects();
        }
    }

    private void SetObjects()
    {
        _isOpen = true;
        closedGate.SetActive(false);
        openedGate.SetActive(true);
        _boxCollider2D.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponentInChildren<Player>();
        if (player)
        {
            Open();
            GameManager.instance.EndLevel();
        }
    }
}

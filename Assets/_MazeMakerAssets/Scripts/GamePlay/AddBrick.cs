using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AddBrick : MonoBehaviour
{
    [SerializeField] private GameObject brick;
    private Renderer renderer;
    private bool isUsed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isUsed)
        {
            other.GetComponent<Player>().AddBrick();
            renderer = brick.GetComponent<Renderer>();
            renderer.enabled = false;
            isUsed = true;
        }

    }
}

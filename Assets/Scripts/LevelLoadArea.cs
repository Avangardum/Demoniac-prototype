using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoadArea : MonoBehaviour
{
    [SerializeField] private string sceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            SceneLoader.LoadScene(sceneName);
    }
}

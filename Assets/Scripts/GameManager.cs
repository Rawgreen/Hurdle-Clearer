using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton GameManager class to manage game-wide tasks and state.
/// </summary>
public class GameManager : MonoBehaviour
{
    // Singleton pattern property
    private static GameManager _instance;

    /// <summary>
    /// Gets the singleton instance of the GameManager.
    /// If an instance does not already exist, it creates one.
    /// </summary>
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("GameManager");
                _instance = obj.AddComponent<GameManager>();
                DontDestroyOnLoad(obj);
            }
            return _instance;
        }
    }

    // Queue to manage respawn coroutines
    private Queue<IEnumerator> respawnQueue = new Queue<IEnumerator>();
    // Flag to indicate if a respawn coroutine is currently running
    private bool isRespawning = false;

    /// <summary>
    /// Enqueues a respawn coroutine to be processed.
    /// </summary>
    /// <param name="coroutine">The respawn coroutine to be enqueued.</param>
    public void StartRespawnCoroutine(IEnumerator coroutine)
    {
        respawnQueue.Enqueue(coroutine);
        if (!isRespawning)
        {
            StartCoroutine(ProcessRespawnQueue());
        }
    }

    /// <summary>
    /// Processes the respawn queue by starting each coroutine in the queue.
    /// Ensures that only one coroutine is processed at a time.
    /// </summary>
    private IEnumerator ProcessRespawnQueue()
    {
        isRespawning = true;
        while (respawnQueue.Count > 0)
        {
            yield return StartCoroutine(respawnQueue.Dequeue());
        }
        isRespawning = false;
    }
}

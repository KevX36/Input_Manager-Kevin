using UnityEngine;
using UnityEngine.InputSystem;

public class playerSpawner : MonoBehaviour
{

    public Transform[] spawnPoints;
    public int m_playerCount;

    public void OnPlayerJoined(PlayerInput player)
    {
        player.transform.position = spawnPoints[m_playerCount].transform.position;
        m_playerCount++;
    }










}
    

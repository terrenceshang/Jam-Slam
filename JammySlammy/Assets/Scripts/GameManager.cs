using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;

    private void Update()
    {
        CheckPlayers();
    }

    private void CheckPlayers()
    {
        if (player1 == null && player2 == null)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        Debug.Log("Both players are destroyed! Game Over.");

        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}

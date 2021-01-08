using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameEnded = false;
    [SerializeField] GameObject gameOver;
    public float restartDelay;
    [SerializeField] 
    public void EndGame()
    {

        if(gameEnded == false)
        {
            gameEnded = true;
            Debug.Log("GameOver");
            gameOver.SetActive(true);
            Invoke("Restart", restartDelay);
        }
        
    }
    
    void Restart()
    {
       
        SceneManager.LoadScene("MainMenu");
    }

}

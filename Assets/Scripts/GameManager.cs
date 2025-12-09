using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public void OnPlay()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        NewScene(nextScene);
    }

    public void OnPlayAgain()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex;
        NewScene(nextScene);
    }

    public void OnExit()
    {
        Application.Quit();
    }

    public void NewScene(int nextScene)
    {
        StartCoroutine(NextScene(nextScene));
    }

    IEnumerator NextScene(int nextScene)
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(nextScene);
    }
}

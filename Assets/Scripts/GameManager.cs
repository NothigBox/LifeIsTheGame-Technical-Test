using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private CharacterManager character;

    private CharacterManager Character => character?? (character = FindObjectOfType<CharacterManager>());

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SetAnimation(string animName)
    {
        Character.ChangeAnimation(animName);
    }

    public void SetScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private CharacterManager character;
    private ShotInfo shotInfo;
    private Weapon[] weapons;

    private CharacterManager Character => character?? (character = FindObjectOfType<CharacterManager>());
    private ShotInfo ShotInfo => shotInfo?? (shotInfo = (ShotInfo) Resources.Load("ScriptableObjects/ShotsInfo"));

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadMode)
    { 
        if (scene.name == "WeaponScene") 
        {
            weapons = FindObjectsOfType<Weapon>();
            foreach (var weapon in weapons) 
            {
                weapon.OnShot += Shot;
            }
        }
    }

    public void SetAnimation(string animName)
    {
        Character.ChangeAnimation(animName);
    }

    public void SetScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void Shot(string id, Transform spawnPoint)
    {
        var model = ShotInfo.GetWeaponInfo(id).ProjectileModel;
        if(model != null)
        {
            var projectile = Instantiate(model, spawnPoint.position, spawnPoint.rotation);

            ShotInfo.Shot(id, projectile);
        }
    }
}

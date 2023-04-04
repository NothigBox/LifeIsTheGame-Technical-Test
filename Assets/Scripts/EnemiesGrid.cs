using UnityEngine;

public class EnemiesGrid : MonoBehaviour
{
    [SerializeField] private GameObject enemiesGrid;
    [SerializeField] private GameObject prefab;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            RestartGrid();
        }
    }

    public void RestartGrid() 
    {
        Destroy(enemiesGrid);

        enemiesGrid = Instantiate(prefab, transform);
    }
}

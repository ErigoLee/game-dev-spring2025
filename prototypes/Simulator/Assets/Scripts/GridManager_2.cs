using UnityEngine;
using TMPro;

public class GridManager_2 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private GameObject keyPrefab;  // The prefab for the key
    [SerializeField] private TMP_Text tMP_Text;
    private int gridSize = 10;
    private float cellSpacing = 1f;
    private  Cell [,] cells;
    void Start()
    {
        cells = new Cell[gridSize, gridSize];
        CreateGrid();
    }

    // Create a 10x10 grid of cellPrefabs
    void CreateGrid()
    {
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                Vector3 position = new Vector3(x * cellSpacing, 0, y * cellSpacing);
                GameObject cell = Instantiate(cellPrefab, position, Quaternion.identity, transform);
                // Add the Cell script to handle click events for this cell
                Cell cellScript = cell.AddComponent<Cell>();
                cellScript.SetKeyPrefab(keyPrefab);  // Assign the key prefab to the cell

                // Optionally, name the cells for easier identification
                cell.name = "Cell_" + x + "_" + y;
                cells[x,y] = cellScript;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button click
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10f; // Distance from the camera, adjust as necessary

            // Convert the mouse position to world space
            Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            // Raycast directly to the point where the mouse is clicking
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 hitPoint = hit.point;
                Vector2Int gridCoord = new Vector2Int(Mathf.RoundToInt(hitPoint.x / cellSpacing), Mathf.RoundToInt(hitPoint.z / cellSpacing));
                Debug.Log("Cell clicked at: " + gridCoord);
                tMP_Text.text = gridCoord.ToString();
                cells[gridCoord.x,gridCoord.y].ClickingKeyMaking();
            }
        }
    }
}

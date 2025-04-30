using UnityEngine;
using TMPro;

public class GridManager_4 : MonoBehaviour
{
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private TMP_Text tMP_Text;

    [SerializeField] private Material waterMaterial;
    [SerializeField] private Material fireMaterial;
    [SerializeField] private Material grassMaterial;
    [SerializeField] private Material roadMaterial;

    private int gridSize = 10;
    private float cellSpacing = 1f;
    private Cell[,] cells;

    void Start()
    {
        cells = new Cell[gridSize, gridSize];
        CreateGrid();
    }

    void CreateGrid()
    {
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                Vector3 position = new Vector3(x * cellSpacing, 0, y * cellSpacing);
                GameObject cell = Instantiate(cellPrefab, position, Quaternion.identity, transform);

                Cell cellScript = cell.AddComponent<Cell>();
                //cellScript.SetKeyPrefab(keyPrefab);
                cellScript.SetTypeMaterials(waterMaterial, fireMaterial, grassMaterial, roadMaterial);

                // Apply either water or fire material randomly (for testing purposes)
                Material chosenMat = Random.value > 0.75f ? waterMaterial : Random.value > 0.5f ? fireMaterial :  Random.value > 0.25f ? grassMaterial : roadMaterial;
                cellScript.SetMaterial(chosenMat);

                cell.name = "Cell_" + x + "_" + y;
                cells[x, y] = cellScript;
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 hitPoint = hit.point;
                Vector2Int gridCoord = new Vector2Int(Mathf.RoundToInt(hitPoint.x / cellSpacing), Mathf.RoundToInt(hitPoint.z / cellSpacing));

                if (gridCoord.x >= 0 && gridCoord.x < gridSize && gridCoord.y >= 0 && gridCoord.y < gridSize)
                {
                    Cell clickedCell = cells[gridCoord.x, gridCoord.y];

                    string type = clickedCell.GetCellTypeByMaterial();
                    Debug.Log($"Clicked Cell Type: {type}");
                    string explainStr = "";
                    if(type == "Water")
                    {
                        explainStr = "A calm and stable environment. \nReduces fire risk and slows unit movement.";
                    }
                    else if(type == "Fire")
                    {
                        explainStr = "An unstable and dangerous environment. \nRapidly spreads and consumes nearby terrain.";
                    }
                    else if(type == "Grass")
                    {
                        explainStr = "A fertile and natural environment. \nSupports growth but is vulnerable to fire.";
                    }
                    else if(type == "Road")
                    {
                        explainStr = "A constructed and efficient environment. \nEnables fast travel but limited natural protection.";
                    }
                    else{
                        explainStr = "No explanation available.";
                    }
                    tMP_Text.text = $"Coord: {gridCoord}, Type: {type} \n {explainStr}";
                }
            }
        }
    }
}

using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class GridManager : MonoBehaviour
{
    // Singleton instance for easy access from other scripts
    public static GridManager Instance { get; private set; }

    // Reference to the prefab used to create each cell
    [SerializeField]
    GameObject cellPrefab;

    // UI text element to display grid coordinates
    [SerializeField]
    TMP_Text indeceseText;

    // Grid dimensions
    [SerializeField]
    int gridW = 10;
    [SerializeField]
    int gridH = 5;

    // Materials for visual feedback when hovering over cells
    [SerializeField]
    Material hoverMaterial;
    [SerializeField]
    Material defaultMaterial;

    // Cell size parameters
    float cellWidth = 1f;
    float cellHeight = 1f;
    float spacing = 0.0f;

    // Maximum height value for cells (used for normalization)
    public float maxHeight = 5;

    // Timing control for simulation updates
    float nextSimulationStepTimer = 0;
    float nextSimulationStepRate = 0.25f;

    // The 2D array that stores all cell references
    public CellScript[,] grid;
    
    // Tracks which cell the mouse is currently hovering over
    public CellScript currentHoverCell;

    // Initializes the singleton instance
    private void Awake()
    {
        // Standard singleton pattern implementation
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Prevent duplicate instances
            return;
        }

        Instance = this;
    }

    // Cleans up the singleton reference when destroyed
    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    // Called once when the script is enabled
    void Start()
    {
        GenereateGrid();
    }

    // Called every frame
    void Update()
    {
        // Handle simulation timing
        nextSimulationStepTimer -= Time.deltaTime;
        if (nextSimulationStepTimer < 0) {
            SimulationStep();
            nextSimulationStepTimer = nextSimulationStepRate;
        }

        // Handle mouse hover detection
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, float.MaxValue, LayerMask.GetMask("cell"))) {
            // Get the cell that was hit
            CellScript cs = hit.collider.gameObject.GetComponentInParent<CellScript>();
            Vector2Int gridPosition = new Vector2Int(cs.State.x, cs.State.y);
            
            // Update UI with current position
            indeceseText.text = gridPosition.ToString();
            
            // Reset previous hover cell's material if we've moved to a new cell
            if (currentHoverCell != null && currentHoverCell != grid[gridPosition.x, gridPosition.y]) {
                currentHoverCell.gameObject.GetComponentInChildren<Renderer>().material = defaultMaterial;
                currentHoverCell.Unhover();
            }
            
            // Update current hover cell and change its material
            currentHoverCell = grid[gridPosition.x, gridPosition.y];
            currentHoverCell.Hover();

            if (Input.GetMouseButtonDown(0)) {
                currentHoverCell.Clicked();
            }
            if (Input.GetMouseButtonDown(1)) {
                currentHoverCell.RightClicked();
            }
        }
    }

    // Advances the simulation by one step
    void SimulationStep() 
    {
        // Calculate the next state for all cells
        // Store all of the updated cells in a new array so that we don't "contaminate" the cells
        // in state "time" with the cells in state "time + 1".
        CellState[,] nextState = new CellState[gridW, gridH];
        for (int x = 0; x < gridW; x++) {
            for (int y = 0; y < gridH; y++) {
                nextState[x,y] = grid[x,y].GenereateNextSimulationStep();
            }
        }

        // Apply the new states (now that we are done updating all the cells)
        for (int x = 0; x < gridW; x++) {
            for (int y = 0; y < gridH; y++) {
                grid[x,y].State = nextState[x,y];
            }
        }
    }

    // Gets a cell state with wrapping at grid boundaries
    public CellState GetCellStateByIndexWithWrap(int x, int y) {
        // Wrap coordinates to stay within grid bounds
        x = (x + gridW) % gridW;
        y = (y + gridH) % gridH;
        return grid[x,y].State;
    }

    // Returns null if it is out of bounds
    public CellState GetCellStateByIndex(int x, int y) {
        if (x < gridW && x >= 0 && y < gridH && y >= 0) {
            return grid[x,y].State;
        }
        return null;
    }

    // Gets all cell states within a specified range around a center cell.
    public List<CellState> GetCellStatesInRange(int centerX, int centerY, int rangeX, int rangeY) {
        List<CellState> cellStates = new List<CellState>();
        
        // Loop through all cells in the rectangular range around the center cell
        for (int x = centerX - rangeX; x <= centerX + rangeX; x++) {
            for (int y = centerY - rangeY; y <= centerY + rangeY; y++) {
                // Skip cells that are outside the grid boundaries
                if (x < 0 || x >= gridW || y < 0 || y >= gridH) {
                    continue;
                }
                
                // Skip the center cell since we only want neighbors
                if (x == centerX && y == centerY) continue;
                
                // Add the neighbor's state to our collection
                cellStates.Add(grid[x, y].State);
            }
        }
        
        return cellStates;
    }

    // Converts a world position to grid indices
    Vector2Int WorldPointToGridIndices(Vector3 worldPoint) {
        Vector2Int gridPosition = new Vector2Int();
        gridPosition.x = Mathf.FloorToInt(worldPoint.x / (cellWidth + spacing));
        gridPosition.y = Mathf.FloorToInt(worldPoint.z / (cellHeight + spacing));
        return gridPosition;
    }

    // Creates the grid of cells
    public void GenereateGrid() {
        // Clear any existing cells
        for (int i = transform.childCount-1; i >= 0; i--) {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
        
        // Initialize the grid array
        grid = new CellScript[gridW, gridH];
        
        // Create each cell in the grid
        for (int x = 0; x < gridW; x++) {
            for (int y = 0; y < gridH; y++) {
                // Calculate position based on cell size and spacing
                Vector3 pos = new Vector3((cellWidth+spacing) * x, 0, (cellHeight+spacing) * y);
                
                // Instantiate the cell and get its script component
                GameObject cell = Instantiate(cellPrefab, pos, Quaternion.identity);
                CellScript cs = cell.GetComponent<CellScript>();
                
                // Initialize cell state with Perlin noise for height variation
                cs.State.height = Mathf.PerlinNoise(x/15f, y/15f) * maxHeight;
                cs.State.x = x;
                cs.State.y = y;
                
                // Set cell size and parent
                cell.transform.localScale = new Vector3(cellWidth, 1, cellHeight);
                cell.transform.SetParent(transform);
                
                // Store reference in the grid array
                grid[x,y] = cell.GetComponent<CellScript>();
            }
        }
    }
}

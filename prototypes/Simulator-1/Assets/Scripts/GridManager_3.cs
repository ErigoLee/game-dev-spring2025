using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class GridManager_3 : MonoBehaviour
{
    public static GridManager_3 Instance { get; private set; }

    [SerializeField] GameObject cellPrefab;
    [SerializeField] TMP_Text indeceseText;
    [SerializeField] Material hoverMaterial;
    [SerializeField] Material defaultMaterial;
    [SerializeField] GameObject selectionPlane; // 📌 선택 표시 Plane

    [SerializeField] int gridW = 10;
    [SerializeField] int gridH = 5;

    float cellWidth = 1;
    float cellHeight = 1;
    float spacing = 0.0f;

    public float maxHeight = 5;

    float nextSimulationStepTimer = 0;
    float nextSimulationStepRate = 0.25f;

    public CellScript[,] grid;
    public CellScript currentHoverCell;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    void Start()
    {
        GenereateGrid();

        // 초기 상태에서 SelectionPlane 비활성화 (선택 전까지)
        if (selectionPlane != null)
        {
            selectionPlane.SetActive(false);
        }
    }

    void Update()
    {
        nextSimulationStepTimer -= Time.deltaTime;
        if (nextSimulationStepTimer < 0)
        {
            SimulationStep();
            nextSimulationStepTimer = nextSimulationStepRate;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, float.MaxValue, LayerMask.GetMask("cell")))
        {
            CellScript cs = hit.collider.gameObject.GetComponentInParent<CellScript>();
            Vector2Int gridPosition = new Vector2Int(cs.State.x, cs.State.y);

            indeceseText.text = gridPosition.ToString();

            if (currentHoverCell != null && currentHoverCell != grid[gridPosition.x, gridPosition.y])
            {
                currentHoverCell.gameObject.GetComponentInChildren<Renderer>().material = defaultMaterial;
                currentHoverCell.Unhover();
            }

            currentHoverCell = grid[gridPosition.x, gridPosition.y];
            currentHoverCell.Hover();

            if (Input.GetMouseButtonDown(0))
            {
                currentHoverCell.Clicked();

                // 📌 셀 클릭 시 SelectionPlane 이동
                if (selectionPlane != null)
                {
                    Vector3 targetPosition = currentHoverCell.transform.position;
                    targetPosition.y += 0.01f; // 셀 위로 살짝 띄움
                    selectionPlane.transform.position = targetPosition;
                    selectionPlane.SetActive(true);
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                currentHoverCell.RightClicked();
            }
        }
    }

    void SimulationStep()
    {
        CellState[,] nextState = new CellState[gridW, gridH];
        for (int x = 0; x < gridW; x++)
        {
            for (int y = 0; y < gridH; y++)
            {
                nextState[x, y] = grid[x, y].GenereateNextSimulationStep();
            }
        }

        for (int x = 0; x < gridW; x++)
        {
            for (int y = 0; y < gridH; y++)
            {
                grid[x, y].State = nextState[x, y];
            }
        }
    }

    public CellState GetCellStateByIndexWithWrap(int x, int y)
    {
        x = (x + gridW) % gridW;
        y = (y + gridH) % gridH;
        return grid[x, y].State;
    }

    public CellState GetCellStateByIndex(int x, int y)
    {
        if (x < gridW && x >= 0 && y < gridH && y >= 0)
        {
            return grid[x, y].State;
        }
        return null;
    }

    public List<CellState> GetCellStatesInRange(int centerX, int centerY, int rangeX, int rangeY)
    {
        List<CellState> cellStates = new List<CellState>();

        for (int x = centerX - rangeX; x <= centerX + rangeX; x++)
        {
            for (int y = centerY - rangeY; y <= centerY + rangeY; y++)
            {
                if (x < 0 || x >= gridW || y < 0 || y >= gridH)
                    continue;

                if (x == centerX && y == centerY)
                    continue;

                cellStates.Add(grid[x, y].State);
            }
        }

        return cellStates;
    }

    Vector2Int WorldPointToGridIndices(Vector3 worldPoint)
    {
        Vector2Int gridPosition = new Vector2Int();
        gridPosition.x = Mathf.FloorToInt(worldPoint.x / (cellWidth + spacing));
        gridPosition.y = Mathf.FloorToInt(worldPoint.z / (cellHeight + spacing));
        return gridPosition;
    }

    public void GenereateGrid()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }

        grid = new CellScript[gridW, gridH];

        for (int x = 0; x < gridW; x++)
        {
            for (int y = 0; y < gridH; y++)
            {
                Vector3 pos = new Vector3((cellWidth + spacing) * x, 0, (cellHeight + spacing) * y);

                GameObject cell = Instantiate(cellPrefab, pos, Quaternion.identity);
                CellScript cs = cell.GetComponent<CellScript>();

                cs.State.height = Mathf.PerlinNoise(x / 15f, y / 15f) * maxHeight;
                cs.State.x = x;
                cs.State.y = y;

                cell.transform.localScale = new Vector3(cellWidth, 1, cellHeight);
                cell.transform.SetParent(transform);

                grid[x, y] = cs;
            }
        }
    }
}

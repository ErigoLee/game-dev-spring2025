using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
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

    //Buttons
    [SerializeField] private Button coffeeBut;
    [SerializeField] private Button bookBut;
    [SerializeField] private Button movieBut;
    [SerializeField] private Button houseBut;
    [SerializeField] private Button deleteBut;
    [SerializeField] private Button startBut;
    [SerializeField] private Button resetBut;

    //prefabs
    [SerializeField] private GameObject coffee;
    [SerializeField] private GameObject book;
    [SerializeField] private GameObject movie;
    [SerializeField] private GameObject house;


    private string nameObj;

    private bool startChecking;

    private List<Cell> cellsObjs;
    void Start()
    {
        cells = new Cell[gridSize, gridSize];
        CreateGrid();

        //addListener
        coffeeBut.onClick.AddListener(OnButtonClickCoffee);
        bookBut.onClick.AddListener(OnButtonClickBook);
        movieBut.onClick.AddListener(OnButtonClickMovie);
        houseBut.onClick.AddListener(OnButtonClickHouse);
        deleteBut.onClick.AddListener(OnButtonClickDelete);
        startBut.onClick.AddListener(OnButtonClickStart);
        resetBut.onClick.AddListener(OnButtonClickReset);
        nameObj = "";
        startChecking = false;

        cellsObjs = new List<Cell>();
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
                // Optionally, name the cells for easier identification
                cell.name = "Cell_" + x + "_" + y;
                cells[x,y] = cellScript;


                
                
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(startChecking){
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
                    if(gridCoord.x >= gridSize || gridCoord.y >= gridSize)
                     return;

                    if(nameObj.Equals("coffee")){//nameObj == "coffee"
                        print("coffee2");
                        Cell cellScript = cells[gridCoord.x,gridCoord.y];
                        //cells[gridCoord.x,gridCoord.y].ClickingKeyMaking();
                        if(cellScript.GettingIsObj()==false){
                            print("coffee3");
                            cellScript.SetKeyPrefab(coffee);  // Assign the key prefab to the cell
                            cellsObjs.Add(cellScript);
                            cellScript.ClickingKeyMaking();
                        }
                    }

                    if(nameObj.Equals("book")){
                        Cell cellScript = cells[gridCoord.x,gridCoord.y];
                        if(cellScript.GettingIsObj()==false){
                            cellScript.SetKeyPrefab(book);  // Assign the key prefab to the cell
                            cellsObjs.Add(cellScript);
                            cellScript.ClickingKeyMaking();
                        }
                    
                    }

                    if(nameObj.Equals("movie")){
                        Cell cellScript = cells[gridCoord.x,gridCoord.y];
                        if(cellScript.GettingIsObj()==false){
                            cellScript.SetKeyPrefab(movie);  // Assign the key prefab to the cell
                            cellsObjs.Add(cellScript);
                            cellScript.ClickingKeyMaking();
                        }
                    
                    }
                    if(nameObj.Equals("house")){
                        Cell cellScript = cells[gridCoord.x,gridCoord.y];
                        if(cellScript.GettingIsObj()==false){
                            cellScript.SetKeyPrefab(house);  // Assign the key prefab to the cell
                            cellsObjs.Add(cellScript);
                            cellScript.ClickingKeyMaking();
                        }
                    
                     }


                    
                }
            }
        }
        
    }


    void OnButtonClickCoffee(){
        nameObj = "coffee";
        print("coffee");
    }

    void OnButtonClickBook(){
        nameObj = "book";
    }

    void OnButtonClickMovie(){
        nameObj = "movie";
    }
    void OnButtonClickHouse(){
        nameObj = "house";
    }

    void OnButtonClickDelete(){
        nameObj = "";
    }

    void OnButtonClickStart(){
        startChecking = true;
    }

    void OnButtonClickReset(){
        startChecking = false;
       
        int size = cellsObjs.Count;
        for(int i=0;i<size;i++){
            cellsObjs[i].SettingIsObj();
            cellsObjs[i].RemoveObj();
        }
        cellsObjs.Clear();
    }
}

using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager2 : MonoBehaviour
{
    public Tilemap tilemap; // 타일맵을 할당할 변수

    void Update()
    {
        // 마우스 클릭 입력 처리
        if (Input.GetMouseButtonDown(0)) // 좌클릭
        {
            // 마우스 위치를 월드 좌표로 얻음
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // 월드 좌표를 Grid 좌표로 변환
            Vector3Int gridPosition = tilemap.WorldToCell(worldPosition);

            // 변환된 Grid 좌표 출력
            Debug.Log("클릭한 위치의 Grid 좌표: " + gridPosition);
        }
    }
}
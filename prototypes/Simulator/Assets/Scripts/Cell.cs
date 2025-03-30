using UnityEngine;
using TMPro;

public class Cell : MonoBehaviour
{
    private GameObject keyPrefab;  // Reference to the key prefab

    
    public void SetKeyPrefab(GameObject keyObj){
        if (keyObj != null){
            keyPrefab = keyObj;
        }
    }



    public void ClickingKeyMaking(){
        Debug.Log("Cell clicked: " + gameObject.name);
        // Instantiate the key prefab on the clicked cell's position
        Quaternion rotation = Quaternion.Euler(0, 0, 90f);
        Instantiate(keyPrefab, transform.position + Vector3.up * 1.5f, rotation);  // Offset to position above the cell
    }
}
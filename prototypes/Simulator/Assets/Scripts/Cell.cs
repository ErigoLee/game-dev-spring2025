using UnityEngine;
using TMPro;

public class Cell : MonoBehaviour
{
    private GameObject keyPrefab;  // Reference to the key prefab
    private GameObject actualKeyPrefab;
    private bool isObj;

    void Start(){
        isObj = false;
    }
    public void SetKeyPrefab(GameObject keyObj){
        if (keyObj != null){
            keyPrefab = keyObj;
        }
    }



    public void ClickingKeyMaking(){
        Debug.Log("Cell clicked: " + gameObject.name);
        // Instantiate the key prefab on the clicked cell's position
        Quaternion rotation = Quaternion.Euler(0, 0, 90f);
        actualKeyPrefab = Instantiate(keyPrefab, transform.position + Vector3.up * 1.5f, rotation);  // Offset to position above the cell
        isObj = true;
    }

    public GameObject GettingGameObj(){
        return actualKeyPrefab;
    }

    public bool GettingIsObj(){
        return isObj;
    }

    public void RemoveObj(){
        if (actualKeyPrefab != null){
            Destroy(actualKeyPrefab);
        }
    }

    public void SettingIsObj(){
        isObj = false;
    }
}
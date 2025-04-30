using UnityEngine;

public class Cell : MonoBehaviour
{
    private GameObject keyPrefab;
    private Renderer rend;

    private Material waterMaterial;
    private Material fireMaterial;
    private Material grassMaterial;
    private Material roadMaterial;

    private void Awake()
    {
        rend = transform.Find("HeightCube").Find("Cube").GetComponent<Renderer>();
    }

    public void SetKeyPrefab(GameObject prefab)
    {
        keyPrefab = prefab;
    }

    public void SetMaterial(Material mat)
    {
        transform.Find("HeightCube").Find("Cube").GetComponent<Renderer>().material = mat;
    }

    public string GetCellTypeByMaterial()
    {
        if (rend.material.name.StartsWith(waterMaterial.name))
            return "Water";
        else if (rend.material.name.StartsWith(fireMaterial.name))
            return "Fire";
        else if (rend.material.name.StartsWith(grassMaterial.name))
            return "Grass";
        else if (rend.material.name.StartsWith(roadMaterial.name))
            return "Road";
        else
            return "Unknown";
    }

    public void SetTypeMaterials(Material water, Material fire, Material grass, Material road)
    {
        waterMaterial = water;
        fireMaterial = fire;
        grassMaterial = grass;
        roadMaterial = road;
    }

    /*
    public void ClickingKeyMaking()
    {
        if (keyPrefab != null)
        {
            Instantiate(keyPrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);
        }
    }
    */
}

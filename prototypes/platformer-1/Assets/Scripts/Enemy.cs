using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private GameObject coin;
    private GameManager gameManager;
    private Vector3 pos;
    private Quaternion rot;
    void Start()
    {
        GameObject obj = GameObject.FindWithTag("GM");
        gameManager = obj.GetComponent<GameManager>();
    }

    void Update()
    {
        
    }

    public void MakingCoin(){
        float x = transform.position.x + 2.7f;
        float z = transform.position.z;
        int level = gameManager.GettingLevel();
        float y = 1.5f;
        switch (level){
            case 1:
                y = 1.5f;
            break;
            case 2:
                y = transform.position.y + 0.7f;
            break;
        }
        pos = new Vector3(x,y,z); //1.5f
        gameManager.GettingEnemies();
        GameObject _coin = Instantiate(coin,pos,Quaternion.Euler(0f, 90f, 90f)); 
        gameManager.settingCoinObj(_coin);
    }

    
}

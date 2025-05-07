using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //background
    [SerializeField] private AudioClip startSound;
    [SerializeField] private AudioClip tutoSound;
    [SerializeField] private AudioClip playerSound;
    [SerializeField] private AudioClip deadSound;
    [SerializeField] private AudioClip bulletShootSound;
    [SerializeField] private AudioClip bulletBumpSound;
    [SerializeField] private AudioClip rewardSound;
    [SerializeField] private AudioClip jumpSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if(audioSource != null)
        {
            audioSource.clip = startSound;
            audioSource.loop = true;
            audioSource.Play();
        }
    }


    public void StartBG(){
        if(audioSource != null)
        {
            audioSource.clip = startSound;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void TutoBG(){
        if(audioSource != null)
        {
            audioSource.clip = tutoSound;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void PlayerBG(){
        if(audioSource != null)
        {
            audioSource.clip = playerSound;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void DeadBG(){
        if(audioSource != null)
        {
            audioSource.clip = deadSound;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
    
    public void ShootBulletEffect(){
        GameObject tempGO = new GameObject("shootEffect");
        AudioSource shootSource = tempGO.AddComponent<AudioSource>();
        shootSource.clip = bulletShootSound;
        shootSource.Play();
        
        Destroy(tempGO,3f);
    }

    public void ShootBumpEffect(){
        GameObject tempGO2 = new GameObject("shootBumpEffect");
        AudioSource shootSource = tempGO2.AddComponent<AudioSource>();
        shootSource.clip = bulletBumpSound;
        shootSource.Play();
        
        Destroy(tempGO2,3f);
    }

    public void RewardEffect(){
        GameObject tempGO3 = new GameObject("rewardEffect");
        AudioSource shootSource = tempGO3.AddComponent<AudioSource>();
        shootSource.clip = rewardSound;
        shootSource.Play();
        
        Destroy(tempGO3,3f);
    }

    public void JumpEffect(){
        GameObject tempGO4 = new GameObject("jumpEffect");
        AudioSource shootSource = tempGO4.AddComponent<AudioSource>();
        shootSource.clip = jumpSound;
        shootSource.Play();
        
        Destroy(tempGO4,3f);
    }
}

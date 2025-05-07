using UnityEngine;

public class CharacterAnim : MonoBehaviour
{
    private Animation anim;

    void Start()
    {
        anim = GetComponent<Animation>();

        /*
        // 각 애니메이션 클립별 wrapMode 설정
        anim["Idle"].wrapMode = WrapMode.Loop;
        anim["Walk"].wrapMode = WrapMode.Loop;
        anim["CharacterArmature|Dagger_Attack"].wrapMode = WrapMode.Once;
        anim["Death"].wrapMode = WrapMode.ClampForever;

        anim.Play("Idle");
        */
        anim = GetComponent<Animation>();
    
        Debug.Log("등록된 애니메이션 목록:");
        foreach (AnimationState state in anim)
        {
            Debug.Log(state.name);
        }
    }

    void Update()
    {
        /*
        // Death 상태일 경우 다른 애니메이션 무시
        if (anim.IsPlaying("Death")) return;

        // 공격 애니메이션 우선 적용
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.CrossFade("CharacterArmature|Dagger_Attack");
            return;
        }

        // 걷기 키 입력 시 Walk
        if (Input.GetKey(KeyCode.W))
        {
            if (!anim.IsPlaying("CharacterArmature|Dagger_Attack"))
                anim.CrossFade("Walk");
        }
        else
        {
            // Idle 복귀 (공격/죽음/걷기 외일 때만)
            if (!anim.IsPlaying("CharacterArmature|Dagger_Attack"))
                anim.CrossFade("Idle");
        }

        // 죽음 키 입력 시
        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.CrossFade("Death");
        }
        */
    }
}

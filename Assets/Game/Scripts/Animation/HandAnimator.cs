using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class HandAnimator : MonoBehaviour
{
    private Animator _animator;

    private readonly string _applyingLipstickAnimation = "��������� ������";
    private readonly string _applyingCreamAnimation = "��������� ����� �� ����";
    private readonly string _applyingBlushAnimation = "��������� �����";

    private Dictionary<string, float> _animationLength = new();


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        //UpdateAnimClipTimes();
    }

    public void UpdateAnimClipTimes()
    {
        AnimationClip[] clips = _animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            _animationLength.Add(clip.name, clip.length);

        }
    }

    public float PlayApplyCreamAnimation()
    {
        Debug.Log($"Playing apply \"{_applyingCreamAnimation}\" animaiton");
        return 0.2f;
    }

    public float PlayApplyBlushAnimation()
    {
        Debug.Log($"Playing apply \"{_applyingBlushAnimation}\" animaiton");
        return 0.2f;
    }

    public float PlayApplyLipstickAnimation()
    {
        Debug.Log($"Playing apply \"{_applyingLipstickAnimation}\" animaiton");
        return 0.2f;
    }
}
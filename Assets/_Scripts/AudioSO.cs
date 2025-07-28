using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName="Audio", menuName = "SO/CombinedAudioClips")]
public class AudioSO : ScriptableObject
{
    public AudioClip flip;
    public AudioClip match;
    public AudioClip mismatch;

}

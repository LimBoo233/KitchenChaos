using UnityEngine;

[CreateAssetMenu()]
public class AudioClipRefsSO : ScriptableObject
{
    [SerializeField] private AudioClip[] chop;
    [SerializeField] private AudioClip[] deliveryFailed;
    [SerializeField] private AudioClip[] deliverySuccess;
    [SerializeField] private AudioClip[] footstep;
    [SerializeField] private AudioClip[] objectDrop;
    [SerializeField] private AudioClip[] objectPickUp;
    [SerializeField] private AudioClip stoveSizzle;
    [SerializeField] private AudioClip[] trash;
    [SerializeField] private AudioClip[] warning;
    
    public AudioClip[] GetChop => chop;
    public AudioClip[] GetDeliveryFailed => deliveryFailed;
    public AudioClip[] GetDeliverySuccess => deliverySuccess;
    public AudioClip[] GetFootstep => footstep;
    public AudioClip[] GetObjectDrop => objectDrop;
    public AudioClip[] GetObjectPickUp => objectPickUp;
    public AudioClip GetStoveSizzle => stoveSizzle;
    public AudioClip[] GetTrash => trash;
    public AudioClip[] GetWarning => warning;
}

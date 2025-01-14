// using UnityEngine;
//
// namespace Match3Linked.Core
// {
//     [RequireComponent(typeof(AudioSource))]
//     public class SoundManager : Singleton<SoundManager>
//     {
//         [SerializeField] public AudioClip selectionClip;
//         [SerializeField] public AudioClip despawnClip;
//         [SerializeField] public AudioClip spawnClip;
//         
//         private AudioSource _audioSource;
//         
//         protected override void Awake()
//         {
//             base.Awake();
//             _audioSource = GetComponent<AudioSource>();
//         }
//         
//         
//     }
// }
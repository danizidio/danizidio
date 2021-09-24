using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Give a Name", menuName = "Player Class", order = 1)]
public class ScriptableCharacters : ScriptableObject
{
    [Header("HERO!!")]

    [SerializeField] int _id;
    public int ID { get { return _id; } }

    [SerializeField] float _life;
    public float Life { get { return _life; } }

    [SerializeField] int _attack;
    public int Attack { get { return _attack; } }

    [SerializeField] int _defense;
    public int Defense { get { return _defense; } }
    [SerializeField] float _criticalAttack;
    public float CriticalAttack { get { return _criticalAttack; } }

    [SerializeField] float _moveSpeed;
    public float MoveSpeed { get { return _moveSpeed; } }

    [SerializeField] float _jumpSpeed;
    public float JumpSpeed { get { return _jumpSpeed; } }

    [SerializeField] GameObject _dust;
    public GameObject Dust { get { return _dust; } }

    [SerializeField] AudioClip[] _soundFx;
    public AudioClip[] SoundFx { get { return _soundFx; } }

    [SerializeField] float _pushX;
    public float PushX { get { return _pushX; } set { _pushX = value; } }
    
    [SerializeField] float _pushY;
    public float PushY { get { return _pushY; } set { _pushY = value; } }
}

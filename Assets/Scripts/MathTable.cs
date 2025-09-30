using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathTable : MonoBehaviour
{
    [System.NonSerialized] public bool isCorrect = false;
    [System.NonSerialized] public MathGate gate;
    [SerializeField] private Vector3 spinSpeed;
    [SerializeField] private float spinDuration = 1f;
    [SerializeField] private AnimationCurve spinDecline;


    private float spin = 0f;
    private Player _player;
    private TextMesh textMesh;
    private MeshRenderer _renderer;
    private Transform trs;

    public void Initialize(string answer, bool isCorrect)
    {
        textMesh = GetComponentInChildren<TextMesh>();
        _renderer = GetComponent<MeshRenderer>();
        _player = FindObjectOfType<Player>();
        trs = transform;
        textMesh.text = answer;
        this.isCorrect = isCorrect;
    }
    private void Update()
    {
        if (spin > 0f)
        {
            spin = Mathf.MoveTowards(spin, 0f, Time.deltaTime / spinDuration);
            trs.Rotate(spinSpeed * spinDecline.Evaluate(1f - spin) * Time.deltaTime, Space.Self);
        }
    }

    public void SetMaterial(Material mat)
    {
        _renderer.sharedMaterial = mat;
    }
    public void SetAlpha(float alpha)
    {
        Color col = textMesh.color;
        col.a = alpha;
        textMesh.color = col;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gate.answered) return;
        if (other.GetComponent<Player>() != null) {
            if (isCorrect)
            {
                gate.OnCorrectAnswer();
                _player.TakeScore();
                spin = 1f;
            }
            else
            {
                gate.OnWrongAnswer();
                _player.TakeDamage();
                spin = 1f;
            }
        }
        
    }
}

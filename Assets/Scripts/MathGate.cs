using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MathGate : MonoBehaviour
{
    public delegate void EmptyHandler();
    public static event EmptyHandler onAnswer;

    [SerializeField] private Material _regularMaterial, _correctMaterial, _wrongMaterial;
    [SerializeField] private float displayDistance = 10f;
    [SerializeField] private TextMesh questionText;
    [SerializeField] private AudioClip _damage, _score;

    private PlayerMovement _player;
    private int _maxNumber, _minNumber;
    private MathTable[] tabs = new MathTable[0];
    private Transform cameraTrs;
    private AudioSource _audioSource;
    private float lastAlpha = 0f;
    private float alpha = 0f;
    [System.NonSerialized] public bool answered = false;
    private Transform trs;
    private bool building = false;

    private void Awake()
    {
        trs = transform;
        _player = FindObjectOfType<PlayerMovement>();
        cameraTrs = Camera.main.transform;
        tabs = GetComponentsInChildren<MathTable>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Build()
    {
        int a = 0;
        int b;
        if (Stats.Level == 10) b = Random.Range(1, 9);
        else b = Stats.Level;
        string[] operations = new string[] { "+", "-", "x" };
        int answer = 0;
        a = Random.Range(1, 9);
        if (Stats.PreviousNumber == a) 
        {
            a = Random.Range(1, 9);
        } 
        else Stats.PreviousNumber = a;
        switch (Stats.Option)
        {
            case 0:
                answer = a + b;
                _maxNumber = b + 9;
                break;
            case 1:
                answer = a - b;
                _maxNumber = 18;
                break;
            case 2:
                answer = a * b;
                _maxNumber = b * 9;
                break;
        }
        questionText.text = a + " " + operations[Stats.Option] + " " + b;
        int correctTab = Random.Range(0, tabs.Length);
        List<int> numbers = new List<int>();
        numbers.Add(answer);
        for (int i = 0; i < tabs.Length; i++)
        {
            tabs[i].gate = this;
            if (i == correctTab) tabs[i].Initialize(answer.ToString(), true);
            else
            {
                int randomAnswer = Random.Range(1, _maxNumber);
                while (numbers.Contains(randomAnswer)) 
                    randomAnswer = Random.Range(1, _maxNumber);
                tabs[i].Initialize(randomAnswer.ToString(), false);
                numbers.Add(randomAnswer);
            }
            tabs[i].SetMaterial(_regularMaterial);
            tabs[i].SetAlpha(0f);
        }
        SetTextAlpha(questionText, 0f);
        building = true;
    }


    private void Update()
    {
        if (Vector3.Distance(trs.position, cameraTrs.position) < displayDistance * 1.1 && !building) Build();
        if (!answered)
        {
            if (Vector3.Distance(trs.position, cameraTrs.position) <= displayDistance) alpha = Mathf.MoveTowards(alpha, 1f, Time.deltaTime * _player.GetSpeed() * 0.1f);
            else alpha = Mathf.MoveTowards(alpha, 0f, Time.deltaTime);
        }
        else
        {
            alpha = Mathf.MoveTowards(alpha, 0f, Time.deltaTime * 0.1f);
        }
        if (alpha != lastAlpha)
        {
            SetTextAlpha(questionText, alpha);
            for (int i = 0; i < tabs.Length; i++) tabs[i].SetAlpha(alpha);
            lastAlpha = alpha;
        }
    }

    private void SetTextAlpha(TextMesh tm, float alpha)
    {
        Color col = tm.color;
        col.a = alpha;
        tm.color = col;
    }

    public void OnWrongAnswer()
    {
        _audioSource.PlayOneShot(_damage);
        questionText.gameObject.SetActive(false);
        for (int i = 0; i < tabs.Length; i++)
        {
            if (tabs[i].isCorrect) tabs[i].SetMaterial(_correctMaterial);
            else tabs[i].SetMaterial(_wrongMaterial);
        }
        answered = true;
        if (onAnswer != null) onAnswer();
    }

    public void OnCorrectAnswer()
    {
        _audioSource.PlayOneShot(_score);
        questionText.gameObject.SetActive(false);
        for (int i = 0; i < tabs.Length; i++)
        {
            if (tabs[i].isCorrect) tabs[i].SetMaterial(_correctMaterial);
        }
        answered = true;
        if (onAnswer != null) onAnswer();
    }
}


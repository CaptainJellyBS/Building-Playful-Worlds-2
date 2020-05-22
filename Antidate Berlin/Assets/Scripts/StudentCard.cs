using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StudentCard : MonoBehaviour
{
    public Student student;
    //public Vector3 studentPos;
    public RectTransform studPos;
    public bool studentOnCard;

    [SerializeField]
    Text studName;
    [SerializeField]
    Text gender;
    [SerializeField]
    Text intoList;
    [SerializeField]
    Image studImg;

    GameController control;

    void Awake()
    {
        control = FindObjectOfType<GameController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(student != null) { GenerateCard(); }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateCard()
    {
        studName.text = student.studentName;
        gender.text = student.gender.ToString();
        studImg.sprite = student.GetComponent<SpriteRenderer>().sprite;
        string temp = string.Empty;
        studentOnCard = true;

        //student.cardPos = studPos.TransformPoint(0, 0, 1);
        student.card = this;

        foreach (Gender g in student.isInto)
        { temp += g.ToString() + "\n"; }

        intoList.text = temp;
        GetComponent<BoxCollider2D>().enabled = false;
    }
}

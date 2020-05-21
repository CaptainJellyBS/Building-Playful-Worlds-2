using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Gender { Boy, Girl, Enby}
public class Student : MonoBehaviour
{
    public Gender gender;
    public List<Gender> isInto;
    public Room room;
    public Bed bed;
    public bool selected = false;
    public Vector3 startPos;
    public StudentCard card;
    public string studentName;
    public bool isKissing;
    Student kisser;

    public Sprite[] sprites;

    [SerializeField]
    StatusAnimation kissLeft;
    [SerializeField]
    StatusAnimation kissRight;
    [SerializeField]
    StatusAnimation sleepZ;
    [SerializeField]
    float speed = 1.0f;

    GameController control;

    void Awake()
    {
        control = FindObjectOfType<GameController>();
        card = FindObjectOfType<StudentCard>();
    }

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length - 1)];
        studentName = GenerateName();

    }

    // Update is called once per frame
    void Update()
    {
        if (kisser != null)
        {
            if (Vector3.Distance(transform.position, kisser.transform.position + new Vector3(0.3f, 0, 0)) > 0.02)
            {
                transform.position += (kisser.transform.position + new Vector3(0.3f, 0, 0) - transform.position).normalized * speed * Time.deltaTime;
            }
            else
            {
                kissRight.gameObject.SetActive(true);
                kisser.kissLeft.gameObject.SetActive(true);
            }
            return;
        }

        if (selected)
        {
            Vector3 temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(temp.x, temp.y, 1);
        }
        else
        {
            if (bed != null)
            {
                transform.position = bed.transform.position;
            }
            else
            {
                transform.position = startPos;
            }
        }

        
    }

    /// <summary>
    /// Return true if the students can be in the same room without kissing, return false otherwise
    /// </summary>
    /// <param name="other">The student to be compared to</param>
    /// <returns></returns>
    public bool IsStudentCompatible(Student other)
    {
        bool isIntoOther = false;
        bool isOtherIntoMe = false;

        foreach(Gender g in other.isInto)
        {
            isOtherIntoMe = gender == g || isOtherIntoMe;
        }

        foreach (Gender g in isInto)
        {
            isIntoOther = other.gender == g || isIntoOther;
        }

        return !(isIntoOther && isOtherIntoMe);
    }

    string GenerateName()
    {
        string[] boyNames = new string[] { "Jack", "Oscar", "Miles", "Zach", "Bob", "Miles", "Oliver", "William", "Jeffrey"};
        string[] girlNames = new string[] { "Emma", "Ava", "Liv", "Rose", "Lily", "Mia", "Astrid", "Liz", "Lucy" };
        string[] enbyNames = new string[] {"Harley", "Alex", "Sam", "Rory", "Skye", "Jamie", "MOnroe", "Jesse", "Jordan" };

        string[] lastNames = new string[] { "Smith", "Johnson", "Allen", "Young", "Ross", "Powell", "Murphy", "Bell", "Adams", "Anderson", "Parker", "Watson" };

        string generatedName = string.Empty;

        switch (gender)
        {
            case Gender.Boy: generatedName += boyNames[Random.Range(0,boyNames.Length)]; break;
            case Gender.Girl: generatedName += girlNames[Random.Range(0, girlNames.Length)]; break;
            case Gender.Enby: generatedName +=  enbyNames[Random.Range(0, enbyNames.Length)]; break;
            default: throw new System.ArgumentException("wtf did you do to that Enum");
        }

        generatedName += " ";
        generatedName += lastNames[Random.Range(0, lastNames.Length)];
        return generatedName;
    }

    #region sprite thingies

    public void KissAnim(Student other)
    {
        if (isKissing || other.isKissing) { return; }
        isKissing = true;
        other.isKissing = true;
        kisser = other;

        //Odd bug caused both animations to be active, ugly code to prevent it.
        sleepZ.gameObject.SetActive(false);
        other.sleepZ.gameObject.SetActive(false);
    }

    void ActiveKissAnim(Student other)
    {
        kissRight.gameObject.SetActive(true);
        other.kissLeft.gameObject.SetActive(true);

        //Odd bug caused both animations to be active, ugly code to prevent it.
        sleepZ.gameObject.SetActive(false);
        other.sleepZ.gameObject.SetActive(false);
    }

    public void SleepAnim()
    {
        if (isKissing) { return; }
        sleepZ.gameObject.SetActive(true);
    }

    #endregion

    #region input
    void OnMouseUp()
    {
        Debug.Log("clicked");
        if (selected) { throw new System.ArgumentException("Student somehow clicked while selected"); }
        Select();
    }

    void OnMouseEnter()
    {
        if(control.currentStudent != null) { return; }
        card.student = this;
        card.GenerateCard();
    }

    /// <summary>
    /// Actions to perform when student is selected
    /// </summary>
    public void Select()
    {
        if(control.currentStudent != null) { return; }

        card = FindObjectOfType<StudentCard>();
        card.student = this;
        card.GenerateCard();

        if (room != null)
        {
            room.students.Remove(this);
            room = null;
            bed = null;

        }

        control.currentStudent = this;
        control.GetComponent<BoxCollider2D>().enabled = true;
        selected = true;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    /// <summary>
    /// Actions to perform when student is unselected
    /// </summary>
    public void Unselect()
    {
        selected = false;
        control.currentStudent = null;
        GetComponent<BoxCollider2D>().enabled = true;
        control.GetComponent<BoxCollider2D>().enabled = false;

    }
    #endregion
}

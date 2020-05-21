using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    public Room room;
    public Student student;
    GameController control;

    void Awake()
    {
        control = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    /// <summary>
    ///If clicked while holding a student, put that student on this bed and in the room student list.
    /// </summary>
    void OnMouseUp()
    {
        Debug.Log("clicked");

        if (control.currentStudent == null) { return; }

        student = control.currentStudent;
        room.students.Add(student);
        student.bed = this;
        student.room = room;

        student.Unselect();
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<Student> students;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Bed b in GetComponentsInChildren<Bed>())
        {
            b.room = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Returns false if the room is not compatible, returns true otherwise. Also triggers result sprites.
    /// </summary>
    /// <returns></returns>
    public bool IsRoomCompatible()
    {
        bool success = true;

        for (int i = 0; i < students.Count-1; i++)
        {
            for (int j = i+1; j < students.Count; j++)
            {
                if(!students[i].IsStudentCompatible(students[j]))
                {
                    success = false;
                    students[i].KissAnim(students[j]);

                }
                else
                {
                    students[i].SleepAnim();
                }
            }
        }

        if (!students[students.Count - 1].isKissing) { students[students.Count - 1].SleepAnim(); }

        return success;
    }
}

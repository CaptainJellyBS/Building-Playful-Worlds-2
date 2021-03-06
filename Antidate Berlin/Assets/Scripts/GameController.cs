﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public List<Room> rooms;
    public List<Student> students;
    public Student currentStudent;

    public string nextScene;

    GameObject success;
    GameObject failed;


    // Start is called before the first frame update
    void Awake()
    {
        foreach (Room r in FindObjectsOfType<Room>())
        {
            rooms.Add(r);
        }

        foreach (Student s in FindObjectsOfType<Student>())
        {
            students.Add(s);
        }

        success = GameObject.Find("LevelComplete");
        success.SetActive(false);
        failed = GameObject.Find("LevelFailed");
        failed.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void EndLevel()
    {
        foreach (Student s in students)
        {
            if (s.bed == null) { Debug.Log("Not all students are in a room yet"); return; }
        }
        //Debug.Log(IsLevelComplete());

        if (IsLevelComplete())
        {
            Invoke("NextScene", 5);
        }
        else
        {
            Invoke("RestartScene", 5);
        }

    }

    private void NextScene()
    {
        SceneManager.LoadScene(nextScene);
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public bool IsLevelComplete()
    {
        bool success = true;
        foreach(Room r in rooms)
        {
            if (!r.IsRoomCompatible()) { success = false; }
        }

        ShowEndLevelText(success);

        return success;
    }

    public void OnMouseUp()
    {
        if (currentStudent == null) { throw new System.ArgumentException("Student should not be null when controller box collider is active"); }
        currentStudent.Unselect();
    }

    public void ShowEndLevelText(bool isCompleted)
    {
        if (isCompleted) { success.SetActive(true); return; }
        failed.SetActive(true);
    }
}

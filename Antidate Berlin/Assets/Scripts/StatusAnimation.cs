using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusAnimation : MonoBehaviour
{
    [SerializeField]
    float fps = 25;
    float timer = 0.0f;
    float nextFrameTime;
    int i = 0;

    [SerializeField]
    Sprite[] sprites;

    SpriteRenderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        nextFrameTime = (1 / fps);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= nextFrameTime)
        {
            timer = 0;
            i++;
            i %= sprites.Length;
            renderer.sprite = sprites[i];
        }
    }
}

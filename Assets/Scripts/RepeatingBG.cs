using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBG : MonoBehaviour
{

    [SerializeField] float speed;

    float xPosStart;
    float xPostEnd;
    float counter = 0f;

    bool backgroundLoaded = false;

    // set the start and end points of the background
    void Start()
    {
        xPostEnd = -Camera.main.orthographicSize * Camera.main.aspect * 2;
        xPosStart = Camera.main.orthographicSize * Camera.main.aspect * 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (backgroundLoaded)
        {
            MoveBackground();
        }
        else
        {
            LoadBackground();
        }
    }

    //Move and repeat background
    private void MoveBackground()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (transform.position.x <= xPostEnd)
        {
            Vector2 pos = new Vector2(xPosStart, transform.position.y);
            transform.position = pos;
        }
    }

    private void LoadBackground()
    {
        Vector3 targetPos = new Vector3(transform.position.x, 0);
        transform.position = Vector2.MoveTowards(transform.position, targetPos, 5f * Time.deltaTime);

        counter += 5f * Time.deltaTime;

        if(counter >= 7f)
        {
            backgroundLoaded = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE / POLYMORPHISM / ABSTRACTION
public class MoveLeft : MonoBehaviour
{
    protected GameManager gameManagerScript;
    [SerializeField] protected float m_offsetSpeed = 0; 

    // Start is called before the first frame update
    private void Start()
    {
        DoStart();
    }
    // Update is called once per frame
    private void Update()
    {
        DoUpdate();
    }

    // INHERITANCE
    virtual protected void DoStart()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    virtual protected void DoUpdate()
    {
        if (gameManagerScript.GameIsPlaying)
        {
            DoMoveLeft(m_offsetSpeed);
        }
        if (transform.position.x < gameManagerScript.LeftDestroyBoundary)
        {
            DestroyGameObject();
        }
    }

    // POLYMORPHISM (OVERLOADING), INHERITANCE and ABSTRACTION all in one
    virtual protected void DoMoveLeft()
    {
        transform.Translate(Vector3.left * Time.deltaTime * gameManagerScript.SceneSpeed, Space.World);
    }
    virtual protected void DoMoveLeft(float speedOffset)
    {
        transform.Translate(Vector3.left * Time.deltaTime * (gameManagerScript.SceneSpeed + speedOffset), Space.World);
    }

    virtual protected void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}

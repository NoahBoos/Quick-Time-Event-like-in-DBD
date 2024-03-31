using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class QTE_Cursor_Behaviour : MonoBehaviour
{
    [Header("QTE Cursor related variables")]
    // The GameObject of the QTE Cursor.
    public GameObject qteCursor;
    // The rotation speed of the GameObject we want to see making a rotation.
    public int rotationSpeed;

    // A boolean turning TRUE if the QTE Cursor is colliding a skillcheck.
    public bool onSkillcheck;

    [Header("Pivot point related variables")]
    // The GameObject we want to rotate around.
    public GameObject pivotPoint;

    // Awake is called at the very beginning of the app' execution
    private void Awake()
    {
        // Assigning the GameObject this script is attached to as the qteCursor value.
        qteCursor = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        qteCursor.transform.RotateAround(
            pivotPoint.transform.position, 
            Vector3.back, 
            rotationSpeed * Time.deltaTime
        );
    }
    
    // OnTriggerEnter2D is called whenever the element this script is applied to enter in a collision with another element.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the element which we have collided with wear the tag "QTE skillcheck area", isOnQTE is equal TRUE.
        if (collision.gameObject.tag == "QTE skillcheck area")
        {
            onSkillcheck = true;
        }
    }

    // OnTriggerEnter2D is called whenever the element this script is applied to exit a collision with another element.
    private void OnTriggerExit2D(Collider2D collision)
    {
        // If the element which we have stop collided with wear the tag "QTE skillcheck area", isOnQTE is equal FALSE.
        if (collision.gameObject.tag == "QTE skillcheck area")
        {
            onSkillcheck = false;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class QTE_Skillcheck_Behaviour : MonoBehaviour
{
    [Header("Skillcheck related variables")]
    // An array of skillcheck.
    public GameObject[] skillcheckArray;
    // An int value pointing on the skillcheck currently active.
    public int currentSkillcheck;

    [Header("Pivot point related variables")]
    // A pivot point which is located at the center of the QTE area.
    public GameObject pivotPoint;
    // A vector3 equal to the pivot point position (x, y, z coordonates).
    public Vector3 pivotPointPosition;

    [Header("Input")]
    // A player input C# Class allowing us to call a PlayerInput called "QTE".
    [SerializeField]
    public PIA playerInputActions;

    [Header("Other variables and dependencies")]
    public QTE_Cursor_Behaviour cursorBehaviour;

    // Awake is called at the very beginning of the app' execution
    private void Awake()
    {
        // Get the pivot point transform.position.
        pivotPointPosition = pivotPoint.transform.position;

        // A for loop executing as many times as we have elements in the skillcheckArray[].
        for (int i = 0; i < skillcheckArray.Length; i++)
        {
            // Disabling each skillcheck so they are invisible to the player until they need to be revealed.
            skillcheckArray[i].gameObject.SetActive(false);
            // Initialize each skillcheck position and rotation based on rotation around a pivot point
            skillcheckArray[i].gameObject.transform.RotateAround(pivotPointPosition, Vector3.back, 30 * i);
        }

        // Change currentSkillcheck value to a random value between 0 and the maximum index in the skillcheck array.
        currentSkillcheck = Random.Range(0, skillcheckArray.Length);
        // Find a skillcheck into the skillcheckArray having the value of currentSkillcheck as an index.
        // Enable it GameObject.
        skillcheckArray[currentSkillcheck].gameObject.SetActive(true);

        // Initializing everything related to the player input.
        playerInputActions = new PIA();
        playerInputActions.PlayerInput.Enable();
        // Allow the script to rung the QTE() method each time the QTE action is performed.
        playerInputActions.PlayerInput.QTE.performed += QTE;
    }

    public void QTE(InputAction.CallbackContext context)
    {
        // If the context of the QTE action is performed and onSkillcheck is TRUE
        if (context.performed && cursorBehaviour.onSkillcheck)
        {
            ChangeActiveSkillcheck();
        }
    }

    // Method allowing to disable the current skillcheck and enable a new skillcheck randomly.
    public void ChangeActiveSkillcheck()
    {
        // Turn the current skillcheck GameObject to FALSE.
        skillcheckArray[currentSkillcheck].gameObject.SetActive(false);

        // Change currentSkillcheck value to a random value between 0 and the maximum index in the skillcheck array.
        // Also, we are blocking the value from being itself or itself - 1 or itself + 1, why ?
        // It's avoiding to get skillcheck glue together.
        int randomNumber;
        randomNumber = Random.Range(0, skillcheckArray.Length);
        while (randomNumber == currentSkillcheck ||
            randomNumber == currentSkillcheck - 1 ||
            randomNumber == currentSkillcheck + 1)
        {
            randomNumber = Random.Range(0, skillcheckArray.Length);
        }

        currentSkillcheck = randomNumber;
        // Find a skillcheck into the skillcheckArray having the value of currentSkillcheck as an index.
        // Enable it GameObject.
        skillcheckArray[currentSkillcheck].gameObject.SetActive(true);
    }
}

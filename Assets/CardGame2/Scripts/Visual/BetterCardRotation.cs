using UnityEngine;

/// <summary>
/// This script should be attached to the card game object to display card`s rotation correctly.
/// </summary>
[ExecuteInEditMode]
public class BetterCardRotation : MonoBehaviour
{
    // parent game object for all the card face graphics
    public GameObject cardFront;

    // parent game object for all the card back graphics
    public GameObject cardBack;

    private Camera main;

    private void Awake()
    {
        main = Camera.main;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!cardFront || !cardBack)
        {
            return;
        }
        var frontFacingCamera = IsCardFrontFacingCamera();

        cardFront.SetActive(frontFacingCamera);
        cardBack.SetActive(!frontFacingCamera);
    }

    private bool IsCardFrontFacingCamera()
    {
        return Vector3.Dot(
            cardFront.transform.forward,
            main.transform.position - cardFront.transform.position) < 0;
    }
}
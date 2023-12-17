using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    // Reference to the text instructions
    public Image tutorialImage;
    public Sprite[] tutorialImages;
    public TextMeshProUGUI tutorialHead;
    public TextMeshProUGUI tutorialContent;
    public Button nextButton;
    public Button backButton;

    // The current page index
    private int currentPage = 0;

    // The number of pages in the book
    private int pageCount = 4;

    // The speed of turning pages (in seconds)
    // private float pageSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        // Set the initial page index to zero
        currentPage = 0;
        TurnPage(currentPage);
        // Add listeners to the buttons to handle page navigation
        nextButton.onClick.AddListener(() => TurnPage(1));
        backButton.onClick.AddListener(() => TurnPage(-1));
        // Disable the buttons initially
        nextButton.interactable = true;
        backButton.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow) && currentPage != pageCount - 1)
        {
            TurnPage(1);
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow) && currentPage != 0)
        {
            TurnPage(-1);
        }
    }

    // A method to turn a page up or down based on the current page index and direction
    void TurnPage(int direction)
    {
        currentPage += direction;
        if (currentPage < 0) currentPage = pageCount - 1; // Clamp the current page index to be within the range of pageCount - 1 and 0
        else if (currentPage >= pageCount) currentPage = 0;

        if (currentPage == pageCount - 1) // If reaching the last page, go back to the first page and disable the buttons until reaching it again
        {
            nextButton.interactable = false;
            backButton.interactable = true;
        }
        else if (currentPage == 0) // If reaching the first page, go forward to the last page and enable both buttons until reaching it again
        {
            nextButton.interactable = true;
            backButton.interactable = false;
        }

        tutorialContent.text = GettutorialContent(currentPage); // Set the text instructions based on the current page index
    }

    // A method to get a string of text instructions based on a given current page index, using an array of strings from an XML file or a hard-coded array for demonstration purposes.
    string GettutorialContent(int currentPage)
    {
        string[] tutorialContentArray;
        tutorialContentArray = new string[] { "Welcome to Unity C# tutorial book!", "This book will teach you how to create various types of games using Unity C#.", "You can turn pages up and down by clicking on Next and Back buttons.", "Have fun learning!" };
        switch(currentPage)
        {
            case 0:
                tutorialImage.sprite = tutorialImages[0];
                tutorialHead.text = "Trash";
                return tutorialContentArray[0];
            case 1:
                tutorialImage.sprite = tutorialImages[1];
                tutorialHead.text = "Plastic";
                return tutorialContentArray[1];
            case 2:
                tutorialImage.sprite = tutorialImages[2];
                tutorialHead.text = "Paper";
                return tutorialContentArray[2];
            case 3:
                tutorialImage.sprite = tutorialImages[3];
                tutorialHead.text = "Have fun learning!";
                return tutorialContentArray[3];
            default:
                return "Error!";
        }
    }
}
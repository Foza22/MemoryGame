using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Amount of cards and offset between cards
    public const int gridRows = 2;
    public const int gridCols = 4;
    public const float offsetX = 2f;
    public const float offsetY = 2.5f;

    // Card on scene
    [SerializeField]
    private MemoryCard originalCard;

    // Array of card suits
    [SerializeField]
    private Sprite[] images;

    // Game score
    [SerializeField]
    private TextMesh scoreLabel;

    // Stores revealed cards
    private MemoryCard firstRevealed;
    private MemoryCard secondRevealed;

    // Stores player current result
    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Set the location of first card
        Vector3 startPos = originalCard.transform.position;

        // Array of indicators
        int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3 };
        numbers = ShuffleArray(numbers);

        // Run through every socket in grid
        for (int i = 0; i < gridCols; ++i)
        {
            for (int j = 0; j < gridRows; ++j)
            {
                MemoryCard card;

                // If it is first element - card is original
                if (i == 0 && j == 0)
                {
                    card = originalCard;
                }

                // Else create a copy of card
                else
                {
                    card = Instantiate(originalCard) as MemoryCard;
                }

                // Generate id for card and set it
                int index = j * gridCols + i;
                int id = numbers[index];
                card.SetCard(id, images[id]);

                // Get location of card depending on grid
                float posX = (offsetX * i) + startPos.x;
                float posY = -(offsetY * j) + startPos.y;

                // Set card location
                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }
    }

    private int[] ShuffleArray(int[] numbers)
    {
        // create temporary array and clone original
        int[] newArray = numbers.Clone() as int[];

        // go through every element and swap it with random element
        for (int i = 0; i < newArray.Length; ++i)
        {
            int tmp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }

        // return shuffled array
        return newArray;
    }

    // Check if second card is not revealed
    public bool canReveal
    {
        get { return !secondRevealed; }
    }

    public void CardRevealed(MemoryCard card)
    {
        // if we don't have revealed card - set first
        if (!firstRevealed)
        {
            firstRevealed = card;
        }
        // if we already have one revealed - set second and check for matching
        else
        {
            secondRevealed = card;
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        // make pause 0.5s
        yield return new WaitForSeconds(0.5f);

        // If cards match - increment score and hide cards
        if (firstRevealed.getId == secondRevealed.getId)
        {
            ++score;
            scoreLabel.text = "Score: " + score;

            firstRevealed.gameObject.SetActive(false);
            secondRevealed.gameObject.SetActive(false);
        }

        // Otherway unreveal cards
        else
        {
            firstRevealed.Unreveal();
            secondRevealed.Unreveal();
        }

        // No more revealed maps
        firstRevealed = null;
        secondRevealed = null;
    }

    // Is called on mouse click
    public void Restart()
    {
        // Loads given scene
        SceneManager.LoadScene("SampleScene");
    }
}

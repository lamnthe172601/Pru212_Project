using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelected : MonoBehaviour
{
    private int index;
    [SerializeField] GameObject[] characters;
    [SerializeField] TextMeshProUGUI characterName;
    [SerializeField] GameObject[] characterPrefabs;
    public static GameObject selectedCharacter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        index = 0;
        SelectCharacter();
        DontDestroyOnLoad(selectedCharacter);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PreBtnClick();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextPreBtnClick();
        }
        
    }
    public void PreBtnClick()
    {
        if(index > 0)
        {
            index--;
        }
        else
        {
            index = characters.Length - 1;
        }
        SelectCharacter();
    }
    public void OnPlayClick()
    {
        SceneManager.LoadScene("Level One");
    }
    public void NextPreBtnClick()
    {
        if (index < characters.Length - 1)
        {
            index++;
        }
        else
        {
            index = 0;
        }
        SelectCharacter();
    }
    private void SelectCharacter()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            if (i == index)
            {
                characters[i].GetComponent<SpriteRenderer>().color = Color.white;
                characters[i].GetComponent<Animator>().enabled = true;
                
                selectedCharacter = characterPrefabs[i];
                characterName.text = characterPrefabs[i].name;
            }
            else
            {
                characters[i].GetComponent<SpriteRenderer>().color = Color.black;
                characters[i].GetComponent<Animator>().enabled = false;
            }
        }
    }
}

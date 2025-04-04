using UnityEngine;
using UnityEngine.TextCore.Text;

public class ChangeInstancePlayer : MonoBehaviour
{
    public GameObject characterY;
    public float scaleRate = 0.65f;
    void Start()
    {
        if (CharacterSelected.selectedCharacter != null)
        {
            
            GameObject selectedClone = Instantiate(CharacterSelected.selectedCharacter, characterY.transform.position, Quaternion.identity);

           
            SpriteRenderer selectedSpriteRenderer = selectedClone.GetComponent<SpriteRenderer>();
            SpriteRenderer ySpriteRenderer = characterY.GetComponent<SpriteRenderer>();

            if (selectedSpriteRenderer != null && ySpriteRenderer != null)
            {
                ySpriteRenderer.sprite = selectedSpriteRenderer.sprite;
            }

            Animator selectedAnimator = selectedClone.GetComponent<Animator>();
            Animator yAnimator = characterY.GetComponent<Animator>();

            if (selectedAnimator != null && yAnimator != null)
            {
                yAnimator.runtimeAnimatorController = selectedAnimator.runtimeAnimatorController;
            }

            characterY.transform.localScale = selectedClone.transform.localScale* scaleRate;
            characterY.name = selectedClone.name;
            Destroy(selectedClone);
        }
    }
}

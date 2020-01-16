using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Animatortext : MonoBehaviour
{
    // Zeit die man für jeden Buchstaben braucht ( je kleiner, desto schneller kommen die Buchstaben) 
    public float letterPaused = 0.01f;
    //Message die bis zum Ende ausgegeben wird, buchstabe nach buchstabe 
    public string message;
    // Text damit die Message anzeigt wird 
    public Text textComp;

    // Use this for initialization
    void Start()
    {
        //Get text component
        textComp = GetComponent<Text>();
        //Message will display will be at Text
        message = textComp.text;
        //Set the text to be blank first
        textComp.text = "";
        //Call the function and expect yield to return
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        // Teilt jedes char in einen char array
        foreach (char letter in message.ToCharArray())
        {
            // Fügt 1 Buchstaben hinzu 
            textComp.text += letter;
            yield return 0;
            yield return new WaitForSeconds(letterPaused);
        }
    }
}
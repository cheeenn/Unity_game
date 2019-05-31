using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restart_button_test : MonoBehaviour {

    public void restart_test()
    {
        SceneManager.LoadScene("the_game");
    }
}

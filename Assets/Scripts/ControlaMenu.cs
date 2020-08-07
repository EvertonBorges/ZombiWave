using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaMenu : MonoBehaviour
{

    [SerializeField]
    private GameObject botaoSair;

    void Start()
    {
        #if UNITY_STANDALONE || UNITY_EDITOR
            botaoSair.SetActive(true);
        #endif
    }

    public void JogarJogo()
    {
        SceneManager.LoadScene(Scenes.SCENE_GAME);
    }

    public void SairDoJog()
    {
        Application.Quit();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

}

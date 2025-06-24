using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject pauseMenu;
    public GameObject exitMenu;
    public GameObject winScreen;
    public GameObject deathScreen;

    private bool isPaused = false;
    private bool isDead = false;
    private bool isWin = false;

    private Player player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player")?.GetComponent<Player>();
        if (player != null && player.TryGetComponent(out HealthComponent health))
        {
            health.onDeath.AddListener(HandleDeath);
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (!isPaused) ActivatePause();
            else ResumeGame();
        }
    }

    private void ActivatePause()
    {
        pauseMenu.SetActive(true);
        isPaused = true;
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        PauseAllAudio();
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        exitMenu.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        ResumeAllAudio();
    }

    private void HandleDeath()
    {
        if (isDead) return;

        deathScreen.SetActive(true);
        isDead = true;
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        PauseAllAudio();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isWin)
        {
            winScreen.SetActive(true);
            isWin = true;
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            PauseAllAudio();
        }
    }

    private void PauseAllAudio()
    {
        foreach (var audio in FindObjectsOfType<AudioSource>())
            audio.Pause();
    }

    private void ResumeAllAudio()
    {
        foreach (var audio in FindObjectsOfType<AudioSource>())
            audio.Play();
    }

    public void LoadMenu(string menuName)
    {
        SceneManager.LoadScene(menuName);
    }

    public void RestartLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

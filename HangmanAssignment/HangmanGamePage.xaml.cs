using Microsoft.Maui.Controls.Compatibility;
using System.ComponentModel;
using System.Text;

namespace HangmanAssignment;

public partial class HangmanGamePage : ContentPage, INotifyPropertyChanged
{
    public HangmanGamePage()
    {
        InitializeComponent();
        BindingContext = this;
        PickWord();
        CalculateWord(answer, guessed);
    }
    #region UserInterface
    public string Spotlight
    {
        get => spotlight;
        set
        {
            spotlight = value;
            OnPropertyChanged();
        }
    }

    public string IncorrectGuesses 
    {
        get => incorrectGuesses;
        set 
        {   incorrectGuesses = value;
            OnPropertyChanged();
        }
    }

    public List<char> Letters
    { get => letters;
        set
        {
            letters = value;
            OnPropertyChanged();
        }
    }

    public string Message
    {
        get => message;
        set
        {
            message = value;
            OnPropertyChanged();
        }
    }

    public string GameStatus
    {
        get => gameStatus;
        set
        {
            gameStatus = value;
            OnPropertyChanged();
        }
    }

    public string CurrentImage
    {
        get => Image;
        set
        {
            Image = value;
            OnPropertyChanged();
        }
    }
    #endregion

    #region WordList
    List<string> words = new()
    {
            "HAPPINESS",
            "PLETHORA",
            "INSECURE",
            "BURDENSOME",
            "APPLICATION",
            "BINDING",
            "ORGANIZATION",
            "ABANDON",
            "EXCELLENCE",
            "POWERFUL",
            "CODIFIED",
            "RELOADED",
            "MASTERFUL",
            "EMPRESS",
            "EXECUTION",
            "POETIC",
            "OBLIVION",
            "HELLFIRE",
    };
   
    private string answer = "";
    private string spotlight;
    private List<char> guessed = new();
    private List<char> letters = new();
    private string message;
    private int mistakes = 0;
    private int maxWrong = 7;
    private string incorrectGuesses;
    private string gameStatus;
    private string Image = "hang1.png";
    #endregion
    #region Functionality
    private void ResetClicked(object sender, EventArgs e)
    {
        mistakes = 0;
        guessed = new List<char>();
        CurrentImage = "hang1.png";
        PickWord();
        CalculateWord(answer, guessed);
        Message = "";
        UpdateStatus();
        EnableGuessBox();
        IncorrectGuesses = $"{Build()}";
    }

    private void PickWord()
    {
        answer = words[new Random().Next(words.Count)];
    }

    private void CalculateWord(string answer, List<char> guessed)
    {
        var temp = new char[answer.Length];

        for (int i = 0; i < answer.Length; i++)
        {
            char letter = answer[i];
            temp[i] = guessed.Contains(letter) ? letter : '_';
        }

        Spotlight = string.Join(" ",temp);
    }

    private void HandleGuess(char letter)
    {

        if (!guessed.Contains(letter))
        {
            guessed.Add(letter);

            if (answer.Contains(letter))
            {
                CalculateWord(answer, guessed);
                CheckIfGameWon();
            }
            else
            {
                mistakes++;
                UpdateStatus();
                CheckIfGameLost();
                CurrentImage = $"hang{mistakes + 1}.png";
                IncorrectGuesses = $"Previous Guesses: {Build()}";
            }
        }
    }

    private string Build()
    {
        StringBuilder sb = new StringBuilder();
        foreach(char c in guessed)
        {
            if (!answer.Contains(c))
            {
                sb.Append(c);
            }
        }

        return sb.ToString();
    }

    private void CheckIfGameWon()
    {
        if (Spotlight.Replace(" ", "") == answer)
        {
            Message = "Congratulations!!";
            DisableGuessBox();
        }
    }

    private void CheckIfGameLost()
    {
        if (mistakes == maxWrong)
        {
            Message = "You Lost, It's Okay... Try Again :(";
            Spotlight = answer;
            DisableGuessBox();
        }
    }

    private void DisableGuessBox() 
    { 
        GuessBox.IsEnabled = false;
        GuessBox.IsVisible = false;
        GuessBtn.IsVisible = false;
        GuessBtn.IsEnabled = false;
    }

    private void EnableGuessBox() 
    { 
        GuessBox.IsEnabled = true;
        GuessBox.IsVisible = true;
        GuessBtn.IsEnabled = true;
        GuessBtn.IsVisible=true;
    }

    private void UpdateStatus()
    {
        GameStatus = $"Guesses: {mistakes} of {maxWrong}";
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        string letter = GuessBox.Text;
        HandleGuess(letter[0]);
        GuessBox.Text = string.Empty;
        
    }
    #endregion
}


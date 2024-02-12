using Microsoft.Maui.Controls.Compatibility;
using System.ComponentModel;

namespace HangmanAssignment;

public partial class HangmanGamePage : ContentPage, INotifyPropertyChanged
{
    public HangmanGamePage()
    {
        InitializeComponent();
        Letters.AddRange("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
        BindingContext = this;
        PickWord();
        CalculateWord(answer, guessed);
    }

    public string Spotlight
    {
        get => spotlight;
        set
        {
            spotlight = value;
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
            "POLISH"
    };
   
        string answer = "";
    private string spotlight;
    private List<char> guessed = new();
    private List<char> letters = new();
    private string message;
    private int mistakes = 1;
    private int maxWrong = 7;
    private string gameStatus;
    private string Image = "hang1.png";

    private void Button_Clicked(object sender, EventArgs e)
    {
        var btn = sender as Button;
        if (btn != null)
        {
            var letter = btn.Text;
            btn.IsEnabled = false;
            HandleGuess(letter[0]);
        }
    }

    private void ResetClicked(object sender, EventArgs e)
    {
        mistakes = 0;
        guessed = new List<char>();
        CurrentImage = "hang1.png";
        PickWord();
        CalculateWord(answer, guessed);
        Message = "";
        UpdateStatus();
        EnableLetters();
    }

    private void PickWord()
    {
        answer =
            words[new Random().Next(0, words.Count)];
    }

    private void CalculateWord(string answer, List<char> guessed)
    {
        var temp =
            answer.Select(x => (guessed.IndexOf(x) >= 0 ? x : '_')).ToArray();

        Spotlight = string.Join(" ", temp);
    }

    private void HandleGuess(char letter)
    {
        if (!guessed.Contains(letter))
            guessed.Add(letter);
        if (answer.Contains(letter))
        {
            CalculateWord(answer, guessed);
            CheckIfGameWon();
        }
        else if (!answer.Contains(letter))
        {   
            mistakes++;
            UpdateStatus();
            CheckIfGameLost();
            CurrentImage = $"hang{mistakes+1}.png";
        }
    }

    private void CheckIfGameWon()
    {
        if (Spotlight.Replace(" ", "") == answer)
        {
            Message = "You Win";
            DisableLetters();
        }
    }

    private void CheckIfGameLost()
    {
        if (mistakes == maxWrong)
        {
            Message = "You Lose!!";
            DisableLetters();
        }
    }

    private void DisableLetters()
    {
        foreach (var child in Alphabet.Children)
        {
            var btn = child as Button;
            if (btn != null)
                btn.IsEnabled = false;
        }
    }

    private void EnableLetters()
    {
        foreach (var child in Alphabet.Children)
        {
            var btn = child as Button;
            if (btn != null)
                btn.IsEnabled = true;
        }
    }

    private void UpdateStatus()
    {
        GameStatus = $"Guesses: {mistakes} of {maxWrong}";
    }

}

   
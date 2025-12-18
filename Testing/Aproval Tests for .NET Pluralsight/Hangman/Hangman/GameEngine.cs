using System.Globalization;
using System.Text;

namespace Hangman
{
    public class GameEngine
    {
        private readonly string _wordToGuess;

        public GameEngine(string wordToGuess)
        {
            _wordToGuess = wordToGuess.ToUpperInvariant();
        }

        public List<char> GuessedLetters { get; } = new List<char>();

        public int GuessesRemaining { get; private set; } = 10;

        public string RevealedWord
        {
            get
            {
                var sb = new StringBuilder();

                foreach (var letter in _wordToGuess.ToCharArray())
                {
                    if (GuessedLetters.Contains(letter))
                    {
                        sb.Append(letter);
                    }
                    else
                    {
                        sb.Append('-');
                    }
                }

                return sb.ToString();
            }
        }

        public void Guess(char letter)
        {
            letter = ToUpperChar(letter);

            GuessedLetters.Add(letter);

            var incorrectGuess = !_wordToGuess.ToCharArray().Any(x => x == letter);

            if (incorrectGuess)
            {
                GuessesRemaining--;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendFormat("Guessed letters: {0}", new string(GuessedLetters.ToArray()));
            sb.AppendLine();

            sb.AppendFormat("Guesses remaining: {0}", GuessesRemaining);
            sb.AppendLine();

            sb.Append(RevealedWord);

            return sb.ToString();
        }


        #region helper code
        private static char ToUpperChar(char letter)
        {
            return letter.ToString(CultureInfo.InvariantCulture).ToUpperInvariant().ToCharArray()[0];
        }
        #endregion
    }
}

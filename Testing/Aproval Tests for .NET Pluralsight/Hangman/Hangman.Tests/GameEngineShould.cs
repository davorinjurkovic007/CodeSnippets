using ApprovalTests;
using ApprovalTests.Reporters;
using System.Text;

namespace Hangman.Tests
{
    public class GameEngineShould
    {
        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void UpdateGameState()
        {
            var sut = new GameEngine("Pluralsight");

            sut.Guess('x');
            sut.Guess('p');
            sut.Guess('l');

            Approvals.Verify(sut);
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void UpdateGameState_Intermediate_Steps()
        {
            var sb = new StringBuilder();
            var sut = new GameEngine("Pluralsight");

            sb.AppendLine(sut.ToString());

            sut.Guess('x');
            sb.AppendLine(sut.ToString());

            sut.Guess('p');
            sb.AppendLine(sut.ToString());

            sut.Guess('l');
            sb.AppendLine(sut.ToString());

            Approvals.Verify(sb);
        }
    }
}

using ApprovalTests;
using ApprovalTests.Reporters;

namespace StringUtilities.Tests
{
    public class WordReverserShould
    {
        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void ReverseWords()
        {
            var names = new[] { "Sarah", "Gentry", "Amrit", "Peter", "Joan" };

            var sut = new WordReverser();

            var reversedNames = sut.Reverse(names).ToArray();

            //Assert.Equal("haraS", reversedNames[0]);
            //Assert.Equal("yrtneG", reversedNames[1]);
            //Assert.Equal("tirmA", reversedNames[2]);
            //Assert.Equal("reteP", reversedNames[3]);
            //Assert.Equal("naoJ", reversedNames[4]);

            Dictionary<string, string> inputAndOutput = names.Zip(reversedNames, 
                (originalWord, reversedWord) => new {originalWord, reversedWord })
                .ToDictionary(pair => pair.originalWord, pair => pair.reversedWord);

            //Approvals.VerifyAll(reversedNames, "name", (x) => $"reversed '{x}'");
            Approvals.VerifyAll(inputAndOutput);
        }
    }
}

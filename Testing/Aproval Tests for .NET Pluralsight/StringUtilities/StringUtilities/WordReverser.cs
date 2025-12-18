namespace StringUtilities
{
    public class WordReverser
    {
        public IEnumerable<string> Reverse(IEnumerable<string> strings)
        {
            foreach(var s in strings)
            {
                yield return ReverseWord(s);
            }
        }

        private string ReverseWord(string s)
        {
            var chars = s.ToCharArray();

            Array.Reverse(chars);

            return new string(chars);
        }
    }
}

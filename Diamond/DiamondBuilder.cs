namespace Diamond
{
    public class DiamondBuilder
    {
        private const char Fill = ' ';
        private const char Newline = '\n';

        /// <summary>
        /// Takes in an uppercase letter and creates the string representation of a diamond of letters from A to "input",
        /// where the letter passed in as the argument is in the center of the diamond.
        /// </summary>
        /// <param name="input">The input character. Must be in the range [A-Z].</param>
        /// <returns>A string with the representation of the diamond, including newline characters.</returns>
        /// <exception cref="InvalidCharacterException">The input is ouside of the range [A-Z].</exception>
        public string Build(char input)
        {
            if (!(input >= 'A' && input <= 'Z'))
            {
                throw new InvalidCharacterException(input);
            }

            int diamondCenter = (input - 'A');

            // The logical width (and height) of the diamond
            int logicalWH = 2 * diamondCenter + 1;

            // A string that we'll copy to every new line of the diamond,
            // before putting the correct character into it
            string filledLine = new string(Fill, logicalWH);

            // The width of a line, including the line break character
            int lineWidth = logicalWH + 1;

            // The total number of characters required to display the diamond
            int resultLength = lineWidth * logicalWH;

            Span<char> diamondChars = stackalloc char[resultLength];

            for (int i = 0; i < diamondCenter + 1; i++)
            {
                // Get the first line of the diamond
                var firstLine = diamondChars.Slice(lineWidth * i, lineWidth);

                // Fill it with the current fill character
                filledLine.CopyTo(firstLine);

                WriteDiamondLine(
                    destination: firstLine,
                    lineNumber: i,
                    center: diamondCenter);

                // If we're not yet at the center of the diamond
                if (i != diamondCenter)
                {
                    // Get the bottom (mirrored) line
                    var mirroredLine = diamondChars.Slice(resultLength - (i + 1) * lineWidth, lineWidth);

                    // and copy the same characters to it
                    firstLine.CopyTo(mirroredLine);
                }
            }

            return diamondChars.ToString();
        }

        /// <summary>
        /// This method replaces the characters in the span with what we would expect to see
        /// in a diamond, according to the line we're currently at.
        /// </summary>
        /// <param name="destination">A span representing the current line in the diamond</param>
        /// <param name="lineNumber">The line number being processed</param>
        /// <param name="center">The index number of the center of the line</param>
        private void WriteDiamondLine(Span<char> destination, int lineNumber, int center)
        {
            char currentChar = (char)('A' + lineNumber);

            destination[center - lineNumber] = currentChar;
            destination[center + lineNumber] = currentChar;
            destination[center * 2 + 1] = Newline;
        }
    }
}
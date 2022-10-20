namespace Diamond
{
	
	[Serializable]
	public class InvalidCharacterException : Exception
	{
		public readonly char InvalidCharacter;

		public InvalidCharacterException(char invalidCharacter)
		{
			InvalidCharacter = invalidCharacter;
        }
	}
}


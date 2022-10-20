using Diamond;

namespace UnitTests;

public class DiamondBuilderTests
{
    const char Filler = ' ';
    const char NewLine = '\n';

    [Fact]
    public void A_Diamond_Should_Have_Correct_Characters()
    {
        // Arrange
        var builder = new DiamondBuilder();

        // Act
        var diamond = builder.Build('A');

        // Assert
        Assert.Equal($"A{NewLine}", diamond);
    }

    [Fact]
    public void B_Diamond_Should_Have_Correct_Characters()
    {
        // Arrange
        var builder = new DiamondBuilder();

        // Act
        var diamond = builder.Build('B');

        // Assert
        Assert.Equal($"{Filler}A{Filler}{NewLine}B{Filler}B{NewLine}{Filler}A{Filler}{NewLine}", diamond);
    }

    [Theory]
    [InlineData('A')]
    [InlineData('B')]
    public void Diamond_Should_Have_Correct_Number_Of_Lines(char input)
    {
        // Arrange
        var builder = new DiamondBuilder();

        // Act
        var diamond = builder.Build(input);

        // Assert
        Assert.Equal(2 * (input - 'A') + 1, diamond.Count(a => a == NewLine));
    }

    [Theory]
    [InlineData('A')]
    [InlineData('B')]
    public void Diamond_Should_Have_Correct_Number_Of_Filler_Characters(char input)
    {
        // Arrange
        var builder = new DiamondBuilder();
        var numberOfRows = 2 * (input - 'A') + 1;

        var expectedFillerChars = numberOfRows == 1 ? 0 : numberOfRows * numberOfRows - numberOfRows * 2 + 2;

        // Act
        var diamond = builder.Build(input);

        // Assert
        Assert.Equal(expectedFillerChars, diamond.Count(c => c == Filler));
    }

    [Theory]
    [InlineData('A')]
    [InlineData('B')]
    public void Diamond_Should_Contain_Only_Allowed_Characters(char input)
    {
        // Arrange
        var builder = new DiamondBuilder();

        // Act
        var diamond = builder.Build(input);

        // Assert
        Assert.DoesNotContain(diamond, c => c != Filler && c != NewLine && (c < 'A' || c > 'Z'));
    }

    [Theory]
    [InlineData('B')]
    [InlineData('Z')]
    public void Larger_Than_A_Diamonds_Should_Have_Correct_Corners(char input)
    {
        // Arrange
        var builder = new DiamondBuilder();

        int diamondLineLength = 2 * (input - 'A') + 1;
        int stringLineLength = diamondLineLength + 1;

        int topCorner = (input - 'A');
        int bottomCorner = stringLineLength * diamondLineLength - (input - 'A') - 2;
        int leftCorner = stringLineLength * (input - 'A');
        int rightCorner = stringLineLength * (input - 'A') + diamondLineLength - 1;

        // Act
        var diamond = builder.Build(input);

        // Assert
        Assert.Equal('A', diamond[topCorner]);
        Assert.Equal('A', diamond[bottomCorner]);
        Assert.Equal(input, diamond[leftCorner]);
        Assert.Equal(input, diamond[rightCorner]);
    }

    [Theory]
    [InlineData('C')]
    [InlineData('Z')]
    public void Larger_Than_B_Diamonds_Should_Have_Correct_Letter_Count_On_The_Edges(char input)
    {
        // Arrange
        var builder = new DiamondBuilder();

        // Act
        var diamond = builder.Build(input);

        // Assert
        var letterGroup = diamond
            .Where(c => c != 'A' && c != input && c != NewLine && c != Filler)
            .GroupBy(c => c)
            .Select(c => c.Count())
            .ToList();

        Assert.True(letterGroup.All(letterCount => letterCount == 4), "All letters at the edges of the diamond should have a count of 4.");
    }

    [Theory]
    [InlineData('1')]
    [InlineData('a')]
    [InlineData('Á')]
    [InlineData(NewLine)]
    [InlineData(Filler)]
    public void Builder_Should_Not_Accept_Non_AZ_Characters(char input)
    {
        // Arrange
        var builder = new DiamondBuilder();

        // Assert
        var exception = Assert.Throws<InvalidCharacterException>(() => builder.Build(input));
        Assert.Equal(input, exception.InvalidCharacter);
    }
}
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace AdventurerPath.Tests;

public class UnitTests
{

    [Fact]
    public void testAdventurerProperlyInitialized()
    {
        Adventurer testUser = new Adventurer();

        Assert.NotNull(testUser);
        Assert.Equal((0, 0), testUser.getCurrentPosition());
    }

    [Theory]
    [InlineData("1F", 0, 1)]
    [InlineData("1B", 0, -1)]
    [InlineData("1L", -1, 0)]
    [InlineData("1R", 1, 0)]
    public void testParseDirectionsUpdatesAllDirections(string directions, int finalX, int finalY)
    {
        Adventurer testUser = new Adventurer();

        testUser.parseDirections(directions);

        Assert.Equal((finalX, finalY), testUser.getCurrentPosition());
    }

    [Fact]
    public void testParseDirectionsWorksForGivenTestCase()
    {
        Adventurer testUser = new Adventurer();

        string providedDirections = "15F6B6B5L16R8B16F20L6F13F11R";
        int finalX = 2;
        int finalY = 30;

        testUser.parseDirections(providedDirections);

        Assert.Equal((finalX, finalY), testUser.getCurrentPosition());
    }

    [Theory]
    [InlineData("1EF")] // No parsable string 
    [InlineData("1FE")] // Extra char, technically parsable string available
    [InlineData("E1F")] // Extra char, technically parsable string available
    [InlineData("-1F")] // Negative input number, technically parsable string available
    [InlineData("qwerty")] // Random combination of characters
    [InlineData("")] // Blank Input
    public void testParseDirectionsThrowsInvalidInput(string directions)
    {
        Adventurer testUser = new Adventurer();

        Assert.Throws<ArgumentException>(() => testUser.parseDirections(directions));
        Assert.Equal((0, 0), testUser.getCurrentPosition());
    }

    [Theory]
    [InlineData("10F10B", 0)] // No change from start
    [InlineData("10F", 10)] // Directly in each direction
    [InlineData("10B", 10)]
    [InlineData("10L", 10)]
    [InlineData("10R", 10)]
    [InlineData("3F4R", 5)] // Easily calculated whole distance, positive numbers
    [InlineData("3B4L", 5)] // Easily calculated whole distance, negative numbers
    [InlineData("3F4L", 5)] // Easily calculated whole distance, mixed numbers
    public void testGetDistanceFromStartProperlyCalculatesDistance(string directions, double distance)
    {
        Adventurer testUser = new Adventurer();

        testUser.parseDirections(directions);

        Assert.Equal(distance, testUser.getDistanceFromStart());
    }

    [Fact]
    public void testResetPositionProperlyResetsUser() 
    {
        Adventurer testUser = new Adventurer();

        string directions = "1F1R";
        (int, int) expectedEnd = (1, 1);

        testUser.parseDirections(directions);

        // Demonstrate that the position is not (0,0)
        Assert.Equal(expectedEnd, testUser.getCurrentPosition());

        // Call function and determine that position is now (0,0) again
        testUser.resetPosition();
        Assert.Equal((0, 0), testUser.getCurrentPosition());
    }
}

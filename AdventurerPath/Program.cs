using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Linq;

public class Adventurer
{
    // The adventurer's current position on the grid starting at (0,0)
    private int xPos, yPos = 0;
   
    // Parses the directions string and updates the x and y coordinates accordingly
    // References: https://stackoverflow.com/questions/11469710/c-sharp-regex-expression-for-a-repeated-pattern
    public void parseDirections(string directions) {
        var regex = new Regex(@"(\d+)([FBLR])");
        var matches = regex.Matches(directions);

        // If the directions string is not entirely valid, throw an exception and request the user to input a new string
        var matchesLength = matches.Sum(match => match.Length);
        if (matchesLength != directions.Length)
        {
            throw new ArgumentException("Invalid directions: Check your string formatting and try again!");
        }

        // If the directions are valid, move the adventurer accordingly
        foreach (Match match in matches)
        {
            var distance = int.Parse(match.Groups[1].Value);
            var direction = match.Groups[2].Value[0];
            move(direction, distance);
        }
    }

    // Helper method to move the adventurer in the specified direction by the specified distance
    private void move(char direction, int distance)
    {
        switch (direction)
        {
            case 'F':
                yPos += distance;
                break;
            case 'B':
                yPos -= distance;
                break;
            case 'L':
                xPos -= distance;
                break;
            case 'R':
                xPos += distance;
                break;
            default:
                throw new ArgumentException("Invalid direction: Check your string formatting and try again!");
        }
    }

    public void resetPosition()
    {
        xPos = 0;
        yPos = 0;
    }

    // Returns the euclidean distance from the start (0,0) to the currently held (x,y)
    public double getDistanceFromStart() {
        if (xPos == 0 && yPos == 0)
        {
            return 0;
        }

        return Math.Sqrt(Math.Pow(xPos, 2) + Math.Pow(yPos, 2));
    }
}


// Runs the Adventurer Path Program
public class AdventurerPathProgram
{
    private static Adventurer initializeUser() 
    {
        Console.WriteLine("Welcome to Adventurer Path! Discover how far you would travel by providing your directions!");
        Console.WriteLine("Directions must be in the format of a number directly followed by a letter (F, B, L, R).\nAn example would be \'1F2B3L4R5F...\'\n");
        Console.WriteLine("To exit the program at any time, simply press Ctrl+C\n");
        return new Adventurer();
    }

    private static void runAdventurerPath(Adventurer user)
    {
        Console.WriteLine("\nPlease enter your directions:");
        string inputDirections = Console.ReadLine();
        try
        {
            user.parseDirections(inputDirections);
            double distance = user.getDistanceFromStart();
            Console.WriteLine($"\nFollowing this path you will have traveled a distance of {distance} units from the start!\n");
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private static bool continueDialogue() 
    {
        Console.WriteLine("\nWould you like to enter another path? If so, type 'Y', if not enter 'N':");
        string continueCheck = Console.ReadLine();

        switch (continueCheck)
        {
            case "Y":
                return true;
            case "N":
                Console.WriteLine("\nSafe travels on your journey ahead! Goodbye!");
                Thread.Sleep(2000);
                return false;
            default:
                Console.WriteLine("\nInvalid input. Please enter 'Y' or 'N'.\n");
                return continueDialogue();
        }
    }

    static void Main(string[] args)
    {
        Adventurer user = initializeUser();

        bool running = true;

        while (running)
        {
            try
            {
                runAdventurerPath(user);

                bool continueCheck = continueDialogue();

                if (!continueCheck) break;

                user.resetPosition();
            }
            catch (Exception e)
            {
                Console.WriteLine($"An unexpected error occurred. Please review it and try again: {e.Message}");
            }
        }
    }
}



using workout_tracker_backend.Helpers;
namespace workout_tracker_tests;

public class UserTests
{
    Lib lib = new Lib();
    [Fact]
    public void InvalidPassword()
    {
        // Given
        string InvalidPassword = "fout";

        // When
        bool result = lib.IsValidPassword(InvalidPassword);

        // Then
        Assert.False(result);
    }

    [Fact]
    public void ValidPassword()
    {
        // Given
        string ValidPassword = "Jantje123";

        // When
        bool result = lib.IsValidPassword(ValidPassword);

        // Then
        Assert.True(result);
    }

    [Fact]
    public void NoPassword()
    {
        // Given
        string NoPassword = "";

        // When
        bool result = lib.IsValidPassword(NoPassword);

        // Then
        Assert.False(result);
    }
    [Fact]
    public void NoNumberPassword()
    {
        // Given
        string NoNumberPassword = "Jantje";

        // When
        bool result = lib.IsValidPassword(NoNumberPassword);

        // Then
        Assert.False(result);
    }

    [Fact]
    public void NoCapitalLetterPassword()
    {
        // Given
        string NoCapitalLetterPassword = "jantje123";

        // When
        bool result = lib.IsValidPassword(NoCapitalLetterPassword);

        // Then
        Assert.False(result);
    }

    [Fact]
    public void NoLowerLetterPassword()
    {
        // Given
        string NoLowerLetterPassword = "JANTJE123";

        // When
        bool result = lib.IsValidPassword(NoLowerLetterPassword);

        // Then
        Assert.False(result);
    }

    [Fact]
    public void ValidEmail()
    {
        // Given
        string ValidEmail = "sam@boers.family";

        // When
        bool result = lib.IsValidEmail(ValidEmail);

        // Then
        Assert.True(result);
    }

    [Fact]
    public void ValidEmailSecondVariation()
    {
        // Given
        string ValidEmailSecondVariation = "sam.boers@hotmail.com";

        // When
        bool result = lib.IsValidEmail(ValidEmailSecondVariation);

        // Then
        Assert.True(result);
    }

    [Fact]
    public void InvalidEmail()
    {
        // Given
        string InvalidEmail = "samboersfamily.com";

        // When
        bool result = lib.IsValidEmail(InvalidEmail);

        // Then
        Assert.False(result);
    }

    [Fact]
    public void InvalidEmailBecauseOfDotAtTheEnd()
    {
        // Given
        string InvalidEmailBecauseOfDotAtTheEnd = "sam@boers.family.";

        // When
        bool result = lib.IsValidEmail(InvalidEmailBecauseOfDotAtTheEnd);

        // Then
        Assert.False(result);
    }

    [Fact]
    public void InvalidEmailBecauseOfEmptyString()
    {
        // Given
        string InvalidEmailBecauseOfEmptyString = "";

        // When
        bool result = lib.IsValidEmail(InvalidEmailBecauseOfEmptyString);

        // Then
        Assert.False(result);
    }

}
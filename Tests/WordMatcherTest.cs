using BL;
using DAL;
using Moq;

namespace Tests;

public class WordMatcherTest
{
    [Test]
    public void MatchWords_ReturnsMatchingWords_1()
    {
        // Arrange
        var mockReader = new Mock<ICombinationReader>();
        mockReader.Setup(r => r.ReadCombination()).Returns(new List<string> { "fo", "ob","ar", "foobar" });
        
        var matcher = new WordMatcherImpl(mockReader.Object);

        // Act
        var result = matcher.MatchWords(3, 6);

        // Assert
        Assert.IsNotNull(result);
        Assert.That(result.Count, Is.EqualTo(1));
        
        List<List<string>> expected = new List<List<string>> { new() { "fo", "ob","ar" } };
        CollectionAssert.AreEqual(result, expected);
    }
    
    [Test]
    public void MatchWords_ReturnsMatchingWords_2()
    {
        // Arrange
        var mockReader = new Mock<ICombinationReader>();
        mockReader.Setup(r => r.ReadCombination()).Returns(new List<string> { "foo","bar", "foobar" });
        
        var matcher = new WordMatcherImpl(mockReader.Object);

        // Act
        var result = matcher.MatchWords(3, 6);

        // Assert
        Assert.IsNotNull(result);
        Assert.That(result.Count, Is.EqualTo(1));
        
        List<List<string>> expected = new List<List<string>> { new() { "foo","bar" } };
        CollectionAssert.AreEqual(result, expected);
    }
    
    [Test]
    public void MatchWords_ShouldRespectMaxCombinationLength()
    {
        // Arrange
        var mockReader = new Mock<ICombinationReader>();
        mockReader.Setup(r => r.ReadCombination()).Returns(new List<string> { "fo", "ob","ar", "foobar" });
        
        var matcher = new WordMatcherImpl(mockReader.Object);

        // Act
        var result = matcher.MatchWords(2, 6);

        // Assert
        Assert.IsNotNull(result);
        Assert.That(result.Count, Is.EqualTo(0));
    }
    
    [Test]
    public void MatchWords_EmptyListShouldNotReturnCombinations()
    {
        // Arrange
        var mockReader = new Mock<ICombinationReader>();
        mockReader.Setup(r => r.ReadCombination()).Returns(new List<string> {});
        
        var matcher = new WordMatcherImpl(mockReader.Object);

        // Act
        var result = matcher.MatchWords(3, 6);

        // Assert
        Assert.IsNotNull(result);
        Assert.That(result.Count, Is.EqualTo(0));
    }
}

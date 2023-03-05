﻿namespace TrieTest;

using Trees;

public class Tests
{
    private Trie trie;

    [SetUp]
    public void Setup()
    {
        trie = new Trie();
    }

    [TestCase(20, "train", "trailer", "hammer", "hamon", "test")]
    [TestCase(4, "e", " e")]
    [TestCase(2, "e")]
    [TestCase(5, "etet", "ete")]
    public void SuccessAddAndSizeTest(int expectedSize, params string[] words)
    {
        var actualAddSuccess = true;

        foreach (var element in words)
        {
            actualAddSuccess = trie.Add(element) && actualAddSuccess;
        }

        var actualSize = trie.Size;

        Assert.That(actualAddSuccess && actualSize == expectedSize);
    }

    [TestCase(6, "train", "train")]
    [TestCase(1, "")]
    public void FailAddAndSizeTest(int expectedSize, params string[] words)
    {
        var actualAddSuccess = true;

        foreach (var element in words)
        {
            actualAddSuccess = trie.Add(element) && actualAddSuccess;
        }

        var actualSize = trie.Size;

        Assert.That(!actualAddSuccess && actualSize == expectedSize);
    }

    [TestCase("test", true, "literature", "toast", "tes", "test", "lost")]
    [TestCase("", true, "literature", "toast", "tes", "test", "lost")]
    [TestCase("tests", false, "literature", "toast", "tes", "test", "lost")]
    [TestCase("spoiler", false, "literature", "toast", "tes", "test", "lost")]
    public void ContainsElementAfterAddingTest(string word, bool expectedResult, params string[] trieElements)
    {
        foreach (var element in trieElements)
        {
            trie.Add(element);
        }

        Assert.That(expectedResult == trie.Contains(word));
    }

    [TestCase("test", "literature", "toast", "tes", "test", "lost")]
    [TestCase("e", "e")]
    [TestCase("spoiler", "literature", "toast", "tes", "test", "lost")]
    public void FailContainsElementAfterRemovingTest(string word, params string[] trieElements)
    {
        foreach (var element in trieElements)
        {
            trie.Add(element);
        }

        trie.Remove(word);

        Assert.That(!trie.Contains(word));
    }

    [TestCase("train", 21, "train", "trains", "trailer", "hammer", "hamon", "test")]
    [TestCase("trains", 20, "train", "trains", "trailer", "hammer", "hamon", "test")]
    [TestCase("trailer", 18, "train", "trains", "trailer", "hammer", "hamon", "test")]
    [TestCase("e", 21, "train", "trains", "trailer", "hammer", "hamon", "test", "e")]
    [TestCase("e", 1, "e")]
    public void SuccessRemoveElementTest(string word, int expectedSize, params string[] trieElements)
    {
        foreach (var element in trieElements)
        {
            trie.Add(element);
        }

        var actualRemoveResult = trie.Remove(word);

        Assert.That(actualRemoveResult && !trie.Contains(word) && expectedSize == trie.Size);
    }

    [TestCase("coach", 20, "train", "trailer", "hammer", "hamon", "test")]
    [TestCase("f", 4, "e", " e")]
    public void FailRemoveElementTest(string word, int expectedSize, params string[] trieElements)
    {
        foreach (var element in trieElements)
        {
            trie.Add(element);
        }

        var actualRemoveResult = trie.Remove(word);

        Assert.That(!actualRemoveResult && !trie.Contains(word) && expectedSize == trie.Size);
    }

    [TestCase("t", 4, "train", "trains", "trailer", "hammer", "hamon", "test")]
    [TestCase("tr", 3, "train", "trains", "trailer", "hammer", "hamon", "test")]
    [TestCase("trains", 1, "train", "trains", "trailer", "hammer", "hamon", "test")]
    [TestCase("trainse", 0, "train", "trains", "trailer", "hammer", "hamon", "test")]
    [TestCase("", 6, "train", "trains", "trailer", "hammer", "hamon", "test")]
    public void HowManyStartsWithPrefixWithoutRemoveTest(string prefix, int expectedResult, params string[] trieElements)
    {
        foreach (var element in trieElements)
        {
            trie.Add(element);
        }

        var actualResult = trie.HowManyStartsWithPrefix(prefix);

        Assert.That(actualResult, Is.EqualTo(expectedResult));
    }

    [TestCase("t", "train", 3, "train", "trains", "trailer", "hammer", "hamon", "test")]
    [TestCase("tr", "train", 2, "train", "trains", "trailer", "hammer", "hamon", "test")]
    [TestCase("trains", "hammer", 1, "train", "trains", "trailer", "hammer", "hamon", "test")]
    public void HowManyStartsWithPrefixWithRemoveTest(string prefix, string deleteWord, int expectedResult, params string[] trieElements)
    {
        foreach (var element in trieElements)
        {
            trie.Add(element);
        }

        trie.Remove(deleteWord);

        var actualResult = trie.HowManyStartsWithPrefix(prefix);

        Assert.That(actualResult, Is.EqualTo(expectedResult));
    }
}
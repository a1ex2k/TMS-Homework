using TestPair = (string? first, string? second);

namespace PairOf.Tests;

public class ComparablePairFact
{
    [Theory]
    [MemberData(nameof(ForCompareToNull))]
    public void CompareTo_CompareToNull_ReturnsPositive(TestPair pair1)
    {
        var pair = new ComparablePair<string, string>(pair1.first, pair1.second);
        ComparablePair<string, string>? other = null;

        var result = pair.CompareTo(other);

        Assert.True(result > 0);
    }

    [Theory]
    [MemberData(nameof(EqualPairs))]
    public void CompareTo_WithEqualPairs_ReturnsZero(TestPair pair1, TestPair pair2)
    {
        var pair = new ComparablePair<string, string>(pair1.first, pair1.second);
        var other = new ComparablePair<string, string>(pair2.first, pair2.second);

        var result = pair.CompareTo(other);

        Assert.Equal(0, result);
    }


    [Theory]
    [MemberData(nameof(GreaterToSmallerPairs))]
    public void CompareTo_ReturnsPositive(TestPair greaterPair, TestPair smallerPair)
    {
        var pair = new ComparablePair<string, string>(greaterPair.first, greaterPair.second);
        var other = new ComparablePair<string, string>(smallerPair.first, smallerPair.second);

        var result = pair.CompareTo(other);

        Assert.True(result > 0);
    }

    [Theory]
    [MemberData(nameof(GreaterToSmallerPairs))]
    public void CompareTo_ReturnsNegative(TestPair greaterPair, TestPair smallerPair)
    {
        var pair = new ComparablePair<string, string>(smallerPair.first, smallerPair.second);
        var other = new ComparablePair<string, string>(greaterPair.first, greaterPair.second);

        var result = pair.CompareTo(other);

        Assert.True(result < 0);
    }


    public static IEnumerable<object[]> ForCompareToNull =>
    [
        [new TestPair(null, null)],
        [new TestPair("55", null)],
        [new TestPair(null, "66")]
    ];

    public static IEnumerable<object[]> EqualPairs =>
    [
        [new TestPair(null, null), new TestPair(null, null)],
        [new TestPair("55", null), new TestPair("55", null)],
        [new TestPair(null, "66"), new TestPair(null, "66")],
        [new TestPair("55", "66"), new TestPair("55", "66")]
    ];

    public static IEnumerable<object[]> GreaterToSmallerPairs =>
    [
        [new TestPair(null, "55"), new TestPair(null, null)],
        [new TestPair("55", null), new TestPair(null, null)],
        [new TestPair(null, "55"), new TestPair(null, "33")],
        [new TestPair("55", null), new TestPair("33", null)],
        [new TestPair("55", null), new TestPair("33", "22")],
        [new TestPair("55", null), new TestPair("33", "88")]
    ];
}
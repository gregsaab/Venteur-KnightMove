using Common.Types;

namespace Specs.Common.Types;

public class PositionSpecs
{
    [Test]
    public void Should_create_valid_positions_when_constructed_with_string()
    {
        var position = new Position("A1");
        Assert.That(position.Column, Is.EqualTo(0));
        Assert.That(position.Row, Is.EqualTo(0));
        Assert.That(position.ToString(), Is.EqualTo("A1"));
        Assert.That(position.IsValid, Is.True);
    }
    
    [Test]
    public void Should_create_valid_positions_when_constructed_with_lower_case_string()
    {
        var position = new Position("a1");
        Assert.That(position.Column, Is.EqualTo(0));
        Assert.That(position.Row, Is.EqualTo(0));
        Assert.That(position.ToString(), Is.EqualTo("A1"));
        Assert.That(position.IsValid, Is.True);
    }
    
    [Test]
    public void Should_create_valid_positions_when_constructed_with_ints()
    {
        var position = new Position(5,2);
        Assert.That(position.Column, Is.EqualTo(5));
        Assert.That(position.Row, Is.EqualTo(2));
        Assert.That(position.ToString(), Is.EqualTo("F3"));
        Assert.That(position.IsValid, Is.True);
    }
    
    [Test]
    public void Invalid_positions_are_invalid()
    {
        var position1 = new Position("Z1");
        Assert.That(position1.IsValid(), Is.False);
        
        var position2 = new Position("A10");
        Assert.That(position2.IsValid(), Is.False);
        
        var position3 = new Position("ABC");
        Assert.That(position3.IsValid(), Is.False);
    }
}
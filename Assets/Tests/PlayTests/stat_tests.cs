using NUnit.Framework;

public class stat_tests
{
    [Test]
    public void when_not_set_returns_zero()
    {
        var stats = new Stats();
        Assert.AreEqual(0, stats.Get(StatType.MoveSpeed));
    }
    [Test]
    public void can_add()
    {
        var stats = new Stats();
        stats.Add(StatType.MoveSpeed, 3);
        Assert.AreEqual(3, stats.Get(StatType.MoveSpeed));    
        
        stats.Add(StatType.MoveSpeed, 5);
        Assert.AreEqual(8, stats.Get(StatType.MoveSpeed));
    }
    [Test]
    public void can_remove()
    {
        var stats = new Stats();
        stats.Add(StatType.MoveSpeed, 3);
        Assert.AreEqual(3, stats.Get(StatType.MoveSpeed));  
        
        stats.Remove(StatType.MoveSpeed, 3);
        Assert.AreEqual(0, stats.Get(StatType.MoveSpeed));
    }
}

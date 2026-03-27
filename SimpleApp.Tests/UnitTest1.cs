using Xunit;

namespace SimpleApp.Tests;

public class UnitTest1
{
    [Fact]
    public void Test_1_ShouldPass()
    {
        Assert.True(true);
    }

    [Fact]
    public void Test_ProjectExists()
    {
        // Simple indicator that the build environment works
        Assert.Equal("SimpleApp", "SimpleApp");
    }
}

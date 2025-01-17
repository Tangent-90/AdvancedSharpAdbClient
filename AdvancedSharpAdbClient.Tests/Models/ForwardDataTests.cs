﻿using Xunit;

namespace AdvancedSharpAdbClient.Tests
{
    /// <summary>
    /// Tests the <see cref="ForwardData"/> class.
    /// </summary>
    public class ForwardDataTests
    {
        [Fact]
        public void SpecTests()
        {
            ForwardData data = new()
            {
                Local = "tcp:1234",
                Remote = "tcp:4321"
            };

            Assert.Equal("tcp:1234", data.LocalSpec.ToString());
            Assert.Equal("tcp:4321", data.RemoteSpec.ToString());
        }
    }
}

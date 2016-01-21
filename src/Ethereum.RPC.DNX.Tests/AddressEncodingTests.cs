using System;
using Ethereum.RPC.ABI;
using Ethereum.RPC.Util;
using Xunit;
using System.Linq;
using System.Numerics;
using Ethereum.RPC;

namespace Ethereum.ABI.Tests.DNX
{
    public class AddressEncodingTests
    {
        [Fact]
        public virtual void ShouldEncodeAddressString()
        {
            AddressType addressType = new AddressType();
            var result2 = addressType.Encode("1234567890abcdef1234567890abcdef12345678").ToHex();
            Assert.Equal("0000000000000000000000001234567890abcdef1234567890abcdef12345678", result2);
        }
    }


    public class HexBigIntegerEncodingTests
    {

        /*
        
        When encoding QUANTITIES (integers, numbers): encode as hex, prefix with "0x", the most compact representation (slight exception: zero should be represented as "0x0"). Examples:

        0x41 (65 in decimal)
        0x400 (1024 in decimal)
        WRONG: 0x (should always have at least one digit - zero is "0x0")
        WRONG: 0x0400 (no leading zeroes allowed)
        WRONG: ff (must be prefixed 0x)
        
        */
        [Fact]
        public virtual void ShouldEncode0as0x0()
        {
            var x = new HexBigInteger(new BigInteger(0));
            Assert.Equal("0x0", x.HexValue);
        }


        [Fact]
        public virtual void ShouldEncodeCompactNoTraillingZeros()
        {
            var x = new HexBigInteger(new BigInteger(1024));
            Assert.Equal("0x400", x.HexValue); // not "0x0400"

        }

        [Fact]
        public virtual void ShouldDecodeCompactNoTraillingZeros()
        {
            var x = new HexBigInteger("0x400");
            Assert.Equal(1024, x.Value); 
        }

        [Fact]
        public virtual void ShouldDecode0x0()
        {
            var x = new HexBigInteger("0x0");
            Assert.Equal(0, x.Value);
        }
    }
}
﻿using System.Linq;
using System.Numerics;

namespace DUKPTCore
{
    internal static class BigIntegerExtensions
    {
        public static byte[] GetBytes(this BigInteger number)
        {
            return number.ToByteArray().Reverse().SkipWhile(b => b == 0).ToArray();
        }
    }
}
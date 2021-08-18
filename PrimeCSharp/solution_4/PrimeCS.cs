// ---------------------------------------------------------------------------
// PrimeCS.cs : Dave's Garage Prime Sieve in C#
// ---------------------------------------------------------------------------

using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace PrimeSieveCS
{
    class PrimeCS
    {
        /// Based off solution_2, but using a bool[] instead of a ulong[], IndexOf instead of an open-coded loop,
        /// and span-slicing to eliminate bounds checks on the marking pass.

        private sealed class PrimeSieve : IDisposable
        {
            private readonly int _sieveSize;
            private readonly int _halfSize;
            private bool[] _halfBits;

            private static readonly Dictionary<int, int> s_knownCounts = new Dictionary<int, int>
            {
                // Historical data for validating our results - the number of primes
                // to be found under some limit, such as 168 primes under 1000
                {          10,         4 },
                {         100,        25 },
                {       1_000,       168 },
                {      10_000,     1_229 },
                {     100_000,     9_592 },
                {   1_000_000,    78_498 },
                {  10_000_000,   664_579 },
                { 100_000_000, 5_761_455 }
            };

            public PrimeSieve(int size)
            {
                _sieveSize = size;
                _halfSize = (size + 1) / 2;
                _halfBits = ArrayPool<bool>.Shared.Rent(_halfSize + 1);
                Array.Clear(_halfBits, 0, _halfSize);
            }

            public void Dispose()
            {
                ArrayPool<bool>.Shared.Return(_halfBits);
                _halfBits = null;
            }

            /// <summary>Calculate the primes up to the specified limit</summary>
            public void RunSieve()
            {
                // We ignore even numbers by using values that track half of the actuals, and the only
                // number we keep in original form is the prime factor we're walking through the sieve

                int factor = 3;
                int halfFactor = factor >> 1;
                int halfRoot = ((int)(Math.Sqrt(_sieveSize) + 1)) >> 1;
                Span<bool> bits = _halfBits.AsSpan();

                while (halfFactor <= halfRoot)
                {
                    // Scan for the next unset bit which means it is a prime factor
                    int nextUnsetBit = bits[halfFactor.._halfSize].IndexOf(false);
                    int halfNum = nextUnsetBit == -1 ? _halfSize : nextUnsetBit + halfFactor;

                    factor = (halfNum << 1) + 1;
                    halfFactor = halfNum + 1;

                    // Mark off all multiples starting with the factor's square up to the square root of the limit
                    int square = (factor * factor) >> 1;
                    if (square < bits.Length && square < _halfSize)
                    {
                        Span<bool> bitsFromSquareToHalfSize = bits.Slice(square, _halfSize - square);
                        for (int i = 0; i < bitsFromSquareToHalfSize.Length; i += factor)
                        {
                            bitsFromSquareToHalfSize[i] = true;
                        }
                    }
                }
            }

            public void PrintResults(double duration, int passes)
            {
                int count = 1;
                for (int num = 3; num <= _sieveSize; num += 2)
                {
                    if (!_halfBits[num / 2])
                    {
                        count++;
                    }
                }

                bool containsValidResults = s_knownCounts.TryGetValue(_sieveSize, out int expected) && expected == count;

                Console.WriteLine($"Passes: {passes}, Time: {duration}, Avg: {duration / passes}, Limit: {_sieveSize}, Count: {count}, Valid: {containsValidResults}");
                Console.WriteLine();
                Console.WriteLine($"stephentoub;{passes};{duration:G6};1;algorithm=base,faithful=yes,bits=8");
            }
        }

        public static void Main()
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-US", false);

            int passes = 0;
            PrimeSieve sieve = null;

            Stopwatch sw = Stopwatch.StartNew();
            while (sw.ElapsedMilliseconds < 5_000)
            {
                sieve?.Dispose();
                sieve = new PrimeSieve(1_000_000);
                sieve.RunSieve();
                passes++;
            }

            if (sieve != null)
            {
                sieve.PrintResults(sw.Elapsed.TotalSeconds, passes);
                sieve.Dispose();
            }
        }
    }
}

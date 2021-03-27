// ---------------------------------------------------------------------------
// PrimeCS.cs : Dave's Garage Prime Sieve in C++
// ---------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace PrimeSieveCS
{
    class PrimeCS
    {
        private static readonly Dictionary<uint, uint> myDict = new Dictionary<uint, uint>
        {
            [10] = 1,                 // Historical data for validating our results - the number of primes
            [100] = 25,               // to be found under some limit, such as 168 primes under 1000
            [1000] = 168,
            [10000] = 1229,
            [100000] = 9592,
            [1000000] = 78498,
            [10000000] = 664579,
            [100000000] = 5761455,
        };

        class prime_sieve
        {
            private readonly uint sieveSize;
            private readonly byte[] rawbits;

            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            public prime_sieve(uint n)
            {
                sieveSize = n;
                rawbits = GC.AllocateUninitializedArray<byte>((int)((n / 8) + 1), pinned: true);
                rawbits.AsSpan().Fill(0xFF);
            }

            public uint countPrimes()
            {
                var sieveSize = this.sieveSize;
                var rawbits = this.rawbits;

                if (rawbits is null)
                {
                    throw new ArgumentNullException();
                }

                uint count = 0;
                for (uint i = 0; i < sieveSize; i++)
                    if (GetBit(rawbits, i))
                        count++;
                return count;
            }

            private bool validateResults()
            {
                return myDict.TryGetValue(sieveSize, out uint sieveResult) && (sieveResult == countPrimes());
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static bool GetBit(byte[] rawbits, uint index)
            {
                if ((index % 2) == 0)
                {
                    return false;
                }
                else
                {
                    index /= 2;
                    return (getrawbits(rawbits, index / 8U) & (1u << (int)(index % 8))) != 0;
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static void ClearBit(byte[] rawbits, uint index)
            {
                if ((index % 2) == 0)
                {
                    Console.WriteLine("You are setting even bits, which is sub-optimal");
                }
                else
                {
                    index /= 2;
                    getrawbits(rawbits, index / 8) &= (byte)~(1u << (int)(index % 8));
                }
            }

            // primeSieve
            // 
            // Calculate the primes up to the specified limit

            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            public void runSieve()
            {
                var sieveSize = this.sieveSize;
                var rawbits = this.rawbits;

                if (rawbits is null)
                {
                    throw new ArgumentNullException();
                }

                uint factor = 3;
                uint q = (uint)Math.Sqrt(sieveSize);

                while (factor < q)
                {
                    for (uint num = factor; num < sieveSize; num++)
                    {
                        if (GetBit(rawbits, num))
                        {
                            factor = num;
                            break;
                        }
                    }

                    // If marking factor 3, you wouldn't mark 6 (it's a mult of 2) so start with the 3rd instance of this factor's multiple.
                    // We can then step by factor * 2 because every second one is going to be even by definition

                    for (uint num = factor * 3; num < sieveSize; num += factor * 2)
                        ClearBit(rawbits, num);

                    factor += 2;
                }
            }

            public void printResults(bool showResults, double duration, uint passes)
            {
                var sieveSize = this.sieveSize;
                var rawbits = this.rawbits;

                if (rawbits is null)
                {
                    throw new ArgumentNullException();
                }

                if (showResults)
                    Console.Write("2, ");

                uint count = 1;
                for (uint num = 3; num <= sieveSize; num++)
                {
                    if (GetBit(rawbits, num))
                    {
                        if (showResults)
                            Console.Write(num + ", ");
                        count++;
                    }
                }

                if (showResults)
                    Console.WriteLine("");
                Console.WriteLine($"Passes: {passes}, Time: {duration}, Avg: {duration / passes}, Limit: {sieveSize}, Count: {count}, Valid: {validateResults()}");
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static ref byte getrawbits(byte[] rawbits, uint index)
            {
                return ref Unsafe.Add(ref MemoryMarshal.GetArrayDataReference(rawbits), (nint)index);
            }
        }

        static void Main(string[] args)
        {
            const long MillisecondsPerSecond = 1000;

            uint passes = 0;
            prime_sieve sieve = null;

            var stopwatch = Stopwatch.StartNew();

            while (stopwatch.ElapsedMilliseconds < (10 * MillisecondsPerSecond))
            {
                sieve = new prime_sieve(1000000);
                sieve.runSieve();
                passes++;
            }
            stopwatch.Stop();

            if (sieve != null)
                sieve.printResults(false, stopwatch.Elapsed.TotalSeconds, passes);
        }
    }
}

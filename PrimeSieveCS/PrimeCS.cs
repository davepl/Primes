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
        class prime_sieve
        {
            private int sieveSize = 0;
            private byte[] rawbits = null;
            private Dictionary<int, int> myDict = new Dictionary<int, int> 
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

            public prime_sieve(int n)
            {
                sieveSize = n;
                rawbits = new byte[(n / 8) + 1];
                rawbits.AsSpan().Fill(0xFF);
            }

            public int countPrimes()
            {
                int count = 0;
                for (int i = 0; i < sieveSize; i++)
                    if (GetBit((uint)i))
                        count++;
                return count;
            }

            private bool validateResults()
            {
                return myDict.TryGetValue(sieveSize, out int sieveResult) && (sieveResult == countPrimes());
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private bool GetBit(uint index)
            {
                if ((index & 1) == 0) // index % 2
                    return false;
                index /= 2;
                return (getrawbits(index / 8U) & (1u << (int)(index % 8))) != 0;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private void ClearBit(uint index)
            {
                if ((index & 1) == 0) // index % 2
                {
                    Console.WriteLine("You are setting even bits, which is sub-optimal");
                    return;
                }
                index /= 2;
                getrawbits(index / 8) &= (byte)~(1u << (int)(index % 8));
            }

            // primeSieve
            // 
            // Calculate the primes up to the specified limit

            public void runSieve()
            {
                int factor = 3;
                int q = (int)Math.Sqrt(sieveSize);

                while (factor < q)
                {
                    for (int num = factor; num < sieveSize; num++)
                    {
                        if (GetBit((uint)num))
                        {
                            factor = num;
                            break;
                        }
                    }

                    // If marking factor 3, you wouldn't mark 6 (it's a mult of 2) so start with the 3rd instance of this factor's multiple.
                    // We can then step by factor * 2 because every second one is going to be even by definition

                    for (int num = factor * 3; num < sieveSize; num += factor * 2)
                        ClearBit((uint)num);

                    factor += 2;
                }
            }

            public void printResults(bool showResults, double duration, int passes)
            {
                if (showResults)
                    Console.Write("2, ");

                int count = 1;
                for (int num = 3; num <= sieveSize; num++)
                {
                    if (GetBit((uint)num))
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

            private ref byte getrawbits(uint index)
            {
                return ref Unsafe.Add(ref MemoryMarshal.GetArrayDataReference(rawbits), (nint)index);
            }
        }

        static void Main(string[] args)
        {
            var passes = 0;
            prime_sieve sieve = null;

            var stopwatch = Stopwatch.StartNew();

            while (stopwatch.ElapsedTicks < (10 * TimeSpan.TicksPerSecond))
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

"""
Python Prime Sieve using Numpy and Numba
Based on MyFirstPython Program (tm) Dave Plummer 8/9/2018

Adapted by Joshua Estes 08/12/2021

This version is nearly a copy from PrimePython/solution3 but adapted to the Numba Python Compiler.
"""

from numba import njit
import numpy as np
from math import sqrt
import time

prime_numbers = {
    10: 4,
    100: 25,
    1000: 168,
    10000: 1229,
    100000: 9592,
    1000000: 78498,
    10000000: 664579,
    100000000: 5761455
}


@njit(fastmath=True)
def run_sieve(size):
    bits = np.ones(((size + 1) // 2), dtype=np.bool_)
    factor = 1
    q = sqrt(size) / 2
    bits_view = bits[factor:]
    while factor <= q:
        for v in bits_view.flat:
            if v:
                break
            factor += 1
        bits_view = bits[factor + 1:]
        factor2 = 2 * factor
        start = factor2 * (factor + 1) - factor - 1
        step = factor2 + 1
        bits_view[start::step] = False
        factor += 1


def count_primes(size):
    if size < 2:
        return 0
    return np.sum(np.ones(((size + 1) // 2), dtype=np.bool_))


def get_primes(size):
    if size < 2:
        return tuple()
    primes = np.where(np.ones(((size + 1) // 2), dtype=np.bool_))[0] * 2 + 1
    primes[0] = 2
    return primes


def print_results(duration, passes, size):
    print(f"Passes: {passes}, Time: {duration}, Avg: {duration / passes}, Limit: {size}, "
          f"Count: {len(get_primes(size))}", end="\n")
    print("jestes15_numpy/numba; %s; %s; 1; algorithm=base, faithful=no, bits=8" % (passes, duration))


def run():
    run_sieve(100)  # Give numba the function call to compile run_sieve
    passes, size = 0, int(input("Input the highest number: "))
    iStart: float = time.time()
    while (time.time() - iStart) < 5:
        run_sieve(size)
        passes += 1
    iEnd: float = time.time() - iStart
    print_results(iEnd, passes, size)


if __name__ == "__main__":
    run()

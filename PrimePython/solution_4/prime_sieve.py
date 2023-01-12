__author__      = "jugge83"

import timeit
import itertools
from numba import njit, b1, int32
import numpy as np


@njit(b1[:](int32), fastmath = True)
def run_sieve(limit):
    prime_list = np.full(limit // 2, True)  # defines a list for odd integers
    prime_list[0] = False                   # 1 is not a prime
    for factor in range(3, int(limit ** (1/2)) + 1, 2):
        if prime_list[factor // 2]:         # just looking at odd integers everything halves
            prime_list[(factor ** 2) // 2:limit // 2:factor] = False
    return prime_list[:(limit // 2) + 1]

def print_results(time_delta, passes, primes, limit):
    prime_counts = {
        10: 4,  # Historical data for validating our results - the number of primes
        100: 25,  # to be found under some limit, such as 168 primes under 1000
        1000: 168,
        10000: 1229,
        100000: 9592,
        1000000: 78498,
        10000000: 664579,
        100000000: 5761455,
    }
    #print(f"""Passes: {passes}\nTime: {time_delta}\nAvg: {time_delta/passes}\
    #    \nLimit: {limit}\nCount: {len(primes)}\nValid: {prime_counts[limit]==len(primes)}""")
    print(f"jugge83;{passes};{time_delta};1;algorithm=base,faithful=no,bits=1")

def primes(sieve):
    return itertools.chain([2], (prime * 2 + 1 for (prime, is_prime) in enumerate(sieve) if is_prime))

if __name__=="__main__":
    t_start = timeit.default_timer()  # Record our starting time
    passes = 0
    limit = 1_000_000
    while timeit.default_timer() - t_start < 5:
        prime_validators = run_sieve(limit)
        passes = passes + 1
    time_delta = timeit.default_timer() - t_start
    primes = list(primes(prime_validators))
    #print(primes[:100])
    print_results(time_delta, passes, primes, limit)
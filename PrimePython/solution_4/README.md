# Prime Sieve by jugge83

![Algorithm](https://img.shields.io/badge/Algorithm-base-green)
![Faithfulness](https://img.shields.io/badge/Faithful-no-yellowgreen)
![Parallelism](https://img.shields.io/badge/Parallel-no-green)
![Bit count](https://img.shields.io/badge/Bits-1-green)

This solution is the resulting work from the discussion in [issue #25](https://github.com/PlummersSoftwareLLC/Primes/issues?page=3&q=is%3Aissue) of the [original repository](https://github.com/PlummersSoftwareLLC/Primes). The code is optimized and around 1,5 times faster than solution_3. 
The code uses 2 external libraries, numpy and numba.
- Numpy is the de facto standard mathematical library for python.
- Numba is a jit compiler for python.

The solution is **unfaithful** due to the inclusion of two dependencies and is rewritten in a non object oriented manner. 
The rationale is: both libraries are used extensively in the community and the code structure is compact and more readable.
 

## Running with Python

Install Python: https://www.python.org/downloads/

Install the dependencies:
```
pip install numpy, numba
```
or:
```
cd path/to/requirements.txt
pip install -r requirements.txt
```

Run the file:
```
cd path/to/prime_sieve.py
python prime_sieve.py
```

## Running with Dockerfile

Build Docker image.

Run Docker image.

## Output

jugge83;7714;5.0001099;1;algorithm=base,faithful=no,bits=1
# Rust solution by Jesper Olsen; https://github.com/jesper-olsen

![Algorithm](https://img.shields.io/badge/Algorithm-base-green)
![Faithfulness](https://img.shields.io/badge/Faithful-no-yellowgreen)
![Parallelism](https://img.shields.io/badge/Parallel-no-green)
![Bit count](https://img.shields.io/badge/Bits-unknown-yellowgreen)

This implementation is based on Jonathan Sorenson's rolling sieve - it is an incremantal algorithm and unlike Eratosthenes sieve, 
it does NOT allocate and store a vector of bits to indicate membership. Rather it stores a growing wheel - the wheel grows with 
complexity O(sqrt(n)logn). Time complexity is O(nloglogn).

See description in https://arxiv.org/abs/1503.02592

## Run instructions

build-docker.sh	
run-docker.sh


## Output

```
jesper-olsen-rolling;175;5.025;1;algorithm=wheel,faithful=no,bits=0
```

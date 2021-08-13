# Python solution by jestes15

![Algorithm](https://img.shields.io/badge/Algorithm-base-green)
![Faithfulness](https://img.shields.io/badge/Faithful-yes-green)
![Parallelism](https://img.shields.io/badge/Parallel-no-green)
![Bit count](https://img.shields.io/badge/Bits-unknown-yellowgreen)

This is an unfaithful solution due to the inclusion of numpy and numba. Copied from solution 3 with some minor tune ups in the code.

# Results on my machine

 - Intel i7-10759H, Windows 11
 - Python: 3.8.8 64 bit


Report with select other solutions:
```
                                                            Single-threaded                                                             
┌───────┬────────────────┬──────────┬──────────────────────┬─────────┬──────────┬─────────┬───────────┬──────────┬──────┬───────────────┐
│ Index │ Implementation │ Solution │ Label                │ Passes  │ Duration │ Threads │ Algorithm │ Faithful │ Bits │ Passes/Second │
├───────┼────────────────┼──────────┼──────────────────────┼─────────┼──────────┼─────────┼───────────┼──────────┼──────┼───────────────┤
│   1   │ c              │ 1        │ mckoss-c830          │ 12469   │ 5.00000  │    1    │   wheel   │   yes    │ 1    │  2493.80000   │
│   2   │ python         │ 3        │ emillynge_numpy      │  7830   │ 5.00030  │    1    │   base    │    no    │ 8    │  1565.90684   │
│   3   │ python         │ 4        │ jestes15_numpy_numba │ 6917951 │ 5.00100  │    1    │   base    │    no    │ 8    │  1,383,5905   │
│   4   │ python         │ 2        │ ssovest              │  2179   │ 5.00037  │    1    │   base    │   yes    │ 8    │   435.76803   │
│   5   │ python         │ 1        │ davepl               │   40    │ 10.05578 │    1    │   base    │   yes    │      │    3.97781    │
└───────┴────────────────┴──────────┴──────────────────────┴─────────┴──────────┴─────────┴───────────┴──────────┴──────┴───────────────┘
```

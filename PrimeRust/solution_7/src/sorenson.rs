
// Sorenson's rolling sieve - yield next prime each time it is called.
// Complexity: O(nloglogn) time, O(sqrt(n)log(n)) bits,
//             O(logn/loglogn) incremental
// See algorithm description in paper:
// "Two Compact Incremental Prime Sieves", Jonathan P. Sorenson, Journal of Computation and Mathematics, 2015
// https://arxiv.org/abs/1503.02592

pub fn sieve() -> impl Iterator<Item = usize> {
    let start = 100;
    let mut r = f64::sqrt(start as f64) as usize + 1;
    let mut s = r * r;
    let mut delta = r + 2;
    let mut t = vec![vec![]; delta + 1];
    let primes = [
        2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89,
        97,
    ];

    for p in primes.into_iter().take_while(|&p| p <= r - 1) {
        let j = (p - (start % p)) % p;
        t[j].push(p);
    }
    let mut pos = 0;

    primes.into_iter().chain((start..).filter(move |&n| {
        let mut is_prime = true;
        while let Some(p) = t[pos].pop() {
            t[(pos + p) % delta].push(p);
            is_prime = false;
        }
        if n == s {
            if is_prime {
                t[(pos + r) % delta].push(r);
                is_prime = false;
            }
            r += 1;
            s = r * r;
        }
        pos = (pos + 1) % delta;
        if pos == 0 {
            delta += 2;
            t.extend([vec![], vec![]]);
        }
        is_prime
    }))
}

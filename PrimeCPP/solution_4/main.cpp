

#include <cstdio>
#include <cstdint>

typedef struct P10K {
	std::uint64_t prime[10'000];
	std::uint64_t size;
} prime_container;


inline void do_nothing(void) {/* this function just do nothing */}

struct sieve {
	
	private:
		prime_container m_p;
	
	
		void initialize() {
			for( std::uint64_t k = m_p.prime[0]; k < 10'000; k++){
				for( std::uint64_t l=0; l<m_p.size; l++) {
					if( k%m_p.prime[l] == 0)
						goto pass;
				}
				
				// since we reached her the number is prime you know and i know that XD
				m_p.prime[m_p.size++] = k;
				
				pass:
					do_nothing();
			}
		}
	
	public:
		sieve() : m_p({{2}, 1}) { initialize(); }
	
		prime_container get() const {
			return m_p;
		}
	
};


int main() {

    sieve s;
    std::uint64_t* p = s.get().prime;
    std::uint64_t  n = s.get().size;


    for( std::uint64_t i=0; i<n; i++)
        printf("%8ld\n", p[i]);
}


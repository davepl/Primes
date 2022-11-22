use::std::time::{Duration,Instant};

mod sorenson;

fn main() {
    let start=Instant::now();
    //let duration=Duration::new(5,0);

    let mut i=0;
    while Instant::now()-start<Duration::new(5,0) {
        for _p in sorenson::sieve().take_while(|&p|p<1_000_000) {
            //println!("{}", p);
        }
        i+=1;
    }
    let d=(Instant::now()-start).as_millis() as f64 / 1000.0;
    println!("jesper-olsen-rolling;{};{:?};1;algorithm=wheel,faithful=no,bits=0",i,d);
}

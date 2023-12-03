use std::{io, error::Error};

fn main() {
    let lines_bytewise: Vec<Vec<u8>> = io::stdin().lines().map(|x| x.unwrap().bytes().collect()).collect();

    println!("Hello, world!");
}

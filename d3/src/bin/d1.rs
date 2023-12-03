use std::{io, str, cmp::{max, min}};

fn main() {
    let mut arr: Vec<Vec<u8>> = io::stdin().lines().map(|x| x.unwrap().bytes().collect()).collect();

    for sub in arr.iter_mut() {
        sub.push(b'.');
        sub.insert(0, b'.')
    }
    let mut count = 0;
    for i in 0..arr.len() {
        let mut start = 0;
        let mut inside = false;
        for j in 0..arr[i].len() {
            if (arr[i][j] as char).is_digit(10) {
                if !inside {
                    start = j;
                    inside = true;
                }
            } else if inside {
                let num: u64 = str::parse(str::from_utf8(&arr[i][start..j]).expect("evil non utf-8. dafuq?")).expect("you done messed up, a-aron");
                if look_for_surrounding(&arr, i, start, j) {
                    count += num;
                    println!("counted ({start}, {i}) -> ({j}, {i}), representing {num}, sum = {count}");
                }
                else {
                    println!("skipped ({start}, {i}) -> ({j}, {i}), representing {num}, sum = {count}");
                }
                inside = false;
            }

        }
    }
    println!("result: {count}");
}

fn look_for_surrounding(arr: &Vec<Vec<u8>>, i: usize, start: usize, end: usize) -> bool {
    let mut found = false;
    for di in -1..=1 {
        let row = i as isize + di;
        if row < 0 || row as usize >= arr.len() {
            continue;
        }
        let row = row as usize;

        let start = max(0, start as isize - 1) as usize;
        let end = min(end + 1, arr[i].len());
        println!("{}", str::from_utf8(&arr[row][start..end]).unwrap());
        for col in start..end {
            let c = arr[row][col] as char;
            if !c.is_digit(10) && c != '.' {
                found = true;
            }
        }
    }

    found
}

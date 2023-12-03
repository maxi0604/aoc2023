use std::{
    cmp::{max, min},
    collections::HashMap,
    io, str,
};

type Field = Vec<Vec<u8>>;
type GearList = HashMap<(usize, usize), Vec<u64>>;

fn main() {
    println!("p2");
    let mut arr: Field = io::stdin()
        .lines()
        .map(|x| x.unwrap().bytes().collect())
        .collect();
    let mut hm = HashMap::new();

    for sub in arr.iter_mut() {
        sub.push(b'.');
        sub.insert(0, b'.')
    }

    for i in 0..arr.len() {
        let mut start = 0;
        let mut inside = false;
        for j in 0..arr[i].len() {
            if (arr[i][j] as char).is_ascii_digit() {
                if !inside {
                    start = j;
                    inside = true;
                }
            } else if inside {
                let num: u64 =
                    str::parse(str::from_utf8(&arr[i][start..j]).expect("evil non utf-8. dafuq?"))
                        .expect("you done messed up, a-aron");
                search_and_store_gear(&arr, &mut hm, num, i, start, j);
                inside = false;
            }
        }
    }

    dbg!(&hm);
    let result: u64 = hm
        .values()
        .filter(|x| x.len() == 2)
        .map(|x| x[0] * x[1])
        .sum();
    println!("result: {result}");
}

fn search_and_store_gear(
    arr: &Field,
    store: &mut GearList,
    num: u64,
    i: usize,
    start: usize,
    end: usize,
) {
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
            if c == '*' {
                store.entry((row, col)).or_default().push(num);
            }
        }
    }
}

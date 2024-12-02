use std::env;
use std::fs;

fn main() {
    let args: Vec<String> = env::args().collect();

    if args.len() < 2 {
        panic!("Please provide an input argument containing the path to the input file");
    }

    let contents = fs::read_to_string(&args[1])
        .expect("Should have been able to read the file");

    let lines: Vec<&str> = contents.split('\n').collect();

    // iterate over lines collecting an array of 1s and 0s for each based upon the result of is_safe(). Sum to get the answer for Part 1
    let score: i32 = lines.iter().map(|l| is_safe(l) as i32).collect::<Vec<i32>>().iter().sum();

    println!("score: {score}")
}

// takes a line and returns true if safe (according to Part 1), false otherwise
fn is_safe(line: &str) -> bool {
    let values: Vec<i32> = line.split(" ").map(|n| n.parse::<i32>().unwrap()).collect();

    // use windows to get adjacent pairs and collect the differences between them
    let differences: Vec<i32> = values
        .windows(2)
        .map(|vs| {
            let [x, y] = vs else { unreachable!() };
            y - x
        })
        .collect::<Vec<i32>>();

    return
        (differences.iter().all(|&d| d < 0) || differences.iter().all(|&d| d > 0)) // either all decreasing or all increasing
        && !differences.iter().any(|&d| i32::abs(d) > 3) // and no difference greater than 3
}
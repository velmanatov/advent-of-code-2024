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

    // iterate over lines counting all where is_safe1() returns true for Part 1. And then similarly with is_safe2 for Part.
    let answer1: i32 = lines.iter().filter(|l| is_safe1(l)).count() as i32;
    let answer2: i32 = lines.iter().filter(|l| is_safe2(l)).count() as i32;

    println!("Answer to part 1: {answer1}");
    println!("Answer to part 2: {answer2}");
}

// takes a line and returns true if safe (according to Part 1), false otherwise
fn is_safe1(line: &str) -> bool {
    let values: Vec<i32> = line.split(" ").map(|n| n.parse::<i32>().unwrap()).collect();
    return is_safe(values);
}

// takes a line and returns true if safe (according to Part 1) - i.e. as per is_safe1
// otherwise iteratively checks if the line is safe when a single number is removed. Returns true if there is any case where that applies
fn is_safe2(line: &str) -> bool {
    let is_safe1: bool = is_safe1(line);
    let mut is_safe2: bool = false;

    if !is_safe1 {
        let values: Vec<i32> = line.split(" ").map(|n| n.parse::<i32>().unwrap()).collect();
        // this is where I couldn't find a more rust-like way to do what I wanted... so iterate over values... mutate the values to remove each in turn
        // if any one is "safe" with the value removed then we can break as it is safe according to part 2 of the problem
        // this could also be optimised quite a bit. e.g. if the element in question is not one that is part of a pair with an invalid diff we do not really need
        // to check it.
        for (i, _) in values.iter().enumerate() {
            let mut adjusted_values = values.clone();
            adjusted_values.remove(i);
            is_safe2 = is_safe(adjusted_values);
            if is_safe2 {
                break;
            }
        }
    }

    return is_safe1 || is_safe2;   
}

// checks for "safeness" of a given line according to rules given in part 1
fn is_safe(values: Vec<i32>) -> bool {
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
import * as fs from 'fs';
import { PebbleEvolver } from './pebble-evolver';

const fileData: string = fs.readFileSync('input.txt', 'utf-8');
let pebbles: number[] = fileData.split(/\s+/).map(str => +str);
let pebbles2: number[] = Object.assign([], pebbles);


for (let i = 0; i< 25; i++) {
    pebbles = pebbles.flatMap(p => PebbleEvolver.blink(p));
}

console.log('Answer 1: %d', pebbles.length);

let answer2: number = 0;
for (const pebble of pebbles2) {

    answer2 += PebbleEvolver.countPebblesAfterBlinks(pebble, 75);
    console.log('pebble %d - answer now %d', pebble, answer2);
}
console.log('Answer 2: %d', answer2);
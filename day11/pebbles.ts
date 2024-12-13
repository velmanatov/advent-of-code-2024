import * as fs from 'fs';
import { PebbleEvolver } from './pebble-evolver';

const fileData: string = fs.readFileSync('input.txt', 'utf-8');
let pebbles: number[] = fileData.split(/\s+/).map(str => +str);


for (let i =0; i< 25; i++) {
    pebbles = pebbles.flatMap(p => PebbleEvolver.blink(p));
}

console.log('Answer 1: %d', pebbles.length);
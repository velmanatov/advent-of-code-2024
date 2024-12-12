import * as fs from 'fs';

const fileData: string = fs.readFileSync('input.txt', 'utf-8');
const lines: string[] = fileData.split('\n');
let topographicMap: number[][] = [];
let trailheads: [number,number][] = [];
const directions: [number, number][] = [[-1 , 0], [0, -1], [1, 0], [0, 1]];

// parse the file into the topographicMap ( an array of array of numbers)
for(let y= 0; y < lines.length; y++) {
    let line = lines[y];
    topographicMap[y] = [];
    for(let x = 0; x < line.length; x++) {
        const value: number = +line.charAt(x);
        topographicMap[y].push(value);
        if (value === 0) {
            trailheads.push([x, y]);
        }
    }
}

const mapWidth = topographicMap[0].length;
const mapHeight = topographicMap.length;

// find all valid trails from each trailhead then find and count the unique end points and add to the answer for part 1
let answer1: number = 0;
let answer2: number = 0;
for (let trailhead of trailheads) {
    const validTrails = findTrailsFrom(trailhead);
    answer2 += validTrails.length;
    // unfortunately I've used tuples for my coords and [x, y] !== [x, y] so using toString() as a hack so the set is unqiue
    const uniqueTrailEnds = [...new Set(validTrails.map(trail => trail.at(-1).toString()))];
    answer1 += uniqueTrailEnds.length;
}

console.log('Answer 1: %d', answer1)
console.log('Answer 2: %d', answer2)

function findTrailsFrom(startPoint: [number, number]) : [number, number][][] {

    let validSubtrails: [number, number][][] = [[[startPoint[0], startPoint[1]]]];

    for (let height = 1; height <= 9; height ++) {
        validSubtrails = validTrailsNextStep(validSubtrails, height)
    }

    return validSubtrails;
}

function validTrailsNextStep(trailsSoFar: [number, number][][], expectedHeight: number) : [number, number][][] {
    let trailsWithValidNextSteps: [number, number][][] = [];
    for(let trail of trailsSoFar) {
        const lastStep: [number, number] = trail.at(-1);
        // add 0-4 sub-trails on each iteration
        for (let direction of directions) {
            const nextX: number = lastStep[0] + direction[0];
            const nextY: number = lastStep[1] + direction[1];
            if (inBounds(nextX, nextY) && topographicMap[nextY][nextX] === expectedHeight) {
                const newTrail = trail.concat([[nextX, nextY]]);
                trailsWithValidNextSteps.push(newTrail);
            }
        }
    }

    return trailsWithValidNextSteps;
}

function inBounds(x: number, y: number): boolean {
    return x >= 0 && y >= 0 && x < mapWidth && y < mapHeight;
}


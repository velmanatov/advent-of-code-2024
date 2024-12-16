import * as fs from 'fs';
import { GardenPlot } from './garden-plot';
import { GridSquare } from './grid-square';

const fileData: string = fs.readFileSync('input.txt', 'utf-8');

const garden: string[][] = fileData.split('\n').map(x => x.split(''));

// use this to keep track grid square by grid square which plot that square belongs to
let plotMap: GardenPlot[][] = [];

let uniquePlots: Set<GardenPlot> = new Set<GardenPlot>();

for (let y = 0 ; y < garden.length; y++) {
    plotMap[y]= [];
    let previousPlot: GardenPlot = undefined;
    for (let x = 0; x < garden[0].length; x++) {
        const plant = garden[y][x];
        let plot = new GardenPlot();
        // is this actually part of the previous plot?
        if (plant === previousPlot?.plant) {
            plot = previousPlot;
        } else {
            plot.plant = plant;
            uniquePlots.add(plot);
        }
        const square = new GridSquare(x, y, garden);
        plot.squares.add(square);

        // does this plot actually need merged with a plot with squares above?
        if (y > 0 && garden[y-1][x] === plant && plotMap[y-1][x] != plot) {
            plot = merge(plot, plotMap[y-1][x]);
        }

        plotMap[y][x] = plot;
        previousPlot = plot;
    }

}

//uniquePlots.forEach(plot => console.log(plot.toString()));

let answer1: number = 0;
uniquePlots.forEach(plot => answer1 += plot.squares.size * plot.getPerimeter());

console.log(`Answer 1: ${answer1}`);

// Merge one plot with another we already know about. Update the plot map
function merge(plot1: GardenPlot, plot2: GardenPlot): GardenPlot {
    // Merge squares from plot1 into plot2
    plot2.squares = plot2.squares.union(plot1.squares);

    // Update plot1 squares on the plot map so that they are recorded as part of plot2 
    for (let gridSquare of plot1.squares) {
        plotMap[gridSquare.y][gridSquare.x] = plot2;
    }

    // Forget about plot1
    uniquePlots.delete(plot1);

    return plot2;
}

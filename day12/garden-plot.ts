import { GridSquare } from "./grid-square";

export class GardenPlot {
    public plant: string;
    public squares: Set<GridSquare> = new Set <GridSquare>();

    public getPerimeter(): number {
        return this.squares.values().reduce((sum, current) => sum + current.borderCount, 0);
    }

    public toString(): string {
        const firstSquare: GridSquare = this.squares.values().next().value;
        return `Plot at ${firstSquare?.x}, ${firstSquare?.y} with plant ${this.plant} - area ${this.squares.size} - perimeter ${this.getPerimeter()}`;
    }
}
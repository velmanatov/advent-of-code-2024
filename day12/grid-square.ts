export class GridSquare {
    public x: number;
    public y: number;
    public garden: string[][];
    public borderCount: number = 0;

    private maxX: number;
    private maxY: number;

    private static directions: [number, number][] = [[1, 0], [0, 1], [-1, 0], [0, -1]];

    constructor(x: number, y: number, garden: string[][]) {
        this.x = x;
        this.y = y;
        this.garden = garden;
        this.maxY = garden.length - 1;
        this.maxX = garden[0].length - 1;
        this.borderCount = this.countBorders();
    }

    private countBorders(): number {
        let count: number = 0;
        // Count borders on edge of garden. I decided to account for a grid of one square size
        if (this.x === 0)
            count++;
        if (this.y === 0)
            count++;
        if (this.x === this.maxX)
            count++;
        if (this.y === this.maxY)
            count++;
  
        // Count borders onto plots of other kinds
        for (let direction of GridSquare.directions) {
            const nextX = this.x + direction[0];
            const nextY = this.y + direction[1];

            if (nextX >= 0 && nextX <= this.maxX && nextY >= 0 && nextY <= this.maxY ) {
                if (this.garden[nextY][nextX] !== this.garden[this.y][this.x]) {
                    count++;
                    //console.debug(`At ${this.x}, ${this.y} - ${direction} - ${this.garden[nextY][nextX]} != ${this.garden[this.y][this.x]}`);
                }
            }
        }

        return count;
    }
}
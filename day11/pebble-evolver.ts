export class PebbleEvolver {

    // This takes one pebble and applies a single blink, returning an array of evolved pebbles.
    // Used in Part 1 solution, but does not scale to Part
    static blink(pebble: number) : number[] {
        if (pebble === 0) {
            return [1];
        } else {
            const pebbleStr: string = '' + pebble;
            if (pebbleStr.length % 2 === 0) {
                return [+pebbleStr.substring(0, pebbleStr.length/2), +pebbleStr.substring(pebbleStr.length/2)];
            }
            return [pebble * 2024];
        }
    }

    // Took so much help from reddit. Can't imgaine how long it would have taken to come up with this exact thing myself.
    // but we can solve this recursively for Part 2. The above approach, maintaining an actual full expanded array of pebbles, does not scale!
    // We only need the count for our answer and we can observe that the answer we get for each pebble is basically independent of the other pebbles
    static countPebblesAfterBlinks(pebble: number, nBlinks: number): number {
        // If we need zero more blinks we know we just need to add one more (the pebble under consideration)
        if (nBlinks === 0)
            return 1;
        if (pebble === 0)
            // For rule one we just need to apply our recursive count function again one a pebble with value 1
            return PebbleEvolver.countPebblesAfterBlinks(1, nBlinks-1);
        else {
            const pebbleStr: string = '' + pebble;
            if (pebbleStr.length % 2 === 0) {
                // For rule 2 we can apply our count function independently and just add the two results
                return PebbleEvolver.countPebblesAfterBlinks(+pebbleStr.substring(0, pebbleStr.length/2), nBlinks-1) +
                    PebbleEvolver.countPebblesAfterBlinks(+pebbleStr.substring(pebbleStr.length/2), nBlinks-1);
            }
            // For rule 3 - like rule 1 - we can just apply the count function to the pebble with the new number.
            // Preumably this is where we coudl optimise further?
            return PebbleEvolver.countPebblesAfterBlinks(pebble * 2024, nBlinks-1);            
        }     
    }
}
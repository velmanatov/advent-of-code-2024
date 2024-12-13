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

    // For Part 2 we will keep a global track of what a given number of blinks results in for a pebbel value
    // as the answer will always be the same
    private static pebbleCounts: { [key: string]: number } = {};

    // Took so much help from reddit. Can't imgaine how long it would have taken to come up with this exact thing myself.
    // but we can solve this recursively for Part 2. The above approach, maintaining an actual full expanded array of pebbles, does not scale!
    // We only need the count for our answer and we can observe that the answer we get for each pebble is basically independent of the other pebbles
    static countPebblesAfterBlinks(pebble: number, nBlinks: number): number {
        // If we need zero more blinks we know we just need to add one more (the pebble under consideration)
        if (nBlinks === 0)
            return 1;

        if (pebble === 0) {
            return PebbleEvolver.getCount(1, nBlinks - 1);
        } else {
            const pebbleStr: string = '' + pebble;
            if (pebbleStr.length % 2 === 0) {
                // For rule 2 we can apply our count function independently and just add the two results
                return PebbleEvolver.getCount(+pebbleStr.substring(0, pebbleStr.length/2), nBlinks-1) +
                    PebbleEvolver.getCount(+pebbleStr.substring(pebbleStr.length/2), nBlinks-1);
            }
            // For rule 3 - like rule 1 - we can just apply the count function to the pebble with the new number.
            // Preumably this is where we coudl optimise further?
            return PebbleEvolver.getCount(pebble * 2024, nBlinks-1);            
        }     
    }

    // Either get a cached count or otherwise actually work out the count recursively for the current number of blinnks and pebble number
    private static getCount(pebble: number, nBlinks: number, )  {
        // couldn't get index type containing indexed type right so just use a string key instead
        let lookupKey: string = '' + nBlinks + '.' + pebble;

        let count: number = PebbleEvolver.pebbleCounts[lookupKey];
        if (count)
            return count;
        
        // For rule one we just need to apply our recursive count function again one a pebble with value 1
        count = PebbleEvolver.countPebblesAfterBlinks(pebble, nBlinks);
        PebbleEvolver.pebbleCounts[lookupKey] = count;
        return count;
    } 
}
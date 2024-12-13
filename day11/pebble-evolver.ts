export class PebbleEvolver {

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

}
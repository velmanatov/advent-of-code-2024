## Advent of Code 2024

OK. It's time for [Advent of Code 2024](https://adventofcode.com/2024/). From my quick look at the 2023 puzzles I can't see me getting through all 24.
But let's give it a go! I'm still keen to get a taste of different languages and frameworks in the process.
I'll be looking for readable code over performant code and not worrying too much about error handling unless relevant to the puzzle.

### Calendar

1. ["Historian Hysteria"](https://adventofcode.com/2024/day/1) - I completed this in .NET (C#). I use .NET in my job, but on Windows. I set this up on Linux and used VS Code as my IDE to see what that experience was like. Quite honestly it wasn't bad. Would take some getting used to the VS Code tooling. But the cut-down experience might even be preferable. Would be interested to see how well it worked for a larger solution though.
2. ["Red-Nosed Reports"](https://adventofcode.com/2024/day/2) - I've been wanting to try one of these in rust. So gave it a go. Stressed for a long time over the correct way to mutate a vector, removing a single element (which I'm still not happy with). That, plus getting used to syntax in general took me a while!
3. ["Mull It Over"](https://adventofcode.com/2024/day/3) - Solved in python. Got very bogged down in part 2 where I was trying to use a regex to capture a group between do() and don't() and pass that to the function used for part 1. Couldn't get the exact regex magic I needed to do that in the end (also needed to deal with the start of the string differently). So, in the end I went for a manual scan searching for don't() and then do() and flipping a boolean. A tad inelegant, but if I'd gone with something like that from the beginning I'd have saved myself a lot of time!
4. ["Ceres Search"](https://adventofcode.com/2024/day/4) - Used python again. I must have made a meal of it, but Part 1 seemed more laborious today. Constructing diagonals from the grid in particular. Part 2 was quicker to code. I figured we were just looking for all 3x3 blocks in one of four patterns - so just literally started top left and searched every set of 3x3 blocks, checking against those four patterns. In the end I went back and re-implemented Part 1 to take each character in the grid as a potential starting 'X' and search in 8 directions for XMAS. Slightly more readable?
5. ["Print Queue"](https://adventofcode.com/2024/day/5) - Solved in ruby. Losing momentum here. I've spent so much time on off-by-one problems! And working with unfamiliar languages is really slowing this down. Part 1 was fairly straightforward but took me ages to form the code I wanted to write. Part 2 I was unsure of a good algorithm. Settled on a form of simple bubble sort. Eventually hit the problem that had been bothering me all along. Sometimes we don't have a rule that says X must be to the left of something, but we do have one that says something must always be to the left of X. So we need to check both to ensure ordering.
6. ["Guard Gallivant"](https://adventofcode.com/2024/day/6) - Solved in python. Am I am python developer now? Apologies to all python developers. I don't even know stylistic conventions at the moment! Essentially the Part 1 problem is: trace a path through a grid including a set of obstacles following the given movement rules. Keep going until you escape the bounds of the grid. Part 1 was relatively straightforward. Hooray! Part 2 was not! All attempts to be even vaguely clever tripped me up and eventually brute force was the answer.
7. ["Bridge Repair"](https://adventofcode.com/2024/day/7) - I was once a Java developer. It is over 13 years since I wrote any Java. Solved this one in Java. I am rusty. Java did not feel as nice as it used to! Thankfully this was straightforward it seems. My solution is completely unoptimised. Just does all combinations of all operators. Part 2 was the tiniest increment on Part 1. Given how long I spent on day 6 I am going to take this as a quick win and not over-think it!
8. ["Resonant Collinearity"](https://adventofcode.com/2024/day/8) - .NET (C#) again today. Straight towards a valid solution. But ofc ourse introduced a pesky off-by-one probem for yself without noticing. Took me longer than it should have to spot!
9. ["Disk Defragmenter"](https://adventofcode.com/2024/day/9) - .NET (C#) again out of laziness. Essentially two unrelated solutions bunged into one file. So a bit messy! Tripped up over an arithemtic overflow that I somehow failed to spot in Part 1.
10. ["Hoof It"](https://adventofcode.com/2024/day/10) Typescript today I've rarely used it in isolation as opposed to alongside some frontend framework. Yet again using a tuple to represent 2D coordinates. Not sure what the best option is, but feels weird how few languages have an appropriate built-in type. Part 2 was super quick since I'd basically already solved it in Part 1.
11. ["Plutonian Pebbles"](https://adventofcode.com/2024/day/11) Typescript again today. Quite enjoying it. Today I used bun's ability to run typescript directly (it internally does the transpilation). Part 1 was particularly straightforward.

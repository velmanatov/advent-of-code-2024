import re

file_obj = open("input.txt", "r") 
file_data = file_obj.read() 
file_obj.close()

def get_vertical_lines(text_grid: str):
    lines = text_grid.splitlines()
    vertical_lines = [''] * len(lines)

    for line in lines:
        for x, char in enumerate(line):
            vertical_lines[x] += char
    return vertical_lines

def get_diagonal_lines(text_grid: str, min_line_length: int):
    lines = text_grid.splitlines()
    diagonal_lines = []

    max_x = len(lines[0])
    max_y = len(lines)

    # traverse diagonals starting on top row ->
    for x in range(0, max_x - min_line_length + 1):
        line = ''
        offset = 0
        while offset < max_y and x + offset < max_x:
            line += lines[offset][x + offset]
            offset = offset + 1
        diagonal_lines.append(line)

    # traverse diagonals starting in left col ->
    for y in range(1, max_y - min_line_length + 1):
        line = ''
        offset = 0
        while y + offset < max_y and offset < max_x:
            line += lines[y + offset][offset]
            offset = offset + 1
        diagonal_lines.append(line)

    # traverse diagonals starting on top row <-
    for x in range(max_x - 1, min_line_length - 1, -1):
        line = ''
        offset = 0
        while offset < max_y and x - offset >= 0:
            line += lines[offset][x - offset]
            offset = offset + 1
        diagonal_lines.append(line)

    # traverse diagonals starting in right col <-
    for y in range(1, max_y - min_line_length + 1):
        line = ''
        offset = 0
        while y + offset < max_y and offset < max_x:
            line += lines[y + offset][max_x - offset - 1]
            offset = offset + 1
        diagonal_lines.append(line)      

    return diagonal_lines

# part 2 is different count MAS (or SAM) in an x-shape like below.
# I think there are only these 4 sets of 3x3 blocks that we are looking for:

# M-S   S-M   M-M   S-S
# -A-   -A-   -A-   -A-
# M-S   S-M   S-S   M-M
def get_x_mas_count(text_grid):
    lines = text_grid.splitlines()

    max_x = len(lines[0])
    max_y = len(lines)
    count = 0

    for y in range(0, max_x-2):
        for x in range(0, max_y-2):
            if lines[y + 1][x +1] == 'A' and (
                    (lines[y][x] == 'M' and lines[y][x+2] =='S' and lines[y+2][x] == 'M' and lines[y+2][x+2] == 'S') or
                    (lines[y][x] == 'S' and lines[y][x+2] =='M' and lines[y+2][x] == 'S' and lines[y+2][x+2] == 'M') or
                    (lines[y][x] == 'M' and lines[y][x+2] =='M' and lines[y+2][x] == 'S' and lines[y+2][x+2] == 'S') or
                    (lines[y][x] == 'S' and lines[y][x+2] =='S' and lines[y+2][x] == 'M' and lines[y+2][x+2] == 'M') 
                ):
                count = count + 1

    return count

# a re-implementation of Part 1. Maybe slightly more readable?
# starting at the top left and progressing through all letters in the grid,
# check, starting from that letter in the grid, if the 4 chars in any of the 8 valid directions say XMAS
def part_1_reimplementation(text_grid: str):
    lines = text_grid.splitlines()

    max_x = len(lines[0])
    max_y = len(lines)
    count = 0

    for y in range(0, max_x):
        for x in range(0, max_y):
            if x < max_x-3 and check_direction(x, y, 1, 0, lines):
                count = count + 1
            if y < max_y-3 and check_direction(x, y, 0, 1, lines):
                count = count + 1
            if x < max_x-3 and y < max_y-3 and check_direction(x, y, 1, 1, lines):
                count = count + 1
            if x >= 3 and y < max_y-3 and check_direction(x, y, -1, 1, lines):
                count = count + 1

    return count

# check if the 4 letters found in lines starting at a specified x, y position in a direction defined as a dx, dy vector spell out XMAS or SAMX
def check_direction(x: int, y: int, dx: int, dy: int, lines: list[str]):
    return (lines[y][x] == 'X' and lines [y + dy][x + dx] == 'M' and lines[y + 2*dy][x + 2*dx] == 'A' and lines[y + 3*dy][x + 3*dx] == 'S') or (
        lines[y][x] == 'S' and lines [y + dy][x + dx] == 'A' and lines[y + 2*dy][x + 2*dx] == 'M' and lines[y + 3*dy][x + 3*dx] == 'X')

# part 1 is simply to count all occurrences of XMAS - horizontally, vertically and diagonally in the grid. And also considering backwards matches
answer1 = 0
# no point iterating over lines for these ones that are already in the correct orientation
answer1 += len(re.findall("(?=(XMAS|SAMX))", file_data))

# now "rotate the grid" 90 degrees and do the same to get vertical matches
for vertical_line in get_vertical_lines(file_data):
    answer1 += len(re.findall("(?=(XMAS|SAMX))", vertical_line))

# now get diagonal lines of 4 or more chars going L->R, U->D as well as R-L, U->D
for diagonal_line in get_diagonal_lines(file_data, 4):
    answer1 += len(re.findall("(?=(XMAS|SAMX))", diagonal_line))

print('Answer 1', answer1)
print('Answer 1 Re-implemented', part_1_reimplementation(file_data))
print('Answer 2', get_x_mas_count(file_data))

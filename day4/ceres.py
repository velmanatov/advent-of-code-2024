import re

file_obj = open("input.txt", "r") 
file_data = file_obj.read() 
file_obj.close()

def get_vertical_lines(text_grid):
    lines = text_grid.splitlines()
    vertical_lines = [''] * len(lines)

    for line in lines:
        for x, char in enumerate(line):
            vertical_lines[x] += char
    return vertical_lines

def get_diagonal_lines(text_grid, min_line_length):
    lines = text_grid.splitlines()
    diagonal_lines = []

    max_x = len(lines[0])
    max_y = len(lines)

    # traverse diagonals starting on top row
    for x in range(0, max_x - min_line_length + 1):
        line = ''
        offset = 0
        while offset < max_y and x + offset < max_x:
            line += lines[offset][x + offset]
            offset = offset + 1
        diagonal_lines.append(line)

    # traverse diagonals starting in left col
    for y in range(1, max_y - min_line_length + 1):
        line = ''
        offset = 0
        while y + offset < max_y and offset < max_x:
            line += lines[y + offset][offset]
            offset = offset + 1
        diagonal_lines.append(line)

    # traverse diagonals starting on top row
    for x in range(max_x - 1, min_line_length - 1, -1):
        line = ''
        offset = 0
        while offset < max_y and x - offset >= 0:
            line += lines[offset][x - offset]
            offset = offset + 1
        diagonal_lines.append(line)

    # traverse diagonals starting in right col
    for y in range(1, max_y - min_line_length + 1):
        line = ''
        offset = 0
        while y + offset < max_y and offset < max_x:
            line += lines[y + offset][max_x - offset - 1]
            offset = offset + 1
        diagonal_lines.append(line)      

    return diagonal_lines

# part 1 is simply to count all occurrences of XMAS - horizontally, vertically and diagonally in teh grid. And also considering backwards matches
answer1 = 0
# no point iterating over lines for these ones that are already in the correct orientation
answer1 += len(re.findall("(?=(XMAS|SAMX))", file_data))

# now "rotate the grid" 90 degrees and do the same to get vertical matches
for vertical_line in get_vertical_lines(file_data):
    answer1 += len(re.findall("(?=(XMAS|SAMX))", vertical_line))

# now get diagonal lines of 4 or more chars going L->R, U->D as well as R-L, U->D
for diagonal_line in get_diagonal_lines(file_data, 4):
    answer1 += len(re.findall("(?=(XMAS|SAMX))", diagonal_line))

print(answer1)

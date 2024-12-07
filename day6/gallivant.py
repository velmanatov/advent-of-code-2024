file_obj = open("calibration.txt", "r") 
file_data = file_obj.read() 
file_obj.close()

grid = file_data.splitlines()

def update_grid(x, y, char):
    line = grid[y]
    altered_line = line[:x] + char + line[x + 1:]
    grid[y] = altered_line

class Guard:
    current_direction = (0, -1)
    current_position = (-1, -1)
    squares_covered = 1 # count the starting square

    def turn(self):
        dx = self.current_direction[0]
        dy = self.current_direction[1]
        self.current_direction = (-dy, dx)

    # check ahead for obstacle. turn if necessary. return True if would exit grid on next move
    def check_ahead(self, grid):
        next_x = self.current_position[0] + self.current_direction[0]
        next_y = self.current_position[1] + self.current_direction[1]
        
        max_x = len(grid[0]) 
        max_y = len(grid)

        #check if we're out grid bounds now
        if next_x < 0 or next_y < 0 or next_x >= max_x or next_y >= max_y:
            return True

        # look ahead for # and change direction if necessary
        if grid[next_y][next_x] == '#':
            self.turn()

        return False   

    def move_one_square(self, do_trace = True):
        self.current_position = (self.current_position[0] + self.current_direction[0], self.current_position[1] + self.current_direction[1])
        # check if we went here already. if not count the square and trace our step
        if do_trace and not grid[self.current_position[1]][self.current_position[0]] == 'X':
            self.squares_covered = self.squares_covered + 1
            update_grid(self.current_position[0], self.current_position[1], 'X')

# complete patrol as per movement rules from part 1 returning the number of squares covered
# optiional argument check_for_loops can be set to True. This will return -1 if a loop is detected.
def part_1(x, y, check_for_loops = False):
    guard = Guard()
    guard.current_position = (x, y)
    update_grid(x, y, 'X')

    done = False

    path_so_far = set((guard.current_position, guard.current_direction))

    while not done:
        done = guard.check_ahead(grid)
        if not done:
            guard.move_one_square(not check_for_loops) # move - only doing the trace if we aren't checking for loops
            if (check_for_loops):
                if (guard.current_position, guard.current_direction) in path_so_far:
                    return -1
                path_so_far.add((guard.current_position, guard.current_direction))

    return guard.squares_covered

# this assumes that grid has been updated with X for the part 1 path
# it will brute force search for O positions on that path that produce loops
def part_2_brute_force(start_x, start_y):
    # keep track of positions we find that would create a loop for part 2.
    # only technically need the count but we can visualise the positions at the end if we rememembwr them
    obstacles = []

    for y in range(0, len(grid)):
        for x in range(0, len(grid[0])):
            if grid[y][x] == 'X' and (x, y) != (start_x, start_y):
                update_grid(x, y, '#')
                if part_1(start_x, start_y, True) < 0:
                    obstacles.append((x, y))
                update_grid(x, y, 'X')

    return obstacles

# find the guard's starting position. I assume they are always facing up ^ since they are in the test data and my input
for y, line in enumerate(grid):
    x = line.find('^')
    if x >= 0:
        break

print('Answer 1 ', part_1(x, y))
print('\n'.join(grid))
obstacles = part_2_brute_force(x, y)
print('Answer 2 ', len(obstacles))

for obstacle in obstacles:
    update_grid(obstacle[0], obstacle[1], 'O')

print('\n'.join(grid))
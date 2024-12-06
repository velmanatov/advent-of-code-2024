import re

file_obj = open("input.txt", "r") 
file_data = file_obj.read() 
file_obj.close()

maze = file_data.splitlines()
trace = maze

def trace_visited(x, y):
    trace_line = trace[y]
    altered_line = trace_line[:x] + 'X' + trace_line[x + 1:]
    trace[y] = altered_line

class Guard:
    current_direction = (0, -1)
    current_position = (-1, -1)
    squares_covered = 1 # count the starting square

    def check_ahead(self, maze):
        next_x = self.current_position[0] + self.current_direction[0]
        next_y = self.current_position[1] + self.current_direction[1]
        
        max_x = len(maze[0]) 
        max_y = len(maze)

        #check if we're out grid bounds now
        if next_x < 0 or next_y < 0 or next_x >= max_x or next_y >= max_y:
            return self.squares_covered

        # look ahead for # and change direction if necessary
        if maze[next_y][next_x] == '#':
                dx = self.current_direction[0]
                dy = self.current_direction[1]
                self.current_direction = (-dy, dx)

        return False   

    def move_one_square(self):
        self.current_position = (self.current_position[0] + self.current_direction[0], self.current_position[1] + self.current_direction[1])
        # check if we went here already. if not count the square and trace our step
        if not trace[self.current_position[1]][self.current_position[0]] == 'X':
            self.squares_covered = self.squares_covered + 1
            trace_visited(self.current_position[0], self.current_position[1])


guard = Guard()

# find the guard's starting position. I assume they are always facing up ^ since they are in the test data and my input
for y, line in enumerate(maze):
    x = line.find("^")
    if x >= 0:
        break

guard.current_position = (x, y)
trace_visited(x, y)

done = False

while not done:
     done = guard.check_ahead(maze)
     if not done:
        guard.move_one_square()

print(guard.squares_covered)
print('\n'.join(trace))


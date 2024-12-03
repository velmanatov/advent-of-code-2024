import re

file_obj = open("input.txt", "r") 
file_data = file_obj.read() 
file_obj.close()

answer = 0

lines = file_data.splitlines()

muls = re.findall("mul\\((\\d{1,3}),(\\d{1,3})\\)", file_data)

for match in enumerate(muls):
    answer += int(match[1][0]) * int(match[1][1])

print(answer)

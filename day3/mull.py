import re

# given some input text parse out all the mul(a,b) and return the sum of all a*b
def get_line_muls(text):
    answer = 0
    muls = re.findall("mul\\((\\d{1,3}),(\\d{1,3})\\)", text)
    for match in enumerate(muls):
        answer += int(match[1][0]) * int(match[1][1])
    return answer

file_obj = open("input.txt", "r") 
file_data = file_obj.read() 
file_obj.close()

# part 1 is simply to match pattern mul(a, b) and sum a*b for all matches
answer1 = get_line_muls(file_data)

# part 2 introduces do() and don't() patterns. "don't" "switches off" the mul, "do" "switches it back on" - i.e ignore anything found after a "don't" and before a "do"
# my version using a regex was a failure so fell back to a more manual scan. not massively happy, but it gives me the answer!
do = True
end_index = 0
answer2 = 0
remaining_text = file_data

while end_index != -1:
    if do:
        end_index = remaining_text.find("don't()")
        if end_index == -1:
            answer2 += get_line_muls(remaining_text)
        else:
            answer2 += get_line_muls(remaining_text[:end_index])
    else:
        end_index = remaining_text.find("do()")

    # chop the string that we need to scan down as we go so that we don't repeatedly scan the whole thing
    if end_index != -1:
        remaining_text = remaining_text[end_index:]   
    do = not do

print('Part 1 Answer:', answer1)
print('Part 2 Answer:', answer2)

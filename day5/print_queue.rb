file = File.open("input.txt",'rb',&:read)

# in part 1 the input has 2 sections:
# a set of "ordering rules" with pair of "Page numbers" in format X|Y implying in the latter section that X must come before Y
# a set of "page number updates" consisting of numbers P1, P2, p3 etc.
# We need to check for any out of those "page number updates" which include both pages from an "ordering rule" X, Y,, but with Y before X.
# eliminiating any such invalid updates we need to add up the middle page from all remaining updates

sections = file.split("\n\n")
orderingRules = sections[0].scan(/([0-9]+)\|([0-9]+)/).map{ |match| [Integer(match[0]), Integer(match[1])] }
upateLines = sections[1].split("\n")
 
answer1 = 0

 upateLines.each do |line|
    pageNumbers = line.scan(/([0-9]+)/).flatten().map{ |match| Integer(match) }
    valid = true
    orderingRules.each do |rule|
        xIndex = pageNumbers.find_index(rule[0])
        yIndex = pageNumbers.find_index(rule[1])

        if xIndex != nil && yIndex != nil && xIndex >= yIndex
            valid = false
            break
        end
    end

    if valid
        answer1 += pageNumbers[(pageNumbers.length)/2]
    end
 end

puts("Answer 1: %d" % [answer1])


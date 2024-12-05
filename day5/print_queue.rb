file = File.open("input.txt",'rb',&:read)

# in part 1 the input has 2 sections:
# a set of "ordering rules" with pair of "Page numbers" in format X|Y implying in the latter section that X must come before Y
# a set of "page number updates" consisting of numbers P1, P2, p3 etc.
# We need to check for any out of those "page number updates" which include both pages from an "ordering rule" X, Y,, but with Y before X.
# eliminating any such invalid updates we need to add up the middle page from all remaining updates

# part 2 has us correct the invalid lines and sum the middle numbers for those

sections = file.split("\n\n")
ordering_rules = sections[0].scan(/([0-9]+)\|([0-9]+)/).map{ |match| [Integer(match[0]), Integer(match[1])] }
update_lines = sections[1].split("\n")

# hash the rules using the X of X|Y as the key. to provide quicker/easier lookup when sorting for part 2
$left_of = Hash.new{ |h,k| h[k]=[] }.tap{ |h| ordering_rules.each{ |k,v| h[k] << v } }

answer1 = 0
answer2 = 0

# bubbles page left until it is not found to be in a rule stating that it should be left of the element at that position in the sorted_pages sequence
def place_page(sorted_pages, page)
    i = sorted_pages.length
    done = false
    while !done
        i = i-1
        # we are done if... we find we should not be left of i
        if (!($left_of[page].include? sorted_pages[i]) || i == 0)
            done = true
        end
        # ...or the current number *should* be left of i 
        if ($left_of[sorted_pages[i]].include? page)
            done = true
            i = i + 1
        end
    end

    return sorted_pages.insert(i, page)
end

# sort invalid lines for part 2, return the middle number
def sort_line(page_numbers)
    sorted_sequence = [page_numbers[0]]

    (1..page_numbers.length-1).each do |i|
        sorted_sequence = place_page(sorted_sequence, page_numbers[i])
    end

    return sorted_sequence[(sorted_sequence.length)/2]
end

update_lines.each do |line|
    page_numbers = line.scan(/([0-9]+)/).flatten().map{ |match| Integer(match) }
    valid = true
    ordering_rules.each do |rule|
        x_index = page_numbers.find_index(rule[0])
        y_index = page_numbers.find_index(rule[1])

        if x_index != nil && y_index != nil && x_index >= y_index
            valid = false
            answer2 += sort_line(page_numbers)
            break
        end
    end

    if valid
        answer1 += page_numbers[(page_numbers.length)/2]
    end
end



puts("Answer 1: %d" % [answer1])
puts("Answer 2: %d" % [answer2])


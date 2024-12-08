package day7.aoc;

import java.io.File;
import java.io.FileNotFoundException;
import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class BridgeRepair {

    public static void main(String[] args) {
        try {
            Long answer1 = 0l;
            Long answer2 = 0l;
            File file = new File("./day7/input.txt");
            Scanner reader = new Scanner(file);
            while (reader.hasNextLine()) {
                String line = reader.nextLine();
                CalibrationEquation equation = GetEquation(line);
                if (equation.CanBeMadeTrue(1)) {
                    answer1 += equation.testValue;
                }
                if (equation.CanBeMadeTrue(2)) {
                    answer2 += equation.testValue;
                }
             }
             System.out.println("Answer 1: " + answer1);
             System.out.println("Answer 2: " + answer2);
             reader.close();
        }
        catch (FileNotFoundException e) {
            System.out.println("An error occurred.");
            e.printStackTrace();
        }
    }

    private static CalibrationEquation GetEquation(String line) {
        // Sticking with two different ways of extracting the integers for no particular reason!
        String[] parts = line.split(":");
        Pattern numberRegex = Pattern.compile("\\d+");
        Matcher matchTestValue = numberRegex.matcher(parts[0]);
        matchTestValue.find();
        
        CalibrationEquation equation = new CalibrationEquation();
        equation.testValue = Long.parseLong(matchTestValue.group());
        List<Long> operands = new ArrayList<>();
        Scanner scanner = new Scanner(parts[1]);
        while (scanner.hasNextLong()) {
            operands.add(scanner.nextLong());
        }
        scanner.close();

        equation.operands = operands;
        return equation;
    }
}

class CalibrationEquation {
    public Long testValue;
    public List<Long> operands;

    public boolean CanBeMadeTrue(Integer part) {
        // get the first set of possible results from applying all operands to the 1st and 2nd number
        List<Long> results = ApplyAllOperations(List.of(operands.get(0)), operands.get(1), part);

        // note that we are finding all combinations. it would be more efficient to to depth first search and stop when we find any that give the correct answer
        for(Integer i = 2; i < operands.size(); i ++) {
            results = ApplyAllOperations(results, operands.get(i), part);
        }

        return results.contains(testValue);
    }

    private List<Long> ApplyAllOperations(List<Long> interimValues, Long value, Integer part) {
        List<Long> results = new ArrayList<Long>();

        for (Long a: interimValues)
        {
            results.add(a + value);
            results.add(a * value);
            if (part == 2) {
                results.add(Long.parseLong(a.toString() + value.toString()));
            }
        }
        return results;
    }

    public String toString() {
        return testValue.toString() + ": " + operands.toString();
    }
} 
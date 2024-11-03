package Utils;

import java.util.ArrayList;
import java.util.LinkedList;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class RegexMatcher {
    public static LinkedList<String> getMatches(String input, String regex) {
        Pattern pattern = Pattern.compile(regex, Pattern.MULTILINE);
        Matcher matcher = pattern.matcher(input);

        LinkedList<String> matches = new LinkedList<String>();

        while (matcher.find()) {
            matches.add(matcher.group());
        }

        return matches;
    }
}

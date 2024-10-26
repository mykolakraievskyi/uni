package Utils;

import Models.IStudent;

import java.util.Comparator;

public class AverageMarkComparator implements Comparator<IStudent> {

    @Override
    public int compare(IStudent o1, IStudent o2) {
        double firstAvg = 0;
        double secondAvg = 0;

        for (var mark : o1.getMarks().values()) {
            firstAvg += mark;
        }

        for (var mark : o2.getMarks().values()) {
            secondAvg += mark;
        }

        firstAvg /= o1.getMarks().size();
        secondAvg /= o2.getMarks().size();

        return Double.compare(firstAvg, secondAvg);
    }
}

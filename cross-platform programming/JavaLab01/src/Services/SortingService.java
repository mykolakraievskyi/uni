package Services;

import java.util.Comparator;
import java.util.List;

public class SortingService<T> {

    private void swap(List<T> list, int i, int j)
    {
        T temp = list.get(i);
        list.set(i, list.get(j));
        list.set(j, temp);
    }

    private int partition(List<T> list, int low, int high, Comparator<T> comparator)
    {
        T pivot = list.get(high);

        int i = (low - 1);

        for (int j = low; j <= high - 1; j++) {

            if (comparator.compare(list.get(j), pivot) > 0) {
                i++;
                swap(list, i, j);
            }
        }
        swap(list, i + 1, high);
        return (i + 1);
    }

    public void sort(List<T> list, Comparator<T> comparator) {
        quickSort(list, 0, list.size() - 1, comparator);
    }
    private void quickSort(List<T> list, int low, int high, Comparator<T> comparator)
    {
        if (low < high) {
            int pi = partition(list, low, high, comparator);

            quickSort(list, low, pi - 1, comparator);
            quickSort(list, pi + 1, high, comparator);
        }
    }


}

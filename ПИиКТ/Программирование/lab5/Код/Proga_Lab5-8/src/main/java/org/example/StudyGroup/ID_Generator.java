package org.example.StudyGroup;

import java.util.ArrayList;
import java.util.List;

public class ID_Generator {
    static long current = 0;

    private void reset() {
        current = 1;
    }

    private void read_ID() {
        List<Long> ID_xml_read = new ArrayList<Long>();
        ID_xml_read.add(4L);
        ID_xml_read.add(10L);
        ID_xml_read.add(400L);
        long max_ID = 0;

        for (Long ID : ID_xml_read) {
            if (ID > max_ID) {
                max_ID = ID;
            }
        }

        current = max_ID + 1;
        ID_xml_read.add(current);
    }

    private void Generation() {
        if (current <= 0) {
            throw new IllegalArgumentException("Неверный счётчик!");
        }

        else {
            current += 1;
        }
    }
}
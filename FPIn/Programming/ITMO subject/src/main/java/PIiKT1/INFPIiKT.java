package main.java.PIiKT1;

//import main.resources.java.studenthelper.java.PIiKT1.*;
//import main.resources.java.studenthelper.java.PIiKT1.Programming;

import main.java.AccessManager;
import main.java.ExtraFramePIiKT;

import javax.swing.*;
import java.awt.*;
import java.io.*;
import java.util.HashMap;


import javax.swing.table.DefaultTableModel;

public class INFPIiKT extends JFrame {
    public INFPIiKT() {
        setTitle("Информатика 1 курс");
        setSize(720, 800);
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        setLocationRelativeTo(null);

        // Кнопки дисциплин
        JButton point1 = new JButton("Баллы 1 семестр");
        JButton lab1   = new JButton("Лабораторная работа 1");
        JButton lab2   = new JButton("Лабораторная работа 2");
        JButton lab3   = new JButton("Лабораторная работа 3");
        JButton lab4   = new JButton("Лабораторная работа 4");
        JButton lab5   = new JButton("Лабораторная работа 5");
        JButton lab6   = new JButton("Лабораторная работа 6");

        // Массив ссылок, чтобы не дублировать
        JButton[] buttonsInf = { point1, lab1, lab2, lab3, lab5, lab6};

        // Обработка переходов остаётся прежней, только теперь вызываем ваши фреймы
        point1.addActionListener(e -> { new point1(); dispose(); });
        lab1  .addActionListener(e -> { new lab1();  dispose(); });
        lab2  .addActionListener(e -> { new lab2();  dispose(); });
        lab3  .addActionListener(e -> { new lab3();  dispose(); });
        lab4  .addActionListener(e -> { new lab4();  dispose(); });
        lab5  .addActionListener(e -> { new lab5();  dispose(); });
        lab6  .addActionListener(e -> { new lab6();  dispose(); });


        // Верхняя панель с «Назад» и заголовком
        JPanel top = new JPanel(new BorderLayout());
        JButton backButton = new JButton("< Назад");
        backButton.addActionListener(e -> {
            new ExtraFramePIiKT();
            dispose();
        });
        JLabel title = new JLabel("Информатика 1 курс", SwingConstants.CENTER);
        title.setFont(new Font("Arial", Font.PLAIN, 24));
        top.add(backButton, BorderLayout.WEST);
        top.add(title,      BorderLayout.CENTER);


        JPanel table = new JPanel(new GridLayout(0, 2, 8, 8));
        table.setBorder(BorderFactory.createEmptyBorder(12,12,12,12));


        for (JButton btn : buttonsInf) {
            table.add(btn);
            JTextField comment = new JTextField();
            comment.setEnabled(AccessManager.canComment());
            table.add(comment);
        }

        // Собираем всё вместе
        setLayout(new BorderLayout());
        add(top,    BorderLayout.NORTH);
        add(new JScrollPane(table), BorderLayout.CENTER);

        setVisible(true);
    }
}

class point1 extends JFrame{
    private static final String SAVE_FILE = "marksInfPiikt1.ser";
    private final DefaultTableModel model;

    public point1() {
        super("Таблица баллов");
        setSize(600, 390);
        setLocationRelativeTo(null);
        setDefaultCloseOperation(DISPOSE_ON_CLOSE);
        setLayout(new BorderLayout());


        JPanel top = new JPanel(new FlowLayout(FlowLayout.LEFT));
        JButton backButton = new JButton("<");
        backButton.addActionListener(e -> {
            new INFPIiKT();
            dispose();
        });
        top.add(backButton);
        add(top, BorderLayout.NORTH);


        String[] cols = {"Критерий", "Балл"};
        Object[][] data = {
                {"Лабораторная работа 1-2 (5-8)",   ""},
                {"Лабораторная работа 3-5 (12-20)", ""},
                {"Лабораторная работа 6 (5-7)",     ""},
                {"Тест 1 (6-10)",                   ""},
                {"Тест 2 (6-10)",                   ""},
                {"Домашняя работа (18-25)",         ""},
                {"Личные качества (0-3)",           ""},
                {"Экзамен (7-20)",                  ""},
                {"Сумма(0-103)",                    ""},
                {"Итог",                            ""},
        };
        model = new DefaultTableModel(data, cols) {

            @Override
            public boolean isCellEditable(int row, int col) {
                if (col == 0) return false;

                return AccessManager.canSave();
            }
        };

        JTable table = new JTable(model);
        table.setRowHeight(24);
        table.getTableHeader().setReorderingAllowed(false);
        add(new JScrollPane(table), BorderLayout.CENTER);


        if (AccessManager.canSave()) {
            JPanel bottom = new JPanel(new FlowLayout(FlowLayout.RIGHT));
            JButton saveButton = new JButton("Сохранить");
            saveButton.addActionListener(e -> saveData());
            bottom.add(saveButton);
            add(bottom, BorderLayout.SOUTH);
        }

        loadData();

        setVisible(true);
    }

    /** Сохраняем балл и оценку каждой строки в файл marksInfPiikt1.ser */
    private void saveData() {
        HashMap<Integer, String> map = new HashMap<>();
        for (int r = 0; r < model.getRowCount(); r++) {
            map.put(r, String.valueOf(model.getValueAt(r, 1)));
        }
        try (ObjectOutputStream out =
                     new ObjectOutputStream(new FileOutputStream(SAVE_FILE))) {
            out.writeObject(map);
            JOptionPane.showMessageDialog(this, "Данные сохранены");
        } catch (IOException ex) {
            ex.printStackTrace();
        }
    }


    @SuppressWarnings("unchecked")
    private void loadData() {
        try (ObjectInputStream in =
                     new ObjectInputStream(new FileInputStream(SAVE_FILE))) {
            HashMap<Integer, String> map = (HashMap<Integer, String>) in.readObject();
            for (int r = 0; r < model.getRowCount(); r++) {
                String val = map.getOrDefault(r, String.valueOf(model.getValueAt(r, 1)));
                model.setValueAt(val, r, 1);
            }
        } catch (Exception ignored) {}
    }

}

class lab1 extends JFrame{
    public lab1(){
        setTitle("Информатика ФПИиКТ");
        setSize(720, 800);
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        setLocationRelativeTo(null);

        JLabel label = new JLabel("Лабораторная работа 1");
        JLabel bag1 = new JLabel("");
        JLabel task1 = new JLabel("Ваш вариант выдан практиком (или лектором)");
        JLabel task2 = new JLabel("Лабораторная работа находится в PDF-файле");
        JLabel task3 = new JLabel("(Для кафедры ВТ: Варианты лабораторных работ находятся на se.ifmo).");
        JButton backButton = new JButton("<");

        backButton.addActionListener(e -> {
            new INFPIiKT();
            dispose();
        });

        JPanel panel = new JPanel();
        panel.add(backButton);
        panel.add(label);
        panel.add(bag1);
        panel.add(task1);
        panel.add(task2);
        panel.add(task3);

        add(panel);
        label.setFont(new Font("Arial", Font.PLAIN, 20));
        backButton.setFont(new Font("Arial", Font.PLAIN, 25));
        setVisible(true);
    }
}

class lab2 extends JFrame {
    public lab2() {
        setTitle("Информатика ФПИиКТ");
        setSize(720, 800);
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        setLocationRelativeTo(null);

        JLabel label = new JLabel("Лабораторная работа 2");
        JLabel task1 = new JLabel("Ваш вариант выдан практиком (или лектором)");
        JLabel task2 = new JLabel("Лабораторная работа находится в PDF-файле");
        JLabel task3 = new JLabel("(Для кафедры ВТ: Варианты лабораторных работ находятся на se.ifmo).");
        JButton backButton = new JButton("<");

        backButton.addActionListener(e -> {
            new INFPIiKT();
            dispose();
        });

        JPanel panel = new JPanel();
        panel.add(backButton);
        panel.add(label);
        panel.add(label);
        panel.add(task1);
        panel.add(task2);
        panel.add(task3);

        add(panel);
        label.setFont(new Font("Arial", Font.PLAIN, 20));
        backButton.setFont(new Font("Arial", Font.PLAIN, 25));
        setVisible(true);
    }
}

class lab3 extends JFrame {
    public lab3() {
        setTitle("Информатика ФПИиКТ");
        setSize(720, 800);
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        setLocationRelativeTo(null);

        JLabel label = new JLabel("Лабораторная работа 3");
        JLabel task1 = new JLabel("Ваш вариант выдан практиком (или лектором)");
        JLabel task2 = new JLabel("Лабораторная работа находится в PDF-файле");
        JLabel task3 = new JLabel("(Для кафедры ВТ: Варианты лабораторных работ находятся на se.ifmo).");
        JButton backButton = new JButton("<");

        backButton.addActionListener(e -> {
            new INFPIiKT();
            dispose();
        });

        JPanel panel = new JPanel();
        panel.add(backButton);
        panel.add(label);
        panel.add(label);
        panel.add(task1);
        panel.add(task2);
        panel.add(task3);

        add(panel);
        label.setFont(new Font("Arial", Font.PLAIN, 20));
        backButton.setFont(new Font("Arial", Font.PLAIN, 25));
        setVisible(true);
    }
}

class lab4 extends JFrame {
    public lab4() {
        setTitle("Информатика ФПИиКТ");
        setSize(720, 800);
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        setLocationRelativeTo(null);

        JLabel label = new JLabel("Лабораторная работа 4");
        JLabel task1 = new JLabel("Ваш вариант выдан практиком (или лектором)");
        JLabel task2 = new JLabel("Лабораторная работа находится в PDF-файле");
        JLabel task3 = new JLabel("(Для кафедры ВТ: Варианты лабораторных работ находятся на se.ifmo).");
        JButton backButton = new JButton("<");

        backButton.addActionListener(e -> {
            new INFPIiKT();
            dispose();
        });

        JPanel panel = new JPanel();
        panel.add(backButton);
        panel.add(label);
        panel.add(label);
        panel.add(task1);
        panel.add(task2);
        panel.add(task3);

        add(panel);
        label.setFont(new Font("Arial", Font.PLAIN, 20));
        backButton.setFont(new Font("Arial", Font.PLAIN, 25));
        setVisible(true);
    }
}

class lab5 extends JFrame {
    public lab5() {
        setTitle("Информатика ФПИиКТ");
        setSize(720, 800);
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        setLocationRelativeTo(null);

        JLabel label = new JLabel("Лабораторная работа 5");
        JLabel task1 = new JLabel("Ваш вариант выдан практиком (или лектором)");
        JLabel task2 = new JLabel("Лабораторная работа находится в PDF-файле");
        JLabel task3 = new JLabel("(Для кафедры ВТ: Варианты лабораторных работ находятся на se.ifmo).");
        JButton backButton = new JButton("<");

        backButton.addActionListener(e -> {
            new INFPIiKT();
            dispose();
        });

        JPanel panel = new JPanel();
        panel.add(backButton);
        panel.add(label);
        panel.add(label);
        panel.add(task1);
        panel.add(task2);
        panel.add(task3);

        add(panel);
        label.setFont(new Font("Arial", Font.PLAIN, 20));
        backButton.setFont(new Font("Arial", Font.PLAIN, 25));
        setVisible(true);
    }
}

class lab6 extends JFrame {
    public lab6() {
        setTitle("Информатика ФПИиКТ");
        setSize(720, 800);
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        setLocationRelativeTo(null);

        JLabel label = new JLabel("Лабораторная работа 6");
        JLabel task1 = new JLabel("Ваш вариант выдан практиком (или лектором)");
        JLabel task2 = new JLabel("Лабораторная работа находится в PDF-файле");
        JLabel task3 = new JLabel("(Для кафедры ВТ: Варианты лабораторных работ находятся на se.ifmo).");
        JButton backButton = new JButton("<");

        backButton.addActionListener(e -> {
            new INFPIiKT();
            dispose();
        });

        JPanel panel = new JPanel();
        panel.add(backButton);
        panel.add(label);
        panel.add(label);
        panel.add(task1);
        panel.add(task2);
        panel.add(task3);

        add(panel);
        label.setFont(new Font("Arial", Font.PLAIN, 20));
        backButton.setFont(new Font("Arial", Font.PLAIN, 25));
        setVisible(true);
    }
}
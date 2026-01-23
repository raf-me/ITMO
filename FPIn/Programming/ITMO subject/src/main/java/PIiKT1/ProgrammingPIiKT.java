package main.java.PIiKT1;

import main.java.AccessManager;
import main.java.ExtraFramePIiKT;
import main.java.VariantMapper;

import javax.swing.*;
import javax.swing.event.DocumentEvent;
import javax.swing.event.DocumentListener;
import javax.swing.table.DefaultTableModel;
import java.awt.*;
import java.awt.event.*;
import java.io.*;
import java.net.URI;
import java.net.URL;
import java.nio.file.*;
import java.sql.*;
import java.util.ArrayList;
import java.util.Collections;
import java.util.HashMap;
import java.util.Properties;
import java.util.List;


//import static com.sun.org.apache.xml.internal.security.algorithms.implementations.SignatureDSA.URI;


public class ProgrammingPIiKT extends JFrame {
    private final java.util.List<String[]> userTasks = new java.util.ArrayList<>();

    public ProgrammingPIiKT() {
        setTitle("Программирование 1 курс");
        setSize(720, 800);
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        setLocationRelativeTo(null);

        // Исходные кнопки
        JButton[] buttons = {
                new JButton("Баллы 1 семестр"),
                new JButton("Баллы 2 семестр"),
                new JButton("Лабораторная работа 1"),
                new JButton("Лабораторная работа 2"),
                new JButton("Лабораторная работа 3-4"),
                new JButton("Лабораторная работа 5"),
                new JButton("Лабораторная работа 6"),
                new JButton("Лабораторная работа 7"),
                new JButton("Лабораторная работа 8"),
                new JButton("Хакатон 1"),
                new JButton("Хакатон 2")
        };

        // Обработчики (можешь настроить по своему)
        buttons[0].addActionListener(e -> { new pointprog1(); dispose(); });
        buttons[1].addActionListener(e -> { new point2(); dispose(); });
        buttons[2].addActionListener(e -> { new labprog1(); dispose(); });
        buttons[3].addActionListener(e -> { new labprog2(); dispose(); });
        buttons[4].addActionListener(e -> {
            try { new labprog3(); } catch (IOException ex) { throw new RuntimeException(ex); }
            dispose();
        });
        buttons[5].addActionListener(e -> { new labprog5(); dispose(); });
        buttons[6].addActionListener(e -> { new labprog6(); dispose(); });
        buttons[7].addActionListener(e -> { new labprog7(); dispose(); });
        buttons[8].addActionListener(e -> { new labprog8(); dispose(); });
        buttons[9].addActionListener(e -> { new hackprog1(); dispose(); });
        buttons[10].addActionListener(e -> { new hackprog2(); dispose(); });

        // Верхняя панель
        JPanel top = new JPanel(new BorderLayout());
        JButton backButton = new JButton("< Назад");
        backButton.addActionListener(e -> {
            new ExtraFramePIiKT();
            dispose();
        });
        JLabel title = new JLabel("Программирование 1 курс", SwingConstants.CENTER);
        title.setFont(new Font("Arial", Font.PLAIN, 24));
        top.add(backButton, BorderLayout.WEST);
        top.add(title, BorderLayout.CENTER);

        // Таблица для кнопок
        JPanel table = new JPanel(new GridLayout(0, 2, 8, 8));
        table.setBorder(BorderFactory.createEmptyBorder(12, 12, 12, 12));

        for (JButton btn : buttons) {
            table.add(btn);
            JTextField comment = new JTextField();
            comment.setEnabled(AccessManager.canComment());
            table.add(comment);
        }

        // Кнопка "Добавить работу"
        JButton addButton = new JButton("+ Добавить работу");
        addButton.setFont(new Font("Arial", Font.BOLD, 16));
        addButton.addActionListener(e -> showAddDialog(table));
        table.add(addButton);
        table.add(new JLabel()); // пустая ячейка для выравнивания

        // Оформление
        setLayout(new BorderLayout());
        add(top, BorderLayout.NORTH);
        add(new JScrollPane(table), BorderLayout.CENTER);
        setVisible(true);
    }

    public int getNextLabNumber() {
        File dir = new File("src/main/resources/ProgaLabNHTML");
        if (!dir.exists()) return 1;

        int max = 0;
        for (File f : dir.listFiles()) {
            String name = f.getName();
            if (name.startsWith("proglab") && name.endsWith(".html")) {
                try {
                    int num = Integer.parseInt(name.replaceAll("[^0-9]", ""));
                    if (num > max) max = num;
                } catch (NumberFormatException ignored) {}
            }
        }
        return max + 1;
    }


    private void showAddDialog(JPanel table) {
        JDialog dialog = new JDialog(this, "Добавление новой работы", true);
        dialog.setSize(500, 250);
        dialog.setLayout(new GridLayout(6, 1, 10, 10));
        dialog.setLocationRelativeTo(this);

        JTextField nameField = new JTextField();
        JComboBox<String> typeBox = new JComboBox<>(new String[]{"Лабораторная", "Хакатон", "Экзамен"});
        JLabel selectedHtmlLabel = new JLabel("Файл не выбран");
        final File[] selectedHtmlFile = {null};

        JButton chooseHtmlButton = new JButton("Выбрать HTML-файл");
        chooseHtmlButton.addActionListener(e -> {
            JFileChooser chooser = new JFileChooser();
            int result = chooser.showOpenDialog(dialog);
            if (result == JFileChooser.APPROVE_OPTION) {
                selectedHtmlFile[0] = chooser.getSelectedFile();
                selectedHtmlLabel.setText(selectedHtmlFile[0].getName());
            }
        });

        JButton createButton = new JButton("Создать");

        createButton.addActionListener(e -> {
            String name = nameField.getText().trim();
            String type = (String) typeBox.getSelectedItem();

            if (name.isEmpty()) {
                JOptionPane.showMessageDialog(dialog, "Введите название.");
                return;
            }

            if (type.equals("Лабораторная") && selectedHtmlFile[0] == null) {
                JOptionPane.showMessageDialog(dialog, "Выберите HTML-файл для лабораторной.");
                return;
            }

            // Определим номер следующей лабораторной (на основе уже сохранённых)
            int labNumber = getNextLabNumber();

            String labHtmlName = "proglab" + labNumber; // без .html
            if (type.equals("Лабораторная")) {
                try {
                    File dest = new File("ProgaLabNHTML/progalab" + labHtmlName + ".html");
                    dest.getParentFile().mkdirs();
                    Files.copy(selectedHtmlFile[0].toPath(), dest.toPath());
                } catch (IOException ex) {
                    ex.printStackTrace();
                    JOptionPane.showMessageDialog(dialog, "Ошибка при копировании HTML-файла.");
                    return;
                }
            }

            // Добавим кнопку
            String displayName = type + ": " + name;
            JButton newBtn = new JButton(displayName);
            newBtn.addActionListener(ev -> {
                if (type.equals("Лабораторная")) {
                    new LabTemplateViewer(labHtmlName);
                } else {
                    JOptionPane.showMessageDialog(this, type + ": " + name + " пока не реализовано.");
                }
            });

            JTextField comment = new JTextField();
            comment.setEnabled(AccessManager.canComment());
            table.add(newBtn);
            table.add(comment);
            table.revalidate();
            table.repaint();

            // Сохраняем как и раньше (если у тебя уже реализовано сохранение userTasks, можно добавить туда)
            userTasks.add(new String[]{type, name, labHtmlName});
            saveUserTasks();


            dialog.dispose();
        });

        dialog.add(new JLabel("Название работы:"));
        dialog.add(nameField);
        dialog.add(typeBox);
        dialog.add(chooseHtmlButton);
        dialog.add(selectedHtmlLabel);
        dialog.add(createButton);
        dialog.setVisible(true);
    }

    private void saveUserTasks() {
        try (ObjectOutputStream out = new ObjectOutputStream(new FileOutputStream("user_tasks.ser"))) {
            out.writeObject(userTasks);
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

}


class pointprog1 extends JFrame{
    private static final String SAVE_FILE = "marksProgPiikt1.ser";
    private final DefaultTableModel model;

    public pointprog1() {
        super("Таблица баллов");
        setSize(600, 390);
        setLocationRelativeTo(null);
        setDefaultCloseOperation(DISPOSE_ON_CLOSE);
        setLayout(new BorderLayout());


        JPanel top = new JPanel(new FlowLayout(FlowLayout.LEFT));
        JButton backButton = new JButton("<");
        backButton.addActionListener(e -> {
            new ProgrammingPIiKT();
            dispose();
        });
        top.add(backButton);
        add(top, BorderLayout.NORTH);


        String[] cols = {"Критерий", "Балл"};
        Object[][] data = {
                {"Лабораторная работа 1-2 (12-20)", ""},
                {"Лабораторная работа 3-4 (24-40)", ""},
                {"Рубеж (12-20)",                   ""},
                {"Личные качества (0-3)",           ""},
                {"Зачёт (0-20)",                    ""},
                {"Сумма (0-103)",                   ""},
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

    /** Сохраняем информацию в файл marksProgPiikt1.ser */
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

class point2 extends JFrame{
    private static final String SAVE_FILE = "marksProgPiikt2.ser";
    private final DefaultTableModel model;

    public point2() {
        super("Таблица баллов");
        setSize(600, 390);
        setLocationRelativeTo(null);
        setDefaultCloseOperation(DISPOSE_ON_CLOSE);
        setLayout(new BorderLayout());


        JPanel top = new JPanel(new FlowLayout(FlowLayout.LEFT));
        JButton backButton = new JButton("<");
        backButton.addActionListener(e -> {
            new ProgrammingPIiKT();
            dispose();
        });
        top.add(backButton);
        add(top, BorderLayout.NORTH);


        String[] cols = {"Критерий", "Балл"};
        Object[][] data = {
                {"Лабораторная работа 5-6 (24-40)", ""},
                {"Лабораторная работа 7-8 (12-20)", ""},
                {"Рубеж (12-20)",                   ""},
                {"Личные качества (0-3)",           ""},
                {"Экзамен (12-20)",                 ""},
                {"Сумма (0-103)",                   ""},
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

    /** Сохраняем балл и оценку каждой строки в файл marksProgPiikt2.ser */
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
        } catch (Exception ignored) {  }
    }

}

class labprog1 extends JFrame {
    public labprog1() {
        setTitle("Программирование 1 курс");
        setSize(720, 800);
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        setLocationRelativeTo(null);

        JTextField label = new JTextField("Лабораторная работа 1");
        JTextField task1 = new JTextField("Ваш вариант выдан практиком (или лектором)");
        JTextField task2 = new JTextField("Лабораторная работа находится в PDF-файле");
        //JEditorPane task0 = new JEditorPane("");
        JTextField task3 = new JTextField("(Для кафедры ВТ: Варианты лабораторных работ находятся на se.ifmo).");
        JButton backButton = new JButton("<");

        backButton.addActionListener(e -> {
            new ProgrammingPIiKT();
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

class labprog2 extends JFrame {
    private JTextArea instructionArea;
    private JTextField variantField;
    private JTextArea commentArea;
    private JTextField gradeField;
    private Properties data;
    private final String DATA_FILE = "reports/report_data.properties";

    public labprog2() {
        setTitle("Лабораторная работа 2");
        setSize(1000, 800);
        setDefaultCloseOperation(EXIT_ON_CLOSE);
        setLayout(new BoxLayout(getContentPane(), BoxLayout.Y_AXIS));

        // Ensure reports directory exists
        new File("reports").mkdirs();

        // Load data
        data = new Properties();
        try (FileInputStream fis = new FileInputStream(DATA_FILE)) {
            data.load(fis);
        }
        catch (IOException ignored) {
        }

        add(new JLabel("Лабораторная работа 2"));

        instructionArea = new JTextArea(5, 40);
        instructionArea.setText(data.getProperty("instruction", "Введите инструкцию..."));
        add(new JLabel("Инструкция:"));
        add(new JScrollPane(instructionArea));


        variantField = new JTextField(data.getProperty("variant", ""), 140);
        add(new JLabel("Вариант:"));
        add(variantField);

        JButton saveButton = new JButton("Сохранить отчёт");
        saveButton.addActionListener(e -> saveReport());
        add(saveButton);

        commentArea = new JTextArea(3, 40);
        commentArea.setText(data.getProperty("comment", ""));
        add(new JLabel("Комментарий:"));
        add(new JScrollPane(commentArea));

        gradeField = new JTextField(data.getProperty("grade", ""), 5);
        add(new JLabel("Оценка:"));
        add(gradeField);

        JButton saveAllButton = new JButton("Сохранить все изменения");
        saveAllButton.addActionListener(e -> saveAll());
        add(saveAllButton);

        setVisible(true);
    }

    private void saveAll() {
        data.setProperty("instruction", instructionArea.getText());
        data.setProperty("variant", variantField.getText());
        data.setProperty("comment", commentArea.getText());
        data.setProperty("grade", gradeField.getText());

        try (FileOutputStream fos = new FileOutputStream(DATA_FILE)) {
            data.store(fos, "Report Data");
        } catch (IOException ex) {
            JOptionPane.showMessageDialog(this, "Ошибка при сохранении данных: " + ex.getMessage());
        }
    }

    private void saveReport() {
        String filename = "reports/report_" + System.currentTimeMillis() + ".txt";
        try (BufferedWriter writer = Files.newBufferedWriter(Paths.get(filename))) {
            writer.write("Лабораторная работа 1\n\n");
            writer.write("Инструкция:\n" + instructionArea.getText() + "\n\n");
            writer.write("Вариант: " + variantField.getText() + "\n");
            writer.write("Комментарий: " + commentArea.getText() + "\n");
            writer.write("Оценка: " + gradeField.getText() + "\n");
            JOptionPane.showMessageDialog(this, "Отчёт сохранён в файл: " + filename);
        } catch (IOException ex) {
            JOptionPane.showMessageDialog(this, "Ошибка при сохранении отчёта: " + ex.getMessage());
        }
    }
}

class labprog3 extends JFrame {
    public labprog3() throws IOException {
        setTitle("Программирование 1 курс");
        setSize(720, 800);

        JLabel label = new JLabel("Лабораторная работа 3-4");
        JTextArea enter1 = new JTextArea("\nddd");
        enter1.setLineWrap(true);
        JTextField textTask1 = new JTextField(15);
        JLabel task1 = new JLabel("Ваш вариант выдан практиком (или лектором)");
        JTextArea enter2 = new JTextArea("   \n");
        enter2.setLineWrap(true);
        JLabel task2 = new JLabel("Лабораторная работа находится в PDF-файле");
        JTextArea enter3 = new JTextArea("\n");
        JLabel task3 = new JLabel("(Для кафедры ВТ: Варианты лабораторных работ находятся на se.ifmo).");
        JTextArea enter4 = new JTextArea("\n");
        JButton backButton = new JButton("Назад в окно пользователя");
        JLabel pdf = new JLabel("Лабораторная работа 3-4:");



        label.setFont(new Font("Arial", Font.PLAIN, 25));
        task1.setFont(new Font("Arial", Font.PLAIN, 17));
        task2.setFont(new Font("Arial", Font.PLAIN, 17));
        task3.setFont(new Font("Arial", Font.PLAIN, 17));
        pdf.setFont(new Font("Arial", Font.PLAIN, 13));

        backButton.addActionListener(e -> {
            new ProgrammingPIiKT();
            dispose();
        });

        backButton.addActionListener(e -> {
            new ProgrammingPIiKT();
            dispose();
        });

        JLabel number = new JLabel("Ваш вариант: ");
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        setLocationRelativeTo(null);
        JPanel panel = new JPanel();

        panel.add(label);
        panel.add(enter1);
        panel.add(task1);
        panel.add(enter2);
        panel.add(task2);
        panel.add(task3);
        panel.add(task3);
        panel.add(enter4);
        panel.add(number);
        panel.add(textTask1);
        panel.add(pdf);
        panel.add(backButton);
        add(panel);

        backButton.setFont(new Font("Arial", Font.PLAIN, 25));
        setVisible(true);
    }
}

class labprog5 extends JFrame {
    private JTextField variantField;
    private JTextArea commentArea;
    private JTextField gradeField;
    private JLabel fileLabel;
    private static final String SAVE_FILE = "labdata.ser";
    private HashMap<String, String> data = new HashMap<>();
    private JTextPane instructionPane;
    private static final String DATABASE_FILE = "ProgBase\\PIiKT\\lab5.db";


    public labprog5() {
        setTitle("Программирование 1 курс ПИиКТ");
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        setSize(980, 850);

        JPanel contentPanel = new JPanel();
        contentPanel.setLayout(new BoxLayout(contentPanel, BoxLayout.Y_AXIS));
        contentPanel.setBorder(BorderFactory.createEmptyBorder(10, 10, 10, 10));

        // Заголовок
        JLabel titleLabel = new JLabel("Лабораторная работа 5");
        titleLabel.setFont(new Font("SansSerif", Font.BOLD, 24));
        titleLabel.setAlignmentX(Component.LEFT_ALIGNMENT);
        JButton backButton = new JButton("<");
        backButton.setPreferredSize(new Dimension(60, 25));
        backButton.setCursor(Cursor.getPredefinedCursor(Cursor.HAND_CURSOR));
        backButton.addActionListener(e -> {
            new ProgrammingPIiKT(); // возврат назад
            dispose();
        });

        JPanel headerPanel = new JPanel(new FlowLayout(FlowLayout.LEFT));
        headerPanel.setAlignmentX(Component.LEFT_ALIGNMENT);
        headerPanel.add(backButton);
        headerPanel.add(titleLabel);
        contentPanel.add(headerPanel);


        instructionPane = new JTextPane();
        instructionPane.setContentType("text/html");
        instructionPane.setEditable(false);
        instructionPane.setOpaque(false);
        instructionPane.setText("<html><body><p>Введите номер варианта, чтобы загрузить инструкцию.</p></body></html>");

        JScrollPane instructionScroll = new JScrollPane(instructionPane);
        instructionScroll.setAlignmentX(Component.LEFT_ALIGNMENT);
        instructionScroll.setPreferredSize(new Dimension(940, 500));
        contentPanel.add(Box.createVerticalStrut(10));
        contentPanel.add(instructionScroll);

        // Вариант
        JPanel variantPanel = new JPanel(new FlowLayout(FlowLayout.LEFT));
        variantPanel.setAlignmentX(Component.LEFT_ALIGNMENT);
        variantPanel.add(new JLabel("Вариант:"));
        variantField = new JTextField(10);
        variantPanel.add(variantField);
        contentPanel.add(Box.createVerticalStrut(10));
        contentPanel.add(variantPanel);
        setupVariantAutoUpdate();

        variantField.getDocument().addDocumentListener(new DocumentListener() {
            public void insertUpdate(DocumentEvent e) {
                loadVariantInstruction();
            }

            public void removeUpdate(DocumentEvent e) {
                loadVariantInstruction();
            }

            public void changedUpdate(DocumentEvent e) {
                loadVariantInstruction();
            }
        });

        variantField.getDocument().addDocumentListener(new DocumentListener() {
            public void insertUpdate(DocumentEvent e) { loadVariantInstruction(); }
            public void removeUpdate(DocumentEvent e) { loadVariantInstruction(); }
            public void changedUpdate(DocumentEvent e) { loadVariantInstruction(); }
        });

        // Ссылка
        JLabel linkLabel = new JLabel("<html><a href=''>Ссылка на лабораторную работу</a></html>");
        linkLabel.setCursor(new Cursor(Cursor.HAND_CURSOR));
        linkLabel.setAlignmentX(Component.LEFT_ALIGNMENT);
        linkLabel.addMouseListener(new MouseAdapter() {
            public void mouseClicked(MouseEvent e) {
                try {
                    Desktop.getDesktop().browse(new URI("https://se.ifmo.ru/courses/programming#:~:text=Лабораторная%20работа%20%233-4"));
                } catch (Exception ex) {
                    ex.printStackTrace();
                }
            }
        });
        contentPanel.add(linkLabel);

        // Прикрепление отчета
        JPanel attachPanel = new JPanel(new FlowLayout(FlowLayout.LEFT));
        attachPanel.setAlignmentX(Component.LEFT_ALIGNMENT);
        JButton attachButton = new JButton("Прикрепить отчет");
        fileLabel = new JLabel("Файл не выбран");
        attachButton.addActionListener(e -> {
            JFileChooser fileChooser = new JFileChooser();
            int result = fileChooser.showOpenDialog(this);
            if (result == JFileChooser.APPROVE_OPTION) {
                File selected = fileChooser.getSelectedFile();
                fileLabel.setText(selected.getName());
                data.put("filePath", selected.getAbsolutePath());
            }
        });
        attachPanel.add(attachButton);
        attachPanel.add(fileLabel);
        contentPanel.add(Box.createVerticalStrut(10));
        contentPanel.add(attachPanel);

        // Комментарии
        contentPanel.add(Box.createVerticalStrut(10));
        contentPanel.add(new JLabel("Комментарии к работе:"));
        commentArea = new JTextArea(5, 70);
        commentArea.setLineWrap(true);
        commentArea.setWrapStyleWord(true);
        JScrollPane commentScroll = new JScrollPane(commentArea);
        commentScroll.setAlignmentX(Component.LEFT_ALIGNMENT);
        contentPanel.add(commentScroll);

        // Балл
        JPanel gradePanel = new JPanel(new FlowLayout(FlowLayout.LEFT));
        gradePanel.setAlignmentX(Component.LEFT_ALIGNMENT);
        gradePanel.add(new JLabel("Балл:"));
        gradeField = new JTextField(5);
        gradePanel.add(gradeField);
        contentPanel.add(Box.createVerticalStrut(10));
        contentPanel.add(gradePanel);

        // Сохранение и открытие
        JButton saveButton = new JButton("Сохранить");
        saveButton.setAlignmentX(Component.LEFT_ALIGNMENT);
        saveButton.addActionListener(e -> {
            saveData();
            String path = data.get("filePath");
            if (path != null && !path.isEmpty()) {
                try {
                    Desktop.getDesktop().open(new File(path));
                } catch (IOException ex) {
                    JOptionPane.showMessageDialog(this, "Не удалось открыть файл: " + ex.getMessage());
                }
            }
        });
        contentPanel.add(Box.createVerticalStrut(15));
        contentPanel.add(saveButton);

        JScrollPane scrollPane = new JScrollPane(contentPanel);
        scrollPane.getVerticalScrollBar().setUnitIncrement(16);
        setContentPane(scrollPane);

        loadData();
        setVisible(true);

        variantField.setEditable(AccessManager.canEditVariant());
        commentArea.setEditable(AccessManager.canComment());
        gradeField.setEditable(AccessManager.canSetGrade());
        attachButton.setEnabled(AccessManager.canAttachReport());
        saveButton.setEnabled(AccessManager.canSave());
    }

    private void saveData() {
        data.put("variant", variantField.getText());
        data.put("comments", commentArea.getText());
        data.put("grade", gradeField.getText());

        String filePath = data.get("filePath");

        try (Connection conn = DriverManager.getConnection("jdbc:sqlite:lab5.db")) {

            //Убедиться, что таблица существует
            String createTableSQL = """
            CREATE TABLE IF NOT EXISTS lab_reports_5 (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                variant TEXT NOT NULL,
                comments TEXT,
                file_path TEXT,
                grade TEXT,
                timestamp DATETIME DEFAULT CURRENT_TIMESTAMP
            );
        """;
            try (Statement stmt = conn.createStatement()) {
                stmt.execute(createTableSQL);
            }

            // Вставка данных
            String insertSQL = "INSERT INTO lab_reports_5 (variant, comments, file_path, grade) VALUES (?, ?, ?, ?)";
            try (PreparedStatement pstmt = conn.prepareStatement(insertSQL)) {
                pstmt.setString(1, variantField.getText());
                pstmt.setString(2, commentArea.getText());
                pstmt.setString(3, filePath);
                pstmt.setString(4, gradeField.getText());
                pstmt.executeUpdate();
            }

            JOptionPane.showMessageDialog(this, "Данные сохранены в БД!");
        } catch (SQLException e) {
            e.printStackTrace();
            JOptionPane.showMessageDialog(this, "Ошибка при сохранении в БД: " + e.getMessage());
        }
    }

    private void loadData() {
        try (Connection conn = DriverManager.getConnection("jdbc:sqlite:lab5.db")) {

            //Убедиться, что таблица существует
            String createTableSQL = """
            CREATE TABLE IF NOT EXISTS lab_reports_5 (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                variant TEXT NOT NULL,
                comments TEXT,
                file_path TEXT,
                grade TEXT,
                timestamp DATETIME DEFAULT CURRENT_TIMESTAMP
            );
        """;
            try (Statement stmt = conn.createStatement()) {
                stmt.execute(createTableSQL);
            }

            // Загрузка последней записи
            String selectSQL = "SELECT variant, comments, file_path, grade FROM lab_reports_5 ORDER BY timestamp DESC LIMIT 1";
            try (Statement stmt = conn.createStatement(); ResultSet rs = stmt.executeQuery(selectSQL)) {
                if (rs.next()) {
                    variantField.setText(rs.getString("variant"));
                    commentArea.setText(rs.getString("comments"));
                    gradeField.setText(rs.getString("grade"));
                    String path = rs.getString("file_path");
                    data.put("filePath", path);
                    fileLabel.setText(path != null ? new File(path).getName() : "Файл не выбран");
                }
            }
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }


    public static void main(String[] args) {
        SwingUtilities.invokeLater(labprog5::new);
    }

    private void loadVariantInstruction() {
        if (instructionPane == null || variantField == null) return;

        String input = variantField.getText().trim();
        if (input.isEmpty()) return;

        List<Integer> availableVariants = new ArrayList<>();
        try {
            URL url = getClass().getClassLoader().getResource("ProgaLab5HTML");
            if (url == null) {
                instructionPane.setText("<html><body><p style='color:red;'>Папка ProgaLab5HTML не найдена в ресурсах.</p></body></html>");
                return;
            }

            URI uri = url.toURI();
            Path dirPath;
            if ("jar".equals(uri.getScheme())) {
                FileSystem fs = FileSystems.newFileSystem(uri, Collections.emptyMap());
                dirPath = fs.getPath("ProgaLab5HTML");
            } else {
                dirPath = Paths.get(uri);
            }

            try (DirectoryStream<Path> stream = Files.newDirectoryStream(dirPath, "proglab5.*.html")) {
                for (Path entry : stream) {
                    String fileName = entry.getFileName().toString();
                    // Теперь корректно: извлекает только часть между "proglab5." и ".html"
                    if (fileName.startsWith("proglab5.") && fileName.endsWith(".html")) {
                        String numberPart = fileName.substring("proglab5.".length(), fileName.length() - ".html".length());
                        if (!numberPart.isEmpty()) {
                            availableVariants.add(Integer.parseInt(numberPart));
                        }
                    }
                }
            }
        } catch (Exception e) {
            instructionPane.setText("<html><body><p style='color:red;'>Ошибка при получении списка вариантов: " + e.getMessage() + "</p></body></html>");
            return;
        }

        if (availableVariants.isEmpty()) {
            instructionPane.setText("<html><body><p style='color:red;'>Нет доступных HTML-файлов.</p></body></html>");
            return;
        }

        VariantMapper mapper = new VariantMapper(availableVariants);
        int mappedVariant = mapper.mapVariant(input);

        String path = "ProgaLab5HTML/proglab5." + mappedVariant + ".html";
        InputStream inputStream = getClass().getClassLoader().getResourceAsStream(path);

        if (inputStream == null) {
            instructionPane.setText("<html><body><p style='color:red;'>Файл не найден: " + path + "</p></body></html>");
            return;
        }

        try (BufferedReader reader = new BufferedReader(new InputStreamReader(inputStream))) {
            StringBuilder html = new StringBuilder();
            String line;
            while ((line = reader.readLine()) != null) {
                html.append(line);
            }
            instructionPane.setText(html.toString());
        } catch (IOException e) {
            instructionPane.setText("<html><body><p style='color:red;'>Ошибка при чтении файла: " + e.getMessage() + "</p></body></html>");
        }

        System.out.println("Введённый вариант: " + input);
        System.out.println("Доступные варианты: " + availableVariants);
        System.out.println("Маппинг через VariantMapper: " + mappedVariant);
        System.out.println("Путь к HTML: " + path);
    }


    private void setupVariantAutoUpdate() {
        variantField.getDocument().addDocumentListener(new javax.swing.event.DocumentListener() {
            public void insertUpdate(javax.swing.event.DocumentEvent e) { loadVariantInstruction(); }
            public void removeUpdate(javax.swing.event.DocumentEvent e) { loadVariantInstruction(); }
            public void changedUpdate(javax.swing.event.DocumentEvent e) { loadVariantInstruction(); }
        });
    }
}


class labprog6 extends JFrame {
    private JTextField variantField;
    private JTextArea commentArea;
    private JTextField gradeField;
    private JLabel fileLabel;
    private static final String SAVE_FILE = "labdata.ser";
    private HashMap<String, String> data = new HashMap<>();
    private static final String DATABASE_FILE = "data/ProgBase/PIiKT/lab6.db";

    public labprog6() {
        setTitle("Программирование 1 курс ПИиКТ");
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        setSize(980, 850);

        JPanel contentPanel = new JPanel();
        contentPanel.setLayout(new BoxLayout(contentPanel, BoxLayout.Y_AXIS));
        contentPanel.setBorder(BorderFactory.createEmptyBorder(10, 10, 10, 10));

        // Заголовок
        JLabel titleLabel = new JLabel("Лабораторная работа 6");
        titleLabel.setFont(new Font("SansSerif", Font.BOLD, 24));
        titleLabel.setAlignmentX(Component.LEFT_ALIGNMENT);
        JButton backButton = new JButton("<");
        backButton.setPreferredSize(new Dimension(60, 25));
        backButton.setCursor(Cursor.getPredefinedCursor(Cursor.HAND_CURSOR));
        backButton.addActionListener(e -> {
            new ProgrammingPIiKT(); // возврат назад
            dispose();
        });

        JPanel headerPanel = new JPanel(new FlowLayout(FlowLayout.LEFT));
        headerPanel.setAlignmentX(Component.LEFT_ALIGNMENT);
        headerPanel.add(backButton);
        headerPanel.add(titleLabel);
        contentPanel.add(headerPanel);


        JTextPane instructionPane = new JTextPane();
        instructionPane.setContentType("text/html");
        instructionPane.setText("""
<html>
  <body style="font-family: SansSerif; font-size: 12pt; line-height: 1.5; word-wrap: break-word; white-space: normal;">
    <p><b>Внимание!</b> У разных вариантов разный текст задания!</p>
    <p>
      Разделить программу из лабораторной работы №5 на клиентский и серверный модули. Серверный модуль должен осуществлять выполнение команд по управлению коллекцией. Клиентский модуль должен в интерактивном режиме считывать команды, передавать их для выполнения на сервер и выводить результаты выполнения.
    </p>
    <p><b style="cursor: pointer;">Необходимо выполнить следующие требования:</b></p>
    <ul>
      <li>Операции обработки объектов коллекции должны быть реализованы с помощью Stream API с использованием лямбда-выражений.</li>
      <li>Объекты между клиентом и сервером должны передаваться в сериализованном виде.</li>
      <li>Объекты в коллекции, передаваемой клиенту, должны быть отсортированы по местоположению.</li>
      <li>Клиент должен корректно обрабатывать временную недоступность сервера.</li>
      <li>Обмен данными между клиентом и сервером должен осуществляться по протоколу TCP.</li>
      <li>Для обмена данными на сервере необходимо использовать потоки ввода-вывода.</li>
      <li>Для обмена данными на клиенте необходимо использовать сетевой канал.</li>
      <li>Сетевые каналы должны использоваться в неблокирующем режиме.</li>
    </ul>
    <p><b style="cursor: pointer;">Обязанности серверного приложения:</b></p>
    <ul>
      <li>Работа с файлом, хранящим коллекцию.</li>
      <li>Управление коллекцией объектов.</li>
      <li>Назначение автоматически генерируемых полей объектов в коллекции.</li>
      <li>Ожидание подключений и запросов от клиента.</li>
      <li>Обработка полученных запросов (команд).</li>
      <li>Сохранение коллекции в файл при завершении работы приложения.</li>
      <li>Сохранение коллекции в файл при исполнении специальной команды, доступной только серверу (клиент такую команду отправить не может).</li>
    </ul>
    <p><b style="cursor: pointer;">Серверное приложение должно состоять из следующих модулей (реализованных в виде одного или нескольких классов):</b></p>
    <ul>
      <li>Модуль приёма подключений.</li>
      <li>Модуль чтения запроса.</li>
      <li>Модуль обработки полученных команд.</li>
      <li>Модуль отправки ответов клиенту.</li>
      <li>Сервер должен работать в однопоточном режиме.</li>
    </ul>
    <p><b style="cursor: pointer;">Обязанности клиентского приложения:</b></p>
    <ul>
      <li>Чтение команд из консоли.</li>
      <li>Валидация вводимых данных.</li>
      <li>Сериализация введённой команды и её аргументов.</li>
      <li>Отправка полученной команды и её аргументов на сервер.</li>
      <li>Обработка ответа от сервера (вывод результата исполнения команды в консоль).</li>
      <li>Команду <code>save</code> из клиентского приложения необходимо убрать.</li>
      <li>Команда <code>exit</code> завершает работу клиентского приложения.</li>
    </ul>
    <p><b>Важно!</b> Команды и их аргументы должны представлять из себя объекты классов. Недопустим обмен "простыми" строками...</p>
    <p><b style="cursor: pointer;">Дополнительное задание:</b><br>Реализовать логирование различных этапов работы сервера...</p>
    <p><b style="cursor: pointer;">Отчёт по работе должен содержать:</b></p>
    <ul>
      <li>Текст задания.</li>
      <li>Диаграмма классов.</li>
      <li>Исходный код программы.</li>
      <li>Выводы по работе.</li>
    </ul>
    <p><b style="cursor: pointer;">Вопросы к защите лабораторной работы:</b></p>
    <ul>
      <li>Сетевое взаимодействие - клиент-серверная архитектура, протоколы и их различия.</li>
      <li>TCP, UDP, сокеты, каналы, сериализация, Stream API.</li>
      <li>Шаблоны проектирования: Decorator, Iterator, Factory method и др.</li>
    </ul>
  </body>
</html>
""");
        instructionPane.setEditable(false);
        instructionPane.setOpaque(false);
        JScrollPane instructionScroll = new JScrollPane(instructionPane);
        instructionScroll.setAlignmentX(Component.LEFT_ALIGNMENT);
        instructionScroll.setPreferredSize(new Dimension(940, 500));
        contentPanel.add(Box.createVerticalStrut(10));
        contentPanel.add(instructionScroll);

        // Вариант
        JPanel variantPanel = new JPanel(new FlowLayout(FlowLayout.LEFT));
        variantPanel.setAlignmentX(Component.LEFT_ALIGNMENT);
        variantPanel.add(new JLabel("Вариант:"));
        variantField = new JTextField(10);
        variantPanel.add(variantField);
        contentPanel.add(Box.createVerticalStrut(10));
        contentPanel.add(variantPanel);

        // Ссылка
        JLabel linkLabel = new JLabel("<html><a href=''>Ссылка на лабораторную работу</a></html>");
        linkLabel.setCursor(new Cursor(Cursor.HAND_CURSOR));
        linkLabel.setAlignmentX(Component.LEFT_ALIGNMENT);
        linkLabel.addMouseListener(new MouseAdapter() {
            public void mouseClicked(MouseEvent e) {
                try {
                    Desktop.getDesktop().browse(new URI("https://se.ifmo.ru/courses/programming#:~:text=Лабораторная%20работа%20%233-4"));
                } catch (Exception ex) {
                    ex.printStackTrace();
                }
            }
        });
        contentPanel.add(linkLabel);

        // Прикрепление отчета
        JPanel attachPanel = new JPanel(new FlowLayout(FlowLayout.LEFT));
        attachPanel.setAlignmentX(Component.LEFT_ALIGNMENT);
        JButton attachButton = new JButton("Прикрепить отчет");
        fileLabel = new JLabel("Файл не выбран");
        attachButton.addActionListener(e -> {
            JFileChooser fileChooser = new JFileChooser();
            int result = fileChooser.showOpenDialog(this);
            if (result == JFileChooser.APPROVE_OPTION) {
                File selected = fileChooser.getSelectedFile();
                fileLabel.setText(selected.getName());
                data.put("filePath", selected.getAbsolutePath());
            }
        });
        attachPanel.add(attachButton);
        attachPanel.add(fileLabel);
        contentPanel.add(Box.createVerticalStrut(10));
        contentPanel.add(attachPanel);

        // Комментарии
        contentPanel.add(Box.createVerticalStrut(10));
        contentPanel.add(new JLabel("Комментарии к работе:"));
        commentArea = new JTextArea(5, 70);
        commentArea.setLineWrap(true);
        commentArea.setWrapStyleWord(true);
        JScrollPane commentScroll = new JScrollPane(commentArea);
        commentScroll.setAlignmentX(Component.LEFT_ALIGNMENT);
        contentPanel.add(commentScroll);

        // Балл
        JPanel gradePanel = new JPanel(new FlowLayout(FlowLayout.LEFT));
        gradePanel.setAlignmentX(Component.LEFT_ALIGNMENT);
        gradePanel.add(new JLabel("Балл:"));
        gradeField = new JTextField(5);
        gradePanel.add(gradeField);
        contentPanel.add(Box.createVerticalStrut(10));
        contentPanel.add(gradePanel);

        // Сохранение и открытие
        JButton saveButton = new JButton("Сохранить");
        saveButton.setAlignmentX(Component.LEFT_ALIGNMENT);
        saveButton.addActionListener(e -> {
            saveData();
            String path = data.get("filePath");
            if (path != null && !path.isEmpty()) {
                try {
                    Desktop.getDesktop().open(new File(path));
                } catch (IOException ex) {
                    JOptionPane.showMessageDialog(this, "Не удалось открыть файл: " + ex.getMessage());
                }
            }
        });
        contentPanel.add(Box.createVerticalStrut(15));
        contentPanel.add(saveButton);

        JScrollPane scrollPane = new JScrollPane(contentPanel);
        scrollPane.getVerticalScrollBar().setUnitIncrement(16);
        setContentPane(scrollPane);

        loadData();
        setVisible(true);

        variantField.setEditable(AccessManager.canEditVariant());
        commentArea.setEditable(AccessManager.canComment());
        gradeField.setEditable(AccessManager.canSetGrade());
        attachButton.setEnabled(AccessManager.canAttachReport());
        saveButton.setEnabled(AccessManager.canSave());
    }

    private void saveData() {
        data.put("variant", variantField.getText());
        data.put("comments", commentArea.getText());
        data.put("grade", gradeField.getText());

        String filePath = data.get("filePath");

        // Убедиться, что путь существует
        new File(DATABASE_FILE).getParentFile().mkdirs();

        try (Connection conn = DriverManager.getConnection("jdbc:sqlite:" + DATABASE_FILE)) {

            String createTableSQL = """
            CREATE TABLE IF NOT EXISTS lab_reports_6 (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                variant TEXT NOT NULL,
                comments TEXT,
                file_path TEXT,
                grade TEXT,
                timestamp DATETIME DEFAULT CURRENT_TIMESTAMP
            );
        """;
            try (Statement stmt = conn.createStatement()) {
                stmt.execute(createTableSQL);
            }

            String insertSQL = "INSERT INTO lab_reports_6 (variant, comments, file_path, grade) VALUES (?, ?, ?, ?)";
            try (PreparedStatement pstmt = conn.prepareStatement(insertSQL)) {
                pstmt.setString(1, variantField.getText());
                pstmt.setString(2, commentArea.getText());
                pstmt.setString(3, filePath);
                pstmt.setString(4, gradeField.getText());
                pstmt.executeUpdate();
            }

            JOptionPane.showMessageDialog(this, "Данные сохранены в БД!");
        } catch (SQLException e) {
            e.printStackTrace();
            JOptionPane.showMessageDialog(this, "Ошибка при сохранении в БД: " + e.getMessage());
        }
    }

    private void loadData() {
        new File(DATABASE_FILE).getParentFile().mkdirs();

        try (Connection conn = DriverManager.getConnection("jdbc:sqlite:" + DATABASE_FILE)) {

            String createTableSQL = """
            CREATE TABLE IF NOT EXISTS lab_reports_6 (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                variant TEXT NOT NULL,
                comments TEXT,
                file_path TEXT,
                grade TEXT,
                timestamp DATETIME DEFAULT CURRENT_TIMESTAMP
            );
        """;
            try (Statement stmt = conn.createStatement()) {
                stmt.execute(createTableSQL);
            }

            String selectSQL = "SELECT variant, comments, file_path, grade FROM lab_reports_6 ORDER BY timestamp DESC LIMIT 1";
            try (Statement stmt = conn.createStatement(); ResultSet rs = stmt.executeQuery(selectSQL)) {
                if (rs.next()) {
                    variantField.setText(rs.getString("variant"));
                    commentArea.setText(rs.getString("comments"));
                    gradeField.setText(rs.getString("grade"));
                    String path = rs.getString("file_path");
                    data.put("filePath", path);
                    fileLabel.setText(path != null ? new File(path).getName() : "Файл не выбран");
                }
            }
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }


    public static void main(String[] args) {
        SwingUtilities.invokeLater(labprog6::new);
    }
}


class labprog7 extends JFrame {
    public labprog7() {
        setTitle("Программирование 1 курс");
        setSize(720, 800);
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        setLocationRelativeTo(null);

        JTextField label = new JTextField("Лабораторная работа 1");
        JTextField task1 = new JTextField("Ваш вариант выдан практиком (или лектором)");
        JTextField task2 = new JTextField("Лабораторная работа находится в PDF-файле");
        //JEditorPane task0 = new JEditorPane("");
        JTextField task3 = new JTextField("(Для кафедры ВТ: Варианты лабораторных работ находятся на se.ifmo).");
        JButton backButton = new JButton("<");

        backButton.addActionListener(e -> {
            new ProgrammingPIiKT();
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

class labprog8 extends JFrame {
    public labprog8() {
        setTitle("Программирование 1 курс");
        setSize(720, 800);
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        setLocationRelativeTo(null);

        JLabel label = new JLabel("Лабораторная работа 2");
        JLabel task1 = new JLabel("Ваш вариант выдан практиком (или лектором)");
        JLabel task2 = new JLabel("Лабораторная работа находится в PDF-файле");
        JLabel task3 = new JLabel("(Для кафедры ВТ: Варианты лабораторных работ находятся на se.ifmo).");
        JButton backButton = new JButton("Назад в окно пользователя");

        backButton.addActionListener(e -> {
            new ProgrammingPIiKT();
            dispose();
        });

        JPanel panel = new JPanel();
        panel.add(label);
        panel.add(task1);
        panel.add(task2);
        panel.add(task3);
        panel.add(backButton);
        add(panel);
        label.setFont(new Font("Arial", Font.PLAIN, 20));
        backButton.setFont(new Font("Arial", Font.PLAIN, 25));
        setVisible(true);
    }
}

class hackprog1 extends JFrame {
    public static int score = 0;
    public static int value = 0;
    public static int scorenext = 0;

    public hackprog1() {
        setTitle("Хакатон: Программирование");
        setDefaultCloseOperation(EXIT_ON_CLOSE);
        setSize(800, 700);
        setLocationRelativeTo(null); // Центр экрана
        setLayout(new BorderLayout());


        JPanel topPanel = new JPanel(new FlowLayout(FlowLayout.LEFT));
        JButton backButton = new JButton("<");
        backButton.setPreferredSize(new Dimension(40, 30));
        backButton.addActionListener(e -> {
            new ProgrammingPIiKT();
            dispose();
        });
        topPanel.add(backButton);
        add(topPanel, BorderLayout.NORTH);

        JPanel onepanel = new JPanel(new FlowLayout(FlowLayout.CENTER));
        JTextArea Open = new JTextArea("Хакатон");
        Open.setPreferredSize(new Dimension(40, 30));
        onepanel.add(Open);
        add(onepanel, BorderLayout.CENTER);


        //Первая зона
        JTextArea mainArea = new JTextArea("Введите результаты работы (ввод кода обязателен)");
        mainArea.setLineWrap(true);
        mainArea.setWrapStyleWord(true);
        JScrollPane mainScroll = new JScrollPane(mainArea);
        JButton saveMain = new JButton("Сохранить основную зону");
        saveMain.addActionListener(e -> {
            String content = mainArea.getText();
            JOptionPane.showMessageDialog(this, "Сохранено:\n" + content);
        });

        JPanel mainPanel = new JPanel(new BorderLayout());
        mainPanel.setBorder(BorderFactory.createTitledBorder("Основная зона (самая большая)"));
        mainPanel.add(mainScroll, BorderLayout.CENTER);
        mainPanel.add(saveMain, BorderLayout.SOUTH);

        //Вторая зона
        JTextArea secondArea = new JTextArea("Введите описание или ссылку");
        secondArea.setLineWrap(true);
        secondArea.setWrapStyleWord(true);
        JScrollPane secondScroll = new JScrollPane(secondArea);
        JButton saveSecond = new JButton("Сохранить описание");
        saveSecond.addActionListener(e -> {
            String text = secondArea.getText();
            JOptionPane.showMessageDialog(this, "Описание сохранено:\n" + text);
        });

        JPanel secondPanel = new JPanel(new BorderLayout());
        secondPanel.setBorder(BorderFactory.createTitledBorder("Дополнительная зона (средняя)"));
        secondPanel.add(secondScroll, BorderLayout.CENTER);
        secondPanel.add(saveSecond, BorderLayout.SOUTH);

        //Третья зона баллы
        JSpinner scoreSpinner = new JSpinner(new SpinnerNumberModel(0, 0, 100, 1));
        JButton setScore = new JButton("Сохранить баллы");
        setScore.addActionListener(e -> {
            scorenext = score;
            value = (int) scoreSpinner.getValue();
            if (value >= 0 && value <= 20) {
                score = value;
                JOptionPane.showMessageDialog(this, "Баллы установлены: " + score);
            } else {
                JOptionPane.showMessageDialog(this, "Ошибка: Введите балл от 0 до 20.");
                score = 0;
                JOptionPane.showMessageDialog(this, "Баллы сброшены на: " + score);
            }
        });

        JPanel scorePanel = new JPanel(new BorderLayout());
        scorePanel.setBorder(BorderFactory.createTitledBorder("Блок оценивания (маленький)"));
        scorePanel.add(scoreSpinner, BorderLayout.CENTER);
        scorePanel.add(setScore, BorderLayout.SOUTH);

        //Собираем интерфейс
        JPanel centerPanel = new JPanel();
        centerPanel.setLayout(new GridBagLayout());
        GridBagConstraints gbc = new GridBagConstraints();
        gbc.insets = new Insets(8, 8, 8, 8);
        gbc.fill = GridBagConstraints.BOTH;
        gbc.gridx = 0;

        gbc.gridy = 0;
        gbc.weightx = 1.0;
        gbc.weighty = 0.6; // первая зона
        centerPanel.add(mainPanel, gbc);

        gbc.gridy = 1;
        gbc.weighty = 0.25; // вторая зона
        centerPanel.add(secondPanel, gbc);

        gbc.gridy = 2;
        gbc.weighty = 0.15; // третья зона
        centerPanel.add(scorePanel, gbc);

        add(centerPanel, BorderLayout.CENTER);

        setVisible(true);
    }
}


class hackprog2 extends JFrame {
    public hackprog2() {
        setTitle("Хакатон 2 семестр");

    }
}
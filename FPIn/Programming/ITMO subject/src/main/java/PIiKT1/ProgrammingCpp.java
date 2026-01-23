package main.java.PIiKT1;

import main.java.AccessManager;
import main.java.ExtraFramePIiKT;
import main.java.VariantMapper;

import javax.swing.*;
import javax.swing.event.DocumentEvent;
import javax.swing.event.DocumentListener;
import javax.swing.table.DefaultTableModel;
import java.awt.*;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.io.*;
import java.net.URI;
import java.net.URL;
import java.nio.file.*;
import java.sql.*;
import java.util.ArrayList;
import java.util.Collections;
import java.util.HashMap;
import java.util.List;

public class ProgrammingCpp extends JFrame {
    public ProgrammingCpp() {
        setTitle("Программирование на C++");
        setSize(720, 800);
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        setLocationRelativeTo(null);

        // Кнопки дисциплин
        JButton pointprogcpp1 = new JButton("Баллы итоги");
        JButton labcpp1   = new JButton("Лабораторная работа 1");
        JButton labcpp2   = new JButton("Лабораторная работа 2");
        JButton labcpp3   = new JButton("Лабораторная работа 3");
        JButton labcpp4   = new JButton("Лабораторная работа 4");
        JButton labcpp5   = new JButton("Лабораторная работа 5");
        JButton labcpp6   = new JButton("Лабораторная работа 6");

        // Массив ссылок, чтобы не дублировать
        JButton[] buttonsInf = { pointprogcpp1, labcpp1, labcpp2, labcpp3, labcpp4, labcpp5, labcpp6};

        // Обработка переходов остаётся прежней, только теперь вызываем ваши фреймы
        pointprogcpp1.addActionListener(e -> { new pointprogcpp1(); dispose(); });
        labcpp1  .addActionListener(e -> { new labcpp1();  dispose(); });
        labcpp2  .addActionListener(e -> { new labcpp2();  dispose(); });
        /*labcpp3  .addActionListener(e -> { new labcpp3();  dispose(); });
        labcpp4  .addActionListener(e -> { new labcpp4();  dispose(); });
        labcpp5  .addActionListener(e -> { new labcpp5();  dispose(); });
        labcpp6  .addActionListener(e -> { new labcpp6();  dispose(); });*/


        // Верхняя панель с «Назад» и заголовком
        JPanel top = new JPanel(new BorderLayout());
        JButton backButton = new JButton("< Назад");
        backButton.addActionListener(e -> {
            new ExtraFramePIiKT();
            dispose();
        });
        JLabel title = new JLabel("Программирование на C++", SwingConstants.CENTER);
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

class pointprogcpp1 extends JFrame {
    private static final String FILE = "data/ProgCpp/marksProgCpp1.ser";
    private final DefaultTableModel model;

    public pointprogcpp1() {
        super("Таблица баллов");
        setSize(600, 390);
        setLocationRelativeTo(null);
        setDefaultCloseOperation(DISPOSE_ON_CLOSE);
        setLayout(new BorderLayout());


        JPanel top = new JPanel(new FlowLayout(FlowLayout.LEFT));
        JButton backButton = new JButton("<");
        backButton.addActionListener(e -> {
            new ProgrammingCpp();
            dispose();
        });
        top.add(backButton);
        add(top, BorderLayout.NORTH);


        String[] cols = {"Критерий", "Балл"};
        Object[][] data = {
                {"Лабораторная работа 1-2 (12-20)", ""},
                {"Лабораторная работа 3-4 (24-40)", ""},
                {"Коллоквиум (12-20)", ""},
                {"Личные качества (0-3)", ""},
                {"Экзамен (0-20)", ""},
                {"Сумма (0-103)", ""},
                {"Итог", ""},
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

    /**
     * Сохраняем информацию в файл marksProgPiikt1.ser
     */
    private void saveData() {
        HashMap<Integer, String> map = new HashMap<>();
        for (int r = 0; r < model.getRowCount(); r++) {
            map.put(r, String.valueOf(model.getValueAt(r, 1)));
        }
        try (ObjectOutputStream out =
                     new ObjectOutputStream(new FileOutputStream(FILE))) {
            out.writeObject(map);
            JOptionPane.showMessageDialog(this, "Данные сохранены");
        } catch (IOException ex) {
            ex.printStackTrace();
        }
    }

    private void loadData() {
        try (ObjectInputStream in =
                     new ObjectInputStream(new FileInputStream(FILE))) {
            HashMap<Integer, String> map = (HashMap<Integer, String>) in.readObject();
            for (int r = 0; r < model.getRowCount(); r++) {
                String val = map.getOrDefault(r, String.valueOf(model.getValueAt(r, 1)));
                model.setValueAt(val, r, 1);
            }
        } catch (Exception ignored) {}
    }
}

class labcpp1 extends JFrame {
    private JTextField variantField;
    private JTextArea commentArea;
    private JTextField gradeField;
    private JLabel fileLabel;
    private static final String SAVE_FILE = "labdata.ser";
    private HashMap<String, String> data = new HashMap<>();
    private static final String DATABASE_FILE = "data/ProgCpp/lab/lab1.db";

    public labcpp1() {
        setTitle("Программирование на C++");
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        setSize(980, 850);

        JPanel contentPanel = new JPanel();
        contentPanel.setLayout(new BoxLayout(contentPanel, BoxLayout.Y_AXIS));
        contentPanel.setBorder(BorderFactory.createEmptyBorder(10, 10, 10, 10));

        // Заголовок
        JLabel titleLabel = new JLabel("Лабораторная работа 1");
        titleLabel.setFont(new Font("SansSerif", Font.BOLD, 24));
        titleLabel.setAlignmentX(Component.LEFT_ALIGNMENT);
        JButton backButton = new JButton("<");
        backButton.setPreferredSize(new Dimension(60, 25));
        backButton.setCursor(Cursor.getPredefinedCursor(Cursor.HAND_CURSOR));
        backButton.addActionListener(e -> {
            new ProgrammingCpp(); // возврат назад
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
                         <body style="font-family: SansSerif; font-size: 12pt; line-height: 1.6;">
                
                           <h2>Лабораторная работа 1: Система расчёта заказа в кафе</h2>
                
                           <h3>Цель работы:</h3>
                           <ul>
                             <li>Научиться использовать функции.</li>
                             <li>Освоить управление динамической памятью.</li>
                             <li>Изучить базовые шаблоны функций.</li>
                             <li>Закрепить работу с десятичными числами (float / double) и строками.</li>
                           </ul>
                
                           <h3>Условие задачи:</h3>
                           <p>Создайте программу, которая рассчитывает итоговую стоимость заказа в кафе с учётом количества, скидки и НДС. Пользователь вводит блюда, их цены и количество. Программа рассчитывает итоговую сумму, применяет скидку, добавляет налог и выводит чек.</p>
                
                           <h3>Обязательные элементы:</h3>
                           <ul>
                             <li>Использовать функции для:
                               <ul>
                                 <li>ввода данных</li>
                                 <li>расчёта стоимости</li>
                                 <li>вывода чека</li>
                               </ul>
                             </li>
                             <li>Использовать динамическую память (<code>new</code> / <code>delete</code>) для хранения массива заказов.</li>
                             <li>Применить шаблонную функцию для универсального вывода элементов (например, информации о товаре).</li>
                             <li>Использовать строки (<code>std::string</code>) для хранения названий блюд.</li>
                             <li>Использовать десятичные числа (<code>float</code> или <code>double</code>) для расчётов.</li>
                           </ul>
                
                           <h3>Пример интерфейса программы:</h3>
                           <pre>
                       Введите количество блюд: 2
                
                       Блюдо 1:
                       Название: Кофе
                       Цена: 120.5
                       Количество: 2
                
                       Блюдо 2:
                       Название: Пирог
                       Цена: 250.0
                       Количество: 1
                
                       Введите скидку в процентах: 10
                
                       === Чек ===
                       Кофе (2 шт): 241.00
                       Пирог (1 шт): 250.00
                       Скидка: -49.10
                       НДС (20%): 88.38
                       Итог: 530.28
                           </pre>
                
                           <h3>Подсказки по реализации:</h3>
                
                           <p><b>Структура для блюда:</b></p>
                           <pre>
                       struct Dish {
                           std::string name;
                           double price;
                           int quantity;
                       };
                           </pre>
                
                           <p><b>Шаблон для вывода:</b></p>
                           <pre>
                       template&lt;typename T&gt;
                       void printInfo(const T&amp; item) {
                           std::cout &lt;&lt; item &lt;&lt; std::endl;
                       }
                           </pre>
                
                           <p><b>Пример динамического выделения:</b></p>
                           <pre>
                       Dish* menu = new Dish[amount];
                       // ...
                       delete[] menu;
                           </pre>
                
                           <h3>Критерии оценки:</h3>
                           <ul>
                             <li>Корректность расчётов</li>
                             <li>Использование функций</li>
                             <li>Использование динамической памяти</li>
                             <li>Шаблон хотя бы одной функции</li>
                             <li>Работа со строками (<code>std::string</code>)</li>
                             <li>Аккуратный вывод чека</li>
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
        /*JLabel linkLabel = new JLabel("<html><a href=''>Ссылка на лабораторную работу</a></html>");
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
        contentPanel.add(linkLabel);*/

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
            CREATE TABLE IF NOT EXISTS labcpp_reports_1 (
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

            String insertSQL = "INSERT INTO labcpp_reports_1 (variant, comments, file_path, grade) VALUES (?, ?, ?, ?)";
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
            CREATE TABLE IF NOT EXISTS labcpp_reports_1 (
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

            String selectSQL = "SELECT variant, comments, file_path, grade FROM labcpp_reports_1 ORDER BY timestamp DESC LIMIT 1";
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
        SwingUtilities.invokeLater(labcpp1::new);
    }
}

class labcpp2 extends JFrame {
    private JTextField variantField;
    private JTextArea commentArea;
    private JTextField gradeField;
    private JLabel fileLabel;
    private static final String SAVE_FILE = "labdata.ser";
    private HashMap<String, String> data = new HashMap<>();
    private JTextPane instructionPane;
    private static final String DATABASE2 = "data\\ProgCPP\\lab2.db";


    public labcpp2() {
        setTitle("Программирование на C++");
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        setSize(980, 850);

        JPanel contentPanel = new JPanel();
        contentPanel.setLayout(new BoxLayout(contentPanel, BoxLayout.Y_AXIS));
        contentPanel.setBorder(BorderFactory.createEmptyBorder(10, 10, 10, 10));

        // Заголовок
        JLabel titleLabel = new JLabel("Лабораторная работа 2");
        titleLabel.setFont(new Font("SansSerif", Font.BOLD, 24));
        titleLabel.setAlignmentX(Component.LEFT_ALIGNMENT);
        JButton backButton = new JButton("<");
        backButton.setPreferredSize(new Dimension(60, 25));
        backButton.setCursor(Cursor.getPredefinedCursor(Cursor.HAND_CURSOR));
        backButton.addActionListener(e -> {
            new ProgrammingCpp(); // возврат назад
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

        try (Connection conn = DriverManager.getConnection("jdbc:sqlite:labcpp2.db")) {

            //Убедиться, что таблица существует
            String createTableSQL = """
            CREATE TABLE IF NOT EXISTS labcpp_reports_2 (
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
            String insertSQL = "INSERT INTO labcpp_reports_2 (variant, comments, file_path, grade) VALUES (?, ?, ?, ?)";
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
        try (Connection conn = DriverManager.getConnection("jdbc:sqlite:labcpp2.db")) {

            //Убедиться, что таблица существует
            String createTableSQL = """
            CREATE TABLE IF NOT EXISTS labcpp_reports_2 (
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
            String selectSQL = "SELECT variant, comments, file_path, grade FROM labcpp_reports_2 ORDER BY timestamp DESC LIMIT 1";
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
        SwingUtilities.invokeLater(labcpp2::new);
    }

    private void loadVariantInstruction() {
        if (instructionPane == null || variantField == null) return;

        String input = variantField.getText().trim();
        if (input.isEmpty()) return;

        List<Integer> availableVariants = new ArrayList<>();
        try {
            URL url = getClass().getClassLoader().getResource("ProgaC2HTML");
            if (url == null) {
                instructionPane.setText("<html><body><p style='color:red;'>Папка ProgaC2HTML не найдена в ресурсах.</p></body></html>");
                return;
            }

            URI uri = url.toURI();
            Path dirPath;
            if ("jar".equals(uri.getScheme())) {
                FileSystem fs = FileSystems.newFileSystem(uri, Collections.emptyMap());
                dirPath = fs.getPath("ProgaC2HTML");
            } else {
                dirPath = Paths.get(uri);
            }

            try (DirectoryStream<Path> stream = Files.newDirectoryStream(dirPath, "labcpp2.*.html")) {
                for (Path entry : stream) {
                    String fileName = entry.getFileName().toString();
                    // Теперь корректно: извлекает только часть между "labcpp2." и ".html"
                    if (fileName.startsWith("labcpp2.") && fileName.endsWith(".html")) {
                        String numberPart = fileName.substring("labcpp2.".length(), fileName.length() - ".html".length());
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

        String path = "ProgaC2HTML/labcpp2." + mappedVariant + ".html";
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

class labcpp3 extends JFrame {

    private JTextField variantField;
    private JTextArea commentArea;
    private JTextField gradeField;
    private JLabel fileLabel;
    private static final String SAVE_FILE = "labdata.ser";
    private HashMap<String, String> data = new HashMap<>();
    private JTextPane instructionPane;
    private static final String DATABASE2 = "data\\ProgCPP\\lab3.db";

    public labcpp3() {

    }
}
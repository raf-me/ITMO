package main.java.PIn;

import main.java.ExtraFramePIn;
import main.java.Facultets;

import javax.swing.*;
import java.awt.*;
import java.io.*;
import java.nio.charset.StandardCharsets;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.*;
import java.util.List;

public class ASDPIn extends JFrame {

    private static final String PYTHON_CMD = "PythonInterpretator/python.exe";
    private static final Path BASE_DIR = Paths.get("data", "PythonScriptsAlgPIn").toAbsolutePath();

    private final CardLayout cards = new CardLayout();
    private final JPanel root = new JPanel(cards);
    private final JTextArea codeArea = new JTextArea();
    private final JTextArea outputArea = new JTextArea();

    private int lab = 1;
    public static int task = 1;
    private File currentScript;

    public ASDPIn() {
        super("Сервис лабораторных работ АиСД ИТМО ФПИн");

        try {
            ImageIcon icon = new ImageIcon(getClass().getResource("ITMO АиСД Java.png"));
            setIconImage(icon.getImage());
        } catch (Exception e) {
            System.err.println("Не удалось загрузить иконку: " + e.getMessage());
        }

        setDefaultCloseOperation(EXIT_ON_CLOSE);
        setSize(1100, 850);
        setLocationRelativeTo(null);

        root.add(labScreen(), "labs");
        root.add(taskScreen(), "tasks");
        root.add(execScreen(), "exec");

        add(root);
        cards.show(root, "labs");
        setVisible(true);
    }

    private JPanel labScreen() {
        JPanel p = scaffold("Выберите лабораторную работу");
        for (int i = 1; i <= 9; i++) {
            int n = i;
            p.add(button("Лабораторная " + n, () -> {
                lab = n;
                if (lab == 9) {
                    root.add(taskScreenForLabK(), "tasks");
                } else {
                    root.add(taskScreen(), "tasks");
                }
                cards.show(root, "tasks");
            }));
        }
        JButton backButton = new JButton("< Назад");
        backButton.addActionListener(e -> {
            new ExtraFramePIn();
            dispose();
        });
        p.add(backButton, BorderLayout.WEST);
        return p;
    }

    private JPanel taskScreenForLabK() {
        JPanel mainPanel = new JPanel(new BorderLayout(10, 10));
        mainPanel.setBorder(BorderFactory.createEmptyBorder(20, 20, 20, 20));

        JLabel title = new JLabel("ЛР 9: Добавляйте и управляйте своими алгоритмами (.py)", SwingConstants.CENTER);
        title.setFont(title.getFont().deriveFont(Font.BOLD, 16f));
        mainPanel.add(title, BorderLayout.NORTH);

        JPanel taskGrid = new JPanel();
        taskGrid.setLayout(new BoxLayout(taskGrid, BoxLayout.Y_AXIS));

        Path lab9Dir = BASE_DIR.resolve("lab9");
        try {
            Files.createDirectories(lab9Dir);
        } catch (IOException e) {
            e.printStackTrace();
        }

        File[] files = lab9Dir.toFile().listFiles((d, name) -> name.endsWith(".py"));

        if (files != null && files.length > 0) {
            int index = 1;
            for (File file : files) {
                JPanel row = new JPanel(new BorderLayout(5, 5));
                JLabel label = new JLabel(index++ + ". " + file.getName());

                JPanel buttons = new JPanel(new FlowLayout(FlowLayout.RIGHT));
                JButton openBtn = new JButton("Открыть");
                openBtn.addActionListener(e -> {
                    currentScript = file;
                    loadCustomScript();
                    cards.show(root, "exec");
                });

                JButton deleteBtn = new JButton("Удалить");
                deleteBtn.addActionListener(e -> {
                    int confirm = JOptionPane.showConfirmDialog(
                            this,
                            "Удалить файл \"" + file.getName() + "\"?",
                            "Подтверждение",
                            JOptionPane.YES_NO_OPTION
                    );
                    if (confirm == JOptionPane.YES_OPTION) {
                        if (file.delete()) {
                            JOptionPane.showMessageDialog(this, "Файл удалён.");
                            root.add(taskScreenForLabK(), "tasks");
                            cards.show(root, "tasks");
                        } else {
                            JOptionPane.showMessageDialog(this, "Не удалось удалить файл.");
                        }
                    }
                });

                buttons.add(openBtn);
                buttons.add(deleteBtn);

                row.add(label, BorderLayout.CENTER);
                row.add(buttons, BorderLayout.EAST);
                taskGrid.add(row);
            }
        } else {
            taskGrid.add(new JLabel("Нет загруженных скриптов."));
        }

        JScrollPane scroll = new JScrollPane(taskGrid);
        mainPanel.add(scroll, BorderLayout.CENTER);

        JButton upload = new JButton("Добавить .py файл");
        upload.addActionListener(e -> {
            File[] currentFiles = lab9Dir.toFile().listFiles((d, name) -> name.endsWith(".py"));
            if (currentFiles != null && currentFiles.length >= 100) {
                JOptionPane.showMessageDialog(this, "Достигнут лимит в 100 файлов.");
                return;
            }
            JFileChooser chooser = new JFileChooser();
            int result = chooser.showOpenDialog(this);
            if (result == JFileChooser.APPROVE_OPTION) {
                File selected = chooser.getSelectedFile();
                if (!selected.getName().endsWith(".py")) {
                    JOptionPane.showMessageDialog(this, "Можно загрузить только .py файлы.");
                    return;
                }
                try {
                    copyFileToLab9(selected);
                    root.add(taskScreenForLabK(), "tasks");
                    cards.show(root, "tasks");
                } catch (IOException ex) {
                    JOptionPane.showMessageDialog(this, "Ошибка загрузки: " + ex.getMessage());
                }
            }
        });

        JButton backButton = new JButton("Назад");
        backButton.addActionListener(e -> cards.show(root, "labs"));

        JPanel bottom = new JPanel(new FlowLayout(FlowLayout.RIGHT));
        bottom.add(upload);
        bottom.add(backButton);
        mainPanel.add(bottom, BorderLayout.SOUTH);

        return mainPanel;
    }

    private void copyFileToLab9(File source) throws IOException {
        Path destDir = BASE_DIR.resolve("labFreeTask");
        Files.createDirectories(destDir);

        String fileName = source.getName();
        File destFile = destDir.resolve(fileName).toFile();
        int counter = 1;

        while (destFile.exists()) {
            String base = fileName.replaceAll("\\.py$", "");
            destFile = destDir.resolve(base + "_" + counter + ".py").toFile();
            counter++;
        }

        try (
                InputStream in = new FileInputStream(source);
                OutputStream out = new FileOutputStream(destFile)
        ) {
            byte[] buffer = new byte[1024];
            int len;
            while ((len = in.read(buffer)) > 0) {
                out.write(buffer, 0, len);
            }
        }
    }

    private void loadCustomScript() {
        codeArea.setText("");
        try (BufferedReader r = new BufferedReader(new FileReader(currentScript))) {
            String line;
            while ((line = r.readLine()) != null) codeArea.append(line + "\n");
        } catch (IOException ex) {
            codeArea.setText("Ошибка чтения: " + ex.getMessage());
        }
        outputArea.setText("");
    }

    private JPanel execScreen() {
        JPanel panel = new JPanel(new BorderLayout(8, 8));
        panel.setBorder(BorderFactory.createEmptyBorder(10, 10, 10, 10));

        codeArea.setEditable(true);
        codeArea.setFont(new Font("Monospaced", Font.PLAIN, 14));

        outputArea.setEditable(false);
        outputArea.setFont(new Font("Monospaced", Font.PLAIN, 14));

        JScrollPane codeScroll = new JScrollPane(codeArea);
        codeScroll.setBorder(BorderFactory.createTitledBorder("Код Python"));

        JScrollPane outputScroll = new JScrollPane(outputArea);
        outputScroll.setBorder(BorderFactory.createTitledBorder("Результат выполнения"));

        JSplitPane splitPane = new JSplitPane(JSplitPane.VERTICAL_SPLIT, codeScroll, outputScroll);
        splitPane.setResizeWeight(0.8);
        splitPane.setDividerLocation(0.5);

        JButton runButton = new JButton("Запустить");
        runButton.addActionListener(e -> runScript());

        JButton saveButton = new JButton("Сохранить");
        saveButton.addActionListener(e -> saveScript());

        JButton backButton = new JButton("Назад");
        backButton.addActionListener(e -> cards.show(root, "tasks"));

        JPanel buttonPanel = new JPanel();
        buttonPanel.add(runButton);
        buttonPanel.add(saveButton);
        buttonPanel.add(backButton);

        panel.add(buttonPanel, BorderLayout.NORTH);
        panel.add(splitPane, BorderLayout.CENTER);

        return panel;
    }

    private void saveScript() {
        if (currentScript == null || !currentScript.exists()) {
            JOptionPane.showMessageDialog(this, "Файл не выбран или не найден.");
            return;
        }

        if (!currentScript.getName().endsWith(".py")) {
            JOptionPane.showMessageDialog(this, "Можно редактировать только .py файлы.");
            return;
        }

        try (BufferedWriter writer = new BufferedWriter(new FileWriter(currentScript))) {
            writer.write(codeArea.getText());
            JOptionPane.showMessageDialog(this, "Файл успешно сохранён.");
        } catch (IOException e) {
            JOptionPane.showMessageDialog(this, "Ошибка сохранения: " + e.getMessage());
        }
    }

    private void loadScript() {
        String name = "lab" + lab + "." + task + ".py";
        currentScript = BASE_DIR.resolve(name).toFile();

        codeArea.setText("");
        try (BufferedReader r = new BufferedReader(new FileReader(currentScript))) {
            String line;
            while ((line = r.readLine()) != null) codeArea.append(line + "\n");
        } catch (IOException ex) {
            codeArea.setText("Ошибка чтения: " + ex.getMessage());
        }
        outputArea.setText("");
    }

    private JPanel taskScreen() {
        JPanel mainPanel = new JPanel(new BorderLayout(10, 10));
        mainPanel.setBorder(BorderFactory.createEmptyBorder(20, 20, 20, 20));

        JLabel title = new JLabel("Выберите задание для ЛР " + lab, SwingConstants.CENTER);
        title.setFont(title.getFont().deriveFont(Font.BOLD, 16f));
        mainPanel.add(title, BorderLayout.NORTH);

        JPanel taskGrid = new JPanel();
        taskGrid.setLayout(new BoxLayout(taskGrid, BoxLayout.Y_AXIS));

        File[] files = BASE_DIR.toFile().listFiles((d, name) -> name.matches("lab" + lab + "\\.\\d+\\.py"));

        if (files != null && files.length > 0) {
            List<Integer> taskNumbers = new ArrayList<>();

            for (File f : files) {
                String name = f.getName();
                try {
                    String[] parts = name.split("\\.");
                    if (parts.length == 3 && parts[0].equals("lab" + lab)) {
                        int k = Integer.parseInt(parts[1]);
                        taskNumbers.add(k);
                    }
                } catch (Exception ignored) {}
            }

            taskNumbers.sort(Integer::compareTo);

            for (int k : taskNumbers) {
                int taskId = k;
                JPanel row = new JPanel(new BorderLayout(5, 5));
                JLabel label = new JLabel("Задание " + taskId);
                JButton button = new JButton("Открыть");
                button.addActionListener(e -> {
                    task = taskId;
                    loadScript();
                    cards.show(root, "exec");
                });
                row.add(label, BorderLayout.CENTER);
                row.add(button, BorderLayout.EAST);
                taskGrid.add(row);
            }

        } else {
            taskGrid.add(new JLabel("Нет заданий для ЛР " + lab));
        }

        JScrollPane scroll = new JScrollPane(taskGrid);
        mainPanel.add(scroll, BorderLayout.CENTER);

        JButton backButton = new JButton("Назад");
        backButton.addActionListener(e -> cards.show(root, "labs"));
        JPanel bottom = new JPanel(new FlowLayout(FlowLayout.RIGHT));
        bottom.add(backButton);
        mainPanel.add(bottom, BorderLayout.SOUTH);

        return mainPanel;
    }

    private void runScript() {
        if (currentScript == null || !currentScript.exists()) {
            outputArea.setText("! Скрипт не найден");
            return;
        }
        try {
            List<String> cmd = Arrays.asList(PYTHON_CMD, "-u", currentScript.getAbsolutePath());

            ProcessBuilder pb = new ProcessBuilder(cmd).redirectErrorStream(true);
            outputArea.setText("Запуск: " + String.join(" ", cmd) + "\n");

            Process p = pb.start();
            try (BufferedReader r = new BufferedReader(
                    new InputStreamReader(p.getInputStream(), StandardCharsets.UTF_8))) {
                String line;
                while ((line = r.readLine()) != null) outputArea.append(line + "\n");
            }
            outputArea.append("[exit code " + p.waitFor() + "]");
        } catch (Exception ex) {
            outputArea.setText("Ошибка запуска: " + ex.getMessage());
            ex.printStackTrace();
        }
    }

    private static JPanel scaffold(String title) {
        JPanel p = new JPanel(new GridLayout(6, 1, 10, 10));
        p.setBorder(BorderFactory.createEmptyBorder(40, 100, 40, 100));
        p.add(new JLabel(title, SwingConstants.CENTER));
        return p;
    }

    private static JButton button(String text, Runnable action) {
        JButton b = new JButton(text);
        b.addActionListener(e -> action.run());
        return b;
    }

    public static void main(String[] args) {
        SwingUtilities.invokeLater(ASDPIn::new);
    }
}

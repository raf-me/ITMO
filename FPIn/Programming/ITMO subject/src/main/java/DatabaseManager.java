package main.java;

import java.sql.*;

public final class DatabaseManager {
    private static final String URL = "jdbc:sqlite:studenthelper.db";
    private static Connection conn;


    public static void init() throws SQLException {
        conn = DriverManager.getConnection(URL);
        try (Statement st = conn.createStatement()) {
            // Таблица для точечных баллов (point1/point2...)
            st.executeUpdate("""
                CREATE TABLE IF NOT EXISTS point_scores (
                  panel    TEXT,
                  row_idx  INTEGER,
                  score    TEXT,
                  PRIMARY KEY(panel, row_idx)
                );
            """);
            // Таблица для прикреплённых отчётов
            st.executeUpdate("""
                CREATE TABLE IF NOT EXISTS reports (
                  report_id   INTEGER PRIMARY KEY AUTOINCREMENT,
                  panel       TEXT,
                  file_path   TEXT
                );
            """);
        }
    }

    public static Connection getConnection() { return conn; }
}

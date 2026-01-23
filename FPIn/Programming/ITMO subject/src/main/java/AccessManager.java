package main.java;

public final class AccessManager {

    public enum Role { MASTER, TEACHER, STUDENT }

    private static Role current = Role.STUDENT;       // дефолт

    public static void loginAs(Role r) { current = r; }
    public static Role role()          { return current; }


    public static boolean canEditVariant()   { return current != Role.STUDENT; }
    public static boolean canComment()       { return current != Role.STUDENT; }
    public static boolean canSetGrade()      { return current != Role.STUDENT; }
    public static boolean canAttachReport()  { return true; }
    public static boolean canSave()          { return current != Role.STUDENT; }
}

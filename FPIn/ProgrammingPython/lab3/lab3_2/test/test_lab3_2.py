import io
import sys
import unittest
from contextlib import redirect_stdout, redirect_stderr

from lab3.lab3_2.Lab3_2 import (
    build_ranges,
    label,
    contains,
    parse_person,
    main,
)


class Lab3_2TestCase(unittest.TestCase):
    def test_build_ranges(self):
        boundaries = ["18", "25", "35"]
        result = build_ranges(boundaries)
        self.assertEqual(result, [(0, 18), (19, 25), (26, 35), (36, None)])

    def test_label(self):
        self.assertEqual(label((0, 18)), "0-18")
        self.assertEqual(label((19, 25)), "19-25")
        self.assertEqual(label((36, None)), "36+")

    def test_contains(self):
        r1 = (0, 18)
        self.assertTrue(contains(r1, 0))
        self.assertTrue(contains(r1, 18))
        self.assertFalse(contains(r1, 19))

        r2 = (36, None)
        self.assertTrue(contains(r2, 36))
        self.assertTrue(contains(r2, 100))
        self.assertFalse(contains(r2, 35))

    def test_parse_person(self):
        name, age = parse_person("Иванов Иван, 25")
        self.assertEqual(name, "Иванов Иван")
        self.assertEqual(age, 25)

        name, age = parse_person("  Петров Петр Петрович  ,  40  ")
        self.assertEqual(name, "Петров Петр Петрович")
        self.assertEqual(age, 40)

    def test_main_no_args_exit_2(self):
        saved_argv = sys.argv
        saved_stdin = sys.stdin

        try:
            sys.argv = ["Lab3_2.py"]  # нет границ
            sys.stdin = io.StringIO("Иванов Иван, 25\nEND\n")

            out = io.StringIO()
            err = io.StringIO()

            with redirect_stdout(out), redirect_stderr(err):
                with self.assertRaises(SystemExit) as cm:
                    main()

            self.assertEqual(cm.exception.code, 2)
            self.assertIn("Использование:", err.getvalue())

        finally:
            sys.argv = saved_argv
            sys.stdin = saved_stdin

    def test_main_grouping_and_sorting(self):
        saved_argv = sys.argv
        saved_stdin = sys.stdin

        try:
            sys.argv = ["Lab3_2.py", "18", "25", "35"]
            sys.stdin = io.StringIO(
                "\n".join(
                    [
                        "Андрей, 18",
                        "Борис, 19",
                        "Виктор, 25",
                        "Глеб, 26",
                        "Денис, 35",
                        "Егор, 36",
                        "Женя, 50",
                        "END",
                    ]
                )
                + "\n"
            )

            out = io.StringIO()
            with redirect_stdout(out):
                main()

            text = out.getvalue().strip()

            expected = "\n\n".join(
                [
                    "36+: Женя (50), Егор (36)",
                    "26-35: Денис (35), Глеб (26)",
                    "19-25: Виктор (25), Борис (19)",
                    "0-18: Андрей (18)",
                ]
            )

            self.assertEqual(text, expected)

        finally:
            sys.argv = saved_argv
            sys.stdin = saved_stdin

    def test_main_ignores_empty_lines(self):
        saved_argv = sys.argv
        saved_stdin = sys.stdin

        try:
            sys.argv = ["Lab3_2.py", "10"]
            sys.stdin = io.StringIO("\n\nИван, 5\n\nEND\n")

            out = io.StringIO()
            with redirect_stdout(out):
                main()

            self.assertIn("0-10: Иван (5)", out.getvalue())

        finally:
            sys.argv = saved_argv
            sys.stdin = saved_stdin


if __name__ == "__main__":
    unittest.main()
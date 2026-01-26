import os
import unittest
import tempfile
from pathlib import Path

from lab4.lab4 import normalize_products, parse_address, validate_phone, main


class Lab4UnitTests(unittest.TestCase):
    def test_normalize_products_counts(self):
        s = "Сыр, Колбаса, Сыр, Макароны, Колбаса"
        res = normalize_products(s)
        self.assertEqual(res, "Сыр x2, Колбаса x2, Макароны")

    def test_normalize_products_no_repeats(self):
        s = "Хлеб, Молоко, Яблоки"
        res = normalize_products(s)
        self.assertEqual(res, "Хлеб, Молоко, Яблоки")

    def test_normalize_products_spaces(self):
        s = "  Сыр  ,  Сыр,   Макароны "
        res = normalize_products(s)
        self.assertEqual(res, "Сыр x2, Макароны")

    def test_parse_address_valid(self):
        addr = "Россия. Московская область. Москва. улица Пушкина"
        res = parse_address(addr)
        self.assertEqual(res, ("Россия", "Московская область", "Москва", "улица Пушкина"))

    def test_parse_address_invalid(self):
        addr = "Япония. Шибуя. Шибуя-кроссинг"
        res = parse_address(addr)
        self.assertIsNone(res)

    def test_validate_phone_valid(self):
        self.assertTrue(validate_phone("+7-912-345-67-89"))

    def test_validate_phone_invalid(self):
        self.assertFalse(validate_phone("+34-93-1234-567"))
        self.assertFalse(validate_phone("+4-989-234-56"))
        self.assertFalse(validate_phone("89123456789"))
        self.assertFalse(validate_phone(""))


class Lab4IntegrationTests(unittest.TestCase):
    def test_main_creates_output_files(self):
        with tempfile.TemporaryDirectory() as tmp:
            tmp_path = Path(tmp)

            orders_content = "\n".join(
                [
                    "87459;Молоко, Яблоки, Хлеб, Яблоки, Молоко;Иванов Иван Иванович;Россия. Московская область. Москва. улица Пушкина;+7-912-345-67-89;MAX",
                    "56342;Хлеб, Молоко, Хлеб, Молоко;Смирнова Мария Леонидовна;Германия. Бавария. Мюнхен. Мариенплац;+4-989-234-56;LOW",
                    "84756;Печенье, Сыр, Печенье, Сыр;Васильева Анна Владимировна;Япония. Шибуя. Шибуя-кроссинг;+8-131-234-5678;MAX",
                    "90385;Макароны, Сыр, Макароны, Сыр;Николаев Николай;;+1-416-123-45-67;LOW",
                ]
            )

            (tmp_path / "orders.txt").write_text(orders_content, encoding="utf-8")

            old_cwd = os.getcwd()
            os.chdir(tmp_path)
            try:
                main()
            finally:
                os.chdir(old_cwd)

            self.assertTrue((tmp_path / "order_country.txt").exists())
            self.assertTrue((tmp_path / "non_valid_orders.txt").exists())

            valid_text = (tmp_path / "order_country.txt").read_text(encoding="utf-8").strip()
            invalid_text = (tmp_path / "non_valid_orders.txt").read_text(encoding="utf-8").strip()

            self.assertIn("87459;", valid_text)
            self.assertNotIn("56342;", valid_text)
            self.assertNotIn("84756;", valid_text)
            self.assertNotIn("90385;", valid_text)

            self.assertIn("56342;2;", invalid_text)
            self.assertIn("84756;1;", invalid_text)
            self.assertIn("84756;2;", invalid_text)
            self.assertIn("90385;1;no data", invalid_text)


if __name__ == "__main__":
    unittest.main()
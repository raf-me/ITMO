import unittest
from lab1.lab11.vigenere import encrypt_vigenere, decrypt_vigenere


class VigenereTestCase(unittest.TestCase):
    def test_encrypt_no_shift_upper(self):
        self.assertEqual(encrypt_vigenere("PYTHON", "A"), "PYTHON")

    def test_encrypt_no_shift_lower(self):
        self.assertEqual(encrypt_vigenere("python", "a"), "python")

    def test_encrypt_lemon(self):
        self.assertEqual(encrypt_vigenere("ATTACKATDAWN", "LEMON"), "LXFOPVEFRNHR")

    def test_decrypt_no_shift_upper(self):
        self.assertEqual(decrypt_vigenere("PYTHON", "A"), "PYTHON")

    def test_decrypt_no_shift_lower(self):
        self.assertEqual(decrypt_vigenere("python", "a"), "python")

    def test_decrypt_lemon(self):
        self.assertEqual(decrypt_vigenere("LXFOPVEFRNHR", "LEMON"), "ATTACKATDAWN")
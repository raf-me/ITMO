import unittest
from lab1.lab11.caesar import encrypt_caesar, decrypt_caesar


class CaesarTestCase(unittest.TestCase):
    def test_encrypt_uppercase(self):
        self.assertEqual(encrypt_caesar("PYTHON"), "SBWKRQ")

    def test_encrypt_lowercase(self):
        self.assertEqual(encrypt_caesar("python"), "sbwkrq")

    def test_encrypt_mixed(self):
        self.assertEqual(encrypt_caesar("Python3.6"), "Sbwkrq3.6")

    def test_encrypt_empty(self):
        self.assertEqual(encrypt_caesar(""), "")

    def test_decrypt_uppercase(self):
        self.assertEqual(decrypt_caesar("SBWKRQ"), "PYTHON")

    def test_decrypt_lowercase(self):
        self.assertEqual(decrypt_caesar("sbwkrq"), "python")

    def test_decrypt_mixed(self):
        self.assertEqual(decrypt_caesar("Sbwkrq3.6"), "Python3.6")

    def test_decrypt_empty(self):
        self.assertEqual(decrypt_caesar(""), "")
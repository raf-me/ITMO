import unittest
from lab1.lab11.rsa import is_prime, gcd, multiplicative_inverse, generate_keypair


class RSATestCase(unittest.TestCase):
    def test_is_prime(self):
        self.assertTrue(is_prime(2))
        self.assertTrue(is_prime(11))
        self.assertFalse(is_prime(8))

    def test_gcd(self):
        self.assertEqual(gcd(12, 15), 3)
        self.assertEqual(gcd(3, 7), 1)

    def test_multiplicative_inverse(self):
        self.assertEqual(multiplicative_inverse(7, 40), 23)

    def test_generate_keypair(self):
        public, private = generate_keypair(17, 19)
        e, n1 = public
        d, n2 = private
        self.assertEqual(n1, 323)
        self.assertEqual(n2, 323)
        self.assertEqual((d * e) % 288, 1)
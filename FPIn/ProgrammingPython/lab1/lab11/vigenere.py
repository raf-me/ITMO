def encrypt_vigenere(plaintext: str, keyword: str) -> str:
    ciphertext = ""
    key_index = 0
    key_len = len(keyword)
    for char in plaintext:
        if char.isalpha():
            shift = ord(keyword[key_index % key_len].lower()) - ord("a")
            if "A" <= char <= "Z":
                ciphertext += chr((ord(char) - ord("A") + shift) % 26 + ord("A"))
            else:
                ciphertext += chr((ord(char) - ord("a") + shift) % 26 + ord("a"))
            key_index += 1
        else:
            ciphertext += char
    return ciphertext


def decrypt_vigenere(ciphertext: str, keyword: str) -> str:
    plaintext = ""
    key_index = 0
    key_len = len(keyword)
    for char in ciphertext:
        if char.isalpha():
            shift = ord(keyword[key_index % key_len].lower()) - ord("a")
            if "A" <= char <= "Z":
                plaintext += chr((ord(char) - ord("A") - shift) % 26 + ord("A"))
            else:
                plaintext += chr((ord(char) - ord("a") - shift) % 26 + ord("a"))
            key_index += 1
        else:
            plaintext += char
    return plaintext
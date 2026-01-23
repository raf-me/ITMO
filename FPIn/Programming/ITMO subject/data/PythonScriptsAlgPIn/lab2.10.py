import time
start_time = time.time()

def bst(lab):
    def is_bst(index, min_val, max_val):
        if index == -1:
            return True
        key, left, right = lab[index]
        if not (min_val < key < max_val):
            return False
        return is_bst(left, min_val, key) and is_bst(right, key, max_val)

    if not lab:
        print("YES")

    if is_bst(0, float('-inf'), float('inf')):
        print("YES")

    else:
        print("NO")

def readfile(file):
    with open(file, 'r') as f:
        lines = f.read().splitlines()

    n = int(lines[0])
    lab = []

    for i in range(1, n + 1):
        k, l, r = map(int, lines[i].split())
        l = l - 1 if l != 0 else -1
        r = r - 1 if r != 0 else -1
        lab.append([k, l, r])
        print(lab)
    return lab


lab = readfile("PythonScripts/Test2-10/test2-10.txt")
bst(lab)

end_time = time.time()
time = end_time - start_time
print(f"Время выполнения программы: {time} секунд")
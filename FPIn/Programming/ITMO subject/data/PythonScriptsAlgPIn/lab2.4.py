import time
start_time = time.time()

with open("PythonScripts/Test2-4/test2-4.txt", "r") as fin:
    lab = [line.strip() for line in fin if line.strip()]

s = []

def mytree(lab, x):
    left, right = 0, len(lab)

    while left < right:
        mid = (left + right) // 2

        if lab[mid] < x:
            left = mid + 1

        else:
            right = mid

    return left

print(lab)

for line in lab:
    if line.startswith('+'):
        x = int(line[1:].strip())
        idx = mytree(s, x)

        if idx == len(s) or s[idx] != x:
            s.insert(idx, x)  # Вставляем x в отсортированную позицию

    elif line.startswith('?'):
        k = int(line[1:].strip())
        print(s[k - 1])

end_time = time.time()
time = end_time - start_time
print(f"Время выполнения программы: {time} секунд")
